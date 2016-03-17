using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Management
{
    public class Members
    {
        private string id;
        private string nama;
        private string alamat;
        private string no_hp;
        private string join_date;
        private string join_type;
        private string balance;
        private MyDB db = new MyDB();

        public string Nama
        {
            get
            {
                return nama;
            }

            set
            {
                nama = value;
            }
        }

        public string No_hp
        {
            get
            {
                return no_hp;
            }

            set
            {
                no_hp = value;
            }
        }

        public string Join_date
        {
            get
            {
                return join_date;
            }

            set
            {
                join_date = value;
            }
        }

        public string Join_type
        {
            get
            {
                return join_type;
            }

            set
            {
                join_type = value;
            }
        }

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

        public string Alamat
        {
            get
            {
                return alamat;
            }

            set
            {
                alamat = value;
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

        public bool Save()
        {
            string query;
            if(this.Id != null)
            {
                query = "UPDATE members SET nama='"+this.Nama+"',alamat='"+this.Alamat+"',no_hp='"+this.No_hp+"',join_date=#"+this.Join_date+"#,join_type='"+this.Join_type+"' WHERE ID=" + this.Id;
            }
            else
            {
                query = "INSERT INTO members(nama, alamat, no_hp, join_date, join_type) VALUES ('" + this.Nama + "','" + this.Alamat + "','" + this.No_hp + "',#" + this.Join_date + "#,'" + this.Join_type + "')";
            }
            
            return (new MyDB()).QueryNonSelect(query);
        }

        public bool Delete()
        {
            return (new MyDB()).QueryNonSelect("DELETE FROM members WHERE ID=" + this.Id);
        }

        public Members Find(string id)
        {
            Members mb = new Members();
            List<String[]> data = new List<String[]>();
            data = db.Select("SELECT * FROM members WHERE ID="+id);
            
            for (int i = 0; i < data.Count; i++)
            {
                mb.Id = data[i][0];
                mb.Nama = data[i][1];
                mb.Alamat = data[i][2];
                mb.No_hp = data[i][3];
                mb.Join_date = data[i][4];
                mb.Join_type = data[i][5];
            }
            
            return mb;
        }

        public List<Members> FindAll(string condition)
        {
            return GetAll("SELECT * FROM members WHERE "+condition);
        }

        public List<Members> FindAll()
        {
            return GetAll("SELECT * FROM members");
        }

        public List<String[]> Query(string query)
        {
            return db.Select(query);
        }

        public List<Members> GetAll(string query)
        {
            List<Members> mbs = new List<Members>();
            List<String[]> data = db.Select(query);
            for (int i = 0; i < data.Count; i++)
            {
                Members mb = new Members();
                mb.Id = data[i][0];
                mb.Nama = data[i][1];
                mb.Alamat = data[i][2];
                mb.No_hp = data[i][3];
                mb.Join_date = data[i][4];
                mb.Join_type = data[i][5];
                mbs.Add(mb);
            }
            return mbs;
        }
        
        public string GetBalance(string member_id)
        {
            string s = "0";
            List<String[]> data = (new MyDB()).Select(
                "SELECT balance FROM transaksi_balance WHERE ID IN (SELECT MAX(ID) FROM transaksi_balance WHERE member_id=" + member_id + ") "
                );
            if (data.Count > 0) s = data.ElementAt(0)[0];
            return s;
        }

        public string GetSisaPenarikan(string member_id)
        {
            string sisa = "0";
            List<String[]> data = (new MyDB()).Select(
                "SELECT sisa_tarik FROM transaksi_balance WHERE ID IN (SELECT MAX(ID) FROM transaksi_balance WHERE member_id=" + member_id + ") "
                );
            if (data.Count > 0) sisa = data.ElementAt(0)[0];
            return sisa;
        }

        public Transaksi GetLastKredit(string member_id)
        {
            List<String[]> data = (new MyDB()).Select(
                "SELECT * FROM transaksi_balance WHERE input_date IN (SELECT MAX(input_date) FROM transaksi_balance WHERE member_id=" + member_id + " AND kredit<>0) AND kredit<>0"
                );
            Transaksi tr = new Transaksi();
            if(data.Count>0)
            {
                tr.Id = data.ElementAt(0)[0];
                tr.Kredit = data.ElementAt(0)[1];
                tr.Debet = data.ElementAt(0)[2];
                tr.Sisa_tarik = data.ElementAt(0)[3];
                tr.Balance = data.ElementAt(0)[4];
                tr.Input_date = data.ElementAt(0)[5];
                tr.Member_id = data.ElementAt(0)[6];
            }
            return tr;
        }

        public bool BisaTarik(string member_id)
        {
            bool bisa = false;
            Transaksi tr = this.GetLastKredit(member_id);
            string q = "select * from transaksi_balance where member_id="+member_id;
            return bisa;
        }
    }
}
