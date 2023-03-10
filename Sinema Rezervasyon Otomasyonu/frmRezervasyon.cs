using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.OleDb;
using System.Collections;

namespace Sinema_Rezervasyon_Otomasyonu
{
    public partial class frmRezervasyon : Form
    {
        public frmRezervasyon()
        {
            InitializeComponent();
        }
        int su = 0;
        int kola = 0;
        int seker = 0;
        int cips = 0;
        int mısır = 0;
        int mfiyat = 0;
        int kfiyat = 0;
        int sufiyat = 0;
        int cfiyat = 0;
        int sfiyat = 0;

        public string film_Adi = "";
        public string salon_Adi = "";
        public string seans = "";
        ArrayList koltuklar = new ArrayList();
        ArrayList iptalKoltuk = new ArrayList();
        int filmID = 0;
        int salonID = 0;
        int toplamtutar = 0;
        int toplamsatılan = 0;
        int toplamfiyat;
        int tutar = 0;
                
        OleDbConnection connect = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=sinema.accdb");

        void baglantiKur()
        {
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            frmGenel frmG = new frmGenel();
            frmG.Show();
            this.Hide();
        }

        private void btnKoltuk_Click(object sender, EventArgs e)
        {
            //if (((Button)sender).BackColor != Color.Red)
            if (((Button)sender).BackColor == Color.Chartreuse) // yeşil
            {
                ((Button)sender).BackColor = Color.Orange;
                if (!koltuklar.Contains(((Button)sender).Text))
                {
                    koltuklar.Add(((Button)sender).Text);
                }
                koltukYazdir();
            }
            else if (((Button)sender).BackColor == Color.Orange) // turuncu
            {
                ((Button)sender).BackColor = Color.Chartreuse;
                if (koltuklar.Contains(((Button)sender).Text))
                {
                     koltuklar.Remove(((Button)sender).Text);
                }
                koltukYazdir();
            }
            else // kırmızı
            {
                if (!iptalKoltuk.Contains(((Button)sender).Text))
                {
                    iptalKoltuk.Add(((Button)sender).Text);
                }
                else 
                {
                    iptalKoltuk.Remove(((Button)sender).Text);
                }

                string koltuk = "";
                for (int i = 0; i < iptalKoltuk.Count; i++)
                {
                    koltuk += iptalKoltuk[i].ToString() + ",";
                }
                if (iptalKoltuk.Count >= 1)
                {
                    koltuk = koltuk.Remove(koltuk.Length - 1, 1);
                }
                txtKoltukIptal.Text = koltuk;
            }           
        }

        void koltukYazdir()
        {
            string koltuk = "";
            for (int i = 0; i < koltuklar.Count; i++)
            {
                koltuk += koltuklar[i].ToString() + ",";
            }
            if (koltuklar.Count >= 1)
            {
                koltuk = koltuk.Remove(koltuk.Length - 1, 1);
            }
            txtKoltukNo.Text = koltuk;
        }
        

        private void frmRezervasyon_Load(object sender, EventArgs e)
        {
            lblFilmAdi.Text = film_Adi;
            lblSalonSeans.Text = salon_Adi + " / " + seans;
            timer1.Enabled = true;
            filmID = Convert.ToInt32(araGetir("SELECT * FROM filmler WHERE film_Adi='" + film_Adi + "'"));
            salonID = Convert.ToInt32(araGetir("SELECT * FROM salon WHERE salon_Adi='" + salon_Adi + "'"));
            LogAl();

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblSaat.Text = DateTime.Now.ToLongTimeString();
            lblTarih.Text = DateTime.Now.Date.ToShortDateString();
        }

        string araGetir(string sql)
        {
            baglantiKur();
            OleDbCommand cmd = new OleDbCommand(sql, connect);
            OleDbDataReader oku = cmd.ExecuteReader();
            oku.Read();
            string deger = oku[0].ToString();
            connect.Close();
            return deger;
        }

        void biletAyir()
        {
            baglantiKur();
            string ucret = "";
            if (rbOgrenci.Checked)  ucret = "6";
            else ucret = "10";

            for (int i = 0; i < koltuklar.Count; i++)
            {
                string sql = "INSERT INTO satis(film_ID,salon_ID,tarih,seans,satis_Adi,satis_Soyadi,koltuk_No,ucret) VALUES (" + filmID + "," + salonID + ",'" + lblTarih.Text + "','" + seans + "','" + txtAdi.Text + "','" + txtSoyadi.Text + "'," + Convert.ToInt32(koltuklar[i]) + ",'" + ucret + "')";
                OleDbCommand cmd = new OleDbCommand(sql, connect);
                cmd.ExecuteNonQuery();
                this.Controls.Find("btn" + koltuklar[i].ToString(), true)[0].BackColor = Color.Red;
            }

            connect.Close();
        }

