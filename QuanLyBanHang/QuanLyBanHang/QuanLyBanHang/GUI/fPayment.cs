﻿using QuanLyBanHang.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang.BUS
{
    public partial class fPayment : Form
    {
        String lbLoichao = "Cảm ơn quý khách!";
        public fPayment()
        {
            InitializeComponent();
            txtPayment.Text = UserShopping.payment.ToString("###,###");
        }

        private void btn50k_Click(object sender, EventArgs e)
        {
            double money = 50000;
            txtClientMoney.Text = money.ToString("###,###");
        }

        private void btn100k_Click(object sender, EventArgs e)
        {
            double money = 100000;
            txtClientMoney.Text = money.ToString("###,###");
        }

        private void btn200k_Click(object sender, EventArgs e)
        {
            double money = 200000;
            txtClientMoney.Text = money.ToString("###,###");
        }

        private void btn500k_Click(object sender, EventArgs e)
        {
            double money = 500000;
            txtClientMoney.Text = money.ToString("###,###");
        }

        private void btnPayEnough_Click(object sender, EventArgs e)
        {
            txtClientMoney.Text = txtPayment.Text;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtClientMoney_TextChanged(object sender, EventArgs e)
        {
            double payment = double.Parse(txtPayment.Text);
            double clientMoney = 0;
            try
            {
                clientMoney = double.Parse(txtClientMoney.Text);
            }
            catch (Exception)
            {
            }

            //if (txtClientMoney.Text.Equals(""))
            //{
            //    clientMoney = 0;
            //}

            double extraMoney = clientMoney - payment;
            if (extraMoney == 0)
            {
                txtExtraMoney.Text = "0";
            }
            else
            {
                txtExtraMoney.Text = extraMoney.ToString("###,###");
            }


        }

        private void txtClientMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (txtClientMoney.Text.Trim().Length < 1)
            {
                MessageBox.Show("Vui lòng nhập số tiền khách hàng đưa để tiếp tục !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (double.Parse(txtClientMoney.Text) < double.Parse(txtPayment.Text))
            {
                MessageBox.Show("Số tiền khách đưa chưa đủ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                PrintDialog printDialog = new PrintDialog();

                PrintDocument printDocument = new PrintDocument();

                printDialog.Document = printDocument; //add the document to the dialog box...        

                printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(CreateReceipt); //add an event handler that will do the printing

                //on a till you will not want to ask the user where to print but this is fine for the test envoironment.

                DialogResult result = printDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    printDocument.Print();

                }

                Close();
            }
        }

        public void CreateReceipt(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //--------------------------------------------//
            string lbtenshop = "Cửa Hàng Điện Máy CHỢ LỚN";
            string lbdiachi = "99 Lê Văn Việt, P.Tăng Nhơn Phú A, Quận 9\nThành phố Hồ Chí Minh";
            string lbSDT = "DST : 0933.933.933";
            double Percent = UserShopping.Percent;
            double DisCount = UserShopping.DisCount;
            double SumMoney = UserShopping.SumMoney;

            double cash = double.Parse(txtClientMoney.Text);
            double change = 0;

            //this prints the reciept
            Graphics graphic = e.Graphics;

            Font font = new Font("Courier New", 12); //must use a mono spaced font as the spaces need to line up

            double fontHeight = font.GetHeight();

            int startX = 10;
            int startY = 10;
            int offset = 60;

            graphic.DrawString(lbtenshop, new Font("Courier New", 18), new SolidBrush(Color.Black), startX, startY);

            graphic.DrawString(lbdiachi, font, new SolidBrush(Color.Black), startX, 40);

            graphic.DrawString(lbSDT, font, new SolidBrush(Color.Black), startX, 80);

            offset = offset + 50;
            string top = "Sản phẩm".PadRight(21) + "SL".PadRight(10) + "Giá".PadRight(10);
            graphic.DrawString(top, font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight; //make the spacing consistent
            graphic.DrawString("------------------------------------", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight + 5; //make the spacing consistent


            double totalprice = 0;
            foreach (DataRow row in BillDAO.Instance.PrintBill().Rows)
            {
                string productName = row["Product Name"].ToString();
                int amount = (int)row["Amount"];
                double payMoney = (double)row["Into Money"];
                //    MessageBox.Show(productPrice.ToString());
                //create the string to print on the reciept
                //  string productDescription = item;
                //string productTotal = item.Substring(item.Length - 6, 6);
                double productTotal = double.Parse(txtPayment.Text);
                //   float productPrice = float.Parse(item.Substring(item.Length - 5, 5));

                //totalprice += productPrice;
                totalprice = productTotal;

                string ten = productName;
                string dongia = payMoney.ToString();
                string slsp = amount.ToString();
                graphic.DrawString(ten, font, new SolidBrush(Color.Black), startX, startY + offset);
                graphic.DrawString(slsp, font, new SolidBrush(Color.Black), 230, startY + offset);
                graphic.DrawString(dongia, font, new SolidBrush(Color.Black), 320, startY + offset);
                offset = offset + (int)fontHeight + 5; //make the spacing consistent


            }

            change = float.Parse(txtExtraMoney.Text);

            //when we have drawn all of the items add the total

            offset = offset + 20; //make some room so that the total stands out.

            graphic.DrawString("Tổng cộng ".PadRight(30) + SumMoney.ToString("###,###"), new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight + 5;

            if (Percent != 0)
            {
                graphic.DrawString("Giảm % ".PadRight(30) + Percent.ToString("###,###") + "%" , new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
                offset = offset + (int)fontHeight + 5;
            }

            if (DisCount != 0)
            {
                graphic.DrawString("Giảm tiền ".PadRight(30) + DisCount.ToString("###,###"), new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
                offset = offset + (int)fontHeight + 5;
            }

            offset = offset + 20;
            graphic.DrawString("Tiền khách đưa ".PadRight(30) + cash.ToString("###,###"), font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + 15;
            graphic.DrawString("Tiền thối lại ".PadRight(30) + change.ToString("###,###"), font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + 30; //make some room so that the total stands out.

            graphic.DrawString("------------------------------------", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight + 10;

            graphic.DrawString("Thành tiền ".PadRight(30) + totalprice.ToString("###,###"), new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Red), startX, startY + offset);

            graphic.DrawString(lbLoichao, font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + 15;


        }


    }
}
