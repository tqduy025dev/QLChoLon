using QuanLyBanHang.DAO;
using System;
using QuanLyBanHang.DTO;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyBanHang
{
    public partial class UserStatistical : UserControl
    {
        public static String dateFrom;
        public static String dateTo;


        public UserStatistical()
        {
            InitializeComponent();
            LoadStatisticals();
            Fill_CmbMonthAndYear();
            chartAlmostProduct.Hide();
            chartProduct.Hide();
            chartMoney.Show();

            if (BillDAO.Instance.SumPayMoneyByDay(int.Parse(CmbMonth.Text.ToString()), int.Parse(CmbYear.Text.ToString())).Rows.Count > 1)
            {
                LoadChartMoney();
            }
            
            

        }

        private void Fill_CmbMonthAndYear()
        {
            CmbMonth.Text = DateTime.Now.Month.ToString();
            for (int i = 1; i <= 12; i++)
                {
                    CmbMonth.Items.Add(i);
                }

            CmbYear.Text = DateTime.Now.Year.ToString();
            int numYear = int.Parse(CmbYear.Text);
            for (int i = 2010; i<= numYear; i++)
            {
                CmbYear.Items.Add(i);
            }
        }

        

        void LoadStatisticals()
        {
            //-----------------------------------Bill-----------------------------------//
            lbSumBill.Text = BillDAO.Instance.SumBillMonth().ToString();
            if (BillDAO.Instance.SumBillDay()  > 0)
            {
                lbBilltoday.Text = "Today: " + BillDAO.Instance.SumBillDay().ToString();
            }
            else
            {
                lbBilltoday.Text = "Today: " + 0;
            }

            //-----------------------------------Product-----------------------------------//
            lbSumProduct.Text = BillDAO.Instance.SumPayProductMonth().ToString();
            if (BillDAO.Instance.SumPayProductDay() > 0)
            {
                lbSumProductToday.Text = "Today: " + BillDAO.Instance.SumPayProductDay().ToString();
            }
            else
            {
                lbSumProductToday.Text = "Today: " + 0;
            }

            //-----------------------------------Money-----------------------------------//
            lbSumMoney.Text = BillDAO.Instance.SumMoneyMonth().ToString("###,###");
            if (BillDAO.Instance.SumMoneyDay() > 0)
            {
                lbMoneyDay.Text = "Today: " + BillDAO.Instance.SumMoneyDay().ToString("###,###");
            }
            else
            {
                lbMoneyDay.Text = "Today: " + 0;
            }

            //-----------------------------------Product and ProductType Trong Kho, Nhân viên-----------------------------------//
            lbTotalProduct.Text = ProductsDAO.Instance.SumProduct().ToString();
            lbProductType.Text = ProductTypeDAO.Instance.SumProductType().ToString();
            lbStaff.Text = StaffDAO.Instance.SumStaff().ToString();

            dateFrom = dateTimeFrom.Value.ToString("yyyy-MM-dd");
            dateTo = dateTimeTo.Value.ToString("yyyy-MM-dd");
        }

        void LoadChartMoney()
        {
            

            chartProduct.Enabled = false;
            chartMoney.Enabled = true;

            if (BillDAO.Instance.SumPayMoneyByDay(int.Parse(CmbMonth.Text.ToString()), int.Parse(CmbYear.Text.ToString())).Rows.Count < 1)
            {
                //CmbMonth.Text = DateTime.Now.Month.ToString();

                foreach (var series in chartMoney.Series)
                {
                    series.Points.Clear();
                }

                MessageBox.Show("No data for the month " + CmbMonth.Text.ToString(), "Notify", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                chartMoney.DataSource = BillDAO.Instance.SumPayMoneyByDay(int.Parse(CmbMonth.Text.ToString()), int.Parse(CmbYear.Text.ToString()));
            }

            chartMoney.Series["Series1"].XValueType = ChartValueType.DateTime;

            chartMoney.ChartAreas[0].AxisX.LabelStyle.Format = "dd-MM";

            //set the member of the chart data source used to data bind to the X-values of the series  
            chartMoney.Series["Series1"].XValueMember = "Ngay";
            //set the member columns of the chart data source used to data bind to the X-values of the series  
            chartMoney.Series["Series1"].YValueMembers = "tien";
            chartMoney.Series["Series1"].IsValueShownAsLabel = true;
        }
        void LoadChartProduct()
        {
            

           

            chartProduct.Enabled = true;
            chartMoney.Enabled = false;

            if (BillDAO.Instance.TotalPayProductByDay(int.Parse(CmbMonth.Text.ToString()), int.Parse(CmbYear.Text.ToString())).Rows.Count < 1)
            {
                //CmbMonth.Text = DateTime.Now.Month.ToString();

                foreach (var series in chartProduct.Series)
                {
                    series.Points.Clear();
                }

                MessageBox.Show("No data for the month " + CmbMonth.Text.ToString(), "Notify", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                chartProduct.DataSource = BillDAO.Instance.TotalPayProductByDay(int.Parse(CmbMonth.Text.ToString()), int.Parse(CmbYear.Text.ToString()));
            }

            chartProduct.Series["Series1"].XValueType = ChartValueType.DateTime;
            chartProduct.Series["Series1"].YValueType = ChartValueType.Int32;
            chartProduct.ChartAreas[0].AxisX.LabelStyle.Format = "dd-MM";

            //set the member of the chart data source used to data bind to the X-values of the series  
            chartProduct.Series["Series1"].XValueMember = "Ngay";
            //set the member columns of the chart data source used to data bind to the X-values of the series  
            chartProduct.Series["Series1"].YValueMembers = "slsp";
            chartProduct.Series["Series1"].IsValueShownAsLabel = true;
        }
        void LoadchartAlmostProduct()
        {
            chartAlmostProduct.DataSource = ProductsDAO.Instance.ProductAlmostOfStock();

            //set the member of the chart data source used to data bind to the X-values of the series  
            chartAlmostProduct.Series["Series1"].XValueMember = "Ten";
            //set the member columns of the chart data source used to data bind to the X-values of the series  
            chartAlmostProduct.Series["Series1"].YValueMembers = "SL";
            chartAlmostProduct.Series["Series1"].IsValueShownAsLabel = true;
        }

        private void btnCharMoney_Click(object sender, EventArgs e)
        {
            chartMoney.Show();
            chartProduct.Hide();
            chartAlmostProduct.Hide();

            LoadChartMoney();
        }
        private void btnChartProduct_Click(object sender, EventArgs e)
        {
            chartMoney.Hide();
            chartProduct.Show();
            chartAlmostProduct.Hide();

            LoadChartProduct();
        }
        private void btnProductAlmostOfStock_Click(object sender, EventArgs e)
        {
            chartMoney.Hide();
            chartProduct.Hide();
            chartAlmostProduct.Show();

            LoadchartAlmostProduct();
        }

        private void dateTimeFrom_ValueChanged(object sender, EventArgs e)
        {
            string dateNow = DateTime.UtcNow.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            if(DateTime.Now.CompareTo(dateTimeFrom.Value) < 0)
            {
                MessageBox.Show("Ngày không được quá ngày hiện tại!","Thông báo" ,MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimeFrom.Value = DateTime.Now;
                return;
            }
            dateFrom = dateTimeFrom.Value.ToString("yyyy-MM-dd");
        }

        private void dateTimeTo_ValueChanged(object sender, EventArgs e)
        {
            if (DateTime.Now.CompareTo(dateTimeTo.Value) < 0)
            {
                MessageBox.Show("Ngày không được quá ngày hiện tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimeTo.Value = DateTime.Now;
                return;
            }
            dateTo = dateTimeTo.Value.ToString("yyyy-MM-dd");
        }

        //Thong ke tong cac loai theo ngay from - to
        private void btnSearchTime_Click(object sender, EventArgs e)
        {
            //-----------------------------------Bill-----------------------------------//
            lbSumBill.Text = BillDAO.Instance.StatisticalBillFromTo(dateFrom,dateTo).ToString();

            //-----------------------------------Product-----------------------------------//
            lbSumProduct.Text = BillDAO.Instance.StatisticalProductFromTo(dateFrom, dateTo).ToString();

            //-----------------------------------Money-----------------------------------//

            double t = BillDAO.Instance.StatisticalMoneyFromTo(dateFrom, dateTo);
            // setText lbSumMoney.Text = "0" vì toString("###,###") khong hien gia tri
            if (t == 0) 
            {
                lbSumMoney.Text = "0";
            }
            else
            {
                lbSumMoney.Text = t.ToString("###,###");
            }
            

        }


        private void CmbMonth_SelectedValueChanged(object sender, EventArgs e)
        {
            if (chartMoney.Enabled)
            {
                LoadChartMoney();
            }
            else
            {
                LoadChartProduct();
            }

        }

        private void btnDefautMonth_Click(object sender, EventArgs e)
        {
            lbSumBill.Text = BillDAO.Instance.SumBillMonth().ToString();
            lbSumProduct.Text = BillDAO.Instance.SumPayProductMonth().ToString();
            lbSumMoney.Text = BillDAO.Instance.SumMoneyMonth().ToString("###,###");
        }

        private void CmbYear_SelectedValueChanged(object sender, EventArgs e)
        {
            if (chartMoney.Enabled)
            {
                LoadChartMoney();
            }
            else
            {
                LoadChartProduct();
            }
        }
    }
}
