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
    public partial class frmDoktorPaneli : Form
    {
        public frmDoktorPaneli()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void frmDoktorPaneli_Load(object sender, EventArgs e)
        {

            SqlCommand komutt = new SqlCommand("SELECT BRANSAD FROM TBLBRANSLAR", bgl.baglanti());
            SqlDataReader drr = komutt.ExecuteReader();
            while (drr.Read())
            {
                cmbBrans.Items.Add(drr[0]);
            }
            bgl.baglanti().Close();

            DataYukleme();
        }


        private void DataYukleme()
        {
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLDOKTOR", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            


        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtTcNo.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBLDOKTOR([DOKTORAD], [DOKTORSOYAD], [DOKTORBRANS], [DOKTORTC], [DOKTORSIFRE])" +
                "VALUES(@P1,@P2,@P3,@P4,@P5)");
            komut.Connection = bgl.baglanti();

            komut.Parameters.AddWithValue("@P1",txtAd.Text);
            komut.Parameters.AddWithValue("@P2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@P3",cmbBrans.Text);
            komut.Parameters.AddWithValue("@P4",txtTcNo.Text);
            komut.Parameters.AddWithValue("@P5",txtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("başarıyla Eklendi");
            DataYukleme();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("Delete From TBLDOKTOR WHERE DOKTORTC = @P1");
            sqlCommand.Connection = bgl.baglanti();
            sqlCommand.Parameters.AddWithValue("@P1",txtTcNo.Text);

            sqlCommand.ExecuteNonQuery();
            bgl.baglanti().Close();
            DataYukleme();



        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            SqlCommand sqlCommand = new SqlCommand("UPDATE TBLDOKTOR SET DOKTORAD = @P1,DOKTORSOYAD = @P2,DOKTORBRANS = @P3,DOKTORSIFRE=@P4 " +
                "WHERE DOKTORTC = @P5");
            sqlCommand.Connection = bgl.baglanti();
            sqlCommand.Parameters.AddWithValue("@P1",txtAd.Text);
            sqlCommand.Parameters.AddWithValue("@P2",txtSoyad.Text);
            sqlCommand.Parameters.AddWithValue("@P3",cmbBrans.Text);
            sqlCommand.Parameters.AddWithValue("@P4",txtSifre.Text);
            sqlCommand.Parameters.AddWithValue("@P5",txtTcNo.Text);
            sqlCommand.ExecuteNonQuery();

            MessageBox.Show("Kayıt Başarıyla Güncellendi","Bilgi");
            DataYukleme();


        }
    }
}
