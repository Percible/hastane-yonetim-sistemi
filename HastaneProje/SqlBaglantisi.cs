using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HastaneProje
{
    public class SqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglanti = new SqlConnection("Data Source=192.168.1.10;Initial Catalog=HBYS;User ID=sa;Password=Password1");
            baglanti.Open();

            return baglanti;


        }


    }
}
