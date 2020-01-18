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
    public partial class Clerk : Form
    {
        HiTechProjectDBEntities1 HiTechProjectDBEntities1 = new HiTechProjectDBEntities1();
        public Clerk()
        {
            InitializeComponent();
        }
        private void cleanTextBox()
        {
            textBoxcustomerID.Text = string.Empty;
            textBoxorderID.Text = string.Empty;
            textBoxISBN.Text = string.Empty;
            textBoxClerkID.Text = string.Empty;
            comboBox1.Text = string.Empty;
            textBoxNOBook.Text = string.Empty;

        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.OrderID = Convert.ToInt32(textBoxorderID.Text.Trim());
            order.CustomerID = Convert.ToInt32(textBoxcustomerID.Text.Trim());
            order.ISBN = textBoxISBN.Text.Trim();
            order.ClerkID = Convert.ToInt32(textBoxClerkID.Text.Trim());
            order.OrderDate = Convert.ToDateTime(dateTimePicker1.Text.Trim());
            order.OrderType = comboBox1.Text.Trim();
            order.NoOfBook = Convert.ToInt32(textBoxNOBook.Text.Trim());
            HiTechProjectDBEntities1.Orders.Add(order);
            HiTechProjectDBEntities1.SaveChanges();
            MessageBox.Show("Order Successfully saved");
            cleanTextBox();

        }

        private void Clerk_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Phone");
            comboBox1.Items.Add("Fax");
            comboBox1.Items.Add("Email");
        }
        public void PopulateList1()
        {

            var proList1 = (from order in HiTechProjectDBEntities1.Orders
                            select new
                            {
                                orderid = order.OrderID,
                                customerid = order.CustomerID,
                                isbn = order.ISBN,
                                clerkid = order.ClerkID,
                                date = order.OrderDate,
                                type = order.OrderType,
                                quantity = order.NoOfBook

                            }).ToList();
            listView1.Items.Clear();
            foreach (var order in proList1)
            {

                ListViewItem item = new ListViewItem(Convert.ToString(order.orderid));
                item.SubItems.Add(Convert.ToString(order.customerid));
                item.SubItems.Add(Convert.ToString(order.isbn));
                item.SubItems.Add(Convert.ToString(order.clerkid));
                item.SubItems.Add(order.date.ToString());
                item.SubItems.Add(order.type);
                item.SubItems.Add(Convert.ToString(order.quantity));
                listView1.Items.Add(item);
            }
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            int oid = Convert.ToInt32(textBoxorderID.Text);
            Order order = HiTechProjectDBEntities1.Orders.Find(oid);
            order.OrderID = Convert.ToInt32(textBoxorderID.Text.Trim());
            order.CustomerID = Convert.ToInt32(textBoxcustomerID.Text.Trim());
            order.ISBN = textBoxISBN.Text.Trim();
            order.ClerkID = Convert.ToInt32(textBoxClerkID.Text.Trim());
            order.OrderDate = Convert.ToDateTime(dateTimePicker1.Text.Trim());
            order.OrderType = comboBox1.Text.Trim();
            order.NoOfBook = Convert.ToInt32(textBoxNOBook.Text.Trim());

            HiTechProjectDBEntities1.SaveChanges();
            PopulateList1();
            MessageBox.Show("Order Update successful");
            cleanTextBox();
        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            int orderid = Convert.ToInt32(textBoxorderID.Text.Trim());
            order = HiTechProjectDBEntities1.Orders.Find(orderid);

            if (order == null)
            {
                MessageBox.Show("Duplicated OrderID", "Error");
                textBoxorderID.Clear();
                textBoxorderID.Focus();
                return;
            }
            else
            {
                HiTechProjectDBEntities1.Orders.Remove(order);
                HiTechProjectDBEntities1.SaveChanges();
                PopulateList1();
                MessageBox.Show("Deleted Successfully");
                cleanTextBox();

            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Order order = new Order();

            if (order == null)
            {
                MessageBox.Show("Duplicated OrderID", "Error");
                textBoxorderID.Clear();
                textBoxorderID.Focus();
                return;
            }
            var res = Convert.ToInt32(textBoxSearch.Text.Trim());
            var proList1 = (from od in HiTechProjectDBEntities1.Orders
                            where od.OrderID == res
                            select new
                            {
                                oid = od.OrderID,
                                cid = od.CustomerID,
                                isbn = od.ISBN,
                                clerkid = od.ClerkID,
                                date = od.OrderDate,
                                type = od.OrderType,
                                quantity = od.NoOfBook

                            }).ToList();
            listView1.Items.Clear();
            foreach (var od in proList1)
            {

                ListViewItem item = new ListViewItem(Convert.ToString(od.oid));
                item.SubItems.Add(Convert.ToString(od.cid));
                item.SubItems.Add(Convert.ToString(od.isbn));
                item.SubItems.Add(Convert.ToString(od.clerkid));
                item.SubItems.Add(od.date.ToString());
                item.SubItems.Add(od.type);
                item.SubItems.Add(Convert.ToString(od.quantity));
                listView1.Items.Add(item);
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxorderID.Enabled = false;
            ListViewItem item = listView1.SelectedItems[0];
            textBoxorderID.Text = item.SubItems[0].Text;
            textBoxcustomerID.Text = item.SubItems[1].Text;
            textBoxISBN.Text = item.SubItems[2].Text;
            textBoxClerkID.Text = item.SubItems[3].Text;
            dateTimePicker1.Text = item.SubItems[4].Text;
            comboBox1.Text = item.SubItems[5].Text;
            textBoxNOBook.Text = item.SubItems[6].Text;
        }

        private void buttonshow_Click(object sender, EventArgs e)
        {
            PopulateList1();
        }
    }
}
