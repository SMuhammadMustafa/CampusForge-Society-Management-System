using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class member_sigup : Form
    {
        public member_sigup()
        {
            InitializeComponent();
            txtcompassword.PasswordChar = '*';
            txtpassword.PasswordChar = '*';
            this.Load += member_sigup_Load; 
        }

        private void member_sigup_Load(object sender, EventArgs e)
        {
        }


        private void label6_Click_1(object sender, EventArgs e)
        {
            Formlogin loginForm = new Formlogin();
            loginForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            firstpage loginForm = new firstpage();
            loginForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (txtusername.Text == "" && txtcompassword.Text == "" && txtpassword.Text == "")
            {
                MessageBox.Show("Please Fill All Fields", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtpassword.Text != txtcompassword.Text)
            {
                MessageBox.Show("Password Fields Do Not Match", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
                string username = txtusername.Text;
                string password = txtpassword.Text;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string queryInsertUser = "INSERT INTO members (username, namee, passwordHash, batch, position, Executive_Council, Societyname, statuss, experience) " +
                        "VALUES (@Username, NULL, @PasswordHash, NULL, NULL, NULL, NULL, NULL, NULL)";

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
            memberlogin log = new memberlogin();
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
