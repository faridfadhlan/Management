using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Management
{
    public partial class Form_Master : Form
    {
        DataTable dtable;

        public Form_Master()
        {
            InitializeComponent();
            InitBalance();
            LoadData();
        }

        public void InitBalance()
        {
            (new Transaksi()).GenerateSisaPenarikan();
        }

        public void LoadData()
        {
            try
            {
                dtable = (new MyDB()).GetDataTable("SELECT a.*,b.balance FROM members a LEFT JOIN (SELECT balance,member_id FROM transaksi_balance WHERE ID IN (SELECT max(ID) FROM transaksi_balance GROUP BY member_id)) b ON a.id=b.member_id");
                this.dataGridView1.DataSource = dtable;
                this.dataGridView1.Columns[1].HeaderText = "Nama Lengkap";
                this.dataGridView1.Columns[2].HeaderText = "Alamat";
                this.dataGridView1.Columns[3].HeaderText = "No Handphone";
                this.dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[4].HeaderText = "Tanggal Join";
                this.dataGridView1.Columns[4].DefaultCellStyle.Format = "d MMMM yyyy";
                this.dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[5].HeaderText = "Tipe Join";
                this.dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[6].HeaderText = "Modal";
                this.dataGridView1.Columns[6].DefaultCellStyle.Format = "N2";
                this.dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_tambah_Click(object sender, EventArgs e)
        {
            Form_Users fusers = new Form_Users(this);
            fusers.Text = "Tambah User";
            fusers.Show();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            for(int j=0;j< dataGridView1.SelectedRows.Count; j++)
            {
                Members mb = new Members();
                mb.Id = dataGridView1.SelectedRows[j].Cells[0].Value.ToString();
                mb.Delete();
            }
            LoadData();
        }
        

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MemberDetail detail = new MemberDetail(this);
            detail.member_id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            detail.Init();
            detail.Show();
        }

        private void penggunaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingPenarikan f_penarikan = new SettingPenarikan();
            f_penarikan.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string member_id = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            Form_Users fuser = new Form_Users(this);
            fuser.mb = (new Members()).Find(member_id);
            fuser.populate();
            fuser.Show();
        }

        
    }
}
