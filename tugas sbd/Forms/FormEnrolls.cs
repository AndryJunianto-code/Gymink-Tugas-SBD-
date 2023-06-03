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
    public partial class FormEnrolls : Form
    {
        SqlConnection con = new SqlConnection("Data Source=AJ\\SQLEXPRESS01;Initial Catalog=gymink;Integrated Security=True");
        int clientIdUpdated;
        int classIdUpdated;
        public FormEnrolls()
        {
            InitializeComponent();
        }
        
        void LoadAllEnrolls()
        {
            con.Open();
            SqlCommand loadEnrolls = new SqlCommand("Select Enrolls_In.Client_id as 'Client_ID', Client.Fname as 'Client_Name', Enrolls_In.Class_id as 'Class_ID', Class.Class_name as 'Class_Name', Enrolls_In.Date_joined\r\nFROM Enrolls_In\r\nJOIN Client\r\nON Enrolls_In.Client_id = Client.Client_id\r\nJoin Class\r\nON Enrolls_In.Class_id = Class.Class_id\r\nGROUP BY Enrolls_In.Client_id, Client.Fname, Enrolls_In.Class_id, Class.Class_name,Enrolls_In.Date_joined;", con);
            SqlDataAdapter da = new SqlDataAdapter(loadEnrolls);
            DataTable dt = new DataTable();
            da.Fill(dt);
            enrollsDataGrid.DataSource = dt;
            con.Close();
        }

        void LoadClientId()
        {
            con.Open();
            SqlCommand loadClient = new SqlCommand("SELECT Client_id FROM Client", con);
            SqlDataReader dr = loadClient.ExecuteReader();
            while (dr.Read())
            {
                string s = dr["Client_id"].ToString();
                clientId.Items.Add(s);
                clientId.DisplayMember = s;
                clientId.ValueMember = s;
            }
            con.Close();
        }
        void LoadClassId()
        {
            con.Open();
            SqlCommand loadClass = new SqlCommand("SELECT Class_id FROM Class", con);
            SqlDataReader dr = loadClass.ExecuteReader();
            while (dr.Read())
            {
                string s = dr["Class_id"].ToString();
                classId.Items.Add(s);
                classId.DisplayMember = s;
                classId.ValueMember = s;
            }
            con.Close();
        }

        private void clientId_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            string q = "select Fname,Lname from Client where Client_id = '" + clientId.SelectedItem + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                clientname.Text = dr["Fname"].ToString() + " " + dr["Lname"].ToString();
            }
            con.Close();
        }

        private void classId_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            string q = "select Class_name from Class where Class_id = '" + classId.SelectedItem + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                classname.Text = dr["Class_name"].ToString();
            }
            con.Close();
        }

        private void FormEnrolls_Load(object sender, EventArgs e)
        {
            LoadAllEnrolls();
            LoadClientId();
            LoadClassId();
            enrollsDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(143, 115, 235);
            enrollsDataGrid.EnableHeadersVisualStyles = false;
        }

        private void create_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                if (clientId.Text == "")
                {
                    MessageBox.Show("Please enter a client");
                    con.Close();
                    return;
                }
                if (classId.Text == "")
                {
                    MessageBox.Show("Please enter a class");
                    con.Close();
                    return;
                }
                SqlCommand insertEnrolls = new SqlCommand("INSERT INTO Enrolls_In values(@clientId,@classId,@datejoined)", con);
                insertEnrolls.Parameters.AddWithValue("@clientId", clientId.Text);
                insertEnrolls.Parameters.AddWithValue("@classId", classId.Text);
                insertEnrolls.Parameters.AddWithValue("@datejoined", DateTime.Parse(datejoined.Text));
                insertEnrolls.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Client has joined the class!");
                LoadAllEnrolls();   
                clientId.Text = "";
                classId.Text = "";
                clientname.Text = "";
                classname.Text = "";
            }
            catch (SqlException err)
            {
                if(err.Number == 2627)
                {
                    MessageBox.Show("Client already inside this class");
                } else
                {
                    MessageBox.Show(err.Message);
                }
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
                SqlCommand updateEnrolls = new SqlCommand("UPDATE Enrolls_In set Client_id=@clientId,Class_id=@classId,Date_joined=@date where Client_id=@clientIdUpdated AND Class_id=@classIdUpdated", con);
                updateEnrolls.Parameters.AddWithValue("@clientIdUpdated", clientIdUpdated);
                updateEnrolls.Parameters.AddWithValue("@classIdUpdated", classIdUpdated);
                updateEnrolls.Parameters.AddWithValue("@clientId", int.Parse(clientId.Text));
                updateEnrolls.Parameters.AddWithValue("@classId", int.Parse(classId.Text));
                updateEnrolls.Parameters.AddWithValue("@date", DateTime.Parse(datejoined.Text));
                updateEnrolls.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Updated!");
                LoadAllEnrolls();
                clientId.Text = "";
                classId.Text = "";
                clientname.Text = "";
                classname.Text = "";
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

        private void enrollsDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = enrollsDataGrid.Rows[e.RowIndex];
                clientIdUpdated = Convert.ToInt32(row.Cells["Client_ID"].Value);
                classIdUpdated = Convert.ToInt32(row.Cells["Class_ID"].Value);
                clientId.Text = Convert.ToString(row.Cells["Client_ID"].Value);
                classId.Text = Convert.ToString(row.Cells["Class_ID"].Value);
                datejoined.Text = Convert.ToString(row.Cells["Date_joined"].Value);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand deleteEnrolls = new SqlCommand("DELETE from Enrolls_In where Client_id=@clientId AND Class_id=@classId", con);
                deleteEnrolls.Parameters.AddWithValue("@clientId", int.Parse(clientId.Text));
                deleteEnrolls.Parameters.AddWithValue("@classId", int.Parse(classId.Text));
                deleteEnrolls.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Deleted!");
                LoadAllEnrolls();
                clientId.Text = "";
                classId.Text = "";
                clientname.Text = "";
                classname.Text = "";
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
