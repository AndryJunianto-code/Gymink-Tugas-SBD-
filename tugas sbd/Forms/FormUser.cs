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
using FontAwesome.Sharp;
using System.Windows.Media;
using Color = System.Drawing.Color;
using FontFamily = System.Drawing.FontFamily;
using System.Windows;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;
using FontStyle = System.Drawing.FontStyle;
using HorizontalAlignment = System.Windows.Forms.HorizontalAlignment;
using System.Security.RightsManagement;
using FlowDirection = System.Windows.Forms.FlowDirection;
using Size = System.Drawing.Size;
using System.Data.SqlTypes;

namespace tugas_sbd.Forms
{
 
    public partial class FormUser : Form
    {
        SqlConnection con = new SqlConnection("Data Source=AJ\\SQLEXPRESS01;Initial Catalog=gymink;Integrated Security=True");
        private PictureBox profileCard = new PictureBox();
        private TextBox profileTitle = new TextBox();
        private TextBox profileNameTitle = new TextBox();
        private TextBox profileName = new TextBox();
        private TextBox profileAgeTitle = new TextBox();
        private TextBox profileAge = new TextBox();
        private TextBox profileGenderTitle = new TextBox();
        private TextBox profileGender = new TextBox();
        private TextBox profileAddressTitle = new TextBox();
        private TextBox profileAddress = new TextBox();
        private TextBox profilePhoneTitle = new TextBox();
        private TextBox profilePhone = new TextBox();
        private TextBox profileDateTitle = new TextBox();
        private TextBox profileDate = new TextBox();

        private PictureBox greetingCard = new PictureBox();
        private TextBox greetingTitle = new TextBox();
        private TextBox greetingName = new TextBox();
        private TextBox greetingSub = new TextBox();

        private PictureBox memCard = new PictureBox();
        private TextBox memTitle = new TextBox();
        private IconPictureBox memLogo = new IconPictureBox();
        private TextBox memStart = new TextBox();
        private TextBox memEnd = new TextBox();
        private TextBox memDuration = new TextBox();

        private PictureBox totalClassCard = new PictureBox();
        private IconPictureBox totalClassLogo = new IconPictureBox();
        private TextBox totalClassTitle = new TextBox();
        private PictureBox totalClassNumCard = new PictureBox();
        private TextBox totalClassNum = new TextBox();

        private PictureBox totalPaymentCard = new PictureBox();
        private IconPictureBox totalPaymentLogo = new IconPictureBox();
        private TextBox totalPaymentTitle = new TextBox();
        private PictureBox totalPaymentNumCard = new PictureBox();
        private TextBox totalPaymentNum = new TextBox();

        private TextBox payHistoryTitle = new TextBox();
        private FlowLayoutPanel payHistoryCard = new FlowLayoutPanel();
        private PictureBox individualPaymentCard = new PictureBox();
        private IconPictureBox payLogo = new IconPictureBox();
        private TextBox payDescription = new TextBox();
        private TextBox payDate = new TextBox();
        private TextBox payAmount = new TextBox();

        private TextBox classTitle = new TextBox();
        private FlowLayoutPanel classCard = new FlowLayoutPanel();
        private PictureBox individualClassCard = new PictureBox();
        private PictureBox classImage = new PictureBox();
        private TextBox className = new TextBox();
        private TextBox classInsName = new TextBox();
        private TextBox classDate = new TextBox();
        private TextBox classRoomName = new TextBox();
        private TextBox classRoomLoc = new TextBox();
        public FormUser()
        {
            InitializeComponent();
        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            GetProfileData();
            GetMembershipData();
            GetTotalClassData();
            GetTotalPaymentData();
            GetPaymentHistoryData();
            GetProfile();
            GetGreeting();
            GetMembership();
            GetTotalClass();
            GetTotalPayment();
            GetPaymentHistory();
            GetClass();
        }

