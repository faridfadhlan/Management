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
    }
}
