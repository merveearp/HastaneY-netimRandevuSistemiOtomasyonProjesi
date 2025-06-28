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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;

        sqlBaglantisi sqlBaglantisi= new sqlBaglantisi();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;
            SqlCommand sqlCommand = new SqlCommand("Select HastaAd,HastaSoyad From Tbl_Hastalar where HastaTC=@HastaTC",sqlBaglantisi.baglanti());
            sqlCommand.Parameters.AddWithValue("@HastaTC",tc);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            while(dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];

            }
            sqlBaglantisi.baglanti().Close();

            //RANDEVU GEÇMİŞİ
            DataTable dt = new DataTable();
            SqlCommand komut = new SqlCommand("Select * From Tbl_Randevular where HastaTC = @tc", sqlBaglantisi.baglanti());
            komut.Parameters.AddWithValue("@tc", tc);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //BRANŞLARI ÇEKME
            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar", sqlBaglantisi.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while(dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            sqlBaglantisi.baglanti().Close();
            //
            
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorBrans=@p1", sqlBaglantisi.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            sqlBaglantisi.baglanti().Close();
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where RandevuBrans='" + cmbBrans.Text + "'" + "and RandevuDoktor='" +cmbDoktor.Text+ "' and RandevuDurum=0" , sqlBaglantisi.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void lnkBilgiDüzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDüzenle frmBilgiDüzenle = new FrmBilgiDüzenle();
            frmBilgiDüzenle.TCno = lblTC.Text;
            frmBilgiDüzenle.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Tbl_Randevular Set RandevuDurum=1,HastaTc=@p1,HastaSikayet=@p2 where RandevuId=@p3", sqlBaglantisi.baglanti());
            komut.Parameters.AddWithValue("@p1",lblTC.Text);
            komut.Parameters.AddWithValue("@p2" ,rchSikayet.Text);
            komut.Parameters.AddWithValue("@p3",txtId.Text);
            komut.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Randevu Alındı", "Uyarı:",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            DataTable dt = new DataTable();
            SqlCommand komut2 = new SqlCommand("Select * From Tbl_Randevular where HastaTC = @tc", sqlBaglantisi.baglanti());
            komut2.Parameters.AddWithValue("@tc", tc);
            SqlDataAdapter da = new SqlDataAdapter(komut2);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void FrmHastaDetay_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmHastaGiris frmHastaGiris = new FrmHastaGiris();
            frmHastaGiris.Show();
        }
    }
}
