using Hi_Tech_Order_Management_System.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hi_Tech_Order_Management_System.DataAccess
{
    public class SalesManagerDB
    {

        public static System.Data.SqlClient.SqlDataAdapter adapter = new SqlDataAdapter();
        public static SqlConnection conn = UtilityDB.ConnectDB();


        public static DataSet CreateTableDB(DataSet dsProject, DataTable dtCustomer)
        {
            DataColumn custommerID = dtCustomer.Columns.Add("CustomerID", typeof(Int16));
            custommerID.AutoIncrement = true;
            custommerID.AutoIncrementSeed = 1;
            custommerID.AutoIncrementStep = 1;

            dtCustomer.Columns.Add("CustomerName", typeof(string));
            dtCustomer.Columns.Add("Street", typeof(string));
            dtCustomer.Columns.Add("City", typeof(string));
            dtCustomer.Columns.Add("PostalCode", typeof(string));
            dtCustomer.Columns.Add("PhoneNumber", typeof(string));
            dtCustomer.Columns.Add("FaxNumber", typeof(string));
            dtCustomer.Columns.Add("CreditLimit", typeof(float));

            dtCustomer.PrimaryKey = new DataColumn[] { dtCustomer.Columns["CustomerID"] };

            dsProject.Tables.Add(dtCustomer);


            return dsProject;
        }

        public static void ReadDataFromDataset(DataSet dsproject, DataTable dtcustomer)
        {
            adapter = new SqlDataAdapter("select * from  customer", conn);
            adapter.Fill(dsproject.Tables["customer"]);
        }
        public static bool SaveRecordDataSet(DataSet dsproject, DataTable dtcustomer, Salesmanager manager)
        {
            try
            {
                dtcustomer.Rows.Add(null, manager.Customername, manager.Street, manager.City,
                manager.Postalcode, manager.Contactnumber, manager.Fax, manager.Creditlimit);


                string query = String.Format("Insert into customer values ('{0}','{1}','{2}','{3}','{4}','{5}',{6}) ",
                    manager.Customername, manager.Street, manager.City, manager.Postalcode,
                    manager.Contactnumber, manager.Fax, manager.Creditlimit);

                adapter.InsertCommand = new SqlCommand(query, conn);

                adapter.Update(dsproject, "customer");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
        public static bool UpdateRecordDataSet(DataSet dsproject, DataTable dtcustomer, Salesmanager manager)
        {
            try
            {
                DataRow drFindCustomerID = dtcustomer.Rows.Find(manager.CustomerId);

                drFindCustomerID["CustomerID"] = manager.CustomerId;
                drFindCustomerID["CustomerName"] = manager.Customername;
                drFindCustomerID["Street"] = manager.Street;
                drFindCustomerID["City"] = manager.City;
                drFindCustomerID["PostalCode"] = manager.Postalcode;
                drFindCustomerID["PhoneNumber"] = manager.Contactnumber;
                drFindCustomerID["FaxNumber"] = manager.Fax;
                drFindCustomerID["CreditLimit"] = manager.Creditlimit;

                string query = string.Format("Update customerDetails set CustomerName ='{0}', Street ='{1}',City ='{2}', " +
               " PostalCode ='{3}', PhoneNumber ='{4}',FaxNumber ='{5}',CreditLimit ='{6}' where CustomerID = {7}",
                   manager.Customername, manager.Street, manager.City, manager.Postalcode,
                   manager.Contactnumber, manager.Fax, manager.Creditlimit, manager.CustomerId);

                adapter.UpdateCommand = new SqlCommand(query, conn);
                adapter.Update(dsproject, "customer");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public static bool DeleteCustomerDataSet(DataSet dsproject, DataTable dtcustomer, Salesmanager manager)
        {
            try

            {
                DataRow drFindCustomer = dtcustomer.Rows.Find(manager.CustomerId);
                MessageBox.Show("Id = " + manager.CustomerId);
                drFindCustomer.Delete();
                string query = string.Format(" Delete From customer Where CustomerID = {0} ", manager.CustomerId);
                adapter.DeleteCommand = new SqlCommand(query, conn);
                adapter.Update(dsproject, "customer");
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(" " + ex.Message);
                return false;
            }
        }

        public static DataTable SearchCustomerRecord(DataTable dtCustomer, Salesmanager manager)
        {
            DataTable dtSearch = new DataTable("customerSearch");

            string queryText = "";

            if (manager.CustomerId != 0)
            {
                queryText = string.Format(" CustomerID = {0} ", manager.CustomerId);
            }
            else if (!string.IsNullOrEmpty(manager.Customername))
            {
                queryText = string.Format(" CustomerName LIKE '%{0}%' ", manager.Customername);
            }
            else if (!string.IsNullOrEmpty(manager.City))
            {
                queryText = string.Format(" City LIKE '%{0}%' ", manager.City);
            }

            dtSearch = dtCustomer.Select(queryText).CopyToDataTable();
            return dtSearch;
        }
    }



}
