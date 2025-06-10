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
    public partial class FrmBransPanel : Form
    {
        public FrmBransPanel()
        {
            InitializeComponent();
        }
        sqlBaglantisi sqlBaglantisi = new sqlBaglantisi();  

        private void FrmBransPanel_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Branslar",sqlBaglantisi.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Branslar (BransAd) values (@b1)",sqlBaglantisi.baglanti());
            komut.Parameters.AddWithValue("@b1" ,txtBrans.Text);
            komut.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Branş Eklendi!","Bilgi:",MessageBoxButtons.OK,MessageBoxIcon.Information);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Branslar", sqlBaglantisi.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("Delete From Tbl_Branslar where BransId=@b2", sqlBaglantisi.baglanti());
            komut2.Parameters.AddWithValue("@b2", txtId.Text);
            komut2.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Branş Silindi!", "Bilgi:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Branslar", sqlBaglantisi.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtBrans.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();


        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("Update Tbl_Branslar set BransAd=@b1 where BransId=@b2", sqlBaglantisi.baglanti());
            komut3.Parameters.AddWithValue("@b1" ,txtBrans.Text);
            komut3.Parameters.AddWithValue("@b2", txtId.Text);
            komut3.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Branş Güncellendi!", "Bilgi:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Branslar", sqlBaglantisi.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

       
    }
}
