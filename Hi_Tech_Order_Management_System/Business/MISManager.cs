using Hi_Tech_Order_Management_System.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hi_Tech_Order_Management_System.Business
{
    public class MISManager
    {
        private int userId;
        private string firstname;
        private string lastname;
        private string username;
        private string password;
        private int role;
        public string Firstname { get => firstname; set => firstname = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public int Role { get => role; set => role = value; }
        public int UserId { get => userId; set => userId = value; }

        public DataTable ReadUserData()
        {
            return ManagerDB.ReadUserDetails();
        }
        public bool SaveRecored(MISManager manager)
        {
            return ManagerDB.SaveRecoredDB(manager);
        }
        public bool UpdateRecord(MISManager manager)
        {
            return ManagerDB.UpdateRecordDB(manager);
        }

        public bool DeleteRecord(MISManager manager)
        {
            return ManagerDB.DeleteRecordDB(manager);
        }
        public DataTable SearchRecord(MISManager manager)
        {
            return ManagerDB.SearchRecordDB(manager);
        }
    
}
}
