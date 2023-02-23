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
    public partial class frmDoktorDetay : Form
    {

        
        public frmDoktorDetay()
        {
            InitializeComponent();
        }
        public string tc, ad;
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void frmDoktorDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;

            SqlCommand komut = new SqlCommand("SELECT CONCAT(DOKTORAD,' ',DOKTORSOYAD) FROM TBLDOKTOR WHERE DOKTORTC = @P1");
            komut.Connection = bgl.baglanti();
            komut.Parameters.AddWithValue("@P1",tc);
            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                lblAdSoyad.Text = dr[0].ToString();
                RandevuDetay();
            }
        }
        frmDoktorBilgiDuzenle bg;
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (bg == null || bg.IsDisposed==true)
            {
                bg = new frmDoktorBilgiDuzenle();
                bg.tc = tc;
                bg.Show();

            }
            else
            {
                bg.WindowState = FormWindowState.Normal;

            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            frmDuyurular frm = new frmDuyurular();
            frm.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSikayetG.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void RandevuDetay()
        {

            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLRANDEVULAR WHERE RANDEVUDOKTOR = (SELECT CONCAT(DOKTORAD,' ',DOKTORSOYAD)FROM TBLDOKTOR WHERE DOKTORTC =@P1) AND RANDEVUDURUM = 1", bgl.baglanti());
            da.SelectCommand.Parameters.AddWithValue("@P1", tc);
            
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
