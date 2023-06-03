using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MessageBox = System.Windows.Forms.MessageBox;

namespace tugas_sbd.Forms
{
    public partial class FormPayment : Form
    {
        SqlConnection con = new SqlConnection("Data Source=AJ\\SQLEXPRESS01;Initial Catalog=gymink;Integrated Security=True");
        int paymentIdUpdated;
        public FormPayment()
        {
            InitializeComponent();
        }
        void LoadAllPayment()
        {
            con.Open();
            SqlCommand loadEnrolls = new SqlCommand("SELECT * FROM Payment ORDER BY Date DESC", con);
            SqlDataAdapter da = new SqlDataAdapter(loadEnrolls);
            DataTable dt = new DataTable();
            da.Fill(dt);
            PaymentDataGrid.DataSource = dt;
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

        void LoadDescription()
        {
            con.Open();
        
                description.Items.Add("Membership");
                description.DisplayMember = "Membership";
                description.ValueMember = "Membership";
            description.Items.Add("F&B");
            description.DisplayMember = "F&B";
            description.ValueMember = "F&B";
            description.Items.Add("Product");
            description.DisplayMember = "Product";
            description.ValueMember = "Product";
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
                SqlCommand insertPayment = new SqlCommand("INSERT INTO Payment values(@clientId,@date,@amount,@description)", con);
                insertPayment.Parameters.AddWithValue("@clientId", int.Parse(clientId.Text));
                insertPayment.Parameters.AddWithValue("@date", DateTime.Parse(date.Text));
                insertPayment.Parameters.AddWithValue("@amount", Decimal.Parse(amount.Text));
                insertPayment.Parameters.AddWithValue("@description", description.Text);
                insertPayment.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Payment created!");
                LoadAllPayment();
                clientId.Text = "";
                clientname.Text = "";
                amount.Text = "0";
                description.Text = "";
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);
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

        private void FormPayment_Load(object sender, EventArgs e)
        {
            LoadAllPayment();
            LoadClientId();
            LoadDescription();
            PaymentDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(143, 115, 235);
            PaymentDataGrid.EnableHeadersVisualStyles = false;
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

        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand updatePayment = new SqlCommand("UPDATE Payment set Client_id=@clientid,Date=@date,Amount=@amount, Description=@description where Payment_id=@paymentid", con);
                updatePayment.Parameters.AddWithValue("@paymentid", paymentIdUpdated);
                updatePayment.Parameters.AddWithValue("@clientId", int.Parse(clientId.Text));
                updatePayment.Parameters.AddWithValue("@date", DateTime.Parse(date.Text));
                updatePayment.Parameters.AddWithValue("@amount", Decimal.Parse(amount.Text));
                updatePayment.Parameters.AddWithValue("@description", description.Text);
                updatePayment.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Payment Updated!");
                LoadAllPayment();
                clientId.Text = "";
                clientname.Text = "";
                amount.Text = "0";
                description.Text = "";
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

        private void PaymentDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = PaymentDataGrid.Rows[e.RowIndex];
                paymentIdUpdated = Convert.ToInt32(row.Cells["Payment_id"].Value);
                clientId.Text = Convert.ToString(row.Cells["Client_Id"].Value);
                date.Text = Convert.ToString(row.Cells["Date"].Value);
                amount.Text = Convert.ToString(row.Cells["Amount"].Value);
                description.Text = Convert.ToString(row.Cells["Description"].Value);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand deletePayment = new SqlCommand("DELETE from Payment where Payment_id=@paymentId", con);
                deletePayment.Parameters.AddWithValue("@paymentId", paymentIdUpdated);
                deletePayment.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Deleted!");
                LoadAllPayment();
                clientId.Text = "";
                clientname.Text = "";
                amount.Text = "0";
                description.Text = "";
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
