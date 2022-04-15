using System;
using System.Windows.Forms;
using TuningService.Services;

namespace TuningService.Views
{
    public partial class OrderInfoView : Form
    {
        private readonly IDbService _dbService;

        public OrderInfoView(IDbService dbService)
        {
            InitializeComponent();

            _dbService = dbService;
        }

        private void OrderInfoView_Load(object sender, EventArgs e)
        {

        }

        public void LoadOrder(int customerId)
        {
            dataGridOrderView.DataSource = _dbService.ShowOrderByTuningBoxId(customerId);
        }
    }
}
