using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HastaneYönetimRandevuSistemiOtomasyonProjesi
{
    public class sqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-QN7HAT1\\SQLEXPRESS;Initial Catalog=HastaneProje;Integrated Security=True;TrustServerCertificate=True");
            baglan.Open();
            return baglan;

         }
    }
}
