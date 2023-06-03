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
    public partial class FormLogin : Form
    {
        public static int SetClientId;
        SqlConnection con = new SqlConnection("Data Source=AJ\\SQLEXPRESS01;Initial Catalog=gymink;Integrated Security=True");
        private PictureBox loginCard = new PictureBox();
        private TextBox title = new TextBox();
        private TextBox input = new TextBox();
        private Button button = new Button();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            //profile card
            loginCard = new PictureBox();
            loginCard.Width = 250;
            loginCard.Height = 350;
            loginCard.BackColor = Color.FromArgb(244, 182, 37);
            int xspec = (this.Width / 2) - (loginCard.Width / 2);
            int yspec = (this.Height / 2) - (loginCard.Height / 2) - 50;
            loginCard.Location = new Point(xspec, yspec);
            Rectangle r = new Rectangle(0, 0, loginCard.Width, loginCard.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 13;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            loginCard.Region = new Region(gp);

            title = new TextBox();
            title.ReadOnly = true;
            title.Text = "Client ID";
            title.TextAlign = HorizontalAlignment.Center;
            title.BackColor = loginCard.BackColor;
            title.BorderStyle = BorderStyle.None;
            title.Width = loginCard.Width;
            FontFamily fontFamily = new FontFamily("Times New Roman");
            title.Font = new Font(fontFamily, 28, FontStyle.Bold);
            title.ForeColor = Color.White;
            title.Location = new Point(0, 25);
            title.TabStop = false;

            input = new TextBox();
            input.TextAlign = HorizontalAlignment.Center;
            input.BackColor = Color.White;
            input.BorderStyle = BorderStyle.None;
            input.Width = loginCard.Width - 30;
            input.Font = new Font(fontFamily, 22, FontStyle.Bold);
            input.ForeColor = Color.FromArgb(244, 182, 37);
            input.Location = new Point(15, 135);
            input.TabStop = true;

            button = new Button();
            button.Text = "Login";
            button.TextAlign = ContentAlignment.TopCenter;
            button.Width = loginCard.Width - 30;
            button.Location = new Point(15, 185);
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font(fontFamily, 10, FontStyle.Bold);
            button.Height = 30;
            button.ForeColor = Color.FromArgb(141, 98, 74);
            button.Click += (s,ev) => HandleLogin();

            this.Controls.Add(loginCard);
            loginCard.Controls.Add(title);
            loginCard.Controls.Add(input);
            loginCard.Controls.Add(button);
        }
        
        private void HandleLogin()
        {
            con.Open();
            SetClientId = Convert.ToInt32(input.Text);
            SqlCommand loadData = new SqlCommand("SELECT * from Client where Client_id= '"+ Convert.ToInt32(input.Text) + "'", con);
            SqlDataReader dr = loadData.ExecuteReader();
            if(dr.HasRows)
            {
                this.Hide();
                FormUser formUser = new FormUser();
                formUser.ShowDialog();
                this.Close();
            } else
            {
                MessageBox.Show("Client does not exist");
            }
            con.Close();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.ShowDialog();
            this.Close();
        }
    }
}
