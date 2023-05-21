using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Forms;
using TuningService.Factories;
using TuningService.Models;

namespace TuningService.Views.Impl.EditMenu
{
    public partial class EditOrderView : Form, IEditOrderView
    {
        private int _orderId;

        private Order _order;

        public EditOrderView()
        {
            InitializeComponent();
        }

        public event GetOrderDataDelegate GetOrderDataEvent;
        public event UpdateOrderDataDelegate UpdateOrderDataEvent;

        public async void GetOrderData(int orderId)
        {
            _orderId = orderId;
            await GetOrderDataEvent?.Invoke(orderId)!;
        }

        public void ShowInformation(Order order)
        {
            const string pattern = "yyyy-MM-dd";
            textBoxEndDate.Text = order.EndDate.ToString(pattern, CultureInfo.InvariantCulture);
            labelStartDate.Text = order.StartDate.ToString(CultureInfo.InvariantCulture);
            textBoxPrice.Text = order.Price.ToString(CultureInfo.InvariantCulture);
            checkBoxIsDone.Checked = order.IsDone;
            orderDescription.Text = order.Description;
        }

        private void EditOrderView_Load(object sender, EventArgs e)
        {

        }

        private void buttonChangeOrder_Click(object sender, EventArgs e)
        {
            const string pattern = "yyyy-MM-dd";
            try
            {
                var finishData = DateTime.ParseExact(textBoxEndDate.Text, pattern, CultureInfo.InvariantCulture);
                var price = decimal.Parse(textBoxPrice.Text.Replace('.', ','));
                var inWork = checkBoxIsDone.Checked;
                var desc = orderDescription.Text;

                _order = OrderFactory.GetOrderInstance(finishData, price, inWork, desc);
                _order.OrderId = _orderId;
            }
            catch (ValidationException)
            {
                MessageBox.Show("Incorrect data entered!",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);

                return;
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong format!!",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);

                return;
            }

            UpdateOrderDataEvent?.Invoke(_order);

            MessageBox.Show("Order data has been successfully updated!",
                  "Information",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
            Close();
        }
    }
}
