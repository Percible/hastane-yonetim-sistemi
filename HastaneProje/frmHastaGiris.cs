using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneProje
{
    public partial class frmHastaGiris : Form
    {
        public frmHastaGiris()
        {
            this.MaximizeBox = false;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            InitializeComponent();
        }

        frmHastaKayit frmHastaKayit;
        private void lnkKayit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (frmHastaKayit == null || frmHastaKayit.IsDisposed == true)
            {
                frmHastaKayit = new frmHastaKayit();
                frmHastaKayit.Show();
            }
            else
            {
                frmHastaKayit.WindowState = FormWindowState.Normal;
            }





        }

        private void frmHastaGiris_Load(object sender, EventArgs e)
        {
            txtTc.Focus();
            txtSifre.UseSystemPasswordChar = true;
        }


        SqlBaglantisi conn = new SqlBaglantisi();
        frmHastaDetay fr;
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT COUNT(*) FROM TBLHASTALAR WHERE HASTATC=@P1 and HASTASIFRE=@P2";
            komut.Connection = conn.baglanti();
            komut.Parameters.AddWithValue("@P1", txtTc.Text);
            komut.Parameters.AddWithValue("@P2", txtSifre.Text);

            SqlDataReader dr = komut.ExecuteReader();

            SqlCommand komut1 = new SqlCommand();
            komut1.CommandText = "SELECT HASTAAD,HASTASOYAD FROM TBLHASTALAR WHERE HASTATC=@A1 and HASTASIFRE=@A2";
            komut1.Connection = conn.baglanti();
            komut1.Parameters.AddWithValue("@A1", txtTc.Text);
            komut1.Parameters.AddWithValue("@A2", txtSifre.Text);

            SqlDataReader dr1 = komut1.ExecuteReader();


            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0].ToString()) > 0)
                {
                    if (fr == null || fr.IsDisposed == true)
                    {
                        frmHastaDetay fr = new frmHastaDetay();
                        if (dr1.Read())
                        {
                            fr.tc = txtTc.Text;
                            fr.ad = dr1[0].ToString();
                            fr.soyad = dr1[1].ToString();
                        }
                        fr.Show();
                        this.Hide();
                        conn.baglanti().Close();

                    }
                    else
                    {
                        fr.WindowState = FormWindowState.Normal;

                    }

                }
                else
                {
                    MessageBox.Show("Hatalı Kullanıcı Adı ya da Şifre Girdiniz Lütfen Tekrar Deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }
    }
}
