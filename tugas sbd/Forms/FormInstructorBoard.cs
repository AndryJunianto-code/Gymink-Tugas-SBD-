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
using System.IO;
using System.Drawing;
using System.Windows.Input;

namespace tugas_sbd.Forms
{
    public partial class FormInstructorBoard : Form
    {
        SqlConnection con = new SqlConnection("Data Source=AJ\\SQLEXPRESS01;Initial Catalog=gymink;Integrated Security=True");
        private PictureBox card = new PictureBox();
        private PictureBox pic = new PictureBox();
        private TextBox insName = new TextBox();
        private TextBox specialty = new TextBox();
        private Button updateBtn = new Button();

        public FormInstructorBoard()
        {
            InitializeComponent();
        }


        private void FormInstructorBoard_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            con.Open();
            SqlCommand loadData = new SqlCommand("Select Image,Fname,Lname,Specialty,Status from Instructor", con);
            SqlDataReader dr = loadData.ExecuteReader();
            List<Color> cardColors = new List<Color>()
                {
                    Color.FromArgb(114,159,157), Color.FromArgb(136,199,176)
                };
            int colorIndex = 0;
            int colorMod = 1;
            while (dr.Read())
            {
                long len = dr.GetBytes(0, 0, null, 0, 0);
                byte[] array = new byte[System.Convert.ToInt32(len)+1];
                dr.GetBytes(0, 0, array, 0, System.Convert.ToInt32(len));
                card = new PictureBox();

              

                card.Width = 330;
                card.Height = 330;
                card.BackColor = cardColors[colorIndex];
                card.Margin = new Padding(0, 0, 35, 30);
                Rectangle r = new Rectangle(0, 0, card.Width, card.Height);
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                int d = 25;
                gp.AddArc(r.X, r.Y, d, d, 180, 90);
                gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
                gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
                gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
                card.Region = new Region(gp);

                //image
                pic = new PictureBox(); 
                pic.Width = 150;
                pic.Height = 160;
                Rectangle r2 = new Rectangle(0, 0, pic.Width, pic.Height);
                System.Drawing.Drawing2D.GraphicsPath gp2 = new System.Drawing.Drawing2D.GraphicsPath();
                int d2 = 15;
                gp2.AddArc(r2.X, r2.Y, d2, d2, 180, 90);
                gp2.AddArc(r2.X + r2.Width - d2, r2.Y, d2, d2, 270, 90);
                gp2.AddArc(r2.X + r2.Width - d2, r2.Y + r2.Height - d2, d2, d2, 0, 90);
                gp2.AddArc(r2.X, r2.Y + r2.Height - d2, d2, d2, 90, 90);
                pic.Region = new Region(gp2);
                MemoryStream ms = new MemoryStream(array);
                Bitmap bitmap = new Bitmap(ms);
                pic.BackgroundImageLayout = ImageLayout.Stretch;
                int xpic = (card.Width / 2) - (pic.Width / 2);
                pic.Location = new Point(xpic, 20);
                pic.BackgroundImage = bitmap;

                //name
                insName = new TextBox();
                insName.Text = dr["Fname"].ToString() + " " + dr["Lname"].ToString();
                insName.TextAlign = HorizontalAlignment.Center;
                insName.BackColor = cardColors[colorIndex];
                insName.BorderStyle = BorderStyle.None;
                insName.Width = card.Width;
                insName.Font = new Font(insName.Font.FontFamily, 14);
                insName.ForeColor = Color.White;
                int xname = (card.Width / 2) - (insName.Width / 2);
                insName.Location = new Point(xname, 188);

                //specialist
                specialty = new TextBox();
                specialty.Text = dr["Specialty"].ToString();
                specialty.TextAlign = HorizontalAlignment.Center;
                specialty.BackColor = cardColors[colorIndex];
                specialty.BorderStyle = BorderStyle.None;
                specialty.Width = card.Width;
                FontFamily fontFamily = new FontFamily("Times New Roman");
                specialty.Font = new Font(fontFamily, 28,FontStyle.Bold);
                specialty.ForeColor = Color.White; 
                int xspec = (card.Width/2) - (specialty.Width / 2);
                specialty.Location = new Point(xspec, 215);

                //button
                updateBtn = new Button();
                updateBtn.Text = dr["Status"].ToString();
                updateBtn.Visible = true;
                updateBtn.FlatStyle = FlatStyle.Flat;
                updateBtn.FlatAppearance.BorderSize = 0;
                updateBtn.BackColor = Color.FromArgb(255, 230, 137);
                updateBtn.Width = 80;
                updateBtn.Height = 30;
                int xupdate = (card.Width / 2) - (updateBtn.Width / 2);
                updateBtn.Location = new Point(xupdate, 270);
               
                if (colorMod % 3 == 0 )
                {
                    if(colorIndex == 1)
                    {
                        colorIndex = 0;
                        
                    } else
                    {
                    colorIndex = 1;
                    }
                }
                colorMod++;

                card.Controls.Add(pic);
                card.Controls.Add(insName);
                card.Controls.Add(specialty);
                card.Controls.Add(updateBtn);
                flowLayoutPanel1.Controls.Add(card);
            }
            dr.Close();
            con.Close();
        }
        

    }
}
