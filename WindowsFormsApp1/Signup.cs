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
        public FormRegister()
        {
            InitializeComponent();
            txtcompassword.PasswordChar = '*';
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
            if (txtusername.Text == "" || txtcompassword.Text == "" || txtpassword.Text == "")
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
                string batch = "batch1"; 

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string queryInsertUser = "INSERT INTO Heads (username, passwordHash, batch) VALUES (@Username, @PasswordHash, @Batch)";

                    using (SqlCommand cmdInsertUser = new SqlCommand(queryInsertUser, conn))
                    {
                        cmdInsertUser.Parameters.AddWithValue("@Username", username);
                        cmdInsertUser.Parameters.AddWithValue("@PasswordHash", password);
                        cmdInsertUser.Parameters.AddWithValue("@Batch", batch);

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
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            txtusername.Text = "";
            txtpassword.Text = "";
            txtcompassword.Text = "";
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

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
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
    }
}

