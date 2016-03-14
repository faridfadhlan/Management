using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Management
{
    public partial class Form_TambahModal : Form
    {
        private Members membersa;
        private string member_id;
        private MemberDetail fm;
        private string balance;
        public Form_Master f_master;
        public Transaksi tr = null;

        public Form_TambahModal(Form_Master f_master, MemberDetail fm, string member_id)
        {
            InitializeComponent();
            this.f_master = f_master;
            this.fm = fm;
            this.member_id = member_id;
            MyDB members = new MyDB();
            membersa = members.GetMember(member_id);
            this.lbl_nama.Text = membersa.Nama;
            balance = members.GetBalance(member_id);
            if (balance == null) balance = "0";
            this.lbl_balance.Text = Convert.ToDouble(balance).ToString("C", new System.Globalization.CultureInfo("id-ID"));
        }

        public void PopulateData()
        {
            if(tr != null)
            {
                this.txt_modal.Text = tr.Kredit;
                this.dpicker_tambah.Value = DateTime.Parse(tr.Input_date);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] tr_balance = new String[5];
            MyDB db = new MyDB();
            Transaksi tr1 = new Transaksi();

            tr1.Kredit = txt_modal.Text;
            tr_balance[0] = txt_modal.Text;
            tr_balance[1] = "0";
            tr_balance[2] = (Convert.ToInt64(txt_modal.Text) + Convert.ToDouble(balance)).ToString();
            tr_balance[3] = "#" + DateTime.Parse(dpicker_tambah.Text).ToString("yyyy-MM-dd") + "#";
            tr_balance[4] = this.member_id;
            //MessageBox.Show(String.Join(",", tr_balance));
            db.InsertTransaksi(tr_balance);
            fm.LoadData();
            f_master.LoadData();
            this.Dispose();
        }
    }
}
