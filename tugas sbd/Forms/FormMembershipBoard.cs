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
    public class Profile
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int Age { get; set; }
        public String Sex { get; set; }
        public String Date_joined { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
    }
    public partial class FormMembershipBoard : Form
    {
        SqlConnection con = new SqlConnection("Data Source=AJ\\SQLEXPRESS01;Initial Catalog=gymink;Integrated Security=True");
        private PictureBox totalCard = new PictureBox();
        private TextBox totalText = new TextBox();
        private TextBox totalNumber = new TextBox();
        private PictureBox bronzeCard = new PictureBox();
        private PictureBox silverCard = new PictureBox();
        private PictureBox goldCard = new PictureBox();
        private PictureBox platinumCard = new PictureBox();
        private TextBox bronzeTitle = new TextBox();
        private TextBox silverTitle = new TextBox();
        private TextBox goldTitle = new TextBox();
        private TextBox platinumTitle = new TextBox();
        private IconPictureBox bronzeLogo = new IconPictureBox();
        private IconPictureBox silverLogo = new IconPictureBox();
        private IconPictureBox goldLogo = new IconPictureBox();
        private IconPictureBox platinumLogo = new IconPictureBox();
        private TextBox bronzeNumber = new TextBox();
        private TextBox silverNumber = new TextBox();
        private TextBox goldNumber = new TextBox();
        private TextBox platinumNumber = new TextBox();
        public FormMembershipBoard()
        {
            InitializeComponent();
        }

        private void FormMembershipBoard_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private string GetTotal(int duration)
        {
            con.Open();
            SqlCommand loadTotal = new SqlCommand("select Count(Membership_id) as Total from Membership where Duration = '" + duration + "';", con);
            SqlDataReader dr = loadTotal.ExecuteReader();
            int total = 0;
            while(dr.Read())
            {
                total = Convert.ToInt32(dr["Total"]);
            }
            con.Close();
            return total.ToString();
        }

        private string GetAll()
        {
            con.Open();
            SqlCommand loadTotal = new SqlCommand("select Count(Membership_id) as Total from Membership", con);
            SqlDataReader dr = loadTotal.ExecuteReader();
            int total = 0;
            while (dr.Read())
            {
                total = Convert.ToInt32(dr["Total"]);
            }
            con.Close();
            return total.ToString();
        }
        private void GetData()
        {
            
            //total card
                totalCard = new PictureBox();
                totalCard.Width = this.Width - 120;
                totalCard.Height = 175;
                totalCard.Margin = new Padding(27, 20, 0, 20);
                totalCard.BackColor = Color.FromArgb(219, 133, 146);
                totalCard.ForeColor = Color.White;
                Rectangle r = new Rectangle(0, 0, totalCard.Width, totalCard.Height);
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                int d = 15;
                gp.AddArc(r.X, r.Y, d, d, 180, 90);
                gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
                gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
                gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
                totalCard.Region = new Region(gp);

            //total text
            totalText = new TextBox();
            totalText.Text = "Total Membership";
            totalText.TextAlign = HorizontalAlignment.Center;
            totalText.BackColor = totalCard.BackColor;
            totalText.BorderStyle = BorderStyle.None;
            totalText.Width = totalCard.Width;
            FontFamily fontFamily = new FontFamily("Times New Roman");
            totalText.Font = new Font(fontFamily, 28, FontStyle.Bold);
            totalText.ForeColor = Color.White;
            int xspec = (totalCard.Width / 2) - (totalText.Width / 2);
            totalText.Location = new Point(xspec, 30);

            //total number
            totalNumber = new TextBox();
            totalNumber.Text = GetAll();
            totalNumber.TextAlign = HorizontalAlignment.Center;
            totalNumber.BackColor = totalCard.BackColor;
            totalNumber.BorderStyle = BorderStyle.None;
            totalNumber.Width = totalCard.Width;
            FontFamily fontFamily2 = new FontFamily("Times New Roman");
            totalNumber.Font = new Font(fontFamily, 47, FontStyle.Bold | FontStyle.Italic);
            totalNumber.ForeColor = Color.White;
            int xnum = (totalCard.Width / 2) - (totalNumber.Width / 2);
            totalNumber.Location = new Point(xnum, 78);

            //bronze card
            bronzeCard = new PictureBox();
            bronzeCard.Width = 230;
            bronzeCard.Height = 380;
            bronzeCard.Margin = new Padding(27, 20, 0, 0);
            bronzeCard.BackColor = Color.FromArgb(255, 145, 76);
            bronzeCard.ForeColor = Color.White;
            Rectangle r2 = new Rectangle(0, 0, bronzeCard.Width, bronzeCard.Height);
            System.Drawing.Drawing2D.GraphicsPath gp2 = new System.Drawing.Drawing2D.GraphicsPath();
            int d2 = 15;
            gp2.AddArc(r2.X, r2.Y, d2, d2, 180, 90);
            gp2.AddArc(r2.X + r2.Width - d2, r2.Y, d2, d2, 270, 90);
            gp2.AddArc(r2.X + r2.Width - d2, r2.Y + r2.Height - d2, d2, d2, 0, 90);
            gp2.AddArc(r2.X, r2.Y + r2.Height - d2, d2, d2, 90, 90);
            bronzeCard.Region = new Region(gp2);

            //bronze title
            bronzeTitle = new TextBox();
            bronzeTitle.Text = "1 month";
            bronzeTitle.TextAlign = HorizontalAlignment.Center;
            bronzeTitle.BackColor = bronzeCard.BackColor;
            bronzeTitle.BorderStyle = BorderStyle.None;
            bronzeTitle.Width = bronzeCard.Width;
            bronzeTitle.Font = new Font(fontFamily, 30, FontStyle.Bold);
            bronzeTitle.ForeColor = Color.White;
            int xbtitle = (bronzeCard.Width / 2) - (bronzeTitle.Width / 2);
            bronzeTitle.Location = new Point(xbtitle, 40);

            //bronze logo
            bronzeLogo = new IconPictureBox();
            bronzeLogo.IconChar = IconChar.ShieldCat;
            bronzeLogo.ForeColor = Color.Black;
            bronzeLogo.Width = 150;
            bronzeLogo.Height = 150;
            int xblogo = (bronzeCard.Width / 2) - (bronzeLogo.Width / 2) + 5;
            bronzeLogo.Location = new Point(xblogo, 110);

            //bronze number
            bronzeNumber = new TextBox();
            bronzeNumber.Text = GetTotal(1);
            bronzeNumber.TextAlign = HorizontalAlignment.Center;
            bronzeNumber.BackColor = bronzeCard.BackColor;
            bronzeNumber.BorderStyle = BorderStyle.None;
            bronzeNumber.Width = totalCard.Width;
            bronzeNumber.Font = new Font(fontFamily, 42, FontStyle.Bold | FontStyle.Italic);
            bronzeNumber.ForeColor = Color.White;
            int xbnumber = (bronzeCard.Width / 2) - (bronzeNumber.Width / 2);
            bronzeNumber.Location = new Point(xbnumber, 270);


            //silver card
            silverCard = new PictureBox();
            silverCard.Width = 230;
            silverCard.Height = 380;
            silverCard.Margin = new Padding(37, 20, 0, 0);
            silverCard.BackColor = Color.FromArgb(48, 200, 214);
            silverCard.ForeColor = Color.White;
            Rectangle r3 = new Rectangle(0, 0, silverCard.Width, silverCard.Height);
            System.Drawing.Drawing2D.GraphicsPath gp3 = new System.Drawing.Drawing2D.GraphicsPath();
            int d3 = 15;
            gp3.AddArc(r3.X, r3.Y, d3, d3, 180, 90);
            gp3.AddArc(r3.X + r3.Width - d3, r3.Y, d3, d3, 270, 90);
            gp3.AddArc(r3.X + r3.Width - d3, r3.Y + r3.Height - d3, d3, d3, 0, 90);
            gp3.AddArc(r3.X, r3.Y + r3.Height - d3, d3, d3, 90, 90);
            silverCard.Region = new Region(gp3);

            //silver title
            silverTitle = new TextBox();
            silverTitle.Text = "3 months";
            silverTitle.TextAlign = HorizontalAlignment.Center;
            silverTitle.BackColor = silverCard.BackColor;
            silverTitle.BorderStyle = BorderStyle.None;
            silverTitle.Width = silverCard.Width;
            silverTitle.Font = new Font(fontFamily, 30, FontStyle.Bold);
            silverTitle.ForeColor = Color.White;
            int xstitle = (silverCard.Width / 2) - (silverTitle.Width / 2);
            silverTitle.Location = new Point(xstitle, 40);

            //silver logo
            silverLogo = new IconPictureBox();
            silverLogo.IconChar = IconChar.Jedi;
            silverLogo.ForeColor = Color.Black;
            silverLogo.Width = 150;
            silverLogo.Height = 150;
            int xslogo = (silverCard.Width / 2) - (silverLogo.Width / 2) + 5;
            silverLogo.Location = new Point(xslogo, 110);

            //silver number
            silverNumber = new TextBox();
            silverNumber.Text = GetTotal(3);
            silverNumber.TextAlign = HorizontalAlignment.Center;
            silverNumber.BackColor = silverCard.BackColor;
            silverNumber.BorderStyle = BorderStyle.None;
            silverNumber.Width = silverCard.Width;
            silverNumber.Font = new Font(fontFamily, 42, FontStyle.Bold | FontStyle.Italic);
            silverNumber.ForeColor = Color.White;
            int xsnumber = (silverCard.Width / 2) - (silverNumber.Width / 2);
            silverNumber.Location = new Point(xsnumber, 270);

            //gold card
            goldCard = new PictureBox();
            goldCard.Width = 230;
            goldCard.Height = 380;
            goldCard.Margin = new Padding(37, 20, 0, 0);
            goldCard.BackColor = Color.FromArgb(225, 198, 151);
            goldCard.ForeColor = Color.White;
            Rectangle r4 = new Rectangle(0, 0, goldCard.Width, goldCard.Height);
            System.Drawing.Drawing2D.GraphicsPath gp4= new System.Drawing.Drawing2D.GraphicsPath();
            int d4 = 15;
            gp4.AddArc(r4.X, r4.Y, d4,3, 180, 90);
            gp4.AddArc(r4.X + r4.Width - d4, r4.Y, d4, d4, 270, 90);
            gp4.AddArc(r4.X + r4.Width - d4, r4.Y + r4.Height - d3, d3, d3, 0, 90);
            gp4.AddArc(r4.X, r4.Y + r4.Height - d4, d4, d4, 90, 90);
            goldCard.Region = new Region(gp4);

            //gold title
            goldTitle = new TextBox();
            goldTitle.Text = "6 months";
            goldTitle.TextAlign = HorizontalAlignment.Center;
            goldTitle.BackColor = goldCard.BackColor;
            goldTitle.BorderStyle = BorderStyle.None;
            goldTitle.Width = goldCard.Width;
            goldTitle.Font = new Font(fontFamily, 30, FontStyle.Bold);
            goldTitle.ForeColor = Color.White;
            int xgtitle = (goldCard.Width / 2) - (goldTitle.Width / 2);
            goldTitle.Location = new Point(xgtitle, 40);

            //gold logo
            goldLogo = new IconPictureBox();
            goldLogo.IconChar = IconChar.Rocket;
            goldLogo.ForeColor = Color.Black;
            goldLogo.Width = 150;
            goldLogo.Height = 150;
            int xglogo = (goldCard.Width / 2) - (goldLogo.Width / 2) + 5;
            goldLogo.Location = new Point(xglogo, 110);

            //gold number
            goldNumber = new TextBox();
            goldNumber.Text = GetTotal(6);
            goldNumber.TextAlign = HorizontalAlignment.Center;
            goldNumber.BackColor = goldCard.BackColor;
            goldNumber.BorderStyle = BorderStyle.None;
            goldNumber.Width = goldCard.Width;
            goldNumber.Font = new Font(fontFamily, 42, FontStyle.Bold | FontStyle.Italic);
            goldNumber.ForeColor = Color.White;
            int xgnumber = (goldCard.Width / 2) - (goldNumber.Width / 2) -5 ;
            goldNumber.Location = new Point(xgnumber, 275);

            //platinum card
            platinumCard = new PictureBox();
            platinumCard.Width = 230;
            platinumCard.Height = 380;
            platinumCard.Margin = new Padding(37, 20, 0, 0);
            platinumCard.BackColor = Color.FromArgb(101, 101, 111);
            platinumCard.ForeColor = Color.White;
            Rectangle r5 = new Rectangle(0, 0, platinumCard.Width, platinumCard.Height);
            System.Drawing.Drawing2D.GraphicsPath gp5 = new System.Drawing.Drawing2D.GraphicsPath();
            int d5 = 15;
            gp5.AddArc(r5.X, r5.Y, d5, 3, 180, 90);
            gp5.AddArc(r5.X + r5.Width - d5, r5.Y, d5, d5, 270, 90);
            gp5.AddArc(r5.X + r5.Width - d5, r5.Y + r5.Height - d3, d3, d3, 0, 90);
            gp5.AddArc(r5.X, r5.Y + r5.Height - d5, d5, d5, 90, 90);
            platinumCard.Region = new Region(gp5);

            //platinum title
            platinumTitle = new TextBox();
            platinumTitle.Text = "12 months";
            platinumTitle.TextAlign = HorizontalAlignment.Center;
            platinumTitle.BackColor = platinumCard.BackColor;
            platinumTitle.BorderStyle = BorderStyle.None;
            platinumTitle.Width = platinumCard.Width;
            platinumTitle.Font = new Font(fontFamily, 30, FontStyle.Bold);
            platinumTitle.ForeColor = Color.White;
            int xptitle = (platinumCard.Width / 2) - (platinumTitle.Width / 2);
            platinumTitle.Location = new Point(xptitle, 40);

            //platinum logo
            platinumLogo = new IconPictureBox();
            platinumLogo.IconChar = IconChar.Dragon;
            platinumLogo.ForeColor = Color.Black;
            platinumLogo.Width = 150;
            platinumLogo.Height = 150;
            int xplogo = (platinumCard.Width / 2) - (platinumLogo.Width / 2) + 5;
            platinumLogo.Location = new Point(xplogo, 110);

            //platinum number
            platinumNumber = new TextBox();
            platinumNumber.Text = GetTotal(12);
            platinumNumber.TextAlign = HorizontalAlignment.Center;
            platinumNumber.BackColor = platinumCard.BackColor;
            platinumNumber.BorderStyle = BorderStyle.None;
            platinumNumber.Width = platinumCard.Width;
            platinumNumber.Font = new Font(fontFamily, 42, FontStyle.Bold | FontStyle.Italic);
            platinumNumber.ForeColor = Color.White;
            int xpnumber = (platinumCard.Width / 2) - (platinumNumber.Width / 2);
            platinumNumber.Location = new Point(xpnumber, 270);

            flowLayoutPanel1.Controls.Add(totalCard);
            totalCard.Controls.Add(totalText);
            totalCard.Controls.Add(totalNumber);
            flowLayoutPanel1.Controls.Add(bronzeCard);
            flowLayoutPanel1.Controls.Add(silverCard);
            flowLayoutPanel1.Controls.Add(goldCard);
            flowLayoutPanel1.Controls.Add(platinumCard);
            bronzeCard.Controls.Add(bronzeTitle);
            bronzeCard.Controls.Add(bronzeLogo);
            bronzeCard.Controls.Add(bronzeNumber);
            silverCard.Controls.Add(silverTitle);
            silverCard.Controls.Add(silverLogo);
            silverCard.Controls.Add(silverNumber);
            goldCard.Controls.Add(goldTitle);
            goldCard.Controls.Add(goldLogo);
            goldCard.Controls.Add(goldNumber);
            platinumCard.Controls.Add(platinumTitle);
            platinumCard.Controls.Add(platinumLogo);
            platinumCard.Controls.Add(platinumNumber);
        }
    }
}
