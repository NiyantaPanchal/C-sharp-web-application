using Hi_Tech_Order_Management_System.Business;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hi_Tech_Order_Management_System.DataAccess
{
    public class LoginDB
    {
        public static int CheckUserLoginDB(LoginData login)
        {
            using (SqlConnection conn = UtilityDB.ConnectDB())
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandText = String.Format("Select Count(*) from userDetails where UserName = '{0}' and Password = '{1}'",
                                                            login.Username, login.Password);


                    int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (count != 0)
                    {
                        sqlCommand.CommandText = String.Format("Select RoleID from userDetails where Username = '{0}' and Password = '{1}'",
                                                         login.Username, login.Password);

                        int role = Convert.ToInt32(sqlCommand.ExecuteScalar());
                        return role;
                    }
                    return -1;
                }
                catch (Exception)
                {
                    return -1;
                }

            }
        }

    }
}

