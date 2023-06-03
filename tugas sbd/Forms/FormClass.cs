using Microsoft.Win32;
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
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Image = System.Drawing.Image;

namespace tugas_sbd.Forms
{
    public partial class FormClass : Form
    {
        SqlConnection con = new SqlConnection("Data Source=AJ\\SQLEXPRESS01;Initial Catalog=gymink;Integrated Security=True");
        int classIdUpdated;
        public FormClass()
        {
            InitializeComponent();
        }

        void LoadAllClass()
        {
            con.Open();
            SqlCommand loadRooms = new SqlCommand("SELECT Class.Class_id as ID, Class.Class_name as Class_Name, Class.Size,Class.Schedule,Class.Instructor_id,Instructor.Fname as Instructor_Name, Class.Room_id, Room.Name as Room_Name\r\nFROM Class\r\nJOIN Instructor\r\nON Class.Instructor_id = Instructor.Instructor_id\r\nJOIN Room\r\nON Class.Room_id = Room.Room_id\r\nGROUP BY Class.Class_id, Class.Class_name, Class.Size,Class.Schedule,Class.Instructor_id, Class.Room_id, Instructor.Fname, Room.Name;", con);
            SqlDataAdapter da = new SqlDataAdapter(loadRooms);
            DataTable dt = new DataTable();
            da.Fill(dt);
            classDataGrid.DataSource = dt;
            con.Close();
        }

        void LoadInstructor()
        {
            con.Open();
            SqlCommand loadRoom = new SqlCommand("SELECT Instructor_id FROM Instructor", con);
            SqlDataReader dr = loadRoom.ExecuteReader();
            while (dr.Read())
            {
                string s = dr["Instructor_id"].ToString();
                insId.Items.Add(s);
                insId.DisplayMember = s;
                insId.ValueMember = s;
            }
            con.Close();
        }
        private void insId_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            string q = "select Fname,Lname from Instructor where Instructor_id = '" + insId.SelectedItem + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ins.Text = dr["Fname"].ToString() + " " + dr["Lname"].ToString();
            }
            con.Close();
        }

        void LoadRoom()
        {
            con.Open();
            SqlCommand loadRoom = new SqlCommand("SELECT Room_id FROM Room", con);
            SqlDataReader dr = loadRoom.ExecuteReader();
            while (dr.Read())
            {
                string s = dr["Room_id"].ToString();
                roomId.Items.Add(s);
                roomId.DisplayMember = s;
                roomId.ValueMember = s;
            }
            con.Close();
        }

        private void roomid_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            string q = "select Name from Room where Room_id = '" + roomId.SelectedItem + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                roomname.Text = dr["Name"].ToString();
            }
            con.Close();
        }
        private void FormClass_Load(object sender, EventArgs e)
        {
            LoadAllClass();
            LoadInstructor();
            LoadRoom();
            classDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(143, 115, 235);
            classDataGrid.EnableHeadersVisualStyles = false;
        }

        private void create_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
               
                if (cname.Text == "")
                {
                    MessageBox.Show("Please enter a class name");
                    con.Close();
                    return;
                }
                MemoryStream ms = new MemoryStream();
                if(picturebox1.BackgroundImage == null)
                {
                    MessageBox.Show("Please add an image");
                } else
                {
                    picturebox1.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] arrImage = ms.GetBuffer();
                    SqlCommand insertClient = new SqlCommand("INSERT INTO Class values(@cname,@size,@schedule,@insId,@roomId,@image)", con);
                    insertClient.Parameters.AddWithValue("@cname", cname.Text);
                    insertClient.Parameters.AddWithValue("@size", size.Text);
                    insertClient.Parameters.AddWithValue("@schedule", schedule.Text);
                    insertClient.Parameters.AddWithValue("@insId", insId.Text);
                    insertClient.Parameters.AddWithValue("@roomId", roomId.Text);
                    insertClient.Parameters.AddWithValue("@image", arrImage);
                    insertClient.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("New class has been created!");
                    LoadAllClass();
                    cname.Text = "";
                    schedule.Text = "";
                    insId.Text = "";
                    roomId.Text = "";
                    ins.Text = "";
                    roomname.Text = "";
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

        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand updateClass = new SqlCommand("UPDATE Class set Class_name=@cname,Size=@size,Schedule=@schedule,Instructor_id=@insId,Room_id=@roomId where Class_id=@classId", con);
                updateClass.Parameters.AddWithValue("@cname", cname.Text);
                updateClass.Parameters.AddWithValue("@size", int.Parse(size.Text));
                updateClass.Parameters.AddWithValue("@schedule", schedule.Text);
                updateClass.Parameters.AddWithValue("@insId", int.Parse(insId.Text));
                updateClass.Parameters.AddWithValue("@roomId", int.Parse(roomId.Text));
                updateClass.Parameters.AddWithValue("@classId", classIdUpdated);
                updateClass.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Class has been updated!");
                LoadAllClass();
                cname.Text = "";
                schedule.Text = "";
                insId.Text = "";
                roomId.Text = "";
                ins.Text = "";
                roomname.Text = "";
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

        private void classDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = classDataGrid.Rows[e.RowIndex];
                classIdUpdated = Convert.ToInt32(row.Cells["ID"].Value);
                cname.Text = Convert.ToString(row.Cells["Class_Name"].Value);
                size.Text = Convert.ToString(row.Cells["Size"].Value);
                schedule.Text = Convert.ToString(row.Cells["Schedule"].Value);
                insId.Text = Convert.ToString(row.Cells["Instructor_Id"].Value);
                roomId.Text = Convert.ToString(row.Cells["Room_Id"].Value);
            }
        }

        private void browse_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            picturebox1.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand deleteClass = new SqlCommand("DELETE from class where Class_id=@classId", con);
                deleteClass.Parameters.AddWithValue("@classId", classIdUpdated);
                deleteClass.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Deleted!");
                LoadAllClass();
                cname.Text = "";
                schedule.Text = "";
                insId.Text = "";
                roomId.Text = "";
                ins.Text = "";
                roomname.Text = "";
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
    }
}
