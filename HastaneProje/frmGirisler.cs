using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace HastaneProje
{
    public partial class frmGirisler : Form
    {
        public frmGirisler()
        {

            this.MaximizeBox = false;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            InitializeComponent();
        }

        private void btnHasta_Click(object sender, EventArgs e)
        {
            frmHastaGiris fr = new frmHastaGiris();

            fr.Show();
            
        }

        private void btnDoktor_Click(object sender, EventArgs e)
        {
            frmDoktorGiris fr = new frmDoktorGiris();

            fr.Show();
        }

        private void btnSekreter_Click(object sender, EventArgs e)
        {
            frmSekreterGiris fr = new frmSekreterGiris();

            fr.Show();
        }
    }
}
