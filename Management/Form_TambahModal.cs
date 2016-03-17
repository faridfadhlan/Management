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
        private Members mb;
        private MemberDetail f_detail;
        private string balance;
        public Form_Master f_master;
        public Transaksi tr = new Transaksi();

        public Form_TambahModal(Form_Master f_master, MemberDetail f_detail)
        {
            InitializeComponent();
            this.f_master = f_master;
            this.f_detail = f_detail;
            mb = (new Members()).Find(f_detail.member_id);
            this.lbl_nama.Text = mb.Nama;
            //this.txt_modal.
            this.balance = mb.GetBalance(mb.Id);
            this.lbl_balance.Text = Convert.ToDouble(this.balance).ToString("C", new System.Globalization.CultureInfo("id-ID"));
        }

        public void PopulateData()
        {
            if(tr != null)
            {
                this.balance = this.tr.Balance;
                this.txt_modal.Text = tr.Kredit;
                this.dpicker_tambah.Value = DateTime.Parse(tr.Input_date);
                this.lbl_balance.Text = (Convert.ToDouble(balance) - Convert.ToDouble(tr.Kredit)).ToString("C", new System.Globalization.CultureInfo("id-ID"));
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            tr.Debet = "0";
            tr.Balance = (Convert.ToDouble(txt_modal.Text) + Convert.ToDouble(balance) - Convert.ToDouble(this.tr.Kredit)).ToString();
            tr.Sisa_tarik = (Convert.ToDouble(tr.Balance)/2).ToString();
            tr.Kredit = txt_modal.Text;
            tr.Input_date = DateTime.Parse(dpicker_tambah.Text).ToString("yyyy-MM-dd");
            tr.Member_id = this.mb.Id;
            tr.Save();
            f_detail.LoadData();
            f_master.LoadData();
            this.Dispose();
        }

        private void masked_event(object sender, EventArgs e)
        {

            txt_modal.Text = string.Format("{0:#,##0}", double.Parse(txt_modal.Text));
        }
    }
}
