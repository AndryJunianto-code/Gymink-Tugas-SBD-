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
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace tugas_sbd.Forms
{
    public partial class FormRoom : Form
    {
        SqlConnection con = new SqlConnection("Data Source=AJ\\SQLEXPRESS01;Initial Catalog=gymink;Integrated Security=True");
        int roomIdUpdated;
        public FormRoom()
        {
            InitializeComponent();
        }

        void LoadAllRoom()
        {
            con.Open();
            SqlCommand loadRooms = new SqlCommand("select Room_id as ID,Name as Room_Name,Location,Condition from room", con);
            SqlDataAdapter da = new SqlDataAdapter(loadRooms);
            DataTable dt = new DataTable();
            da.Fill(dt);
            roomDataGrid.DataSource = dt;
            con.Close();
        }
        void LoadCondition()
        {
            condition.Items.Add("Good");
            condition.DisplayMember = "Good";
            condition.ValueMember = "Good";
            condition.Items.Add("Bad");
            condition.DisplayMember = "Bad";
            condition.ValueMember = "Bad";
            condition.Items.Add("Not for public");
            condition.DisplayMember = "Not for public";
            condition.ValueMember = "Not for public";
        }

        private void create_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
            
                if (rname.Text == "")
                {
                    MessageBox.Show("Please enter a room name");
                    con.Close();
                    return;
                }
                SqlCommand insertRoom = new SqlCommand("INSERT INTO Room values(@Name,@Location,@Condition)", con);
                insertRoom.Parameters.AddWithValue("@Name", rname.Text);
                insertRoom.Parameters.AddWithValue("@Location", location.Text);
                insertRoom.Parameters.AddWithValue("@Condition", condition.Text);
                insertRoom.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("New Room has been created!");
                LoadAllRoom();
                rname.Text = "";
                location.Text = "";
                condition.Text = "";
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
                if (rname.Text == "")
                {
                    MessageBox.Show("Please enter a room name");
                    con.Close();
                    return;
                }

                SqlCommand updateRoom = new SqlCommand("UPDATE Room set Name=@Name,Location=@Location,Condition=@Condition where Room_id=@Room_id", con);
                updateRoom.Parameters.AddWithValue("@Room_id", roomIdUpdated);
                updateRoom.Parameters.AddWithValue("@Name", rname.Text);
                updateRoom.Parameters.AddWithValue("@Location", location.Text);
                updateRoom.Parameters.AddWithValue("@Condition", condition.Text);
             
                updateRoom.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Room has been updated!");
                LoadAllRoom();
                rname.Text = "";
                location.Text = "";
                condition.Text = "";
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

        private void FormRoom_Load(object sender, EventArgs e)
        {
            LoadAllRoom();
            LoadCondition();
            roomDataGrid.EnableHeadersVisualStyles = false;
            roomDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(143, 115, 235);
        }

        private void roomDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = roomDataGrid.Rows[e.RowIndex];
                roomIdUpdated = Convert.ToInt32(row.Cells["ID"].Value);
                rname.Text = Convert.ToString(row.Cells["Room_Name"].Value);
                location.Text = Convert.ToString(row.Cells["Location"].Value);
                condition.Text = Convert.ToString(row.Cells["Condition"].Value);
            }
        }

     

        private void condition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void location_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void rname_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {
             try
            {
                con.Open();
                SqlCommand deleteRoom = new SqlCommand("DELETE from Room where Room_id=@roomId", con);
                deleteRoom.Parameters.AddWithValue("@roomId", roomIdUpdated);
                deleteRoom.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Deleted!");
                LoadAllRoom();
                rname.Text = "";
                location.Text = "";
                condition.Text = "";
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
