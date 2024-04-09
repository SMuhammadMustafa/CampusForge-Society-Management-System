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

using System.Data.SqlClient; // Add this using directive for SqlConnection

namespace WindowsFormsApp1
{
    public partial class Formlogin : Form
    {
        public Formlogin()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        private void Formlogin_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True"; 
            string un = textBox3.Text;
            string pass = textBox2.Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM [heads] WHERE Username = @Username AND PasswordHash = @PasswordHash";

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



                        Main mainForm = new Main(un);
                        mainForm.Show();
                        this.Hide();

                    }
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormRegister loginform = new FormRegister();
            loginform.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            firstpage loginform = new firstpage();
            loginform.Show();
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
