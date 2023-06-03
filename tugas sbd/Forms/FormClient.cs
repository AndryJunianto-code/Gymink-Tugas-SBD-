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
    public partial class FormClient : Form
    {
        SqlConnection con = new SqlConnection("Data Source=AJ\\SQLEXPRESS01;Initial Catalog=gymink;Integrated Security=True");
        int clientIdUpdated;
        public FormClient()
        {
            InitializeComponent();
        }

       void LoadAllClient()
        {
            con.Open();
            SqlCommand loadClients = new SqlCommand("SELECT Client_id as ID, Fname as First_Name,Lname as Last_Name,Age,Sex,Date_joined,Address,Phone,Status from client", con);
            SqlDataAdapter da = new SqlDataAdapter(loadClients);
            DataTable dt = new DataTable();
            da.Fill(dt);
            clientDataGrid.DataSource = dt;
            con.Close();
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            LoadAllClient();
            LoadStatus();
            clientDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(143,115,235);
            clientDataGrid.EnableHeadersVisualStyles = false;

        }

        void LoadStatus()
        {
            status.Items.Add("Active");
            status.DisplayMember = "Active";
            status.ValueMember = "Active";
            status.Items.Add("Inactive");
            status.DisplayMember = "Inactive";
            status.ValueMember = "Inactive";
        }


        private void create_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sex;
                if(male.Checked == true)
                {
                    sex = "M";
                } else
                {
                    sex = "F";
                }
                if(fname.Text == "")
                {
                    MessageBox.Show("Please enter a first name");
                    con.Close();
                    return;
                }
                SqlCommand insertClient = new SqlCommand("INSERT INTO Client values(@fname,@lname,@age,@sex,@datejoined,@address,@phone,@status)", con);
                insertClient.Parameters.AddWithValue("@fname", fname.Text);
                insertClient.Parameters.AddWithValue("@lname", lname.Text);
                insertClient.Parameters.AddWithValue("@age", int.Parse(age.Text));
                insertClient.Parameters.AddWithValue("@sex", sex);
                insertClient.Parameters.AddWithValue("@datejoined", DateTime.Parse(datejoined.Text));
                insertClient.Parameters.AddWithValue("@address", address.Text);
                insertClient.Parameters.AddWithValue("@phone", phone.Text);
                insertClient.Parameters.AddWithValue("@status", status.Text);
                insertClient.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("New client has been created!");
                LoadAllClient();
                fname.Text = "";
                lname.Text = "";
                address.Text = "";
                phone.Text = "";
            } catch (SqlException err)
            {
                MessageBox.Show(err.ToString());
            } catch (FormatException)
            {
                MessageBox.Show("Wrong input format");
            } finally
            {
                con.Close();
            }
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
                
                SqlCommand updateClient = new SqlCommand("UPDATE Client set Fname=@Fname,Lname=@Lname,Age=@Age,Sex=@Sex,Date_joined=@Date_joined,Address=@Address,Phone=@Phone,Status=@Status where Client_id=@Client_id", con);
                updateClient.Parameters.AddWithValue("@Client_id", clientIdUpdated);
                updateClient.Parameters.AddWithValue("@Fname", fname.Text);
                updateClient.Parameters.AddWithValue("@Lname", lname.Text);
                updateClient.Parameters.AddWithValue("@Age", int.Parse(age.Text));
                updateClient.Parameters.AddWithValue("@Sex", sex);
                updateClient.Parameters.AddWithValue("@Date_joined", DateTime.Parse(datejoined.Text));
                updateClient.Parameters.AddWithValue("@Address", address.Text);
                updateClient.Parameters.AddWithValue("@Phone", phone.Text);
                updateClient.Parameters.AddWithValue("@Status", status.Text);
                updateClient.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Client has been updated!");
                LoadAllClient();
                fname.Text = "";
                lname.Text = "";
                address.Text = "";
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

        private void clientDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = clientDataGrid.Rows[e.RowIndex];
                clientIdUpdated = Convert.ToInt32(row.Cells["ID"].Value);
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
                datejoined.Text = Convert.ToString(row.Cells["Date_joined"].Value);
                address.Text = Convert.ToString(row.Cells["Address"].Value);
                phone.Text = Convert.ToString(row.Cells["Phone"].Value);
                status.Text = Convert.ToString(row.Cells["Status"].Value);
            }
        }
    }
}
