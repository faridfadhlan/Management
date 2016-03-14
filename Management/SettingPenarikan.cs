using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Management
{
    public partial class SettingPenarikan : Form
    {
        //private int[] tgl_penarikan_arr = new int[] { 1,4,8,20 };
        private ArrayList tgl_penarikan = new ArrayList();
        private ArrayList tgl_tersisa = new ArrayList();
        private MyDB db = new MyDB();

        public SettingPenarikan()
        {
            InitializeComponent();

            this.lbl_judul.Text = "Pilih tanggal penarikan tiap bulan dan pindahkan ke listbox kanan";
            tgl_penarikan = db.GetTanggalPenarikan();
            PopulateLB1();            
            PopulateLB2();
        }

        

        public void PopulateLB1()
        {
            //MessageBox.Show("ss");
            listBox1.Items.Clear();
            for (int j = 1; j <= 31; j++)
            {
                if (!tgl_penarikan.Contains(j))
                {
                    listBox1.Items.Add(j.ToString());
                }
            }
        }

        public void PopulateLB2()
        {
            listBox2.Items.Clear();            

            for(int i = 0; i < tgl_penarikan.Count; i++)
            {
                listBox2.Items.Add(tgl_penarikan[i].ToString());
            }
        }

        

        

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(listBox1.GetItemText(listBox1.SelectedItem));
            string val = listBox1.GetItemText(listBox1.SelectedItem);
            tgl_penarikan.Add(Convert.ToInt32(val));
            tgl_penarikan.Sort();
            PopulateLB1();
            PopulateLB2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tgl_penarikan.RemoveAt(listBox2.SelectedIndex);
            PopulateLB1();
            PopulateLB2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MyDB db = new MyDB();
            db.SimpanTanggalPenarikan(tgl_penarikan);
            this.Dispose();
        }
    }
}
