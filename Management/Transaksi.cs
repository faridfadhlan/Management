using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Management
{
    public class Transaksi
    {
        private string id;
        private string kredit;
        private string debet;
        private string sisa_tarik;
        private string balance;
        private string input_date;
        private string member_id;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Kredit
        {
            get
            {
                return kredit;
            }

            set
            {
                kredit = value;
            }
        }

        public string Debet
        {
            get
            {
                return debet;
            }

            set
            {
                debet = value;
            }
        }

        public string Input_date
        {
            get
            {
                return input_date;
            }

            set
            {
                input_date = value;
            }
        }

        public string Member_id
        {
            get
            {
                return member_id;
            }

            set
            {
                member_id = value;
            }
        }

        public string Balance
        {
            get
            {
                return balance;
            }

            set
            {
                balance = value;
            }
        }

        public string Sisa_tarik
        {
            get
            {
                return sisa_tarik;
            }

            set
            {
                sisa_tarik = value;
            }
        }

        public bool Save()
        {
            string query;
            if (this.Id == null)
                query = "INSERT INTO transaksi_balance(kredit, debet, sisa_tarik, balance, input_date, member_id) VALUES ('" + this.Kredit + "','" + this.Debet + "','" + this.Sisa_tarik + "','" + this.Balance + "',#" + this.Input_date + "#,'" + this.Member_id + "')";
            else
                query = "UPDATE transaksi_balance SET kredit='"+this.Kredit+"', debet='"+this.Debet+"', sisa_tarik='"+this.Sisa_tarik+"', balance='"+this.Balance+"', input_date=#"+this.Input_date+"# WHERE ID=" + this.Id;
            return (new MyDB()).QueryNonSelect(query);
        }

        public bool Delete()
        {
            return (new MyDB()).QueryNonSelect("DELETE FROM transaksi_balance WHERE ID="+this.Id);
        }

        public Transaksi Find(string id)
        {
            Transaksi tr = new Transaksi();
            List<String[]> data = new List<String[]>();
            data = (new MyDB()).Select("SELECT * FROM transaksi_balance WHERE ID=" + id);

            for (int i = 0; i < data.Count; i++)
            {
                tr.Id = id;
                tr.Kredit = data[i][1];
                tr.Debet = data[i][2];
                tr.Sisa_tarik = data[i][3];
                tr.Balance = data[i][4];
                tr.Input_date = data[i][5];
                tr.Member_id = data[i][6];
            }

            return tr;
        } 

        public bool GenerateKredit()
        {
            bool sukses = false;
            List<String[]> mb = (new Members()).Query("SELECT * FROM members WHERE join_type='Non Compound'");
            
            return sukses;
        }
    }
}
