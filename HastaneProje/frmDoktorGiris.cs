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
    public partial class frmDoktorGiris : Form
    {
        public frmDoktorGiris()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        frmDoktorDetay frd;
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM TBLDOKTOR WHERE DOKTORTC = @P1 AND DOKTORSIFRE = @P2");
            komut.Connection = bgl.baglanti();
            komut.Parameters.AddWithValue("@P1",txtTc.Text);
            komut.Parameters.AddWithValue("@P2",txtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                if (int.Parse(dr[0].ToString())>0)
                {
                    if (frd == null || frd.IsDisposed==true)
                    {
                        frd = new frmDoktorDetay();
                        frd.tc = txtTc.Text;
                        frd.Show();
                        this.Hide();
                    }
                    else
                    {
                        frd.WindowState = FormWindowState.Normal;    
                    }
                }
                else
                {
                    MessageBox.Show("KULLANICI ADI VEYA ŞİFRENİZ HATALI LÜTFEN TEKRAR DENEYİNİZ!","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
                }
            }

        }
    }
}
