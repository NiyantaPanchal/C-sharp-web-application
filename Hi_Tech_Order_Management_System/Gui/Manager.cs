using Hi_Tech_Order_Management_System.Business;
using System;
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
    public partial class Manager : Form
    {
        public Manager()
        {
            InitializeComponent();
        }
        public void RefreshData()
        {
            MISManager manager = new MISManager();
            dataGridView1.DataSource = manager.ReadUserData();
            ClearTextBox();
        }
        public bool CheckFieldIsEmptyOrNot(int checkNumber)
        {
            switch (checkNumber)
            {
                case 1:
                    return !String.IsNullOrEmpty(textBoxFN.Text) &&
                        !String.IsNullOrEmpty(textBoxLN.Text) &&
                        !String.IsNullOrEmpty(textBoxUserName.Text) &&
                        !String.IsNullOrEmpty(textBoxPass.Text) &&
                        !String.IsNullOrEmpty(textBoxRole.Text);
                
                case 2:
                    return !String.IsNullOrEmpty(textBoxSearch.Text);

                default:
                    //! Return false in default case 
                    return false;
            }
        }
        public void ClearTextBox()
        {
            textBoxFN.Clear();
            textBoxLN.Clear();
            textBoxUserName.Clear();
            textBoxPass.Clear();
            textBoxRole.Clear();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (CheckFieldIsEmptyOrNot(1))
            {
                MISManager manager = new MISManager();
                manager.Firstname = textBoxFN.Text;
                manager.Lastname = textBoxLN.Text;
                manager.Username = textBoxUserName.Text;
                manager.Password = textBoxPass.Text;
                manager.Role = Convert.ToInt32(textBoxRole.Text);
                if (manager.SaveRecored(manager))  //! TRUE
                {
                    MessageBox.Show("Recored Inserted..");

                }
                else
                {
                    MessageBox.Show("Recored is Not Inserted..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                RefreshData();
                this.ClearTextBox();

            }
            else
            {
                MessageBox.Show("Please Enter Values! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void Manager_Load(object sender, EventArgs e)
        {
            MISManager manager = new MISManager();
            dataGridView1.DataSource = manager.ReadUserData();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (CheckFieldIsEmptyOrNot(1))
            {
                if (MessageBox.Show("Do You Want to Update? ", "Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MISManager manager = new MISManager();
                    manager.UserId = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                    manager.Firstname = textBoxFN.Text;
                    manager.Lastname = textBoxLN.Text;
                    manager.Username = textBoxUserName.Text;
                    manager.Password = textBoxPass.Text;
                    manager.Role = Convert.ToInt32(textBoxRole.Text);

                    if (manager.UpdateRecord(manager))
                    {
                        MessageBox.Show("Record Updated.");


                    }
                    else
                    {
                        MessageBox.Show("Record is Not Updated.");
                    }
                }
                RefreshData();
                this.ClearTextBox();
            }

            else
            {
                MessageBox.Show("Please select a row");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (CheckFieldIsEmptyOrNot(1))
            {
                if (MessageBox.Show("Do You Want to Delete?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MISManager manager = new MISManager();
                    manager.UserId = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                    if (manager.DeleteRecord(manager))
                    {
                        MessageBox.Show("Record is Deleted.");
                    }
                    else
                    {
                        MessageBox.Show("Record can not be deleted.");
                    }
                    RefreshData();
                    this.ClearTextBox();
                }
            }
            else
            {
                MessageBox.Show("Please Select a Row.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                MISManager manager = new MISManager();
                manager.UserId = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);

                textBoxFN.Text = row.Cells["Firstname"].Value.ToString();
                textBoxLN.Text = row.Cells["Lastname"].Value.ToString();
                textBoxUserName.Text = row.Cells["Username"].Value.ToString();
                textBoxPass.Text = row.Cells["Password"].Value.ToString();
                textBoxRole.Text = row.Cells["Role"].Value.ToString();

            }
        }
        public void SearchRecord(MISManager manager)
        {
            DataTable dataTable = manager.SearchRecord(manager);
            if (dataTable.Rows.Count != 0)
            {
                dataGridView2.DataSource = dataTable;
            }
            else
            {
                dataGridView2.DataSource = null;
                MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            this.ClearTextBox();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            {
                int userid = Convert.ToInt32(textBoxSearch.Text);

                if (textBoxSearch.Text == "")
                {
                    MessageBox.Show("Can not be Empty.............", "Error", MessageBoxButtons.OK);
                }
                else
                {
                    MISManager manager = new MISManager();
                    if (textBoxSearch.Text == "")
                    {
                        
                    }
                    else
                    {
                        manager.UserId = userid;
                    }
                    

                    var i = manager.SearchRecord(manager);


                    if (i.Rows.Count > 0)
                    {
                        dataGridView2.DataSource = manager.SearchRecord(manager);
                    }
                    else
                    {
                        MessageBox.Show("User Data is not exist !!");
                    }
                    textBoxSearch.Text = "";
               
                }
            }

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
 }

