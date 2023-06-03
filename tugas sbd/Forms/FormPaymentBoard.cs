using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tugas_sbd.Forms
{
    public partial class FormPaymentBoard : Form
    {
        private PictureBox revenueCard = new PictureBox();
        private TextBox revenueTitle = new TextBox();
        private TextBox revenueNumber = new TextBox();
        private PictureBox membershipCard = new PictureBox();
        private PictureBox fbCard = new PictureBox();
        private PictureBox productCard = new PictureBox();
        private IconPictureBox membershipLogo = new IconPictureBox();
        private IconPictureBox fbLogo = new IconPictureBox();
        private IconPictureBox productLogo = new IconPictureBox();
        private TextBox membershipNumber = new TextBox();
        private TextBox fbNumber = new TextBox();
        private TextBox productNumber = new TextBox();
        private TextBox membershipTitle = new TextBox();
        private TextBox fbTitle = new TextBox();
        private TextBox productTitle = new TextBox();

        SqlConnection con = new SqlConnection("Data Source=AJ\\SQLEXPRESS01;Initial Catalog=gymink;Integrated Security=True");
        public FormPaymentBoard()
        {
            InitializeComponent();
        }

        private void FormPaymentBoard_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private string GetTotalAll()
        {
            con.Open();
            SqlCommand loadTotal = new SqlCommand("SELECT SUM(Amount) as Total from Payment", con);
            SqlDataReader dr = loadTotal.ExecuteReader();
            decimal total = 0;
            while (dr.Read())
            {
                if (!(dr["Total"] is DBNull))
                {
                    total = Convert.ToDecimal(dr["Total"]);
                }
            }
            con.Close();
            return total.ToString();
        }

        private string GetTotalMembership()
        {
            con.Open();
            SqlCommand loadTotal = new SqlCommand("SELECT SUM(Amount) as Total from Payment where Description = 'Membership'", con);
            SqlDataReader dr = loadTotal.ExecuteReader();
            decimal total = 0;
            while (dr.Read())
            {
                if (!(dr["Total"] is DBNull))
                {
                    total = Convert.ToDecimal(dr["Total"]);
                }
            }
            con.Close();
            return total.ToString();
        }

        private string GetTotalFb()
        {
            con.Open();
            SqlCommand loadTotal = new SqlCommand("SELECT SUM(Amount) as Total from Payment where Description = 'F&B'", con);
            SqlDataReader dr = loadTotal.ExecuteReader();
            decimal total = 0;
            while (dr.Read())
            {
                if (!(dr["Total"] is DBNull))
                {
                total = Convert.ToDecimal(dr["Total"]);
                }
            }
            con.Close();
            return total.ToString();
        }

        private string GetTotalProduct()
        {
            con.Open();
            SqlCommand loadTotal = new SqlCommand("SELECT SUM(Amount) as Total from Payment where Description = 'Product'", con);
            SqlDataReader dr = loadTotal.ExecuteReader();
            decimal total = 0;
            while (dr.Read())
            {
                if (!(dr["Total"] is DBNull))
                {
                    total = Convert.ToDecimal(dr["Total"]);
                }
            }
            con.Close();
            return total.ToString();
        }

        private void GetData()
        {
            //total card
            revenueCard = new PictureBox();
            revenueCard.Width = this.Width - 120;
            revenueCard.Height = 175;
            revenueCard.Margin = new Padding(27, 20, 0, 20);
            revenueCard.BackColor = Color.FromArgb(166, 142, 174);
            revenueCard.ForeColor = Color.White;
            Rectangle r = new Rectangle(0, 0, revenueCard.Width, revenueCard.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 15;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            revenueCard.Region = new Region(gp);

            //total text
            revenueTitle = new TextBox();
            revenueTitle.Text = "Total Revenue";
            revenueTitle.TextAlign = HorizontalAlignment.Center;
            revenueTitle.BackColor = revenueCard.BackColor;
            revenueTitle.BorderStyle = BorderStyle.None;
            revenueTitle.Width = revenueCard.Width;
            FontFamily fontFamily = new FontFamily("Times New Roman");
            revenueTitle.Font = new Font(fontFamily, 28, FontStyle.Bold);
            revenueTitle.ForeColor = Color.White;
            int xspec = (revenueCard.Width / 2) - (revenueTitle.Width / 2);
            revenueTitle.Location = new Point(xspec, 30);

            //total number
            revenueNumber = new TextBox();
            revenueNumber.Text = "$ " + GetTotalAll();
            revenueNumber.TextAlign = HorizontalAlignment.Center;
            revenueNumber.BackColor = revenueCard.BackColor;
            revenueNumber.BorderStyle = BorderStyle.None;
            revenueNumber.Width = revenueCard.Width;
            FontFamily fontFamily2 = new FontFamily("Times New Roman");
            revenueNumber.Font = new Font(fontFamily, 47, FontStyle.Bold | FontStyle.Italic);
            revenueNumber.ForeColor = Color.White;
            int xnum = (revenueCard.Width / 2) - (revenueNumber.Width / 2);
            revenueNumber.Location = new Point(xnum, 78);


            //membership card
            membershipCard = new PictureBox();
            membershipCard.Width = 320;
            membershipCard.Height = 380;
            membershipCard.Margin = new Padding(27, 20, 0, 0);
            membershipCard.BackColor = Color.FromArgb(54, 165, 208);
            membershipCard.ForeColor = Color.White;
            Rectangle r2 = new Rectangle(0, 0, membershipCard.Width, membershipCard.Height);
            System.Drawing.Drawing2D.GraphicsPath gp2 = new System.Drawing.Drawing2D.GraphicsPath();
            int d2 = 15;
            gp2.AddArc(r2.X, r2.Y, d2, d2, 180, 90);
            gp2.AddArc(r2.X + r2.Width - d2, r2.Y, d2, d2, 270, 90);
            gp2.AddArc(r2.X + r2.Width - d2, r2.Y + r2.Height - d2, d2, d2, 0, 90);
            gp2.AddArc(r2.X, r2.Y + r2.Height - d2, d2, d2, 90, 90);
            membershipCard.Region = new Region(gp2);

            //membership title
            membershipTitle = new TextBox();
            membershipTitle.Text = "Membership";
            membershipTitle.TextAlign = HorizontalAlignment.Center;
            membershipTitle.BackColor = membershipCard.BackColor;
            membershipTitle.BorderStyle = BorderStyle.None;
            membershipTitle.Width = membershipCard.Width;
            membershipTitle.Font = new Font(fontFamily, 30, FontStyle.Bold);
            membershipTitle.ForeColor = Color.White;
            int xbtitle = (membershipCard.Width / 2) - (membershipTitle.Width / 2);
            membershipTitle.Location = new Point(xbtitle, 40);

            //membership logo
            membershipLogo = new IconPictureBox();
            membershipLogo.IconChar = IconChar.AddressCard;
            membershipLogo.ForeColor = Color.Black;
            membershipLogo.Width = 150;
            membershipLogo.Height = 150;
            int xblogo = (membershipCard.Width / 2) - (membershipLogo.Width / 2) + 5;
            membershipLogo.Location = new Point(xblogo, 110);

            //membership number
            membershipNumber = new TextBox();
            membershipNumber.Text = "$ " + GetTotalMembership();
            membershipNumber.TextAlign = HorizontalAlignment.Center;
            membershipNumber.BackColor = membershipCard.BackColor;
            membershipNumber.BorderStyle = BorderStyle.None;
            membershipNumber.Width = membershipCard.Width;
            membershipNumber.Font = new Font(fontFamily, 38, FontStyle.Bold | FontStyle.Italic);
            membershipNumber.ForeColor = Color.White;
            int xbnumber = (membershipCard.Width / 2) - (membershipNumber.Width / 2);
            membershipNumber.Location = new Point(xbnumber, 270);

            //fb card
            fbCard = new PictureBox();
            fbCard.Width = 320;
            fbCard.Height = 380;
            fbCard.Margin = new Padding(37, 20, 0, 0);
            fbCard.BackColor = Color.FromArgb(230, 165, 193);
            fbCard.ForeColor = Color.White;
            Rectangle r3 = new Rectangle(0, 0, fbCard.Width, fbCard.Height);
            System.Drawing.Drawing2D.GraphicsPath gp3 = new System.Drawing.Drawing2D.GraphicsPath();
            int d3 = 15;
            gp3.AddArc(r3.X, r3.Y, d3, d3, 180, 90);
            gp3.AddArc(r3.X + r3.Width - d3, r3.Y, d3, d3, 270, 90);
            gp3.AddArc(r3.X + r3.Width - d3, r3.Y + r3.Height - d3, d3, d3, 0, 90);
            gp3.AddArc(r3.X, r3.Y + r3.Height - d3, d3, d3, 90, 90);
            fbCard.Region = new Region(gp3);

            //fb title
            fbTitle = new TextBox();
            fbTitle.Text = "F & B";
            fbTitle.TextAlign = HorizontalAlignment.Center;
            fbTitle.BackColor = fbCard.BackColor;
            fbTitle.BorderStyle = BorderStyle.None;
            fbTitle.Width = fbCard.Width;
            fbTitle.Font = new Font(fontFamily, 30, FontStyle.Bold);
            fbTitle.ForeColor = Color.White;
            int xstitle = (fbCard.Width / 2) - (fbTitle.Width / 2);
            fbTitle.Location = new Point(xstitle, 40);

            //fb logo
            fbLogo = new IconPictureBox();
            fbLogo.IconChar = IconChar.Utensils;
            fbLogo.ForeColor = Color.Black;
            fbLogo.Width = 150;
            fbLogo.Height = 150;
            int xslogo = (fbCard.Width / 2) - (fbLogo.Width / 2) + 5;
            fbLogo.Location = new Point(xslogo, 110);

            //fb number
            fbNumber = new TextBox();
            fbNumber.Text = "$ " + GetTotalFb();
            fbNumber.TextAlign = HorizontalAlignment.Center;
            fbNumber.BackColor = fbCard.BackColor;
            fbNumber.BorderStyle = BorderStyle.None;
            fbNumber.Width = fbCard.Width;
            fbNumber.Font = new Font(fontFamily, 42, FontStyle.Bold | FontStyle.Italic);
            fbNumber.ForeColor = Color.White;
            int xsnumber = (fbCard.Width / 2) - (fbNumber.Width / 2);
            fbNumber.Location = new Point(xsnumber, 270);

            //product card
            productCard = new PictureBox();
            productCard.Width = 320;
            productCard.Height = 380;
            productCard.Margin = new Padding(37, 20, 0, 0);
            productCard.BackColor = Color.FromArgb(244, 182, 37);
            productCard.ForeColor = Color.White;
            Rectangle r4 = new Rectangle(0, 0, productCard.Width, productCard.Height);
            System.Drawing.Drawing2D.GraphicsPath gp4 = new System.Drawing.Drawing2D.GraphicsPath();
            int d4 = 15;
            gp4.AddArc(r4.X, r4.Y, d4, d4, 180, 90);
            gp4.AddArc(r4.X + r4.Width - d4, r4.Y, d4, d4, 270, 90);
            gp4.AddArc(r4.X + r4.Width - d4, r4.Y + r4.Height - d4, d4, d4, 0, 90);
            gp4.AddArc(r4.X, r4.Y + r4.Height - d4, d4, d4, 90, 90);
            productCard.Region = new Region(gp4);

            //product title
            productTitle = new TextBox();
            productTitle.Text = "Products";
            productTitle.TextAlign = HorizontalAlignment.Center;
            productTitle.BackColor = productCard.BackColor;
            productTitle.BorderStyle = BorderStyle.None;
            productTitle.Width = productCard.Width;
            productTitle.Font = new Font(fontFamily, 30, FontStyle.Bold);
            productTitle.ForeColor = Color.White;
            int xptitle = (productCard.Width / 2) - (productTitle.Width / 2);
            productTitle.Location = new Point(xptitle, 40);

            //product logo
            productLogo = new IconPictureBox();
            productLogo.IconChar = IconChar.BoxesStacked;
            productLogo.ForeColor = Color.Black;
            productLogo.Width = 150;
            productLogo.Height = 150;
            int xplogo = (productCard.Width / 2) - (productLogo.Width / 2) + 5;
            productLogo.Location = new Point(xplogo, 110);

            //product number
            productNumber = new TextBox();
            productNumber.Text = "$ " + GetTotalProduct();
            productNumber.TextAlign = HorizontalAlignment.Center;
            productNumber.BackColor = productCard.BackColor;
            productNumber.BorderStyle = BorderStyle.None;
            productNumber.Width = productCard.Width;
            productNumber.Font = new Font(fontFamily, 42, FontStyle.Bold | FontStyle.Italic);
            productNumber.ForeColor = Color.White;
            int xpnumber = (productCard.Width / 2) - (productNumber.Width / 2);
            productNumber.Location = new Point(xpnumber, 270);

            flowLayoutPanel1.Controls.Add(revenueCard);
            revenueCard.Controls.Add(revenueTitle);
            revenueCard.Controls.Add(revenueNumber);
            flowLayoutPanel1.Controls.Add(membershipCard);
            membershipCard.Controls.Add(membershipTitle);
            membershipCard.Controls.Add(membershipLogo);
            membershipCard.Controls.Add(membershipNumber);
            flowLayoutPanel1.Controls.Add(fbCard);
            fbCard.Controls.Add(fbTitle);
            fbCard.Controls.Add(fbLogo);
            fbCard.Controls.Add(fbNumber);
            flowLayoutPanel1.Controls.Add(productCard);
            productCard.Controls.Add(productTitle);
            productCard.Controls.Add(productLogo);
            productCard.Controls.Add(productNumber);
        }
    }
}