        private void btnBiletAyir_Click(object sender, EventArgs e)
        {
            if (txtKoltukNo.Text != "")
            {
                if (txtKoltukNo.Text != "" && txtAdi.Text != "" && txtSoyadi.Text != "")
                {
                    biletAyir();
                    MessageBox.Show(txtAdi.Text + " " + txtSoyadi.Text + " bilgili kişinin " + txtKoltukNo.Text + " no'lu koltukları ayrılmıştır");
                    txtKoltukNo.Text = "";
                    txtAdi.Text = "";
                    txtSoyadi.Text = "";
                    koltuklar.Clear();
                }
                else
                {
                    MessageBox.Show("Tüm bilgileri eksiksiz doldurmalısınız.");
                }
            }
            else
            {
                MessageBox.Show("Koltuk numarasını seçmediniz.","Dikkat");
            }
        }

        void LogAl()
        {
            baglantiKur();
            string sql = "SELECT * FROM satis WHERE film_ID=" + filmID + " AND salon_ID=" + salonID + " AND seans='" + seans + "'";
            OleDbCommand cmd = new OleDbCommand(sql, connect);
            OleDbDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                string koltuk_No = oku[7].ToString();
                this.Controls.Find("btn" + koltuk_No, true)[0].BackColor = Color.Red;
            }
            connect.Close();
        }

        private void btnBilet_iptal_Click(object sender, EventArgs e)
        {
            if (txtKoltukIptal.Text != "")
            {
                baglantiKur();
                for (int i = 0; i < iptalKoltuk.Count; i++)
                {
                    string sql = "DELETE FROM satis WHERE koltuk_No=" + Convert.ToInt32(iptalKoltuk[i]);
                    OleDbCommand cmd = new OleDbCommand(sql, connect);
                    cmd.ExecuteNonQuery();
                    this.Controls.Find("btn" + iptalKoltuk[i].ToString(), true)[0].BackColor = Color.Chartreuse;
                }

                connect.Close();
                iptalKoltuk.Clear();
                MessageBox.Show(txtKoltukNo.Text + " koltuk numaraları bileti iptal edilmiştir.");
                txtKoltukIptal.Text = "";
                txtAdi.Text = "";
                txtSoyadi.Text = "";                
            } 
            else
            {
                MessageBox.Show("Lütfen tüm bilgileri eksiksiz giriniz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);               
            }
        }
        

        private void frmRezervasyon_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmGenel frmG = new frmGenel();
            frmG.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            mısır++;

            mfiyat = mfiyat + 3;

            textBox2.Text = mısır.ToString();
            textBox3.Text = mfiyat.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            su++;

            sufiyat = sufiyat + 2;

            textBox5.Text = su.ToString();
            textBox4.Text = sufiyat.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            kola++;

            kfiyat = kfiyat + 6;

            textBox7.Text = kola.ToString();
            textBox6.Text = kfiyat.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            cips++;

            cfiyat = cfiyat + 7;

            textBox9.Text = cips.ToString();
            textBox8.Text = cfiyat.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            seker++;

            sfiyat = sfiyat + 5;

            textBox11.Text = seker.ToString();
            textBox10.Text = sfiyat.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mısır--;

            mfiyat = mfiyat - 3;

            textBox2.Text = mısır.ToString();
            textBox3.Text = mfiyat.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            su--;

            sufiyat = sufiyat - 2;

            textBox5.Text = su.ToString();
            textBox4.Text = sfiyat.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            kola--;

            kfiyat = kfiyat - 6;

            textBox7.Text = kola.ToString();
            textBox6.Text = kfiyat.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cips--;

            cfiyat = cfiyat - 7;

            textBox9.Text = cips.ToString();
            textBox8.Text = cfiyat.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            seker--;

            sfiyat = sfiyat - 5;

            textBox11.Text = seker.ToString();
            textBox10.Text = sfiyat.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (toplamsatılan > 5)
            {
                MessageBox.Show("En az 5 adet Yiyecek - İçecek almanız gerekiyor!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                toplamsatılan = mısır + kola + su + seker + cips;

                toplamfiyat = sfiyat + mfiyat + sufiyat + cfiyat + kfiyat;
                textBox1.Text = toplamfiyat.ToString();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void rbTam_CheckedChanged(object sender, EventArgs e)
        {
            rbTam.Enabled = false;
            rbOgrenci.Enabled = false;

            toplamtutar = toplamtutar + 30;
        }

        private void rbOgrenci_CheckedChanged(object sender, EventArgs e)
        {
            rbTam.Enabled = false;
            rbOgrenci.Enabled = false;


            toplamtutar = toplamtutar + 24;
        }

        private void button12_Click(object sender, EventArgs e)
        {
           
                Form2 frm2 = new Form2();
                frm2.Show();
                this.Hide();

                frm2.label2.Text = textBox1.Text.ToString();
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            


        }
    }
}
