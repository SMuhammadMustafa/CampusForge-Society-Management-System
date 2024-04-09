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
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True"; 
            string un = textBox3.Text;
            string pass = textBox2.Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM [Admin] WHERE Username = @Username AND PasswordHash = @PasswordHash";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", un);
                    cmd.Parameters.AddWithValue("@PasswordHash", pass); 

                    int count = (int)cmd.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("No such username found Please Check USer Name and Password"); 
                    }
                    else
                    {
                        MessageBox.Show("Successfully logged in!");
                        adminMenu login = new adminMenu();
                        login.Show();
                        this.Hide();
                    }
                }
            }

        }

        private void AdminLogin_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            firstpage loginForm = new firstpage();
            loginForm.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0'; 
                Timer timer = new Timer();
                timer.Interval = 1000; 
                timer.Tick += (s, args) =>
                {
                    textBox2.PasswordChar = '*'; 
                    timer.Stop(); 
                    timer.Dispose();
                    checkBox1.Checked = false; 
                };
                timer.Start();
            }
            else
            {
                textBox2.PasswordChar = '*'; 
            }
        }
    }
}
