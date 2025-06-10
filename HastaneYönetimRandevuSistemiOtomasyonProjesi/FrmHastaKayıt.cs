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
    public partial class FrmHastaKayıt : Form
    {
        public FrmHastaKayıt()
        {
            InitializeComponent();
        }

        sqlBaglantisi sqlBaglantisi = new sqlBaglantisi();
        private void btnKayitYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Hastalar (HastaAd,HastaSoyad,HastaTC,HastaTelefon,HastaSifre,HastaCinsiyet) values (@HastaAd,@HastaSoyad,@HastaTC,@HastaTelefon,@HastaSifre,@HastaCinsiyet)" ,sqlBaglantisi.baglanti());
            komut.Parameters.AddWithValue("@HastaAd", txtAd.Text);
            komut.Parameters.AddWithValue("@HastaSoyad",txtSoyad.Text);
            komut.Parameters.AddWithValue("@HastaTC", mskTC.Text);
            komut.Parameters.AddWithValue("@HastaTelefon", mskTelefon.Text);
            komut.Parameters.AddWithValue("@HastaSifre",txtSifre.Text);
            komut.Parameters.AddWithValue("@HastaCinsiyet", cmbCinsiyet.Text);
            komut.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Kaydınız Gerçekleşmiştir Şifreniz:" +txtSifre.Text, "Bilgi", MessageBoxButtons.OK,MessageBoxIcon.Information);
            FrmHastaGiris frmHastaGiris = new FrmHastaGiris();
            frmHastaGiris.Show();
            this.Hide();
        }

       
    }
}
