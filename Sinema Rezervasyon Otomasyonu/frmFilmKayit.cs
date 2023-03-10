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
    public partial class frmFilmKayit : Form
    {
        public frmFilmKayit()
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

        private void frmFilmKayit_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmGenel frmG = new frmGenel();
            frmG.Show();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglantiKur();
            string sql = "INSERT INTO filmler(film_Adi,yonetmen,film_tur) VALUES('" + txtFilmAdi.Text + "','" + txtYonetmen.Text + "','" + txtTur.Text + "')";
            OleDbCommand cmd = new OleDbCommand(sql, connect);
            cmd.ExecuteNonQuery();
            connect.Close();

            MessageBox.Show(txtFilmAdi.Text + " / " + txtYonetmen.Text + " / " +txtTur.Text + " filmi eklendi");
            txtFilmAdi.Text = "";
            txtTur.Text = "";
            txtYonetmen.Text = "";
        }

        private void frmFilmKayit_Load(object sender, EventArgs e)
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

            timer1.Start();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmGenel frmG = new frmGenel();
            frmG.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.05;
        }        
    }
}
