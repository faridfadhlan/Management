using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Management
{
    public partial class Penarikan : Form
    {
        public MemberDetail memberdetail;
        public Form_Master f_master;
        public string member_id;
        public Members member;
        public MyDB db = new MyDB();

        public Penarikan(Form_Master f_master, MemberDetail memberdetail, Members member, string sisa)
        {
            InitializeComponent();
            this.memberdetail = memberdetail;
            this.f_master = f_master;

            this.member = member;
            DateTime sekarang = DateTime.Now;
            this.lbl_nama.Text = member.Nama;
            this.lbl_sisa.Text = sisa;
            this.lbl_bulan.Text = sekarang.ToString("MMM");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] tr_balance = new String[5];
            tr_balance[0] = "0";
            tr_balance[1] = txt_penarikan.Text;
            tr_balance[2] = db.GetBalance(this.member.Id);
            tr_balance[3] = "#" + DateTime.Parse(dpicker_tarik.Text).ToString("yyyy-MM-dd") + "#";
            tr_balance[4] = this.member.Id;
            db.InsertTransaksi(tr_balance);
            memberdetail.LoadData();
            f_master.LoadData();
            this.Dispose();
        }
    }
}
