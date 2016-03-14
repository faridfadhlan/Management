using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
