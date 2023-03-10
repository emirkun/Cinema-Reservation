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
    public partial class frmKayıt : Form
    {

        public frmKayıt()
        {
            InitializeComponent();
        }

        
        private void frmKayıt_Load(object sender, EventArgs e)
        {
            pictureBox2.Parent = pictureBox1;
            label6.Parent = pictureBox1;
            label6.BackColor = Color.Transparent;
            label7.Parent = pictureBox1;
            label7.BackColor = Color.Transparent;
            label8.Parent = pictureBox1;
            label8.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;

            button1.Parent = pictureBox1;
            button1.BackColor = Color.Transparent;

            textBox1.Parent = pictureBox1;
            

            textBox2.Parent = pictureBox1;
            


            this.Opacity = 0;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.07;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection bag = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=giris.accdb");

            DataTable tablo = new DataTable();

            OleDbDataAdapter adtr = new OleDbDataAdapter();

            OleDbCommand kmt = new OleDbCommand();

            if (textBox1.Text == "" || textBox2.Text == "" || textBox1.Text == "Kullanıcı Adı" || textBox2.Text == "Sifre")
            {
                MessageBox.Show("Lütfen tüm bilgileri eksiksiz giriniz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                bag.Open();

                kmt.Connection = bag;

                kmt.CommandText = "INSERT INTO kullanici(k_ad,k_sifre) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "')";

                kmt.ExecuteNonQuery();

                kmt.Dispose();

                bag.Close();

                textBox1.Text = "Kullanıcı Adı";

                textBox2.Text = "Sifre";
                textBox2.PasswordChar = '\0';

                MessageBox.Show("Bilgileriniz başarıyla kaydedildi!", "Bravo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox1.Text = "Kullanıcı Adı";
        }

        private void textBox2_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox2_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.Text = "Sifre";
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            frmGiris frmg = new frmGiris();
            this.Hide();
            frmg.Show();
        }

        private void textBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox2_MouseDown_1(object sender, MouseEventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox2_MouseUp_1(object sender, MouseEventArgs e)
        {
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
