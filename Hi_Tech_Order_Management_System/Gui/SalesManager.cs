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
    public partial class SalesManager : Form
    {
        DataSet dsproject = new DataSet("HiTechProjectDB");
        DataTable dtcustomer = new DataTable("customer");
        public SalesManager()
        {
            InitializeComponent();
        }
        public void ClearTextBox()
        {
            textBoxID.Clear();
            textBoxCustomerN.Clear();
            textBoxStreet.Clear();
            textBoxCity.Clear();
            textBoxCode.Clear();
            textBoxCNumber.Clear();
            textBoxFax.Clear();
            textBoxCreditLimit.Clear();
        }

        public bool ValidationEmptyTextBox(int checkNumber)
        {
            switch (checkNumber)
            {
                case 1:
                    return !String.IsNullOrEmpty(textBoxID.Text) && !String.IsNullOrEmpty(textBoxStreet.Text)
                        && !String.IsNullOrEmpty(textBoxCity.Text) && !String.IsNullOrEmpty(textBoxCode.Text)
                        && !String.IsNullOrEmpty(textBoxCNumber.Text) && !String.IsNullOrEmpty(textBoxFax.Text)
                        && !String.IsNullOrEmpty(textBoxCreditLimit.Text);
                case 2:
                    return !String.IsNullOrEmpty(textBoxSearch.Text);

                default:

                    return false;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (ValidationEmptyTextBox(1))
            {
                Salesmanager manager = new Salesmanager();
                manager.CustomerId = Convert.ToInt32(textBoxID.Text);
                manager.Customername = textBoxCustomerN.Text;
                manager.Street = textBoxStreet.Text;
                manager.City = textBoxCity.Text;
                manager.Postalcode = textBoxCode.Text;
                manager.Contactnumber = Convert.ToInt32(textBoxCNumber.Text);
                manager.Fax = textBoxFax.Text;
                manager.Creditlimit = float.Parse(textBoxCreditLimit.Text);

                if (manager.SaveCustomerRecord(this.dsproject, this.dtcustomer, manager))
                {
                    MessageBox.Show("Customer Record is Inserted..");

                }
                else
                {
                    MessageBox.Show("Customer Record is not Inserted..", "Error", MessageBoxButtons.OK);

                }
                this.ClearTextBox();

            }
        }

        private void SalesManager_Load(object sender, EventArgs e)
        {
            Salesmanager manager = new Salesmanager();
            this.dsproject = manager.CreateTable(this.dsproject, this.dtcustomer);
            manager.ReadData(this.dsproject, this.dtcustomer);
            dataGridView1.DataSource = dtcustomer;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (ValidationEmptyTextBox(1) && !string.IsNullOrEmpty(textBoxID.Text))
            {
                if (MessageBox.Show("Do You Want to Update this Record", "Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    Salesmanager manager = new Salesmanager();
                    manager.CustomerId = Convert.ToInt32(textBoxID.Text);
                    manager.Customername = textBoxCustomerN.Text;
                    manager.Street = textBoxStreet.Text;
                    manager.City = textBoxCity.Text;
                    manager.Postalcode = textBoxCode.Text;
                    manager.Contactnumber = Convert.ToInt32(textBoxCNumber.Text);
                    manager.Fax = textBoxFax.Text;
                    manager.Creditlimit = float.Parse(textBoxCreditLimit.Text);


                    if (manager.UpdateCustomerRecord(this.dsproject, this.dtcustomer, manager))
                    {
                        MessageBox.Show("Customer Record Is Updated.");
                    }
                    else
                    {
                        MessageBox.Show("Customer Recored Is Not Updated..", "Error", MessageBoxButtons.OK);

                    }
                }
                this.ClearTextBox();
            }
            else
            {
                MessageBox.Show("Please Select a Row For Update Operation.");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (ValidationEmptyTextBox(1) && !string.IsNullOrEmpty(textBoxID.Text))
            {
                if (MessageBox.Show("Do You Want to Delete this Record", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Salesmanager manager = new Salesmanager();
                    manager.CustomerId = Convert.ToInt32(textBoxID.Text);

                    if (manager.DeleteCustomerRecord(dsproject, dtcustomer, manager))
                    {
                        MessageBox.Show("Customer record is deleted...");
                    }
                    else
                    {
                        MessageBox.Show("Customer record is not deleted..", "Error", MessageBoxButtons.OK);
                    }
                    this.ClearTextBox();
                }
            }
            else
            {
                MessageBox.Show("Please Select a Row For Delete Operation.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBoxID.Text = row.Cells["CustomerID"].Value.ToString();
                textBoxCustomerN.Text = row.Cells["CustomerName"].Value.ToString();
                textBoxStreet.Text = row.Cells["Street"].Value.ToString();
                textBoxCity.Text = row.Cells["City"].Value.ToString();
                textBoxCode.Text = row.Cells["PostalCode"].Value.ToString();
                textBoxCNumber.Text = row.Cells["PhoneNumber"].Value.ToString();
                textBoxFax.Text = row.Cells["FaxNumber"].Value.ToString();
                textBoxCreditLimit.Text = row.Cells["CreditLimit"].Value.ToString();
            }
        }

        
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (ValidationEmptyTextBox(2))
            {
                try
                {
                    Salesmanager salesManager = new Salesmanager();

                    if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[0])) // CustomerID
                    {
                        salesManager.CustomerId = Convert.ToInt32(textBoxSearch.Text);
                    }
                    else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[1])) // CustomerName
                    {
                        salesManager.Customername = textBoxSearch.Text;
                    }
                    else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[2])) // CustomerName
                    {
                        salesManager.City = textBoxSearch.Text;
                    }
                    DataTable dataTable = salesManager.SearchRecord(dtcustomer, salesManager);

                    if (dataTable != null)
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
                catch (Exception ex)
                {
                    MessageBox.Show("Please Select and Enter Correct Search Field and Search ");
                    this.ClearTextBox();
                }
            }
            else
            {
                MessageBox.Show("Please enter some data in search box");

            }

        }

    }
}
