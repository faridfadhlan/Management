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
        public Transaksi tr = new Transaksi();

        public Penarikan(Form_Master f_master, MemberDetail memberdetail)
        {
            InitializeComponent();
            this.memberdetail = memberdetail;
            this.f_master = f_master;
            DateTime sekarang = DateTime.Now;
            this.lbl_nama.Text = this.memberdetail.member.Nama;
            this.lbl_sisa.Text = Convert.ToDouble(this.memberdetail.member.GetSisaPenarikan(memberdetail.member.Id)).ToString();// "C", new System.Globalization.CultureInfo("id-ID"));
            this.lbl_bulan.Text = sekarang.ToString("MMM");
        }

        public void PopulateData()
        {
            if(tr != null)
            {
                this.txt_penarikan.Text = this.tr.Debet;
                this.dpicker_tarik.Value = DateTime.Parse(this.tr.Input_date);
                this.lbl_bulan.Text = DateTime.Parse(this.tr.Input_date).ToString("MMM");
                this.lbl_sisa.Text = (Convert.ToDouble(this.tr.Sisa_tarik)+ Convert.ToDouble(this.tr.Debet)).ToString();// "C", new System.Globalization.CultureInfo("id-ID"));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            tr.Kredit = "0";
            tr.Debet = txt_penarikan.Text;
            tr.Sisa_tarik = (Convert.ToDouble(this.lbl_sisa.Text)-Convert.ToDouble(tr.Debet)).ToString();
            tr.Balance = memberdetail.member.GetBalance(memberdetail.member.Id);
            tr.Input_date = DateTime.Parse(dpicker_tarik.Text).ToString("yyyy-MM-dd");
            tr.Member_id = memberdetail.member.Id;
            tr.Save();
            memberdetail.LoadData();
            f_master.LoadData();
            this.Dispose();
        }
    }
}
