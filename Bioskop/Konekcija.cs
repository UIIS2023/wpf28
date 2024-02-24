using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bioskop
{
    class Konekcija
    {
        public SqlConnection KreirajKonekciju() 
        {
            SqlConnectionStringBuilder ccnSb = new SqlConnectionStringBuilder
            {
                DataSource = @"DESKTOP-V573UKG\SQLEXPRESS", 
                InitialCatalog = "Bioskop", 
                IntegratedSecurity = true 
            };
            string con = ccnSb.ToString();
            SqlConnection konekcija = new SqlConnection(con);
            return konekcija; 
        }
    }
}
