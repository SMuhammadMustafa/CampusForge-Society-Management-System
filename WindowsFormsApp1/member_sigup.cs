using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class member_sigup : Form
    {
        string username;
        public member_sigup(string username)
        {
            this.username = username;
            InitializeComponent();
            txtcompassword.PasswordChar = '*';
            txtpassword.PasswordChar = '*';
            this.Load += member_sigup_Load; 
        }

        private void member_sigup_Load(object sender, EventArgs e)
        {
        }


       

        private void button3_Click_1(object sender, EventArgs e)
        {

            string username = txtusername.Text;
            string password = txtpassword.Text;
            string confirmPassword = txtcompassword.Text;

            if (username.Length < 3 || username.Length > 10)
            {
                MessageBox.Show("Username must be between 3 and 10 characters long.", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password.Length < 5 || password.Length > 10)
            {
                MessageBox.Show("Password must be between 5 and 10 characters long.", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtusername.Text == "" || txtcompassword.Text == "" || txtpassword.Text == "")
            {
                MessageBox.Show("Please Fill All Fields", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Password Fields Do Not Match", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            {
                string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
                

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string queryInsertUser = "INSERT INTO members (username, namee, passwordHash, batch, position, Executive_Council, Societyname, statuss, experience) " +
                        "VALUES (@Username, NULL, @PasswordHash, NULL, NULL, NULL, NULL, NULL, NULL)";

                    try
                    {
                        using (SqlCommand cmdInsertUser = new SqlCommand(queryInsertUser, conn))
                        {
                            cmdInsertUser.Parameters.AddWithValue("@Username", username);
                            cmdInsertUser.Parameters.AddWithValue("@PasswordHash", password);

                            int rowsAffected = cmdInsertUser.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Member registered successfully!");
                            }
                            else
                            {
                                MessageBox.Show("Failed to register user. Please try again.");
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627) 
                        {
                            MessageBox.Show("Username already exists. Please choose a different username.", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("An error occurred. Please try again.", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            txtusername.Text = "";
            txtpassword.Text = "";
            txtcompassword.Text = "";
        }

        private void checkbxShowPas_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtcompassword.PasswordChar = '\0';
                txtpassword.PasswordChar = '\0';
                Timer timer = new Timer();
                timer.Interval = 1000;
                timer.Tick += (s, args) =>
                {
                    txtpassword.PasswordChar = '*';
                    txtcompassword.PasswordChar = '*';
                    timer.Stop();
                    timer.Dispose();
                    checkbxShowPas.Checked = false;
                };
                timer.Start();
            }
            else
            {
                txtcompassword.PasswordChar = '*';
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Formlogin log = new Formlogin(username);
            log.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            firstpage log = new firstpage();
            log.Show();
            this.Hide();
        }
    }
}
