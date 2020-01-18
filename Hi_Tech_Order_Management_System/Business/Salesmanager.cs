using Hi_Tech_Order_Management_System.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hi_Tech_Order_Management_System.Business
{
    public class Salesmanager
    {
        private int customerId;
        private int contactnumber;
        private float creditlimit;
        private string customername;
        private string street;
        private string city;
        private string postalcode;
        private string fax;

        public int CustomerId { get => customerId; set => customerId = value; }
        public int Contactnumber { get => contactnumber; set => contactnumber = value; }
        public string Customername { get => customername; set => customername = value; }
        public string Street { get => street; set => street = value; }
        public string City { get => city; set => city = value; }
        public string Postalcode { get => postalcode; set => postalcode = value; }
        public float Creditlimit { get => creditlimit; set => creditlimit = value; }
        public string Fax { get => fax; set => fax = value; }

        public DataSet CreateTable(DataSet ds, DataTable dt)
        {
            return SalesManagerDB.CreateTableDB(ds, dt);
        }
        public void ReadData(DataSet ds, DataTable dt)
        {
            SalesManagerDB.ReadDataFromDataset(ds, dt);
        }
        public bool SaveCustomerRecord(DataSet dsproject, DataTable dtcustomer, Salesmanager manager)
        {
            return SalesManagerDB.SaveRecordDataSet(dsproject, dtcustomer, manager);
        }


        public bool UpdateCustomerRecord(DataSet dsproject, DataTable dtcustomer, Salesmanager manager)
        {
            return SalesManagerDB.UpdateRecordDataSet(dsproject, dtcustomer, manager);
        }

        public bool DeleteCustomerRecord(DataSet dsproject, DataTable dtcustomer, Salesmanager manager)
        {
            return SalesManagerDB.DeleteCustomerDataSet(dsproject, dtcustomer, manager);
        }
        public DataTable SearchRecord(DataTable dtCustomer, Salesmanager manager)
        {
            return SalesManagerDB.SearchCustomerRecord(dtCustomer, manager);
        }

    }
}
  
