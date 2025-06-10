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
    public partial class FrmDoktorBilgiDüzenle : Form
    {
        public FrmDoktorBilgiDüzenle()
        {
            InitializeComponent();
        }

        sqlBaglantisi sqlBaglantisi = new sqlBaglantisi();
        public string TCNO;
        private void FrmDoktorBilgiDüzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text = TCNO;

            SqlCommand komut = new SqlCommand("Select * From Tbl_Doktorlar where DoktorTC=@p1",sqlBaglantisi.baglanti());
            komut.Parameters.AddWithValue("@p1",mskTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                cmbBrans.Text = dr[3].ToString();
                txtSifre.Text = dr[5].ToString();
            }
            sqlBaglantisi.baglanti().Close();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p4 where DoktorTC=@p5" ,sqlBaglantisi.baglanti());
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",cmbBrans.Text);
            komut.Parameters.AddWithValue("@p4",txtSifre.Text);
            komut.Parameters.AddWithValue("@p5", mskTC.Text);
            komut.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Kayıt Güncellendi!");

                
        }
    }
}
