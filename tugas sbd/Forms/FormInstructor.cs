using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tugas_sbd.Forms
{
    public partial class FormInstructor : Form
    {
        SqlConnection con = new SqlConnection("Data Source=AJ\\SQLEXPRESS01;Initial Catalog=gymink;Integrated Security=True");
        int insIdUpdated;
        public FormInstructor()
        {
            InitializeComponent();
        }

        void LoadAllInstructor()
        {
            con.Open();
            SqlCommand loadClients = new SqlCommand("select Instructor_id as ID, Fname as First_Name, Lname as Last_Name,Age,Sex,Specialty,Phone,Status from instructor", con);
            SqlDataAdapter da = new SqlDataAdapter(loadClients);
            DataTable dt = new DataTable();
            da.Fill(dt);
            insDataGrid.DataSource = dt;
            con.Close();
        }

        void LoadStatus()
        {
            status.Items.Add("Active");
            status.DisplayMember = "Active";
            status.ValueMember = "Active";
            status.Items.Add("Resigned");
            status.DisplayMember = "Resigned";
            status.ValueMember = "Resigned";
        }

    
        private void create_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sex;
                if (male.Checked == true)
                {
                    sex = "M";
                }
                else
                {
                    sex = "F";
                }
                if (fname.Text == "")
                {
                    MessageBox.Show("Please enter a first name");
                    con.Close();
                    return;
                }
                if(picturebox1.BackgroundImage == null )
                {
                    MessageBox.Show("Please add an image");
                } else
                {
                    MemoryStream ms = new MemoryStream();
                    picturebox1.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] arrImage = ms.GetBuffer();
                    SqlCommand insertIns = new SqlCommand("INSERT INTO Instructor values(@fname,@lname,@age,@sex,@specialty,@phone,@status,@image)", con);
                    insertIns.Parameters.AddWithValue("@fname", fname.Text);
                    insertIns.Parameters.AddWithValue("@lname", lname.Text);
                    insertIns.Parameters.AddWithValue("@age", int.Parse(age.Text));
                    insertIns.Parameters.AddWithValue("@sex", sex);
                    insertIns.Parameters.AddWithValue("@specialty", specialty.Text);
                    insertIns.Parameters.AddWithValue("@phone", phone.Text);
                    insertIns.Parameters.AddWithValue("@status", status.Text);
                    insertIns.Parameters.AddWithValue("@image", arrImage);
                    insertIns.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("New Instructor has been created!");
                    LoadAllInstructor();
                    fname.Text = "";
                    lname.Text = "";
                    specialty.Text = "";
                    phone.Text = "";
                }
               
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.ToString());
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong input format");
            }
            finally
            {
                con.Close();
            }
        }

        private void FormInstructor_Load(object sender, EventArgs e)
        {
            LoadAllInstructor();
            LoadStatus();
            insDataGrid.EnableHeadersVisualStyles = false;
            insDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(143, 115, 235);
        }

        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sex;
                if (male.Checked == true)
                {
                    sex = "M";
                }
                else
                {
                    sex = "F";
                }
                if (fname.Text == "")
                {
                    MessageBox.Show("Please enter a first name");
                    con.Close();
                    return;
                }

                SqlCommand updateIns = new SqlCommand("UPDATE Instructor set Fname=@Fname,Lname=@Lname,Age=@Age,Sex=@Sex,Specialty=@Specialty,Phone=@Phone,Status=@Status where Instructor_id=@Instructor_id", con);
                updateIns.Parameters.AddWithValue("@Instructor_id", insIdUpdated);
                updateIns.Parameters.AddWithValue("@Fname", fname.Text);
                updateIns.Parameters.AddWithValue("@Lname", lname.Text);
                updateIns.Parameters.AddWithValue("@Age", int.Parse(age.Text));
                updateIns.Parameters.AddWithValue("@Sex", sex);
                updateIns.Parameters.AddWithValue("@Specialty", specialty.Text);
                updateIns.Parameters.AddWithValue("@Phone", phone.Text);
                updateIns.Parameters.AddWithValue("@Status", status.Text);
                updateIns.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Instructor has been updated!");
                LoadAllInstructor();
                fname.Text = "";
                lname.Text = "";
                specialty.Text = "";
                phone.Text = "";
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.ToString());
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong input format");
            }
            finally
            {
                con.Close();
            }
    }

        private void insDataGrid_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = insDataGrid.Rows[e.RowIndex];
                insIdUpdated = Convert.ToInt32(row.Cells["ID"].Value);
                fname.Text = Convert.ToString(row.Cells["First_Name"].Value);
                lname.Text = Convert.ToString(row.Cells["Last_Name"].Value);
                age.Text = Convert.ToString(row.Cells["Age"].Value);
                if (Convert.ToString(row.Cells["Sex"].Value) == "M")
                {
                    male.Checked = true;
                }
                else
                {
                    female.Checked = true;
                }
                specialty.Text = Convert.ToString(row.Cells["Specialty"].Value);
                phone.Text = Convert.ToString(row.Cells["Phone"].Value);
                status.Text = Convert.ToString(row.Cells["Status"].Value);
            }
        }

        private void InstructorDashboard_Click(object sender, EventArgs e)
        {
                                                                                                
        }

        private void browse_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            picturebox1.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
        }
    }
}
