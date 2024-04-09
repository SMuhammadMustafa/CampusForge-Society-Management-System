
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{


    public partial class MemntorMemberDetails : Form
    {


        private string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
        string username, musername;
           
        public MemntorMemberDetails(string username,string musername)
        {
            InitializeComponent();
            this.musername = musername;
            this.username = username;


            comboBox2.Items.Add("Suspend");
            comboBox2.Items.Add("Not Approve");
            comboBox2.Items.Add("Approve");
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
                    string updateQuery = "UPDATE members SET Societyname = NULL, position = NULL, statuss = NULL, experience = NULL, Executive_Council = NULL WHERE username = @musername";
                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@musername", musername);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Columns updated to NULL");

                        Mentor_Member log = new Mentor_Member(username);
                        log.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("No rows updated");
                    }

                }
                else if (status == "Approve")
                {
                    Mentor_Member log = new Mentor_Member(username);
                    log.Show();
                    this.Hide();
                }

                connection.Close();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Mentor_Member login = new Mentor_Member(username);
            login.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            string status = comboBox2.Text;

            UpdateStatus(status, musername);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MemntorMemberDetails_Load(object sender, EventArgs e)
        {

        }
    }
}


