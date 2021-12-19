using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.DTO
{
    public class Client
    {
        private string phoneNumber;
        private string clientName;
        private string address;
        private int accumulated;

        public string ClientName { get => clientName; set => clientName = value; }
        public string Address { get => address; set => address = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public int Accumulated { get => accumulated; set => accumulated = value; }

        public Client()
        {
        }

        public Client(string _phoneNumber, string _clientName, string _address, int _accumulated)
        {
            this.ClientName = _clientName;
            this.Address = _address;
            this.PhoneNumber = _phoneNumber;
            this.Accumulated = _accumulated;
        }

    }
}
