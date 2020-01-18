using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Hi_Tech_Order_Management_System.DataAccess
{
    public class UtilityDB
    {
        public static SqlConnection ConnectDB()
        {
            //connect database with winodws authentication
            SqlConnection connection = new SqlConnection( "data source = (local)\\MSSQLSERVER2017 ; database = HiTechProjectDB; user = sa; password = demo");
            connection.Open();
            return connection;

        }
    }
}
