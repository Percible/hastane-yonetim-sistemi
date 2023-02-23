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
    public partial class frmRandevuPanel : Form
    {
        private frmSekreterDetay detay;
        public frmRandevuPanel(frmSekreterDetay de)
        {
            detay = de;
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void frmRandevuPanel_Load(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLRANDEVULAR",bgl.baglanti());
            da.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            dataTable.Dispose();
        }
        int id;
        string tarih, saat, brans, doktor, durum, hastatc;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            tarih = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            saat = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            brans = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            doktor = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

            detay.txtId.Text = id.ToString();
            detay.txtTarih.Text = tarih;
            detay.txtSaat.Text = saat;
            detay.cmbBrans.Text = brans;
            detay.cmbDoktor.Text = doktor;

            

            

        }
    }
}
