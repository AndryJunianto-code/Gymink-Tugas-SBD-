using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tugas_sbd.Forms
{

    public class Item
    {
        public int Class_id { get; set; }
        public int Total { get; set; } = 0;
    }
    public partial class FormClassBoard : Form
    {
        SqlConnection con = new SqlConnection("Data Source=AJ\\SQLEXPRESS01;Initial Catalog=gymink;Integrated Security=True;MultipleActiveResultSets=true;");
        private PictureBox card = new PictureBox();
        private TextBox className = new TextBox();
        private TextBox insName = new TextBox();
        private TextBox schedule = new TextBox();
        private PictureBox pic = new PictureBox();
        private TextBox size = new TextBox();
        private TextBox slots = new TextBox();
        public FormClassBoard()
        {
            InitializeComponent();
        }
     

            private void FormClassBoard_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private List<Item> GetTotal()
        {
            SqlCommand loadTotal = new SqlCommand("SELECT DISTINCT Class_id, COUNT (DISTINCT Client_id) as 'Total' from Enrolls_In GROUP BY Class_id;", con);
            SqlDataReader dr = loadTotal.ExecuteReader();
            List<Item> items = new List<Item>();
            while(dr.Read())
            {
                var type = typeof(Item);
                Item obj = (Item)Activator.CreateInstance(type);
                foreach(var prop in type.GetProperties())
                {
                    var propType = prop.PropertyType;
                    prop.SetValue(obj, Convert.ChangeType(dr[prop.Name].ToString(), propType));
                }
                items.Add(obj);
            }
            return items;
        }
        private void GetData()
        {
            try
            {

                con.Open();
                using (SqlCommand loadData = new SqlCommand("Select Class_image,Class_name,Size,Schedule,Instructor.Fname,Instructor.Lname,Class_id from Class JOIN Instructor ON Class.Instructor_id = Instructor.Instructor_id GROUP BY Class_image,Class_name,Size,Schedule,Instructor.Fname,Instructor.Lname,Class_id", con))
                {
                    SqlDataReader dr = loadData.ExecuteReader();
                    List<Color> cardColors = new List<Color>()
                {
                    Color.FromArgb(54,165,207), Color.FromArgb(230,166,193),Color.FromArgb(245,182,38),Color.FromArgb(255,123,123),Color.FromArgb(170,178,248),Color.FromArgb(155,205,123),
                };
                    int colorIndex = 0;
                    while (dr.Read())
                    {

                        List<Item> members = GetTotal();
                        long len = dr.GetBytes(0, 0, null, 0, 0);
                        byte[] array = new byte[Convert.ToInt32(len) + 1];
                        dr.GetBytes(0, 0, array, 0, Convert.ToInt32(len));

                        card = new PictureBox();
                        card.Width = 1000;
                        card.Height = 230;
                        card.BackColor = cardColors[colorIndex];
                        card.Margin = new Padding(30, 10, 0, 20);
                        Rectangle r = new Rectangle(0, 0, card.Width, card.Height);
                        System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                        int d = 25;
                        gp.AddArc(r.X, r.Y, d, d, 180, 90);
                        gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
                        gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
                        gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
                        card.Region = new Region(gp);

                        //class name
                        className = new TextBox();
                        className.Text = dr["Class_name"].ToString();
                        className.BackColor = card.BackColor;
                        className.BorderStyle = BorderStyle.None;
                        className.Width = card.Width / 3;
                        FontFamily fontFamily = new FontFamily("Times New Roman");
                        className.Font = new Font(fontFamily, 32, FontStyle.Bold);
                        className.ForeColor = Color.White;
                        className.Location = new Point(30, 20);

                        //ins name
                        insName = new TextBox();
                        insName.Text = "with " + dr["Fname"].ToString() + " " + dr["Lname"].ToString();
                        insName.BackColor = card.BackColor;
                        insName.BorderStyle = BorderStyle.None;
                        insName.Width = card.Width / 3;
                        insName.Font = new Font(insName.Font.FontFamily, 14, FontStyle.Italic);
                        insName.ForeColor = Color.FromArgb(233, 233, 233);
                        insName.Location = new Point(30, 70);

                        //schedule
                        schedule = new TextBox();
                        schedule.Text = dr["Schedule"].ToString();
                        schedule.BackColor = card.BackColor;
                        schedule.BorderStyle = BorderStyle.None;
                        schedule.Width = card.Width / 2 - 50;
                        schedule.Font = new Font(schedule.Font.FontFamily, 26, FontStyle.Bold);
                        schedule.ForeColor = Color.White;
                        schedule.Location = new Point(30, 115);

                        //picture
                        pic = new PictureBox();
                        pic.Width = card.Width / 2;
                        pic.Height = card.Height - 20;
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
                        pic.Location = new Point(card.Width - pic.Width - 10, 10);
                        pic.BackgroundImage = bitmap;

                        //size slots
                        slots = new TextBox();
                        slots.Text = "Slots";
                        slots.ForeColor = Color.FromArgb(255, 230, 137);
                        slots.Font = new Font(size.Font.FontFamily, 14,FontStyle.Bold);
                        slots.BackColor = card.BackColor;
                        slots.BorderStyle = BorderStyle.None;
                        slots.Location = new Point(30, 165);
                        slots.Width = 45;
                        size = new TextBox();
                        int memberjoined = 0;
                        foreach(Item member in members)
                        {
                            if(member.Class_id == Convert.ToInt32(dr["Class_id"]))
                            {
                                memberjoined = member.Total;
                            }
                        }
                        size.Text = memberjoined.ToString() + " / " + dr["Size"];
                        size.BackColor = card.BackColor;
                        size.BorderStyle = BorderStyle.None;
                        size.TextAlign = HorizontalAlignment.Center;
                        size.Width = 60;
                        size.Font = new Font(size.Font.FontFamily, 14);
                        size.ForeColor = Color.Black;
                        size.Location = new Point(90, 165);

                        colorIndex++;
                        if(colorIndex > 5)
                        {
                            colorIndex = 0;
                        }
                        card.Controls.Add(className);
                        card.Controls.Add(insName);
                        card.Controls.Add(schedule);
                        card.Controls.Add(pic);
                        card.Controls.Add(slots);
                        card.Controls.Add(size);
                        flowLayoutPanel1.Controls.Add(card);
                    }
                }
                
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } finally
            {
                con.Close();
            }
        }
    }
}
