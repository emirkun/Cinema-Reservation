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
    public partial class frmGenel : Form
    {
        public frmGenel()
        {
            InitializeComponent();
        }

        OleDbConnection connect = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=sinema.accdb");
        void baglantiKur()
        {
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }
        }

        private void btnRezerve_Click(object sender, EventArgs e)
        {
            if (cbSalon.SelectedIndex != -1 && cbFilm.SelectedIndex != -1 && cbSeans.SelectedIndex != -1)
            {
                frmRezervasyon rezerve = new frmRezervasyon();
                rezerve.film_Adi = cbFilm.Text;
                rezerve.salon_Adi = cbSalon.Text;
                rezerve.seans = cbSeans.Text;
                rezerve.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Lütfen tüm bilgileri eksiksiz giriniz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }           
        }       


        void bilgiAl(string sql , ComboBox cmb)
        {
            cmb.Items.Clear();
            baglantiKur();            
            OleDbCommand cmd = new OleDbCommand(sql, connect);
            OleDbDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                cmb.Items.Add(oku[1].ToString());
            }
            connect.Close();
        }
        
        private void cbSalon_Click(object sender, EventArgs e)
        {
            bilgiAl("SELECT * FROM salon", cbSalon);
        }

        private void cbFilm_Click(object sender, EventArgs e)
        {
            bilgiAl("SELECT * FROM filmler", cbFilm);
        }

        private void btnFilmEkle_Click(object sender, EventArgs e)
        {
            Form3 frmK = new Form3();
            frmK.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void frmGenel_Load(object sender, EventArgs e)
        {

            pictureBox2.Parent = pictureBox1;
            label6.Parent = pictureBox1;
            label6.BackColor = Color.Transparent;
            label7.Parent = pictureBox1;
            label7.BackColor = Color.Transparent;
            label8.Parent = pictureBox1;
            label8.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;

            this.Opacity = 0;
            timer1.Enabled = true;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           
                
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.07;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

    }
}
