using System;
using System.Collections;
using System.Diagnostics;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace Management
{
    public class MyDB
    {
        private OleDbConnection db_koneksi;
        private OleDbCommand db_perintah;
        private OleDbDataAdapter db_adapter;
        private DataTable db_datatable;
        private string str_koneksi = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Admini\\Documents\\Visual Studio 2015\\Projects\\Management\\management.accdb";
        
        public MyDB()
        {
            try
            {
                db_koneksi = new OleDbConnection(str_koneksi);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        

        public DataTable GetData(string query)
        {
            db_datatable = new DataTable();
            try
            {
                db_koneksi.Open();
                db_perintah = new OleDbCommand();
                db_perintah.Connection = db_koneksi;
                db_perintah.CommandType = CommandType.Text;
                db_perintah.CommandText = query;
                //MessageBox.Show(query);
                db_adapter = new OleDbDataAdapter(db_perintah);                
                db_adapter.Fill(db_datatable);
                db_koneksi.Close();                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return db_datatable;
        }

        public ArrayList[] GetMemberTidakTarik()
        {
            OleDbDataReader reader = null;
            ArrayList[] members = new ArrayList[2];
            ArrayList mb_compound = new ArrayList();
            ArrayList mb_non_compound = new ArrayList();
            try
            {

                db_koneksi.Open();
                db_perintah = new OleDbCommand();
                db_perintah.Connection = db_koneksi;
                db_perintah.CommandType = CommandType.Text;
                db_perintah.CommandText = "SELECT a.*, b.balance FROM(SELECT id, join_type FROM members WHERE ID NOT IN(SELECT member_id FROM transaksi_balance WHERE DatePart('m', input_date) = "+(DateTime.Now.Month-1)+" AND kredit = 0 GROUP BY member_id, DatePart('m', input_date)))  AS a INNER JOIN (SELECT member_id, balance FROM transaksi_balance WHERE id IN (SELECT MAX(id) FROM transaksi_balance GROUP BY member_id))  AS b ON a.id = b.member_id";
                reader = db_perintah.ExecuteReader();
                while (reader.Read())
                {
                    Members mb = new Members();
                    mb.Id = reader.GetValue(0).ToString();
                    mb.Join_type = reader.GetValue(1).ToString();
                    if(mb.Join_type == "Compound")
                    {
                        mb_compound.Add(mb);
                    }
                    else
                    {
                        mb_non_compound.Add(mb);
                    }
                }
                reader.Close();
                db_koneksi.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            members[0] = mb_compound;
            members[1] = mb_non_compound;

            return members;
        }

        public void InsertUsers(Members mb)
        {
            try
            {
                
                db_koneksi.Open();
                db_perintah = new OleDbCommand();
                db_perintah.Connection = db_koneksi;
                db_perintah.CommandType = CommandType.Text;
                db_perintah.CommandText = "INSERT INTO members(nama,alamat,no_hp,join_date,join_type) "+
                    "VALUES('" + mb.Nama+"','"+mb.Alamat+"','"+mb.No_hp+ "',#" + mb.Join_date + "#,'" + mb.Join_type + "')";
                //MessageBox.Show(db_perintah.CommandText);
                try
                {
                    db_perintah.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                
                db_koneksi.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void UpdateMember(Members mb)
        {
            try
            {

                db_koneksi.Open();
                db_perintah = new OleDbCommand();
                db_perintah.Connection = db_koneksi;
                db_perintah.CommandType = CommandType.Text;
                db_perintah.CommandText = "UPDATE members SET nama='" +mb.Nama + "', alamat='" + mb.Alamat + "',no_hp='" + mb.No_hp + "',join_date=" + mb.Join_date + ",join_type='" + mb.Join_type + "' WHERE ID="+mb.Id;
                Debug.WriteLine(db_perintah.CommandText);
                try
                {
                    db_perintah.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }

                db_koneksi.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void DeleteUsers(string[] id)
        {
            string ids = String.Join(",", id);
            try
            {

                db_koneksi.Open();
                db_perintah = new OleDbCommand();
                db_perintah.Connection = db_koneksi;
                db_perintah.CommandType = CommandType.Text;
                db_perintah.CommandText = "DELETE FROM members WHERE id IN ("+ids+")";
                //MessageBox.Show(db_perintah.CommandText);
                try
                {
                    db_perintah.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }

                db_koneksi.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public Members GetMember(string member_id)
        {
            OleDbDataReader reader = null;
            Members membersa = new Members();
            try
            {

                db_koneksi.Open();                
                db_perintah = new OleDbCommand();
                db_perintah.Connection = db_koneksi;
                db_perintah.CommandType = CommandType.Text;
                db_perintah.CommandText = "SELECT * FROM members WHERE id=" + member_id;
                //MessageBox.Show(db_perintah.CommandText);
                reader = db_perintah.ExecuteReader();
                while (reader.Read())
                {
                    //MessageBox.Show(reader.GetValue(1).ToString());
                    membersa.Id = reader.GetValue(0).ToString();
                    membersa.Nama = reader.GetValue(1).ToString();
                    membersa.Alamat = reader.GetValue(2).ToString();
                    membersa.No_hp = reader.GetValue(3).ToString();
                    membersa.Join_date = reader.GetValue(4).ToString();
                    membersa.Join_type = reader.GetValue(5).ToString();
                    //Insert code to process data.
                }
                reader.Close();
                db_koneksi.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return membersa;
        }

        public string GetBalance(string member_id)
        {
            OleDbDataReader reader = null;
            string balance = null;
            try
            {

                db_koneksi.Open();
                db_perintah = new OleDbCommand();
                db_perintah.Connection = db_koneksi;
                db_perintah.CommandType = CommandType.Text;
                db_perintah.CommandText = "SELECT balance FROM transaksi_balance WHERE id IN (SELECT MAX(id) FROM transaksi_balance WHERE member_id="+member_id+")";
                //MessageBox.Show(db_perintah.CommandText);
                reader = db_perintah.ExecuteReader();
                while (reader.Read())
                {
                    balance = reader.GetValue(0).ToString();
                }
                reader.Close();
                db_koneksi.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return balance;
        }

        public string GetBalanceAtMonth(string member_id, string bulan)
        {
            OleDbDataReader reader = null;
            string balance = null;
            try
            {

                db_koneksi.Open();
                db_perintah = new OleDbCommand();
                db_perintah.Connection = db_koneksi;
                db_perintah.CommandType = CommandType.Text;
                db_perintah.CommandText = "SELECT balance FROM transaksi_balance WHERE id IN (SELECT MAX(id) FROM transaksi_balance WHERE member_id=" + member_id + " AND DatePart('m', [input_date])="+bulan+")";
                //Debug.WriteLine(db_perintah.CommandText);
                reader = db_perintah.ExecuteReader();
                while (reader.Read())
                {
                    balance = reader.GetValue(0).ToString();
                }
                reader.Close();
                db_koneksi.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return balance;
        }



        public void InsertTransaksi(string[] balance)
        {
            try
            {

                db_koneksi.Open();
                db_perintah = new OleDbCommand();
                db_perintah.Connection = db_koneksi;
                db_perintah.CommandType = CommandType.Text;

                db_perintah.CommandText = "INSERT INTO transaksi_balance(kredit,debet,balance,input_date,member_id) " +
                    "VALUES('" + balance[0] + "','" + balance[1] + "','" + balance[2] + "'," + balance[3] + ",'" + balance[4]+"')";
                //MessageBox.Show(db_perintah.CommandText);
                try
                {
                    db_perintah.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }

                db_koneksi.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        
        public string GetSisaPenarikan(string member_id, string bulan)
        {
            string sisa = null;
            OleDbDataReader reader = null;
            try
            {
                string balance = GetBalanceAtMonth(member_id, (Convert.ToInt32(bulan)-1).ToString());
                //MessageBox.Show(balance);
                if (balance == null || Convert.ToInt32(balance) == 0)
                {
                    sisa = "0";
                }
                else
                {
                    db_koneksi.Open();
                    db_perintah = new OleDbCommand();
                    db_perintah.Connection = db_koneksi;
                    db_perintah.CommandType = CommandType.Text;
                    db_perintah.CommandText = "select b.bulan,b.penarikan from (select id as bulan from bulan where id=" + bulan + ") a LEFT JOIN (select DatePart('m', [input_date]) as bulan,sum(debet) as penarikan from transaksi_balance WHERE member_id=" + member_id + " AND DatePart('m', [input_date])=" + bulan + " group by DatePart('m', [input_date])) b ON a.bulan=b.bulan";
                    Debug.WriteLine(db_perintah.CommandText);
                    reader = db_perintah.ExecuteReader();


                    while (reader.Read())
                    {
                        int penarikan = 0;
                        if (reader.GetValue(1) != DBNull.Value) penarikan = Convert.ToInt32(reader.GetValue(1));
                        sisa = (Convert.ToInt32(balance) / 2 - penarikan).ToString();
                    }

                    reader.Close();
                    db_koneksi.Close();
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
            return sisa;
        }

        public bool IsTanggalPenarikan(int tgl)
        {
            OleDbDataReader reader = null;
            bool penarikan = false;
            try
            {
                db_koneksi.Open();
                db_perintah = new OleDbCommand();
                db_perintah.Connection = db_koneksi;
                db_perintah.CommandType = CommandType.Text;
                db_perintah.CommandText = "select tanggal from tanggal where tanggal="+tgl.ToString();
                reader = db_perintah.ExecuteReader();
                if (reader.HasRows) penarikan = true;
                reader.Close();
                db_koneksi.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return penarikan;
        }

        public ArrayList GetTanggalPenarikan()
        {
            OleDbDataReader reader = null;
            ArrayList tgl = new ArrayList();
            try
            {
                    db_koneksi.Open();
                    db_perintah = new OleDbCommand();
                    db_perintah.Connection = db_koneksi;
                    db_perintah.CommandType = CommandType.Text;
                    db_perintah.CommandText = "select tanggal from tanggal";
                    //Debug.WriteLine(db_perintah.CommandText);
                    reader = db_perintah.ExecuteReader();


                    while (reader.Read())
                    {
                        tgl.Add(reader.GetValue(0));
                    }

                    reader.Close();
                    db_koneksi.Close();
                

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return tgl;
        }
        
        public void SimpanTanggalPenarikan(ArrayList tgl)
        {
            try
            {
                db_koneksi.Open();
                db_perintah = new OleDbCommand();
                db_perintah.Connection = db_koneksi;
                db_perintah.CommandType = CommandType.Text;

                db_perintah.CommandText = "DELETE FROM tanggal";
                bool sukses = false;
                //MessageBox.Show(db_perintah.CommandText);
                try
                {
                    db_perintah.ExecuteNonQuery();
                    sukses = true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }

                db_koneksi.Close();

                if (sukses)
                {
                    db_koneksi.Open();
                    for (int i = 0; i < tgl.Count; i++)
                    {
                        db_perintah.CommandText = "INSERT INTO tanggal (tanggal) VALUES ("+tgl[i].ToString()+")";
                        db_perintah.ExecuteNonQuery();
                    }
                    db_koneksi.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }  
        
        public Transaksi GetTransaksi(string id)
        {
            OleDbDataReader reader = null;
            Transaksi tr = new Transaksi();
            try
            {

                db_koneksi.Open();
                db_perintah = new OleDbCommand();
                db_perintah.Connection = db_koneksi;
                db_perintah.CommandType = CommandType.Text;
                db_perintah.CommandText = "SELECT * FROM transaksi_balance WHERE id=" + id;
                //MessageBox.Show(db_perintah.CommandText);
                reader = db_perintah.ExecuteReader();
                while (reader.Read())
                {
                    //MessageBox.Show(reader.GetValue(1).ToString());
                    tr.Id = reader.GetValue(0).ToString();
                    tr.Kredit = reader.GetValue(1).ToString();
                    tr.Debet = reader.GetValue(2).ToString();
                    tr.Balance = reader.GetValue(3).ToString();
                    tr.Input_date = reader.GetValue(4).ToString();
                    tr.Member_id = reader.GetValue(5).ToString();
                    //Insert code to process data.
                }
                reader.Close();
                db_koneksi.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return tr;
        }  
    }
}
