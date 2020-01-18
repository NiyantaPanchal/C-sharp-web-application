using Hi_Tech_Order_Management_System.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hi_Tech_Order_Management_System.Business
{
   public class LoginData
    {
        private string username;
        private string password; 

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }

        public int CheckUserLogin(LoginData login)
        {
            return LoginDB.CheckUserLoginDB(login);
        }
    }
}
