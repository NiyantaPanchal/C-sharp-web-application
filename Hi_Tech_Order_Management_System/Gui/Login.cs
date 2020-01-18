using System;
using Hi_Tech_Order_Management_System.Business;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hi_Tech_Order_Management_System.Gui
{
    public partial class Login : Form
    {
        

        public Login()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }
        public bool ValidateEmptyTextBox(int checkNumber)
        {
            switch (checkNumber)
            {
                case 1:
                    return !String.IsNullOrEmpty(textBoxUser.Text) && !String.IsNullOrEmpty(textBoxPass.Text);
                default:
                    return false;
            }
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(1))
            {
               
                LoginData login = new LoginData();
                login.Username = textBoxUser.Text;
                login.Password = textBoxPass.Text;
                int role = login.CheckUserLogin(login);
                if (role != -1)
                {
                    switch (role)
                    {
                        case 1:
                            MessageBox.Show("Login Successful");
                            this.Hide();
                            Manager manager = new Manager();
                            manager.Show(); // Open manager Form
                            

                            break;
                        case 2:
                            MessageBox.Show("Login Successful");
                            this.Hide();
                            SalesManager managesale = new SalesManager();
                            managesale.ShowDialog(); // Open SalesManager Form
                            this.Close();
                            break;
                        case 3:
                            MessageBox.Show("Login Successful");
                            this.Hide();
                            Clerk orderClerks = new Clerk();
                            orderClerks.ShowDialog(); // Open clerks Form
                            this.Close();
                            break;
                        case 4:
                            MessageBox.Show("Login Successful");
                            this.Hide();
                            Accountant accountant = new Accountant();
                            accountant.ShowDialog(); // Open Accountant Form
                            this.Close();
                            break;
                        case 5:
                            MessageBox.Show("Login Successful");
                            this.Hide();
                            Inventory inventoy = new Inventory();
                            inventoy.ShowDialog(); // Open Inventory Form
                            this.Close();
                            break;
                        default:
                            MessageBox.Show("Invalid Username & Password");
                            break;

                    }
                }
                else
                {
                    MessageBox.Show("Login Unsuccessfull");
                }

            }
        }
        private void textBoxPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                // Enter key pressed
                buttonSubmit_Click(sender, e);
            }
        }
    }
}
