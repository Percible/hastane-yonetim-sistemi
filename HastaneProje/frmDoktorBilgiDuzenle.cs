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
    public partial class frmDoktorBilgiDuzenle : Form
    {
        public frmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string tc;
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("UPDATE TBLDOKTOR SET DOKTORAD = @P1,DOKTORSOYAD = @P2,DOKTORBRANS = @P3,DOKTORSIFRE=@P4 " +
                "WHERE DOKTORTC = @P5");
            sqlCommand.Connection = bgl.baglanti();
            sqlCommand.Parameters.AddWithValue("@P1", txtAd.Text);
            sqlCommand.Parameters.AddWithValue("@P2", txtSoyad.Text);
            sqlCommand.Parameters.AddWithValue("@P3", cmbBrans.Text);
            sqlCommand.Parameters.AddWithValue("@P4", txtSifre.Text);
            sqlCommand.Parameters.AddWithValue("@P5", txtTc.Text);
            sqlCommand.ExecuteNonQuery();

            MessageBox.Show("Kayıt Başarıyla Güncellendi", "Bilgi");
        }

        private void frmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("SELECT BRANSAD FROM TBLBRANSLAR",bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();

            while (dr1.Read())
            {
                cmbBrans.Items.Add(dr1[0].ToString());

            }
            bgl.baglanti().Close();


            //veri aktarma
            txtTc.Text = tc;

            SqlCommand komut = new SqlCommand("SELECT * FROM TBLDOKTOR WHERE DOKTORTC = @P1");
            komut.Connection = bgl.baglanti();
            komut.Parameters.AddWithValue("@P1",tc);

            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                cmbBrans.Text = dr[3].ToString();
                txtSifre.Text = dr[5].ToString();
            }

            bgl.baglanti().Close();
        }
    }
}