        private List<User> GetProfileData()
        {
            try
            {
                con.Open();
                SqlCommand loadData = new SqlCommand("SELECT * from Client where Client_id= '" + FormLogin.SetClientId + "'", con);
                SqlDataReader dr = loadData.ExecuteReader();
                List<User> user = new List<User>();
                while (dr.Read())
                {
                    var type = typeof(User);
                    User obj = (User)Activator.CreateInstance(type);
                    foreach (var prop in type.GetProperties())
                    {
                        var propType = prop.PropertyType;
                        prop.SetValue(obj, Convert.ChangeType(dr[prop.Name].ToString(), propType));
                    }
                    user.Add(obj);
                }
                return user;
                con.Close();
            } catch(Exception err)
            {
                MessageBox.Show(err.ToString());
                return null;
            } finally
            {
                con.Close();
            }
        }
        private List<Membership> GetMembershipData()
        {
            try
            {
                con.Open();
                SqlCommand loadData = new SqlCommand("SELECT * from Membership where Client_id= '" + FormLogin.SetClientId + "'", con);
                SqlDataReader dr = loadData.ExecuteReader();
                List<Membership> member = new List<Membership>();
                while (dr.Read())
                {
                    var type = typeof(Membership);
                    Membership obj = (Membership)Activator.CreateInstance(type);
                    foreach (var prop in type.GetProperties())
                    {
                        var propType = prop.PropertyType;
                        prop.SetValue(obj, Convert.ChangeType(dr[prop.Name].ToString(), propType));
                    }
                    member.Add(obj);
                }
                return member;
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        string totalClass = "0";
        private string GetTotalClassData()
        {
            try
            {
                con.Open();
                SqlCommand loadData = new SqlCommand("select COUNT(Client_id) as 'Total' from Enrolls_In where Client_id = '" + FormLogin.SetClientId + "'", con);
                SqlDataReader dr = loadData.ExecuteReader();
                while (dr.Read())
                {
                    totalClass = dr["Total"].ToString();
                }
                return totalClass;
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        string totalPayment = "0";
        private string GetTotalPaymentData()
        {
            try
            {
                con.Open();
                SqlCommand loadData = new SqlCommand("select Sum(Amount) as 'Total' from Payment where Client_id = '" + FormLogin.SetClientId + "'", con);
                SqlDataReader dr = loadData.ExecuteReader();
                while (dr.Read())
                {
                    totalPayment = dr["Total"].ToString();
                }
                return totalPayment;
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        
        private List<Payment> GetPaymentHistoryData()
        {
            try
            {
                con.Open();
                SqlCommand loadData = new SqlCommand("SELECT Date,Amount,Description from Payment where Client_id= '" + FormLogin.SetClientId + "'", con);
                SqlDataReader dr = loadData.ExecuteReader();
                List<Payment> payments = new List<Payment>();
                while (dr.Read())
                {
                    var type = typeof(Payment);
                    Payment obj = (Payment)Activator.CreateInstance(type);
                    foreach (var prop in type.GetProperties())
                    {
                        var propType = prop.PropertyType;
                        prop.SetValue(obj, Convert.ChangeType(dr[prop.Name].ToString(), propType));
                    }
                    payments.Add(obj);
                }
                return payments;
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                return null;
            }
            finally
            {
                con.Close();
            }
        }


        private void GetProfile()
        {
            List<User> user = GetProfileData();
            //profile card
            profileCard = new PictureBox();
            profileCard.Width = 300;
            profileCard.Height = 250;
            profileCard.BackColor = Color.White;
            profileCard.Location = new Point(20,200);
            Rectangle r = new Rectangle(0, 0, profileCard.Width, profileCard.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 13;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            profileCard.Region = new Region(gp);

            int titleWidth = profileCard.Width / 2 - 55;

            //title
            profileTitle = new TextBox();
            profileTitle.ReadOnly = true;
            profileTitle.Text = "Profile";
            profileTitle.BackColor = profileCard.BackColor;
            profileTitle.BorderStyle = BorderStyle.None;
            profileTitle.Width = profileCard.Width;
            FontFamily fontFamily = new FontFamily("Times New Roman");
            profileTitle.Font = new Font(fontFamily, 28, FontStyle.Bold);
            profileTitle.ForeColor = Color.FromArgb(143, 106, 101);
            profileTitle.Location = new Point(20, 15);
            profileTitle.TabStop = false;

            //name
            profileNameTitle = new TextBox();
            profileNameTitle.ReadOnly = true;
            profileNameTitle.TabStop = false;
            profileNameTitle.Text = "Name";
            profileNameTitle.BackColor = profileCard.BackColor;
            profileNameTitle.BorderStyle = BorderStyle.None;
            profileNameTitle.Width = titleWidth;
            profileNameTitle.Font = new Font(fontFamily, 14, FontStyle.Bold);
            profileNameTitle.ForeColor = Color.FromArgb(143, 106, 101);
            profileNameTitle.Location = new Point(20, 65);

            profileName = new TextBox();
            profileName.ReadOnly = true;
            profileName.TabStop = false;
            profileName.Text = user[0].Fname + " " + user[0].Lname;
            profileName.BackColor = profileCard.BackColor;
            profileName.BorderStyle = BorderStyle.None;
            profileName.Width = profileCard.Width / 2;
            profileName.Font = new Font(fontFamily, 14, FontStyle.Bold);
            profileName.ForeColor = Color.FromArgb(240, 180, 48);
            profileName.Location = new Point(130, 65);

            profileAgeTitle = new TextBox();
            profileAgeTitle.ReadOnly = true;
            profileAgeTitle.TabStop = false;
            profileAgeTitle.Text = "Age";
            profileAgeTitle.BackColor = profileCard.BackColor;
            profileAgeTitle.BorderStyle = BorderStyle.None;
            profileAgeTitle.Width = titleWidth;
            profileAgeTitle.Font = new Font(fontFamily, 14, FontStyle.Bold);
            profileAgeTitle.ForeColor = Color.FromArgb(143, 106, 101);
            profileAgeTitle.Location = new Point(20, 95);

            profileAge = new TextBox();
            profileAge.ReadOnly = true;
            profileAge.TabStop = false;
            profileAge.Text = user[0].Age.ToString();
            profileAge.BackColor = profileCard.BackColor;
            profileAge.BorderStyle = BorderStyle.None;
            profileAge.Width = profileCard.Width / 2;
            profileAge.Font = new Font(fontFamily, 14, FontStyle.Bold);
            profileAge.ForeColor = Color.FromArgb(240, 180, 48);
            profileAge.Location = new Point(130, 95);

            profileGenderTitle = new TextBox();
            profileGenderTitle.ReadOnly = true;
            profileGenderTitle.TabStop = false;
            profileGenderTitle.Text = "Gender";
            profileGenderTitle.BackColor = profileCard.BackColor;
            profileGenderTitle.BorderStyle = BorderStyle.None;
            profileGenderTitle.Width = titleWidth;
            profileGenderTitle.Font = new Font(fontFamily, 14, FontStyle.Bold);
            profileGenderTitle.ForeColor = Color.FromArgb(143, 106, 101);
            profileGenderTitle.Location = new Point(20, 125);

            profileGender = new TextBox();
            profileGender.ReadOnly = true;
            profileGender.TabStop = false;
            profileGender.Text = user[0].Sex;
            profileGender.BackColor = profileCard.BackColor;
            profileGender.BorderStyle = BorderStyle.None;
            profileGender.Width = profileCard.Width / 2;
            profileGender.Font = new Font(fontFamily, 14, FontStyle.Bold);
            profileGender.ForeColor = Color.FromArgb(240, 180, 48);
            profileGender.Location = new Point(130, 125);

            profileAddressTitle = new TextBox();
            profileAddressTitle.ReadOnly = true;
            profileAddressTitle.TabStop = false;
            profileAddressTitle.Text = "Address";
            profileAddressTitle.BackColor = profileCard.BackColor;
            profileAddressTitle.BorderStyle = BorderStyle.None;
            profileAddressTitle.Width = titleWidth;
            profileAddressTitle.Font = new Font(fontFamily, 14, FontStyle.Bold);
            profileAddressTitle.ForeColor = Color.FromArgb(143, 106, 101);
            profileAddressTitle.Location = new Point(20, 155);

            profileAddress = new TextBox();
            profileAddress.ReadOnly = true;
            profileAddress.TabStop = false;
            profileAddress.Text = user[0].Address;
            profileAddress.BackColor = profileCard.BackColor;
            profileAddress.BorderStyle = BorderStyle.None;
            profileAddress.Width = profileCard.Width / 2;
            profileAddress.Font = new Font(fontFamily, 14, FontStyle.Bold);
            profileAddress.ForeColor = Color.FromArgb(240, 180, 48);
            profileAddress.Location = new Point(130, 155);

            profilePhoneTitle = new TextBox();
            profilePhoneTitle.ReadOnly = true;
            profilePhoneTitle.TabStop = false;
            profilePhoneTitle.Text = "Phone";
            profilePhoneTitle.BackColor = profileCard.BackColor;
            profilePhoneTitle.BorderStyle = BorderStyle.None;
            profilePhoneTitle.Width = titleWidth;
            profilePhoneTitle.Font = new Font(fontFamily, 14, FontStyle.Bold);
            profilePhoneTitle.ForeColor = Color.FromArgb(143, 106, 101);
            profilePhoneTitle.Location = new Point(20, 185);

            profilePhone = new TextBox();
            profilePhone.ReadOnly = true;
            profilePhone.TabStop = false;
            profilePhone.Text = user[0].Phone;
            profilePhone.BackColor = profileCard.BackColor;
            profilePhone.BorderStyle = BorderStyle.None;
            profilePhone.Width = profileCard.Width / 2;
            profilePhone.Font = new Font(fontFamily, 14, FontStyle.Bold);
            profilePhone.ForeColor = Color.FromArgb(240, 180, 48);
            profilePhone.Location = new Point(130, 185);

            profileDateTitle = new TextBox();
            profileDateTitle.ReadOnly = true;
            profileDateTitle.TabStop = false;
            profileDateTitle.Text = "Date joined";
            profileDateTitle.BackColor = profileCard.BackColor;
            profileDateTitle.BorderStyle = BorderStyle.None;
            profileDateTitle.Width = titleWidth;
            profileDateTitle.Font = new Font(fontFamily, 14, FontStyle.Bold);
            profileDateTitle.ForeColor = Color.FromArgb(143, 106, 101);
            profileDateTitle.Location = new Point(20, 215);

            profileDate = new TextBox();
            profileDate.ReadOnly = true;
            profileDate.TabStop = false;
            profileDate.Text = Convert.ToDateTime(user[0].Date_joined).ToShortDateString();
            profileDate.BackColor = profileCard.BackColor;
            profileDate.BorderStyle = BorderStyle.None;
            profileDate.Width = profileCard.Width / 2;
            profileDate.Font = new Font(fontFamily, 14, FontStyle.Bold);
            profileDate.ForeColor = Color.FromArgb(240, 180, 48);
            profileDate.Location = new Point(130, 215);

            panel1.Controls.Add(profileCard);
            profileCard.Controls.Add(profileTitle);
            profileCard.Controls.Add(profileNameTitle);
            profileCard.Controls.Add(profileName);
            profileCard.Controls.Add(profileAgeTitle);
            profileCard.Controls.Add(profileAge);
            profileCard.Controls.Add(profileGenderTitle);
            profileCard.Controls.Add(profileGender);
            profileCard.Controls.Add(profileAddressTitle);
            profileCard.Controls.Add(profileAddress);
            profileCard.Controls.Add(profilePhoneTitle);
            profileCard.Controls.Add(profilePhone);
            profileCard.Controls.Add(profileDateTitle);
            profileCard.Controls.Add(profileDate);
        }

        private void GetGreeting()
        {
            List<User> user = GetProfileData();
            //card
            greetingCard = new PictureBox();
            greetingCard.Width = this.Width - 400;
            greetingCard.Height = 150;
            greetingCard.BackColor = Color.White;
            greetingCard.Location = new Point(350, 20);
            Rectangle r = new Rectangle(0, 0, greetingCard.Width, greetingCard.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 10;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            greetingCard.Region = new Region(gp);

            greetingTitle = new TextBox();
            greetingTitle.ReadOnly = true;
            greetingTitle.Text = "Hi";
            greetingTitle.BackColor = greetingName.BackColor;
            greetingTitle.BorderStyle = BorderStyle.None;
            greetingTitle.Width = 60;
            FontFamily fontFamily = new FontFamily("Times New Roman");
            greetingTitle.Font = new Font(fontFamily, 36, FontStyle.Bold);
            greetingTitle.ForeColor = Color.FromArgb(143, 106, 101);
            greetingTitle.Location = new Point(30, 25);
            greetingTitle.TabStop = false;

            greetingName = new TextBox();
            greetingName.ReadOnly = true;
            greetingName.Text = user[0].Fname + " !";
            greetingName.BackColor = greetingCard.BackColor;
            greetingName.BorderStyle = BorderStyle.None;
            greetingName.Width = greetingCard.Width - 70;
            greetingName.Font = new Font(fontFamily, 36, FontStyle.Bold);
            greetingName.ForeColor = Color.FromArgb(240, 180, 48);
            greetingName.Location = new Point(100, 25);
            greetingName.TabStop = false;

            greetingSub = new TextBox();
            greetingSub.ReadOnly = true;
            greetingSub.Text = "It's great to have you here, stay consistent & drink water";
            greetingSub.BackColor = greetingCard.BackColor;
            greetingSub.BorderStyle = BorderStyle.None;
            greetingSub.Width = greetingCard.Width - 70;
            greetingSub.Font = new Font(fontFamily, 22, FontStyle.Bold);
            greetingSub.ForeColor = Color.FromArgb(143, 106, 101);
            greetingSub.Location = new Point(30, 90);
            greetingSub.TabStop = false;

            panel1.Controls.Add(greetingCard);
            greetingCard.Controls.Add(greetingTitle);
            greetingCard.Controls.Add(greetingSub);
            greetingCard.Controls.Add(greetingName);
        }

        private void GetMembership()
        {
            List<Membership> member = GetMembershipData();
           memCard = new PictureBox();
            memCard.Width = 300;
            memCard.Height = 285;
            memCard.BackColor = member[0].Duration == 1 ? Color.FromArgb(255, 145, 76) : member[0].Duration == 3 ? Color.FromArgb(48, 200, 214) : member[0].Duration == 6 ? Color.FromArgb(225, 198, 151) : Color.FromArgb(101, 101, 111);
            memCard.ForeColor = Color.White;
            memCard.Location = new Point(20, 470);
            Rectangle r = new Rectangle(0, 0, memCard.Width, memCard.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 10;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            memCard.Region = new Region(gp);

            memTitle = new TextBox();
            memTitle.ReadOnly = true;
            memTitle.Text = "Membership";
            memTitle.BackColor = memCard.BackColor;
            memTitle.BorderStyle = BorderStyle.None;
            memTitle.Width = memCard.Width;
            memTitle.TextAlign = HorizontalAlignment.Center;
            FontFamily fontFamily = new FontFamily("Times New Roman");
            memTitle.Font = new Font(fontFamily, 28, FontStyle.Bold);
            memTitle.ForeColor = Color.White;
            memTitle.Location = new Point(0, 15);
            memTitle.TabStop = false;

            memLogo = new IconPictureBox();
            memLogo.IconChar = member[0].Duration == 1 ? IconChar.ShieldCat : member[0].Duration == 3 ? IconChar.Jedi : member[0].Duration == 6 ? IconChar.Rocket : IconChar.Dragon;
            memLogo.ForeColor = Color.Black;
            memLogo.Width = 110;
            memLogo.Height = 110;
            int xslogo = (memCard.Width / 2) - (memLogo.Width / 2) + 5;
            memLogo.Location = new Point(xslogo, 60);

            memStart = new TextBox();
            memStart.ReadOnly = true;
            memStart.Text = "Start : " + Convert.ToDateTime(member[0].Start_date).ToShortDateString();
            memStart.BackColor = memCard.BackColor;
            memStart.BorderStyle = BorderStyle.None;
            memStart.Width = memCard.Width;
            memStart.TextAlign = HorizontalAlignment.Center;
            memStart.Font = new Font(fontFamily, 18, FontStyle.Bold);
            memStart.ForeColor = Color.White;
            memStart.Location = new Point(0, 167);
            memStart.TabStop = false;

            memEnd = new TextBox();
            memEnd.ReadOnly = true;
            memEnd.Text = "Ends in";
            memEnd.BackColor = memCard.BackColor;
            memEnd.BorderStyle = BorderStyle.None;
            memEnd.Width = memCard.Width;
            memEnd.TextAlign = HorizontalAlignment.Center;
            memEnd.Font = new Font(fontFamily, 14, FontStyle.Bold);
            memEnd.ForeColor = Color.White;
            memEnd.Location = new Point(0, 200);
            memEnd.TabStop = false;

            memDuration = new TextBox();
            memDuration.ReadOnly = true;
            memDuration.Text = member[0].Duration + " Month";
            memDuration.BackColor = memCard.BackColor;
            memDuration.BorderStyle = BorderStyle.None;
            memDuration.Width = memCard.Width;
            memDuration.TextAlign = HorizontalAlignment.Center;
            memDuration.Font = new Font(fontFamily, 30, FontStyle.Bold | FontStyle.Italic);
            memDuration.ForeColor = Color.Black;
            memDuration.Location = new Point(0, 220);
            memDuration.TabStop = false;

            panel1.Controls.Add(memCard);
            memCard.Controls.Add(memTitle);
            memCard.Controls.Add(memLogo);
            memCard.Controls.Add(memStart);
            memCard.Controls.Add(memEnd);
            memCard.Controls.Add(memDuration);
        }

        private void GetTotalClass()
        {
            totalClassCard = new PictureBox();
            totalClassCard.Width = 250;
            totalClassCard.Height = 220;
            totalClassCard.BackColor = Color.White;
            totalClassCard.Location = new Point(350, 200);
            Rectangle r = new Rectangle(0, 0, totalClassCard.Width, totalClassCard.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 10;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            totalClassCard.Region = new Region(gp);

            totalClassLogo = new IconPictureBox();
            totalClassLogo.IconChar = IconChar.Dumbbell;
            totalClassLogo.ForeColor = Color.Black;
            totalClassLogo.Width = 150;
            totalClassLogo.Height = 150;
            int xslogo = (totalClassCard.Width / 2) - (totalClassLogo.Width / 2) + 5;
            totalClassLogo.Location = new Point(xslogo, -5);

            totalClassTitle = new TextBox();
            totalClassTitle.ReadOnly = true;
            totalClassTitle.Text = "Total Class";
            totalClassTitle.TextAlign = HorizontalAlignment.Center;
            totalClassTitle.BackColor = totalClassCard.BackColor;
            totalClassTitle.BorderStyle = BorderStyle.None;
            totalClassTitle.Width = totalClassCard.Width;
            totalClassTitle.TextAlign = HorizontalAlignment.Center;
            FontFamily fontFamily = new FontFamily("Times New Roman");
            totalClassTitle.Font = new Font(fontFamily, 24, FontStyle.Bold);
            totalClassTitle.ForeColor = Color.FromArgb(45, 99, 108);
            totalClassTitle.Location = new Point(0, 115);
            totalClassTitle.TabStop = false;

            totalClassNumCard = new PictureBox();
            totalClassNumCard.Width = 250;
            totalClassNumCard.Height = 60;
            totalClassNumCard.BackColor = Color.FromArgb(48, 200, 214);
            totalClassNumCard.Location = new Point(350, 360);

            totalClassNum = new TextBox();
            totalClassNum.ReadOnly = true;
            totalClassNum.Text = totalClass;
            totalClassNum.TextAlign = HorizontalAlignment.Center;
            totalClassNum.BackColor = totalClassNumCard.BackColor;
            totalClassNum.BorderStyle = BorderStyle.None;
            totalClassNum.Width = totalClassCard.Width;
            totalClassNum.Font = new Font(fontFamily, 30, FontStyle.Bold | FontStyle.Italic);
            totalClassNum.ForeColor = Color.White;
            totalClassNum.Location = new Point(0, 5);
            totalClassNum.TabStop = false;


            panel1.Controls.Add(totalClassNumCard);
            panel1.Controls.Add(totalClassCard);
            totalClassCard.Controls.Add(totalClassTitle);
            totalClassCard.Controls.Add(totalClassLogo);
            totalClassNumCard.Controls.Add(totalClassNum);
        }

        private void GetTotalPayment()
        {
            totalPaymentCard = new PictureBox();
            totalPaymentCard.Width = 250;
            totalPaymentCard.Height = 220;
            totalPaymentCard.BackColor = Color.White;
            totalPaymentCard.Location = new Point(630, 200);
            Rectangle r = new Rectangle(0, 0, totalPaymentCard.Width, totalPaymentCard.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 10;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            totalPaymentCard.Region = new Region(gp);

            totalPaymentTitle = new TextBox();
            totalPaymentTitle.ReadOnly = true;
            totalPaymentTitle.Text = "Total Payment";
            totalPaymentTitle.TextAlign = HorizontalAlignment.Center;
            totalPaymentTitle.BackColor = totalPaymentCard.BackColor;
            totalPaymentTitle.BorderStyle = BorderStyle.None;
            totalPaymentTitle.Width = totalPaymentCard.Width;
            totalPaymentTitle.TextAlign = HorizontalAlignment.Center;
            FontFamily fontFamily = new FontFamily("Times New Roman");
            totalPaymentTitle.Font = new Font(fontFamily, 24, FontStyle.Bold);
            totalPaymentTitle.ForeColor = Color.FromArgb(45, 99, 108);
            totalPaymentTitle.Location = new Point(0, 115);
            totalPaymentTitle.TabStop = false;

            totalPaymentLogo = new IconPictureBox();
            totalPaymentLogo.IconChar = IconChar.MoneyBill;
            totalPaymentLogo.ForeColor = Color.Black;
            totalPaymentLogo.Width = 150;
            totalPaymentLogo.Height = 150;
            int xslogo = (totalClassCard.Width / 2) - (totalPaymentLogo.Width / 2) ;
            totalPaymentLogo.Location = new Point(xslogo, 0);

            totalPaymentNumCard = new PictureBox();
            totalPaymentNumCard.Width = 250;
            totalPaymentNumCard.Height = 60;
            totalPaymentNumCard.BackColor = Color.FromArgb(48, 200, 214);
            totalPaymentNumCard.Location = new Point(630, 360);

            totalPaymentNum = new TextBox();
            totalPaymentNum.ReadOnly = true;
            totalPaymentNum.Text = "$ " + totalPayment;
            totalPaymentNum.TextAlign = HorizontalAlignment.Center;
            totalPaymentNum.BackColor = totalPaymentNumCard.BackColor;
            totalPaymentNum.BorderStyle = BorderStyle.None;
            totalPaymentNum.Width = totalClassCard.Width;
            totalPaymentNum.Font = new Font(fontFamily, 30, FontStyle.Bold | FontStyle.Italic);
            totalPaymentNum.ForeColor = Color.White;
            totalPaymentNum.Location = new Point(0, 5);
            totalPaymentNum.TabStop = false;

            


            panel1.Controls.Add(totalPaymentNumCard);
            panel1.Controls.Add(totalPaymentCard);
            totalPaymentCard.Controls.Add(totalPaymentTitle);
            totalPaymentCard.Controls.Add(totalPaymentLogo);
            totalPaymentNumCard.Controls.Add(totalPaymentNum);
        }

        private void GetPaymentHistory()
        {
            List<Payment> payments = GetPaymentHistoryData();
            payHistoryTitle = new TextBox();
            payHistoryTitle.ReadOnly = true;
            payHistoryTitle.TabStop = false;
            payHistoryTitle.Text = "My Payment History";
            payHistoryTitle.BackColor = Color.FromArgb(244,182, 37);
            payHistoryTitle.ForeColor = Color.White;
            payHistoryTitle.BorderStyle = BorderStyle.None;
            payHistoryTitle.Width = 400;
            FontFamily fontFamily = new FontFamily("Times New Roman");
            payHistoryTitle.Font = new Font(fontFamily, 30, FontStyle.Bold);
            payHistoryTitle.Location = new Point(350, 450);

            payHistoryCard = new FlowLayoutPanel();
            payHistoryCard.Size = new Size(530, 240);
            payHistoryCard.Dock = DockStyle.None;
            payHistoryCard.FlowDirection = FlowDirection.TopDown;
            payHistoryCard.AutoScroll = false;
            payHistoryCard.HorizontalScroll.Enabled = false;
            payHistoryCard.AutoScroll = true;
            payHistoryCard.WrapContents = false;
            payHistoryCard.BackColor = Color.White;
            payHistoryCard.Location = new Point(350, 510);
            Rectangle r = new Rectangle(0, 0, payHistoryCard.Width, payHistoryCard.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 10;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            payHistoryCard.Region = new Region(gp);

            foreach(Payment payment in payments)
            {
                individualPaymentCard = new PictureBox();
                individualPaymentCard.Width = 530;
                individualPaymentCard.Height = 60;
                individualPaymentCard.BackColor = Color.White;

                payLogo = new IconPictureBox();
                payLogo.IconChar = payment.Description == "F&B" ? IconChar.Utensils : payment.Description == "Membership" ? IconChar.AddressCard : IconChar.BoxesStacked;
                payLogo.ForeColor = Color.Black;
                payLogo.Width = 55;
                payLogo.Height = 55;
                payLogo.Location = new Point(15, 15);

                payDescription = new TextBox();
                payDescription.ReadOnly = true;
                payDescription.Text = payment.Description;
                payDescription.TextAlign = HorizontalAlignment.Center;
                payDescription.BackColor = Color.White;
                payDescription.BorderStyle = BorderStyle.None;
                payDescription.Width = payHistoryCard.Width / 4;
                payDescription.Font = new Font(fontFamily, 18, FontStyle.Bold);
                payDescription.ForeColor = profileName.ForeColor;
                payDescription.Location = new Point(75, 25);
                payDescription.TabStop = false;

                payDate = new TextBox();
                payDate.ReadOnly = true;
                payDate.Text = Convert.ToDateTime(payment.Date).ToShortDateString();
                payDate.TextAlign = HorizontalAlignment.Center;
                payDate.BackColor = Color.White;
                payDate.BorderStyle = BorderStyle.None;
                payDate.Width = payHistoryCard.Width / 4;
                payDate.Font = new Font(fontFamily, 18, FontStyle.Bold);
                payDate.ForeColor = profileName.ForeColor;
                payDate.Location = new Point(233, 25);
                payDate.TabStop = false;

                payAmount = new TextBox();
                payAmount.ReadOnly = true;
                payAmount.Text = "$ " + payment.Amount.ToString();
                payAmount.TextAlign = HorizontalAlignment.Center;
                payAmount.BackColor = Color.White;
                payAmount.BorderStyle = BorderStyle.None;
                payAmount.Width = payHistoryCard.Width / 4;
                payAmount.Font = new Font(fontFamily, 20, FontStyle.Bold);
                payAmount.ForeColor = Color.FromArgb(240, 180, 48);
                payAmount.Location = new Point(373, 20);
                payAmount.TabStop = false;

                payHistoryCard.Controls.Add(individualPaymentCard);
                individualPaymentCard.Controls.Add(payLogo);
                individualPaymentCard.Controls.Add(payDescription);
                individualPaymentCard.Controls.Add(payDate);
                individualPaymentCard.Controls.Add(payAmount);
            }

            panel1.Controls.Add(payHistoryTitle);
            panel1.Controls.Add(payHistoryCard);
        }

        private void GetClass()
        {
            FontFamily fontFamily = new FontFamily("Times New Roman");
            classTitle = new TextBox();
            classTitle.ReadOnly = true;
            classTitle.TabStop = false;
            classTitle.Text = "My Class";
            classTitle.BackColor = Color.FromArgb(244, 182, 37);
            classTitle.ForeColor = Color.White;
            classTitle.BorderStyle = BorderStyle.None;
            classTitle.Width = 600;
            classTitle.Font = new Font(fontFamily, 30, FontStyle.Bold);
            classTitle.Location = new Point(905, 200);

            classCard = new FlowLayoutPanel();
            classCard.Width = 360;
            classCard.Height = 488;
            classCard.Dock = DockStyle.None;
            classCard.FlowDirection = FlowDirection.TopDown;
            classCard.AutoScroll = false;
            classCard.HorizontalScroll.Enabled = false;
            classCard.AutoScroll = true;
            classCard.WrapContents = false;
            classCard.BackColor = Color.FromArgb(255, 242, 210);
            classCard.Location = new Point(905, 260);
            classCard.Padding = new Padding(7, 7, 7, 17);
            Rectangle r = new Rectangle(0, 0, classCard.Width, classCard.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 10;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            classCard.Region = new Region(gp);

            panel1.Controls.Add(classTitle);
            panel1.Controls.Add(classCard);
            try
            {
                con.Open();
                SqlCommand loadData = new SqlCommand("select Class.Class_image, Class.Class_name, Instructor.Fname, Class.Schedule, Room.Name, Room.Location\r\nfrom class\r\njoin Instructor\r\non Class.Instructor_id = Instructor.Instructor_id\r\njoin Room\r\non Class.Room_id = Room.Room_id\r\njoin Enrolls_In\r\non Enrolls_In.Class_id = Class.Class_id\r\nwhere Client_id = '" + FormLogin.SetClientId + "' group by Class.Class_image, Class.Class_name, Instructor.Fname, Class.Schedule, Room.Name, Room.Location; ", con);
                SqlDataReader dr = loadData.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                   
                        
                        individualClassCard = new PictureBox();
                        individualClassCard.Width = 340;
                        individualClassCard.Height = 90;
                        individualClassCard.BackColor = Color.White;
                        individualClassCard.Location = new Point(20, 20);

                        long len = dr.GetBytes(0, 0, null, 0, 0);
                        byte[] array = new byte[Convert.ToInt32(len) + 1];
                        dr.GetBytes(0, 0, array, 0, Convert.ToInt32(len));
                      
                        classImage = new PictureBox();
                        classImage.Width = 70;
                        classImage.Height = 70;
                        Rectangle r2 = new Rectangle(0, 0, classImage.Width, classImage.Height);
                        System.Drawing.Drawing2D.GraphicsPath gp2 = new System.Drawing.Drawing2D.GraphicsPath();
                        int d2 = 10;
                        gp2.AddArc(r2.X, r2.Y, d2, d2, 180, 90);
                        gp2.AddArc(r2.X + r2.Width - d2, r2.Y, d2, d2, 270, 90);
                        gp2.AddArc(r2.X + r2.Width - d2, r2.Y + r2.Height - d2, d2, d2, 0, 90);
                        gp2.AddArc(r2.X, r2.Y + r2.Height - d2, d2, d2, 90, 90);
                        classImage.Region = new Region(gp2);
                        MemoryStream ms = new MemoryStream(array);
                        Bitmap bitmap = new Bitmap(ms);
                        classImage.BackgroundImageLayout = ImageLayout.Stretch;
                        classImage.BackgroundImage = bitmap;
                        classImage.BackColor = Color.Red;
                        classImage.Location = new Point(10, 10);

                        className = new TextBox();
                        className.ReadOnly = true;
                        className.TabStop = false;
                        className.Text = dr["Class_name"].ToString();
                        className.ForeColor = Color.FromArgb(249, 182, 39);
                        className.BackColor = Color.White;
                        className.BorderStyle = BorderStyle.None;
                        className.Width = 100;
                        className.Font = new Font(fontFamily, 16, FontStyle.Bold);
                        className.Location = new Point(90, 10);

                        classInsName = new TextBox();
                        classInsName.ReadOnly = true;
                        classInsName.TabStop = false;
                        classInsName.Text = dr["Fname"].ToString();
                        classInsName.ForeColor = Color.FromArgb(249, 182, 39);
                        classInsName.BackColor = Color.White;
                        classInsName.BorderStyle = BorderStyle.None;
                        classInsName.Width = 100;
                        classInsName.Font = new Font(fontFamily, 14, FontStyle.Bold | FontStyle.Italic);
                        classInsName.Location = new Point(90, 50);

                        classDate = new TextBox();
                        classDate.ReadOnly = true;
                        classDate.TabStop = false;
                        classDate.Text = dr["Schedule"].ToString();
                        classDate.ForeColor = Color.FromArgb(145, 111, 87);
                        classDate.BackColor = Color.White;
                        classDate.BorderStyle = BorderStyle.None;
                        classDate.Width = 180;
                        classDate.Font = new Font(fontFamily, 14, FontStyle.Bold);
                        classDate.Location = new Point(190, 10);

                        classRoomName = new TextBox();
                        classRoomName.ReadOnly = true;
                        classRoomName.TabStop = false;
                        classRoomName.Text = dr["Name"].ToString() + ",";
                        classRoomName.ForeColor = Color.FromArgb(145, 111, 87);
                        classRoomName.BackColor = Color.White;
                        classRoomName.BorderStyle = BorderStyle.None;
                        classRoomName.Width = 180;
                        classRoomName.Font = new Font(fontFamily, 16, FontStyle.Bold);
                        classRoomName.Location = new Point(190, 40);

                        classRoomLoc = new TextBox();
                        classRoomLoc.ReadOnly = true;
                        classRoomLoc.TabStop = false;
                        classRoomLoc.Text = dr["Location"].ToString();
                        classRoomLoc.ForeColor = Color.FromArgb(145, 111, 87);
                        classRoomLoc.BackColor = Color.White;
                        classRoomLoc.BorderStyle = BorderStyle.None;
                        classRoomLoc.Width = 180;
                        classRoomLoc.Font = new Font(fontFamily, 14, FontStyle.Bold);
                        classRoomLoc.Location = new Point(190, 63);

                        classCard.Controls.Add(individualClassCard);
                        individualClassCard.Controls.Add(classImage);
                        individualClassCard.Controls.Add(className);
                        individualClassCard.Controls.Add(classInsName);
                        individualClassCard.Controls.Add(classDate);
                        individualClassCard.Controls.Add(classRoomName);
                        individualClassCard.Controls.Add(classRoomLoc);
                    }
                }
                con.Close();
            } catch(Exception err)
            {
                MessageBox.Show(err.ToString());
            } finally
            {
                con.Close();
            }
           
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin formLogin = new FormLogin();
            formLogin.ShowDialog();
            this.Close();
        }
    }
    public class User
    {
        public String Fname { get; set; }
        public String Lname { get; set; }
        public int Age { get; set; }
        public String Sex { get; set; }
        public String Date_joined { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
    }

    public class Membership
    {
        public String Start_date { get; set; }
        public int Duration { get; set; }
    }

    public class Payment
    {
        public string Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }

   
}
