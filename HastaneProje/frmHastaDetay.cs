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
    public partial class frmHastaDetay : Form
    {
        public frmHastaDetay()
        {
            InitializeComponent();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBLRANDEVULAR SET RANDEVUDURUM = @P1 ,HASTASIKAYET = @P2,HASTATC=@P5 where ID = @P4");
            komut.Connection = baglanti.baglanti();
            komut.Parameters.AddWithValue("@P1",1);
            komut.Parameters.AddWithValue("@P5",lblTc.Text);
            komut.Parameters.AddWithValue("@P2",txtSikayet.Text);
            komut.Parameters.AddWithValue("@P4",int.Parse(txtId.Text));
            komut.ExecuteNonQuery();
            baglanti.baglanti().Close();
            randevuGecmisiyenile();
            RandevuYenile();

        }
        public string tc, ad, soyad;

        private void cmBrans_SelectedIndexChanged(object sender, EventArgs e)
        {


            cmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("SELECT DOKTORAD,DOKTORSOYAD FROM TBLDOKTOR WHERE DOKTORBRANS = @K1", baglanti.baglanti());
            komut3.Parameters.AddWithValue("@K1", cmBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();

            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            baglanti.baglanti().Close();


        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            RandevuYenile();


        }

        private void RandevuYenile()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLRANDEVULAR WHERE RANDEVUBRANS = @S1 AND RANDEVUDOKTOR = @S2 and RANDEVUDURUM =0", baglanti.baglanti());
            da.SelectCommand.Parameters.AddWithValue("@S1", cmBrans.Text);
            da.SelectCommand.Parameters.AddWithValue("@S2", cmbDoktor.Text);
            da.Fill(dt);
            dataGridView2.DataSource = dt;

        }

        private void lnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmBilgiDuzenle fr = new frmBilgiDuzenle();
            fr.Tc = lblTc.Text;
            fr.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        SqlBaglantisi baglanti = new SqlBaglantisi();
        private void frmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;
            lblAdSoyad.Text = ad + " " + soyad;
            randevuGecmisiyenile();
            

            //Branş combo box

            SqlCommand komut1 = new SqlCommand("SELECT BRANSAD FROM TBLBRANSLAR", baglanti.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                cmBrans.Items.Add(dr1[0]);
            }
            baglanti.baglanti().Close();

        }
        private void randevuGecmisiyenile()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLRANDEVULAR WHERE HASTATC = @P1", baglanti.baglanti());

            //Randevu Geçmişi
            da.SelectCommand.Parameters.AddWithValue("@P1", tc);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
    }
}
