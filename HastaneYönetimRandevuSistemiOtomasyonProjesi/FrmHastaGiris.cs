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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }
        sqlBaglantisi sqlBaglantisi = new sqlBaglantisi();

        private void lnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayıt frmHastaKayıt =new FrmHastaKayıt();
            frmHastaKayıt.Show();
            this.Hide();
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("Select * From Tbl_Hastalar where HastaTC=@HastaTC and HastaSifre=@HastaSifre", sqlBaglantisi.baglanti());
            sqlCommand.Parameters.AddWithValue("@HastaTC", mskTC.Text);
            sqlCommand.Parameters.AddWithValue("HastaSifre", txtSifre.Text);
            SqlDataReader dr =sqlCommand.ExecuteReader();
            if (dr.Read())
            {
                FrmHastaDetay frmHastaDetay = new FrmHastaDetay();
                frmHastaDetay.tc = mskTC.Text;
                frmHastaDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC & Şifre");
            }

            sqlBaglantisi.baglanti().Close();
        }

        private void FrmHastaGiris_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmGirisler frmGirisler = new FrmGirisler();
            frmGirisler.Show();
        }
    }
}
