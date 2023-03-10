using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Sinema_Rezervasyon_Otomasyonu
{
    public partial class Form2 : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;

        public Form2()
        {
            InitializeComponent();
        }

        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=sinema.accdb");

        private void verilerigoruntule()
        {
            bag.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = bag;
            komut.CommandText = ("Select * From satis");
            OleDbDataReader oku =  komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["satis_ID"].ToString();
                ekle.SubItems.Add(oku["film_ID"].ToString());
                ekle.SubItems.Add(oku["salon_ID"].ToString());
                ekle.SubItems.Add(oku["tarih"].ToString());
                ekle.SubItems.Add(oku["seans"].ToString());
                ekle.SubItems.Add(oku["satis_Adi"].ToString());
                ekle.SubItems.Add(oku["satis_Soyadi"].ToString());
                ekle.SubItems.Add(oku["koltuk_No"].ToString());

                listView1.Items.Add(ekle);
            }
            bag.Close(); 
        }
        private void verilerisil()
        {
            bag.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = bag;
            komut.CommandText = ("DELETE FROM satis WHERE koltuk_No=");
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["satis_ID"].ToString();
                ekle.SubItems.Add(oku["film_ID"].ToString());
                ekle.SubItems.Add(oku["salon_ID"].ToString());
                ekle.SubItems.Add(oku["tarih"].ToString());
                ekle.SubItems.Add(oku["seans"].ToString());
                ekle.SubItems.Add(oku["satis_Adi"].ToString());
                ekle.SubItems.Add(oku["satis_Soyadi"].ToString());
                ekle.SubItems.Add(oku["koltuk_No"].ToString());

                listView1.Items.Add(ekle);
            }
            bag.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;
            label2.Parent = pictureBox1;
            label2.BackColor = Color.Transparent;

            pictureBox2.Parent = pictureBox1;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox3.Parent = pictureBox1;
            pictureBox3.BackColor = Color.Transparent;

            label6.Parent = pictureBox1;
            label6.BackColor = Color.Transparent;
            label7.Parent = pictureBox1;
            label7.BackColor = Color.Transparent;
            label8.Parent = pictureBox1;
            label8.BackColor = Color.Transparent;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            verilerigoruntule();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmRezervasyon frmr = new frmRezervasyon();
            this.Hide();
            frmr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
