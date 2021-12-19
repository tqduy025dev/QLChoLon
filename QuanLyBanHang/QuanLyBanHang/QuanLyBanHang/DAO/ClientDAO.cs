﻿using QuanLyBanHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.DAO
{
    public class ClientDAO
    {
        private static ClientDAO instance;

        public static ClientDAO Instance 
        {
            get 
            {
                if (instance == null) instance = new ClientDAO();
                return instance; 
            }
            private set => instance = value; 
        }
        private ClientDAO() { }
        public bool InsertClient(string phoneNumber, string clientName, string address)
        {
            string query = string.Format("insert Client ( phoneNumber , clientName , address , accumulated) values (N'{0}', N'{1}', N'{2}', {3})", phoneNumber, clientName, address, 0);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateClient(string phoneNumber, string clientName, string address)
        {
            string query = string.Format("Update Client set clientName = '{1}' , address = '{2}' where phoneNumber = '{0}' ", phoneNumber, clientName, address);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateClient(string phoneNumber, int accumulated)
        {
            string query = string.Format("Update Client set accumulated = accumulated + {1}  where phoneNumber = '{0}' ", phoneNumber, accumulated);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public Client SearchClientByPhone(string phoneNumber)
        {
            string query = string.Format("Select * from Client where phoneNumber = '" + phoneNumber + "'");
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            
            if(data.Rows.Count > 0)
            {
                Client client = new Client();
                foreach (DataRow row in data.Rows)
                {
                    client.PhoneNumber = row["phoneNumber"].ToString();
                    client.ClientName = row["clientName"].ToString();
                    client.Address = row["address"].ToString();
                    client.Accumulated = (int) row["accumulated"];
                }
                return client;
            }
            else
            {
                return null;
            }
        }
    }
}
