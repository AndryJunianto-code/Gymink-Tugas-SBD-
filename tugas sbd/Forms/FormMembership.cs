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
    public partial class FormMembership : Form
    {
        SqlConnection con = new SqlConnection("Data Source=AJ\\SQLEXPRESS01;Initial Catalog=gymink;Integrated Security=True");
        int membershipIdUpdated;
        public FormMembership()
        {
            InitializeComponent();
        }
        void LoadAllMembership()
        {
            con.Open();
            SqlCommand loadEnrolls = new SqlCommand("Select Membership.Membership_id as 'Membership_ID', Membership.Client_id as 'Client_ID', Client.Fname as 'Client_Name', Membership.Start_date, Membership.Exp_date as 'Expiry_date' ,Membership.Duration as 'Duration(M)', Membership.Fee, Membership.Status\r\nFROM Membership\r\nJoin Client\r\nON Membership.Client_id = Client.Client_id\r\nGROUP BY Membership.Membership_id,Membership.Client_id,Client.Fname,Membership.Start_date,Membership.Exp_date,Membership.Duration,Membership.Fee,Membership.Status ORDER BY Expiry_date ASC;", con);
            SqlDataAdapter da = new SqlDataAdapter(loadEnrolls);
            DataTable dt = new DataTable();
            da.Fill(dt);
            MembershipDataGrid.DataSource = dt;
            con.Close();
        }
        void LoadClientId()
        {
            con.Open();
            SqlCommand loadMember = new SqlCommand("SELECT Client_id FROM Client", con);
            SqlDataReader dr = loadMember.ExecuteReader();
            while (dr.Read())
            {
                string s = dr["Client_id"].ToString();
                clientId.Items.Add(s);
                clientId.DisplayMember = s;
                clientId.ValueMember = s;
            }
            con.Close();
        }

        void LoadStatus()
        {
            con.Open();
                status.Items.Add("Active");
                status.DisplayMember = "Active";
                status.ValueMember = "Active";
            status.Items.Add("Inactive");
            status.DisplayMember = "Inactive";
            status.ValueMember = "Inactive";
            con.Close();
        }

        void LoadDuration()
        {
            List<string> months = new List<string>(){ "1 Month", "3 Months", "6 Months", "12 Months" };
            foreach(string month in months)
            {
                duration.Items.Add(month);
                duration.DisplayMember = month;
                duration.ValueMember = month;
            }
        }

        private void FormMembership_Load(object sender, EventArgs e)
        {
            LoadAllMembership();
            LoadClientId();
            LoadDuration();
            LoadStatus();
            MembershipDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(143, 115, 235);
            MembershipDataGrid.EnableHeadersVisualStyles = false;
            duration.Text = "1 Month";
            fee.Text = "10";
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
              
                SqlCommand insertEnrolls = new SqlCommand("INSERT INTO Membership values(@clientId,@startdate,@expirydate,@fee,@status,@duration)", con);
                insertEnrolls.Parameters.AddWithValue("@clientId", int.Parse(clientId.Text));
                insertEnrolls.Parameters.AddWithValue("@startdate", DateTime.Parse(startdate.Text));
                insertEnrolls.Parameters.AddWithValue("@expiryDATE", DateTime.Parse(expirtydate.Text));
                insertEnrolls.Parameters.AddWithValue("@duration", int.Parse(duration.Text.ToString().Split()[0]));
                insertEnrolls.Parameters.AddWithValue("@fee", Decimal.Parse(fee.Text));
                insertEnrolls.Parameters.AddWithValue("@status", status.Text);
                insertEnrolls.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Membership created!");
                LoadAllMembership();
                clientId.Text = "";
                duration.Text = "1 Month";
                clientname.Text = "";
                fee.Text = "10";
                status.Text = "Active";
            }
            catch (SqlException err)
            {
                if (err.Number == 2627)
                {
                    MessageBox.Show("Client already have membership");
                }
                else
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

        private void startdate_ValueChanged(object sender, EventArgs e)
        {
            var month = duration.Text.ToString().Split()[0];
            expirtydate.Text = startdate.Value.AddMonths(int.Parse(month)).ToString();
            if (month == "1")
            {
                fee.Text = "10";
            }
            else if (month == "3")
            {
                fee.Text = "20";
            }
            else if (month == "6")
            {
                fee.Text = "30";
            }
            else if (month == "12")
            {
                fee.Text = "40";
            }
        }
        private void duration_SelectedIndexChanged(object sender, EventArgs e)
        {
            var month = duration.Text.ToString().Split()[0];
            expirtydate.Text = startdate.Value.AddMonths(int.Parse(month)).ToString();
            if(month == "1")
            {
                fee.Text = "10";
            } else if(month == "3")
            {
                fee.Text = "20";
            } else if(month == "6")
            {
                fee.Text = "30";
            } else if(month == "12")
            {
                fee.Text = "40";
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand updateMembership = new SqlCommand("UPDATE Membership set Start_date=@startdate,Exp_date=@expdate,Duration=@duration, Fee=@fee, status=@status where Membership_id=@membershipid", con);
                updateMembership.Parameters.AddWithValue("@membershipid", membershipIdUpdated);
                updateMembership.Parameters.AddWithValue("@startdate", DateTime.Parse(startdate.Text));
                updateMembership.Parameters.AddWithValue("@expdate", DateTime.Parse(expirtydate.Text));
                updateMembership.Parameters.AddWithValue("@duration", int.Parse(duration.Text.ToString().Split()[0]));
                updateMembership.Parameters.AddWithValue("@fee", Decimal.Parse(fee.Text));
                updateMembership.Parameters.AddWithValue("@status", status.Text);
                updateMembership.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Membership Updated!");
                LoadAllMembership();
                clientId.Text = "";
                duration.Text = "1 Month";
                clientname.Text = "";
                fee.Text = "10";
                status.Text = "Active";
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

        private void MembershipDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = MembershipDataGrid.Rows[e.RowIndex];
                membershipIdUpdated = Convert.ToInt32(row.Cells["Membership_ID"].Value);
                clientId.Text = Convert.ToString(row.Cells["Client_ID"].Value);
                clientname.Text = Convert.ToString(row.Cells["Client_Name"].Value);
                startdate.Text = Convert.ToString(row.Cells["Start_date"].Value);
                expirtydate.Text = Convert.ToString(row.Cells["Expiry_date"].Value);
                duration.Text = Convert.ToString(row.Cells["Duration(M)"].Value) + " Months";
                fee.Text = Convert.ToString(Convert.ToDecimal(row.Cells["Fee"].Value));
            }
        }
    }
}
