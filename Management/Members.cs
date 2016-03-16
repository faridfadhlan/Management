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
        private MyDB db;

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

        public Members FindMember(string id)
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
    }
}
