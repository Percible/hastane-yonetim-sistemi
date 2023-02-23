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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HastaneProje
{
    public partial class frmBilgiDuzenle : Form
    {
        public frmBilgiDuzenle()
        {
            InitializeComponent();
        }


        private string ad, soyad, tc, tel, sifre, cinsiyet;

        public string Ad
        {
            get {return ad ;}
            set {ad = value;}
        }

        public string Soyad
        {
            get { return soyad ;}
            set { soyad = value;}

        }
        public string Tc
        {
            get { return tc ;}
            set
            {
                tc = value;
            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (txtAd.Text=="")
            {
                MessageBox.Show("Ad boş bırakılamaz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAd.Focus();
            }
            else if (txtTc.MaskCompleted == false)
            {
                MessageBox.Show("TC numarası boş veya 11 haneden küçük lütfen doğru giriniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTc.Focus();
            }
            else if (txtSifre.Text == "")
            {
                MessageBox.Show("Şifre Boş Bırakılamaz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSifre.Focus();
            }
            else if (txtTel.MaskCompleted==false)
            {
                MessageBox.Show("Telefon Numarası 11 Haneden Küçük Olamaz","Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTel.Focus();
            }
            else if (txtSoyad.Text == "")
            {
                MessageBox.Show("Soyad Boş Bırakılamaz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoyad.Focus();
            }
            else if (string.IsNullOrEmpty(cmbCins.Text) || cmbCins.SelectedIndex == -1)
            {
                MessageBox.Show("Doğru Cinsiyet Giriniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoyad.Focus();
            }
            else
            {
                tc = txtTc.Text;
                SqlCommand komut = new SqlCommand("UPDATE TBLHASTALAR SET HASTAAD = @P1,HASTASOYAD = @P2,HASTATELEFON = @P3," +
                    "HASTATC = @P4,HASTACINSIYET = @P5,HASTASIFRE=@P6 WHERE HASTATC = @P7",bgl.baglanti());

                komut.Parameters.AddWithValue("@P1", txtAd.Text);
                komut.Parameters.AddWithValue("@P2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@P3", txtTel.Text);
                komut.Parameters.AddWithValue("@P4", txtTc.Text);
                komut.Parameters.AddWithValue("@P5", cmbCins.Text);
                komut.Parameters.AddWithValue("@P6", txtSifre.Text);
                komut.Parameters.AddWithValue("@P7", tc);

                komut.ExecuteNonQuery();

                bgl.baglanti().Close();



            }


            

        }

        public string Cinsiyet
        {
            get { return cinsiyet ;}
            set
            {
                cinsiyet = value;
            }
        }
        public string Tel
        {
            get { return tel; }
            set
            {
                tel = value;
            }
        }
        public string Sifre
        {
            get { return sifre ;}
            set { sifre = value ;}
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        private void frmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            
            txtTc.Text = tc;
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLHASTALAR WHERE HASTATC = @P1",bgl.baglanti());
            komut.Parameters.AddWithValue("@P1",tc);

            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                txtAd.Text = dr[1].ToString(); 
                txtSoyad.Text = dr[2].ToString(); 
                txtSifre.Text = dr[5].ToString(); 
                txtTel.Text = dr[4].ToString(); 
                cmbCins.Text = dr[6].ToString();
            }
            bgl.baglanti().Close();

            
        }
    }
}
