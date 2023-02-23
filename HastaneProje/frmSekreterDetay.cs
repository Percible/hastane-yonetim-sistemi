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
    public partial class frmSekreterDetay : Form
    {
        public frmSekreterDetay()
        {
            InitializeComponent();
        }
        public string tc;
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void frmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;

            SqlCommand komut1 = new SqlCommand("SELECT SEKRETERADSOYAD FROM TBLSEKRETER WHERE SEKRETERTC = @P1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@P1", tc);
            SqlDataReader dr = komut1.ExecuteReader();

            if (dr.Read())
            {
                lblAdSoyad.Text = dr[0].ToString();

            }
            dr.Close();
            bgl.baglanti().Close();

            //DataGrid e Branşları aktarma

            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLBRANSLAR",bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Doktorları aktarma

            DataTable dt2 = new DataTable();

            SqlDataAdapter da2 = new SqlDataAdapter("SELECT CONCAT(DOKTORAD,' ',DOKTORSOYAD) DOKTORLAR,DOKTORBRANS FROM TBLDOKTOR", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //BRANŞLARI COMBOYA AKTARMA


            SqlCommand komutt = new SqlCommand("SELECT BRANSAD FROM TBLBRANSLAR", bgl.baglanti());
            SqlDataReader drr = komutt.ExecuteReader();
            while (drr.Read())
            {
                cmbBrans.Items.Add(drr[0]);
            }
            bgl.baglanti().Close();



        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand k = new SqlCommand();
            k.CommandText = "INSERT INTO TBLRANDEVULAR(RANDEVUTARIH, RANDEVUSAAT, RANDEVUBRANS, RANDEVUDOKTOR)" +
                "VALUES (@P1,@P2,@P3,@P4)";
            k.Connection = bgl.baglanti();

            k.Parameters.AddWithValue("@P1",txtTarih.Text);
            k.Parameters.AddWithValue("@P2",txtSaat.Text);
            k.Parameters.AddWithValue("@P3",cmbBrans.Text);
            k.Parameters.AddWithValue("@P4",cmbDoktor.Text);
            k.ExecuteNonQuery();
            bgl.baglanti().Close();
            
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("SELECT DOKTORAD,DOKTORSOYAD FROM TBLDOKTOR WHERE DOKTORBRANS = @K1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@K1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();

            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            try
            {

                SqlCommand komut = new SqlCommand("INSERT INTO TBLDUYURULAR(DUYURU)" +
                    "VALUES(@P1)");
                komut.Connection = bgl.baglanti();
                komut.Parameters.AddWithValue("@P1", txtDuyuru.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Duyuru oluşturuldu");

            }
            catch (Exception)
            {
                MessageBox.Show("Duyuru oluşturulurken Bir Hata Oluştu");


            }
        }
        frmDoktorPaneli frd;
        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
            if (frd == null || frd.IsDisposed == true)
            {
                frd = new frmDoktorPaneli();
                frd.Show();
            }
            else
            {
                frd.WindowState = FormWindowState.Normal;
            }



        }
        frmBransPaneli fr;
        private void btnBransPanel_Click(object sender, EventArgs e)
        {
            if (fr == null || fr.IsDisposed==true)
            {
                fr = new frmBransPaneli();
                fr.Show();
            }
            else
            {
                fr.WindowState = FormWindowState.Normal;
                this.WindowState = FormWindowState.Minimized;
            }
        }
        frmRandevuPanel frr;
        private void btnRandevuPanel_Click(object sender, EventArgs e)
        {
            if (frr == null || frr.IsDisposed==true)
            {
                frr = new frmRandevuPanel(this);
                frr.Show();
                this.WindowState = FormWindowState.Minimized;

            }
            else
            {
                frr.WindowState = FormWindowState.Normal;

            }
        }
    }
}
