using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuningService.Factories;
using TuningService.Models;

namespace TuningService.Views.Impl.EditMenu
{
    public partial class EditOrderView : Form, IEditOrderView
    {
        private Order _order;
        public EditOrderView()
        {
            InitializeComponent();
        }

        public Order OrderInfo { get => _order; set => _order = value; }

        public event EventHandler<int> GetOrderDataEvent;
        public event EventHandler UpdateOrderDataEvent;

        public void GetOldOrderData(int orderId)
        {
            GetOrderDataEvent?.Invoke(this, orderId);
        }

        public void ShowInformation()
        {
            textBoxEndDate.Text = _order.EndDate.ToString();
            labelStartDate.Text = _order.StartDate.ToString();
            textBoxPrice.Text = _order.Price.ToString();
            checkBoxInWork.Checked = _order.InWork;
            orderDescription.Text = _order.Description;
        }

        private void EditOrderView_Load(object sender, EventArgs e)
        {

        }

        private void buttonChangeOrder_Click(object sender, EventArgs e)
        {
            string pattern = "yyyy-MM-dd";
            var id = _order.Id;
            try
            {
                var finishData = DateTime.ParseExact(textBoxEndDate.Text, pattern, CultureInfo.InvariantCulture);
                var price = decimal.Parse(textBoxPrice.Text);
                var inWork = checkBoxInWork.Checked;
                var desc = orderDescription.Text;

                _order = OrderFactory.GetOrderInstance(finishData, price, inWork, desc);
                _order.Id = id;
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

            UpdateOrderDataEvent?.Invoke(this, EventArgs.Empty);

            MessageBox.Show("Order data has been successfully updated!",
                  "Information",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
            Close();
        }
    }
}
