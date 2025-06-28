using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HastaneYönetimRandevuSistemiOtomasyonProjesi
{
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }
        sqlBaglantisi sqlBaglantisi=new sqlBaglantisi();
        public string TC;
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TC;

            //DOKTOR ad-soyad
            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorTC=@p1",sqlBaglantisi.baglanti());
            komut.Parameters.AddWithValue("@p1",lblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " +dr[1];

            }
            sqlBaglantisi.baglanti().Close();


            DataTable dt = new DataTable();
            SqlCommand komut2 = new SqlCommand("SELECT * FROM Tbl_Randevular WHERE RandevuDoktor = @doktor", sqlBaglantisi.baglanti());
            komut2.Parameters.AddWithValue("@doktor", lblAdSoyad.Text.Trim()); // Trim boşluklara karşı korur

            SqlDataAdapter da = new SqlDataAdapter(komut2);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDüzenle frmDoktorBilgiDüzenle = new FrmDoktorBilgiDüzenle();
            frmDoktorBilgiDüzenle.TCNO = lblTC.Text;
            frmDoktorBilgiDüzenle.Show();


        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmDuyurular=new FrmDuyurular();
            frmDuyurular.Show();

        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            FrmGirisler frmGirisler = new FrmGirisler();
            frmGirisler.Show();
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            rchSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();

        }

        private void FrmDoktorDetay_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmDoktorGiris frmDoktorGiris = new FrmDoktorGiris();
            frmDoktorGiris.Show();
        }
    }
}
