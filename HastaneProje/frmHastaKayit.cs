using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HastaneProje
{
    public partial class frmHastaKayit : Form
    {
        public frmHastaKayit()
        {
            InitializeComponent();
        }

        private void frmHastaKayit_Load(object sender, EventArgs e)
        {
            txtAd.Focus();
        }
        SqlBaglantisi conn = new SqlBaglantisi();
        private void btnKayit_Click(object sender, EventArgs e)
        {


            try
            {
                SqlCommand komut = new SqlCommand("INSERT INTO TBLHASTALAR([HASTAAD], [HASTASOYAD], [HASTATC], [HASTATELEFON], [HASTASIFRE], [HASTACINSIYET])" +
               "VALUES(@P1,@P2,@P3,@P4,@P5,@P6)", conn.baglanti());

                komut.Parameters.AddWithValue("@P1", txtAd.Text);
                komut.Parameters.AddWithValue("@P2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@P3", txtTc.Text);
                komut.Parameters.AddWithValue("@P4", txtTel.Text);
                komut.Parameters.AddWithValue("@P5", txtSifre.Text);
                komut.Parameters.AddWithValue("@P6", cmbCins.Text);

                komut.ExecuteNonQuery();
                MessageBox.Show("Kaydınız Başaryla Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                conn.baglanti().Close();

            }
            catch (Exception)
            {   

                MessageBox.Show("Kaydınız Gerçekleştirilemedi Lütfen Tekrar Deneyiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.baglanti().Close();

            }


        }
    }
}
