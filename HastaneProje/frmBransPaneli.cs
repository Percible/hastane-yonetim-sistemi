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
    public partial class frmBransPaneli : Form
    {
        public frmBransPaneli()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "INSERT INTO TBLBRANSLAR(BRANSAD) VALUES(@P1)";
            komut.Connection = bgl.baglanti();

            komut.Parameters.AddWithValue("@P1",txtBrans.Text);
            komut.ExecuteNonQuery();
            BransGetir();
        }

       

        private void frmBransPaneli_Load(object sender, EventArgs e)
        {
            BransGetir();
            
        }


        private void BransGetir()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLBRANSLAR", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "DELETE FROM TBLBRANSLAR WHERE ID = @P1";
            komut.Connection = bgl.baglanti();

            komut.Parameters.AddWithValue("@P1", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            BransGetir();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "UPDATE TBLBRANSLAR SET BRANSAD = @P1 WHERE ID = @P2";
            komut.Connection = bgl.baglanti();

            komut.Parameters.AddWithValue("@P1", txtBrans.Text);
            komut.Parameters.AddWithValue("@P2", int.Parse(txtId.Text));
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            BransGetir();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBrans.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }
    }
}
