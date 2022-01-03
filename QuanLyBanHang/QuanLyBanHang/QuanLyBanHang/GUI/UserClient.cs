
using System.Data;
using QuanLyBanHang.DAO;
using System.Windows.Forms;

namespace QuanLyBanHang.GUI
{
    public partial class UserClient : UserControl
    {

        public UserClient()
        {
            InitializeComponent();

            loadDataGidViewClient();
            
        }

        


        private void loadDataGidViewClient()
        {
           dtgvClient.DataSource = ClientDAO.Instance.GetListClient();
        }


        private void loadDataAfterSearch()
        {
            dtgvClient.DataSource = ClientDAO.Instance.searchListClientByPhone(txtSearch.Text, txtSearch.Text);
        }

        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            loadDataAfterSearch();
        }
    }
}
