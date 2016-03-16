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
    public partial class MemberDetail : Form
    {
        public string member_id;
        public Form_Master f_master;
        //MyDB db;
        DataTable dtable;
        public Members member;
        public string sisa;

        public MemberDetail(Form_Master f_master)
        {
            this.f_master = f_master;
            InitializeComponent();
        }

        public void Init()
        {
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                dtable = (new MyDB()).GetDataTable("SELECT id,input_date, kredit, debet, sisa_tarik, balance FROM transaksi_balance WHERE member_id=" + this.member_id);
                this.member = (new Members()).Find(member_id);
                this.lbl_nama.Text = this.member.Nama;
                this.lbl_alamat.Text = this.member.Alamat;
                this.lbl_nohp.Text = this.member.No_hp;
                this.lbl_tipe.Text = this.member.Join_type;
                this.dataGridView1.DataSource = dtable;
                this.dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[1].DefaultCellStyle.Format = "d MMMM yyyy";
                this.dataGridView1.Columns[2].DefaultCellStyle.Format = "N2";
                this.dataGridView1.Columns[3].DefaultCellStyle.Format = "N2";
                this.dataGridView1.Columns[4].DefaultCellStyle.Format = "N2";
                this.dataGridView1.Columns[5].DefaultCellStyle.Format = "N2";
                this.dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.sisa = (new Members()).GetSisaPenarikan(member_id);
                
                if (Convert.ToInt32(this.sisa) != 0 && (new MyDB()).IsTanggalPenarikan((DateTime.Now).Day))
                {
                    this.button2.Enabled = true;
                }
                else
                {
                    this.button2.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string member_id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            Form_TambahModal f_tbhmodal = new Form_TambahModal(this.f_master, this);
            f_tbhmodal.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Penarikan f_penarikan = new Penarikan(this.f_master, this);
            //f_penarikan.mem
            f_penarikan.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string transaksi_id = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            Transaksi tr = (new Transaksi()).Find(transaksi_id);
            if (tr.Kredit != "0")
            {
                Form_TambahModal fmodal = new Form_TambahModal(this.f_master, this);
                fmodal.tr = tr;
                fmodal.PopulateData();
                fmodal.Show();
            }
            else
            {
                Penarikan f_penarikan = new Penarikan(this.f_master, this);
                f_penarikan.tr = tr;
                f_penarikan.PopulateData();
                f_penarikan.Show();
            }
            //Form_Users fuser = new Form_Users(this);
            //fuser.mb = db.GetMember(member_id);
            //fuser.populate();
            //fuser.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Transaksi tr = (new Transaksi()).Find(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString());
            tr.Delete();
            this.LoadData();
        }
    }
}
