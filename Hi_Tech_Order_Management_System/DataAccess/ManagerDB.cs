using Hi_Tech_Order_Management_System.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hi_Tech_Order_Management_System.DataAccess
{
    public static class ManagerDB
    {
        
            public static DataTable ExecuteReaderQuery(string queryText)
            {
                DataTable dataTable = new DataTable();
                try
                {
                    using (SqlConnection conn = UtilityDB.ConnectDB())
                    {
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = conn;
                        sqlCommand.CommandText = queryText;
                        SqlDataReader dataReader = sqlCommand.ExecuteReader();
                        dataTable.Load(dataReader);
                        return dataTable;
                    }
                }
                catch (Exception)
                {
                    return dataTable;

                }
            }

            private static bool SqlNonQuery(string queryText)
            {
                try
                {
                    using (SqlConnection conn = UtilityDB.ConnectDB())
                    {
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = conn;
                        sqlCommand.CommandText = queryText;
                        sqlCommand.ExecuteNonQuery();
                        return true;
                    }

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            public static DataTable ReadUserDetails()
            {
                string queryText = String.Format("Select * from userDetails");
                return ManagerDB.ExecuteReaderQuery(queryText);
            }

            public static bool SaveRecoredDB(MISManager manager)
            {
                string queryText = String.Format("Insert into userDetails values ('{0}','{1}','{2}','{3}',{4})", manager.Firstname, manager.Lastname,
                                                    manager.Username, manager.Password, manager.Role);
                return ManagerDB.SqlNonQuery(queryText);
            }
            public static bool UpdateRecordDB(MISManager manager)
            {
                string queryText = String.Format("Update userDetails set Firstname = '{0}', Lastname = '{1}',Username = '{2}',Password = '{3}',PositionId = {4} where UserId = {5} ",
                                                  manager.Firstname, manager.Lastname,
                                                    manager.Username, manager.Password, manager.Role, manager.UserId);
                return ManagerDB.SqlNonQuery(queryText);
            }
            public static bool DeleteRecordDB(MISManager manager)
            {
                string queryText = String.Format("Delete From userDetails where UserId = {0} ", manager.UserId);
                return ManagerDB.SqlNonQuery(queryText);
            }

            public static DataTable SearchRecordDB(MISManager manager)
            {
                string queryText = String.Format("Select * From userDetails where UserId = {0} ", manager.UserId);
                return ManagerDB.ExecuteReaderQuery(queryText);
        }
        }
    }

