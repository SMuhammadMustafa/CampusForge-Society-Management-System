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
    public partial class RegisterSociety : Form
    {
        private string _username;

        public RegisterSociety(string username)
        {
            InitializeComponent();
            _username = username;
            txtsocietyhead.Text = _username; 
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSocietyname.Text) ||
                string.IsNullOrWhiteSpace(txtsocietyhead.Text) ||
                string.IsNullOrWhiteSpace(txtsocietywise.Text) ||
                string.IsNullOrWhiteSpace(txtsocietycontact.Text) ||
                string.IsNullOrWhiteSpace(txtsocietyRules.Text))
            {
                MessageBox.Show("Please fill all fields and select a mentor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();


                    string createTableQuery = @"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Societies')
                                        BEGIN
                                            CREATE TABLE Societies (
                                                societyname NVARCHAR(100) PRIMARY KEY,
                                                societyhead VARCHAR(100) FOREIGN KEY REFERENCES heads(username),
                                                societywise NVARCHAR(255),
                                                societymentor INT ,
                                                societycontact NVARCHAR(100),
                                                societyRules NVARCHAR(MAX),
                                                status NVARCHAR(50) DEFAULT 'Pending'
                                            )
                                        END";

                    using (SqlCommand cmdCreateTable = new SqlCommand(createTableQuery, conn))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    string insertQuery = "INSERT INTO Societies (societyname, societyhead, societywise, societymentor, societycontact, societyRules) VALUES (@societyname, @societyhead, @societywise, @societymentor, @societycontact, @societyRules)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@societyname", txtSocietyname.Text);
                        cmd.Parameters.AddWithValue("@societyhead", txtsocietyhead.Text);
                        cmd.Parameters.AddWithValue("@societywise", txtsocietywise.Text);
                        cmd.Parameters.AddWithValue("@societymentor", "");
                        cmd.Parameters.AddWithValue("@societycontact", txtsocietycontact.Text);
                        cmd.Parameters.AddWithValue("@societyRules", txtsocietyRules.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Your Application inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert data. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                Main loginForm = new Main(_username);
                loginForm.Show();
                this.Hide();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) 
                {
                    MessageBox.Show("Society name already exists. Please change the society name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }





        private void txtSocietyname_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegisterSociety_Load(object sender, EventArgs e)
        {
           
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtsocietycontact.Text = "";
            txtsocietyhead.Text = "";
            txtSocietyname.Text = "";
            txtsocietyRules.Text = "";
            txtsocietywise.Text = "";
        }

        private void txtsocietyhead_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtsocietycontact_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtsocietywise_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtsocietyRules_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Main loginForm = new Main(_username);
            loginForm.Show();
            this.Hide();
        }
    }
    public class MentorItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public MentorItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

}
