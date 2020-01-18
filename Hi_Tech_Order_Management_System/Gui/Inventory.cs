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
    public partial class Inventory : Form
    {
        HiTechProjectDBEntities2 HiTechProjectDBEntities2 = new HiTechProjectDBEntities2();
        public Inventory()
        {
            InitializeComponent();
        }
        private void cleanTextBox()
        {
            textBoxISBN.Text = string.Empty;
            comboBoxCategory.Text = string.Empty;
            textBoxTitle.Text = string.Empty; 
            textBoxYearPublished.Text = string.Empty;
            textBoxQOH.Text = string.Empty;
            textBoxAuthorID.Text = string.Empty;
            textBoxFN.Text = string.Empty;
            textBoxLN.Text = string.Empty;
            textBoxEmail.Text = string.Empty;
            textBoxSupplierID.Text = string.Empty;
            textBoxPublishedBY.Text = string.Empty;
            textBoxUnitPrice.Text = string.Empty;

        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            BookDetail bd = new BookDetail();
            AuthorDetail ad = new AuthorDetail();
            SupplierDetail sd = new SupplierDetail();
            bd.ISBN = Convert.ToInt32(textBoxISBN.Text.Trim());
            bd.B_CATEGORY = comboBoxCategory.Text.Trim();
            bd.B_TITLE = textBoxTitle.Text.Trim();
            bd.B_UNITPRICE = Convert.ToInt32(textBoxUnitPrice.Text);
            bd.B_YEAR = Convert.ToInt32(textBoxYearPublished.Text.Trim());
            bd.B_QOH = Convert.ToInt32(textBoxQOH.Text.Trim());
            bd.AID = Convert.ToInt32(textBoxAuthorID.Text.Trim());
            ad.AID = Convert.ToInt32(textBoxAuthorID.Text.Trim());
            ad.A_FNAME = textBoxFN.Text.Trim();
            ad.A_LNAME = textBoxLN.Text.Trim();
            ad.A_EMAIL = textBoxEmail.Text.Trim();
            bd.SID = Convert.ToInt32(textBoxSupplierID.Text.Trim());
            sd.SID = Convert.ToInt32(textBoxSupplierID.Text.Trim());
            sd.SUPPLIER = textBoxSupplierID.Text.Trim();
            HiTechProjectDBEntities2.BookDetails.Add(bd);
            HiTechProjectDBEntities2.AuthorDetails.Add(ad);
            HiTechProjectDBEntities2.SupplierDetails.Add(sd);
            HiTechProjectDBEntities2.SaveChanges();
            MessageBox.Show("Book Saved successfully");
            cleanTextBox();
            PopulateList1();
        }

        public void PopulateList1()
        {

            var proList1 = (from book in HiTechProjectDBEntities2.BookDetails
                            join author in HiTechProjectDBEntities2.AuthorDetails
                            on book.AID equals author.AID
                            join supplier in HiTechProjectDBEntities2.SupplierDetails
                            on book.SID equals supplier.SID

                            select new
                            {
                                isbn = book.ISBN,
                                category = book.B_CATEGORY,
                                title = book.B_TITLE,
                                price = book.B_UNITPRICE,
                                year = book.B_YEAR,
                                qoh = book.B_QOH,
                                aid = author.AID,
                                fname = author.A_FNAME,
                                lname = author.A_LNAME,
                                email = author.A_EMAIL,
                                sid = supplier.SID,
                                publisher = supplier.SUPPLIER

                            }).ToList();


            listView1.Items.Clear();
            foreach (var book in proList1)
            {

                ListViewItem item = new ListViewItem(Convert.ToString(book.isbn));
                item.SubItems.Add(book.category);
                item.SubItems.Add(book.title);
                item.SubItems.Add(Convert.ToString(book.price));
                item.SubItems.Add(Convert.ToString(book.year));
                item.SubItems.Add(Convert.ToString(book.qoh));
                item.SubItems.Add(Convert.ToString(book.aid));
                item.SubItems.Add(book.fname);
                item.SubItems.Add(book.lname);
                item.SubItems.Add(book.email);
                item.SubItems.Add(Convert.ToString(book.sid));
                item.SubItems.Add(book.publisher);
                listView1.Items.Add(item);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            textBoxISBN.Enabled = false;
            textBoxAuthorID.Enabled = false;
            textBoxSupplierID.Enabled = false;
            int isbn = Convert.ToInt32(textBoxISBN.Text);
            BookDetail updateBook = HiTechProjectDBEntities2.BookDetails.Find(isbn);
            int aid = Convert.ToInt32(textBoxAuthorID.Text);
            AuthorDetail updateAuthor = HiTechProjectDBEntities2.AuthorDetails.Find(aid);
            int sid = Convert.ToInt32(textBoxSupplierID.Text);
            SupplierDetail updateSupplier = HiTechProjectDBEntities2.SupplierDetails.Find(sid);
            updateBook.B_CATEGORY = comboBoxCategory.Text;
            updateBook.B_TITLE = textBoxTitle.Text;
            updateBook.B_UNITPRICE = Convert.ToInt32(textBoxUnitPrice.Text);
            updateBook.B_YEAR = Convert.ToInt32(textBoxYearPublished.Text);
            updateBook.B_QOH = Convert.ToInt32(textBoxQOH.Text);
            updateBook.AID = Convert.ToInt32(textBoxAuthorID.Text);
            updateBook.SID = Convert.ToInt32(textBoxSupplierID.Text);
            updateAuthor.AID = Convert.ToInt32(textBoxAuthorID.Text);
            updateAuthor.A_FNAME = textBoxFN.Text;
            updateAuthor.A_LNAME = textBoxLN.Text;
            updateAuthor.A_EMAIL = textBoxEmail.Text;
            updateSupplier.SID = Convert.ToInt32(textBoxSupplierID.Text);
            updateSupplier.SUPPLIER = textBoxPublishedBY.Text;

            HiTechProjectDBEntities2.SaveChanges();
            PopulateList1();
            MessageBox.Show("Book Update successful");
            cleanTextBox();
            textBoxISBN.Enabled = true;
            textBoxAuthorID.Enabled = true;
            textBoxSupplierID.Enabled = true;

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            BookDetail bd = new BookDetail();
            AuthorDetail ad = new AuthorDetail();
            SupplierDetail sd = new SupplierDetail();
            int isbn = Convert.ToInt32(textBoxISBN.Text.Trim());
            bd = HiTechProjectDBEntities2.BookDetails.Find(isbn);
            int aid = Convert.ToInt32(textBoxAuthorID.Text.Trim());
            ad = HiTechProjectDBEntities2.AuthorDetails.Find(aid);
            int sid = Convert.ToInt32(textBoxSupplierID.Text.Trim());
            sd = HiTechProjectDBEntities2.SupplierDetails.Find(sid);
            if (bd == null && ad == null)
            {
                MessageBox.Show("Duplicated EmployeeId", "Error");
                textBoxISBN.Clear();
                textBoxISBN.Focus();
                return;
            }
            else
            {

                HiTechProjectDBEntities2.BookDetails.Remove(bd);
                HiTechProjectDBEntities2.AuthorDetails.Remove(ad);
                HiTechProjectDBEntities2.SupplierDetails.Remove(sd);
                HiTechProjectDBEntities2.SaveChanges();
                PopulateList1();
                MessageBox.Show("Deleted Successfully");
                cleanTextBox();

            }

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            BookDetail bd = new BookDetail();
            AuthorDetail ad = new AuthorDetail();
            SupplierDetail sd = new SupplierDetail();
            if (bd == null && ad == null && sd == null)
            {
                MessageBox.Show("Duplicated EmployeeId", "Error");
                textBoxISBN.Clear();
                textBoxISBN.Focus();
                return;
            }
            var res = Convert.ToInt32(textBoxSearch.Text.Trim());
            var proList1 = (from book in HiTechProjectDBEntities2.BookDetails
                            join author in HiTechProjectDBEntities2.AuthorDetails
                            on book.AID equals author.AID
                            join supplier in HiTechProjectDBEntities2.SupplierDetails
                            on book.SID equals supplier.SID
                            where book.ISBN == res
                            select new
                            {
                                isbn = book.ISBN,
                                category = book.B_CATEGORY,
                                title = book.B_TITLE,
                                price = book.B_UNITPRICE,
                                year = book.B_YEAR,
                                qoh = book.B_QOH,
                                aid = author.AID,
                                fname = author.A_FNAME,
                                lname = author.A_LNAME,
                                email = author.A_EMAIL,
                                sid = supplier.SID,
                                publisher = supplier.SUPPLIER

                            }).ToList();


            listView2.Items.Clear();
            foreach (var book in proList1)
            {

                ListViewItem item = new ListViewItem(Convert.ToString(book.isbn));
                item.SubItems.Add(book.category);
                item.SubItems.Add(book.title);
                item.SubItems.Add(Convert.ToString(book.price));
                item.SubItems.Add(Convert.ToString(book.year));
                item.SubItems.Add(Convert.ToString(book.qoh));
                item.SubItems.Add(Convert.ToString(book.aid));
                item.SubItems.Add(book.fname);
                item.SubItems.Add(book.lname);
                item.SubItems.Add(book.email);
                item.SubItems.Add(Convert.ToString(book.sid));
                item.SubItems.Add(book.publisher);
                listView2.Items.Add(item);
            }
        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            PopulateList1();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxISBN.Enabled = false;
            textBoxAuthorID.Enabled = false;
            textBoxSupplierID.Enabled = false;
            ListViewItem item = listView1.SelectedItems[0];
            textBoxISBN.Text = item.SubItems[0].Text;
            comboBoxCategory.Text = item.SubItems[1].Text;
            textBoxTitle.Text = item.SubItems[2].Text;
            textBoxUnitPrice.Text = item.SubItems[3].Text;
            textBoxYearPublished.Text = item.SubItems[4].Text;
            textBoxQOH.Text = item.SubItems[5].Text;
            textBoxAuthorID.Text = item.SubItems[6].Text;
            textBoxFN.Text = item.SubItems[7].Text;
            textBoxLN.Text = item.SubItems[8].Text;
            textBoxEmail.Text = item.SubItems[9].Text;
            textBoxSupplierID.Text = item.SubItems[10].Text;
            textBoxPublishedBY.Text = item.SubItems[11].Text;
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            comboBoxCategory.Items.Add("Science");
            comboBoxCategory.Items.Add("Mathematics");
            comboBoxCategory.Items.Add("General Knowledge");
            comboBoxCategory.Items.Add("Comic");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
