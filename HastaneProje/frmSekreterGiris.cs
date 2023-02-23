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
    public partial class frmSekreterGiris : Form
    {
        public frmSekreterGiris()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM TBLSEKRETER WHERE SEKRETERSIFRE = @P1 AND SEKRETERTC = @P2",bgl.baglanti());
            komut.Parameters.AddWithValue("@P1",txtSifre.Text);
            komut.Parameters.AddWithValue("@P2",txtTc.Text);

            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0)
                {
                    frmSekreterDetay fr = new frmSekreterDetay();
                    fr.tc = txtTc.Text;

                    fr.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Hatalı Kullanıcı adı veya Şifre", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            

        }
    }
}
