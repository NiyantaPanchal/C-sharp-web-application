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
    public partial class Accountant : Form
    {
        public Accountant()
        {
            InitializeComponent();
        }

       

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
        Bitmap bitmap;
        private void buttonDisplay_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            bitmap  = new Bitmap(this.Size.Width, this.Size.Height, g);
            Graphics mg = Graphics.FromImage(bitmap);
            mg.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, this.Size);
            printPreviewDialog1.ShowDialog();


        }

        private void Accountant_Load(object sender, EventArgs e)
        {
            using (HiTechProjectDBEntities3 hit = new HiTechProjectDBEntities3())
            {
                orderBindingSource.DataSource = hit.Orders.ToList();
            }
        }
    }
}
