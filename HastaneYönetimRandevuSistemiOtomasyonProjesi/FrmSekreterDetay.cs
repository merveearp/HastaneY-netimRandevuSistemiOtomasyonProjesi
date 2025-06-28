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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        public string TCno;
        sqlBaglantisi sqlBaglantisi=new sqlBaglantisi();
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TCno;
            //ADSOYAD
            SqlCommand komut1 = new SqlCommand("Select SekreterAdSoyad From Tbl_Sekreter where SekreterTC=@p1", sqlBaglantisi.baglanti());
            komut1.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lblAdSoyad.Text = dr1[0].ToString();
            }
            sqlBaglantisi.baglanti().Close();
            
            //Bransları datagride aktarma 
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Branslar", sqlBaglantisi.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource=dt1;

            //Doktorları Listeye Aktarma 
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2= new SqlDataAdapter("Select (DoktorAd +' '+ DoktorSoyad) as 'Doktorlar',DoktorBrans From Tbl_Doktorlar", sqlBaglantisi.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Branşı comboboxa aktarma

            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar", sqlBaglantisi.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0].ToString());
            }
            sqlBaglantisi.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor,HastaTC,RandevuDurum) values (@p1,@p2,@p3,@p4,@p5,@p6)", sqlBaglantisi.baglanti());
            komutkaydet.Parameters.AddWithValue("@p1",mskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@p2",mskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@p3",cmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@p4",cmbDoktor.Text);
            komutkaydet.Parameters.AddWithValue("@p5",mskTC.Text);
            komutkaydet.Parameters.AddWithValue("@p6",chkDurum.Checked);
            komutkaydet.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu.");
            mskTarih.Text = " ";
            mskSaat.Text = " ";
            cmbBrans.Text = " ";
            cmbDoktor.Text = " ";
            mskTC.Text = " ";
            chkDurum.Checked = false;

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorBrans=@p1", sqlBaglantisi.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbDoktor.Items.Add(dr[0] + " " + dr[1]);

            }
            sqlBaglantisi.baglanti().Close();
        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@d1)", sqlBaglantisi.baglanti());
            komut.Parameters.AddWithValue("@d1", rchDuyuru.Text);
            komut.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu!");

        }

        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli frmDoktorPaneli= new FrmDoktorPaneli();
            frmDoktorPaneli.Show();
        }

        private void btnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBransPanel frmBransPanel= new FrmBransPanel();
            frmBransPanel.Show();
        }

        private void btnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frmRandevuListesi = new FrmRandevuListesi();
            frmRandevuListesi.Show();
        }

        private void btnDuyuruPaneli_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmDuyurular = new FrmDuyurular();
            frmDuyurular.Show();
        }

        private void FrmSekreterDetay_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmSekreterGiris frmSekreterGiris = new FrmSekreterGiris();
            frmSekreterGiris.Show();
        }
    }
}
