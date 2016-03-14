using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Management
{
    public partial class Form_Users : Form
    {
        private MyDB users;
        private string[] data_user = new String[5];
        public Form_Master pengguna;
        public Members mb = null;

        public Form_Users(Form_Master pengguna)
        {
            InitializeComponent();
            this.pengguna = pengguna;
            
        }

        public void populate()
        {
            if (mb != null)
            {
                this.txt_nama.Text = mb.Nama;
                this.txt_alamat.Text = mb.Alamat;
                this.txt_no_hp.Text = mb.No_hp;
                this.cbx_join.Text = mb.Join_type;
                //MessageBox.Show(mb.Join_date);
                this.dpicker_join.Value = DateTime.Parse(mb.Join_date);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            users = new MyDB();
            Members member = new Members();
            if (mb != null) { member.Id = mb.Id; }
            member.Nama = txt_nama.Text;
            member.No_hp = txt_no_hp.Text;
            member.Alamat = txt_alamat.Text;
            member.Join_type = cbx_join.Text;
            member.Join_date = "#" + DateTime.Parse(dpicker_join.Text).ToString("yyyy-MM-dd") + "#";
            
            if (mb != null)
            {
                users.UpdateMember(member);
            }
            else
            {
                users.InsertUsers(member);
            }
            
            this.pengguna.LoadData();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_no_hp_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_alamat_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_nama_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
