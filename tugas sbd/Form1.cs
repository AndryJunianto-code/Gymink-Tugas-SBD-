using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using FontAwesome.Sharp;
using tugas_sbd.Forms;
using Color = System.Drawing.Color;

namespace tugas_sbd
{
    public partial class Form1 : Form
    {
        //fields
        private IconButton currentbtn;
        private Panel leftBorderbtn;
        private Form currentChildForm;

        public Form1()
        {
            InitializeComponent();
            leftBorderbtn = new Panel();
            leftBorderbtn.Size = new Size(7, 60);
            ActivateButton(iconButton1, RGBColors.color1, "Client Dashboard", false);
            OpenChildForm(new FormClient());
            //form
            this.Text = String.Empty;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;



        }

        //Struct
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(82, 195, 197);
            public static Color color2 = Color.FromArgb(136, 199, 176);
            public static Color color3 = Color.FromArgb(208, 218, 98);
            public static Color color4 = Color.FromArgb(238, 209, 100);
            public static Color color5 = Color.FromArgb(234, 171, 96);
            public static Color color6 = Color.FromArgb(230, 140, 150);
            public static Color color7 = Color.FromArgb(245, 216, 224);
            public static Color color8 = Color.FromArgb(176, 144, 189);
        }
        
        //Methods
        private void ActivateButton(object senderBtn,Color color, String dashboardText, bool dashboardVisible)
        {
            if(senderBtn != null)
            {
                DisableButton();
                //button
                currentbtn = (IconButton)senderBtn;
                currentbtn.BackColor = Color.FromArgb(61, 44, 120);
                currentbtn.ForeColor = color;
                currentbtn.TextAlign = ContentAlignment.MiddleCenter;
                currentbtn.IconColor = color;
                currentbtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentbtn.ImageAlign = ContentAlignment.MiddleRight;
                //left border button
                leftBorderbtn.BackColor = color;
                leftBorderbtn.Location = new Point(0, currentbtn.Location.Y);
                leftBorderbtn.Visible = true;
                leftBorderbtn.BringToFront();
                //icon current
                current.IconChar = currentbtn.IconChar;
                current.IconColor = color;
                titleCurrent.Text = currentbtn.Text;
                goToDashboard.Text = dashboardText;
                goToDashboard.Visible = dashboardVisible;
            }
        }
        
        private void DisableButton()
        {
            if(currentbtn != null)
            {
                currentbtn.BackColor = Color.FromArgb(49, 35, 99);
                currentbtn.ForeColor = Color.Gainsboro;
                currentbtn.TextAlign = ContentAlignment.MiddleLeft;
                currentbtn.IconColor = Color.Gainsboro;
                currentbtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentbtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        public void OpenChildForm(Form childform)
        {
            if(currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            paneldesktop.Controls.Add(childform);
            paneldesktop.Tag = childform;
            childform.BringToFront();
            childform.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1, "Client Dashboard", false);
            OpenChildForm(new FormClient());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2, "Edit Instructor", true); 
            OpenChildForm(new FormInstructorBoard());
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3, "Edit Class", true);
            OpenChildForm(new FormClassBoard());
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4, "Instructor Dashboard", false);
            OpenChildForm(new FormRoom());
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5, "Instructor Dashboard", false);
            OpenChildForm(new FormEnrolls());
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6, "Edit Membership", true);
            OpenChildForm(new FormMembershipBoard());
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color7, "Edit Payment", true);
            OpenChildForm(new FormPaymentBoard());
        }
        private void iconButton8_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color8, "Instructor Dashboard", true);
        }

        private void goToDashboard_Click(object sender, EventArgs e)
        {
            if(goToDashboard.Text == "Edit Instructor")
            {
                goToDashboard.Text = "Instructor Dashboard";
                OpenChildForm(new FormInstructor());
            } else if(goToDashboard.Text == "Instructor Dashboard")
            {
                goToDashboard.Text = "Edit Instructor";
                OpenChildForm(new FormInstructorBoard());
            } else if(goToDashboard.Text == "Edit Class")
            {
                goToDashboard.Text = "Class Dashboard";
                OpenChildForm(new FormClass());
            } else if(goToDashboard.Text == "Class Dashboard")
            {
                goToDashboard.Text = "Edit Class";
                OpenChildForm(new FormClassBoard());
            }
            else if (goToDashboard.Text == "Edit Membership")
            {
                goToDashboard.Text = "Membership Dashboard";
                OpenChildForm(new FormMembership());
            }
            else if (goToDashboard.Text == "Membership Dashboard")
            {
                goToDashboard.Text = "Edit Membership";
                OpenChildForm(new FormMembershipBoard());
            }
            else if (goToDashboard.Text == "Edit Payment")
            {
                goToDashboard.Text = "Payment Dashboard";
                OpenChildForm(new FormPayment());
            }
            else if (goToDashboard.Text == "Payment Dashboard")
            {
                goToDashboard.Text = "Edit Payment";
                OpenChildForm(new FormPaymentBoard());
            }

        }

        private void iconButton8_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.ShowDialog();
            this.Close();
        }
    }
}
