using Azure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using System.Data.SqlClient; 

namespace WindowsFormsApp1
{
    
    public partial class FormRegister : Form
    {
        string username;
        public FormRegister(string username)
        {
            this.username = username;
            InitializeComponent();
            txtpassword.PasswordChar = '*';
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }



        private void label6_Click(object sender, EventArgs e)
        {
            
            
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text;
            string password = txtpassword.Text;
            string phoneNumber = textBox2.Text;
            string email = textBox1.Text;
            string batch = txtcompassword.Text;



            int currentYear = DateTime.Now.Year;

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

            if (password.Contains(" "))
                {
                    MessageBox.Show("Password cannot contain spaces.", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(batch) || string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please fill all fields", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (!IsPhoneNumberValid(phoneNumber))
            {
                MessageBox.Show("Invalid phone number. It should contain only numbers and have a length of 11 digits.", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsEmailValid(email))
            {
                MessageBox.Show("Invalid email address.", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (!int.TryParse(batch, out int parsedInt))
            {
                MessageBox.Show("Batch year must be a valid number.", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (parsedInt < currentYear - 5 || parsedInt > currentYear)
            {
                MessageBox.Show("Batch year must be within the range of (current year - 5) to current year.", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string queryInsertUser = "INSERT INTO Heads (username, passwordHash, phoneNumber, email, batch) VALUES (@Username, @PasswordHash, @PhoneNumber, @Email, @Batch)";

                using (SqlCommand cmdInsertUser = new SqlCommand(queryInsertUser, conn))
                {
                    cmdInsertUser.Parameters.AddWithValue("@Username", username);
                    cmdInsertUser.Parameters.AddWithValue("@PasswordHash", password);
                    cmdInsertUser.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmdInsertUser.Parameters.AddWithValue("@Email", email);
                    cmdInsertUser.Parameters.AddWithValue("@Batch", batch);

                    try
                    {
                        int rowsAffected = cmdInsertUser.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User registered successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to register user. Please try again.");
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
                            MessageBox.Show("An error occurred while registering user: " + ex.Message, "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private bool IsPhoneNumberValid(string phoneNumber)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\d{11}$");
        }

        private bool IsEmailValid(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z]{2,}$");
        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            txtusername.Text = "";
            txtpassword.Text = "";
            txtcompassword.Text = "";
        }

        private void label6_Click_1(object sender, EventArgs e)
        {
            Formlogin loginForm = new Formlogin(username);
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

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtpassword.PasswordChar = '\0';
                Timer timer = new Timer();
                timer.Interval = 1000;
                timer.Tick += (s, args) =>
                {

                    txtpassword.PasswordChar = '*';
                    timer.Stop();
                    timer.Dispose();
                    checkbxShowPas.Checked = false;
                };
                timer.Start();
            }
            else
            {
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_3(object sender, EventArgs e)
        {

        }

        private void txtcompassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

