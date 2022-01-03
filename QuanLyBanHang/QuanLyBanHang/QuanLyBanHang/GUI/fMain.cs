using QuanLyBanHang.DTO;
using System;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public partial class fMain : Form
    {
        public static Account loginAccount;
        public static int accountType;
        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; }
        }
        public fMain(Account account)
        {
            this.LoginAccount = account;
            accountType = account.AccountType;
            InitializeComponent();
            txtUserName.Text = LoginAccount.UserName;
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            LoadSidePanel(btnHome);
            userHome1.BringToFront();
            timer1.Enabled = true;
            timer1.Start();
            txtTime.Text = DateTime.Now.ToLongTimeString();
        }
        public void LoadSidePanel(Button bt)
        {
            SidePanel.Height = bt.Height;
            SidePanel.Top = bt.Top;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            LoadSidePanel(btnHome);
            userHome1.BringToFront();
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            if(loginAccount.AccountType == 0)
            {
                LoadSidePanel(btnAccount);
                userAccount1.BringToFront();
            }
            else
            {
                MessageBox.Show("Quản lý mới vào được mục này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnStaff_Click(object sender, EventArgs e)
        {
            if (loginAccount.AccountType == 0)
            {
                LoadSidePanel(btnStaff);
                userStaff1.BringToFront();
            }
            else
            {
                MessageBox.Show("Quản lý mới vào được mục này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShopping_Click(object sender, EventArgs e)
        {
            if((loginAccount.AccountType == 0) || (loginAccount.AccountType == 1))
            {
                LoadSidePanel(btnShopping);
                userShopping1.BringToFront();
            }
            else
            {
                MessageBox.Show("Quản lý hoặc Nhân viên bán hàng mới vào được mục này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            if ((loginAccount.AccountType == 0) || (loginAccount.AccountType == 2))
            {
                LoadSidePanel(btnBill);
                userBill1.BringToFront();
            }
            else
            {
                MessageBox.Show("Quản lý hoặc Kế toán mới vào được mục này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            if ((loginAccount.AccountType == 0) || (loginAccount.AccountType == 3) || (loginAccount.AccountType == 1))
            {
                LoadSidePanel(btnProducts);
                userProducts1.BringToFront();
            }
            else
            {
                MessageBox.Show("Quản lý hoặc Thủ kho mới vào được mục này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStatistical_Click(object sender, EventArgs e)
        {
            if ((loginAccount.AccountType == 0) || (loginAccount.AccountType == 2))
            {
                LoadSidePanel(btnStatistical);
                userStatistical1.BringToFront();
            }
            else
            {
                MessageBox.Show("Quản lý hoặc Kế toán mới vào được mục này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có thật sự muốn đăng xuất ?", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            switch (result)
            {
                case DialogResult.OK:
                    fLogin f = new fLogin();
                    this.Close();
                    f.Show();
                    break;
                default:
                    break;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có thật sự muốn thoát chương trình không ?", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            switch (result)
            {
                case DialogResult.OK:
                    Application.Exit();
                    break;
                default:
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void btnCollapse_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            LoadSidePanel(btnClient);
            userClient1.BringToFront();
        }

        // thu gọn app vào khay icon ở cạnh nút wifi -- notify tạo icon ẩn hiện 
        private void fMain_Resize(object sender, EventArgs e)
        {
            //if (FormWindowState.Minimized == this.WindowState)
            //{
            //    this.Hide();
            //    notifyIcon1.Visible = true;
            //}
        }

        // toolbox notify 
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Show();
            //this.WindowState = FormWindowState.Normal;
            //notifyIcon1.Visible = false;
        }
    }
}
