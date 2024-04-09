using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class MemberDetailDisplay : Form
    {

        private string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
        string username,musername;
        public MemberDetailDisplay(string username,string musername)
        {
            this.musername = musername;  
            this.username = username;   
            InitializeComponent();



            comboBox1.Items.Add("Approve");
            comboBox1.Items.Add("Not Approve");

            comboBox2.Items.Add("Suspend");
            comboBox2.Items.Add("Not Approve");
            comboBox2.Items.Add("Approve");
        }

        private void MemberDetailDisplay_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main login = new Main(username);
            login.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string approvalStatus = comboBox1.Text.ToLower();
            string status = comboBox2.Text;

            string executiveCouncilValue = approvalStatus.Equals("approve") ? "yes" : "no";

            if (status != null)
                UpdateStatus(status, musername);

            UpdateDatabase(executiveCouncilValue, status,this.musername);

        }

        private void UpdateDatabase(string executiveCouncilValue, string status, string musername)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE members SET Executive_council = @Executive_council WHERE username = @musername";
                SqlCommand command = new SqlCommand(updateQuery, connection);

                command.Parameters.AddWithValue("@Executive_council", executiveCouncilValue);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@musername", musername);

                int rowsAffected = command.ExecuteNonQuery();

                connection.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Update successful");
                }
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UpdateStatus(string status, string musername)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (status == "Suspend")
                {
                    string updateQuery = "UPDATE members SET statuss = @status WHERE username = @musername";
                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@musername", musername);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Status updated to Suspend");
                    }
                    else
                    {
                        MessageBox.Show("No rows updated");
                    }
                }
                else if (status == "Not Approve")
                {
                    string deleteQuery = "DELETE FROM members WHERE username = @musername";
                    SqlCommand command = new SqlCommand(deleteQuery, connection);
                    command.Parameters.AddWithValue("@musername", musername);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Row deleted");
                    }
                    else
                    {
                        MessageBox.Show("No rows deleted");
                    }
                }
                else if (status == "Approve")
                {
                    Main login = new Main(username);
                    login.Show();
                    this.Hide();
                }

                connection.Close();
            }
        }



    }
}
