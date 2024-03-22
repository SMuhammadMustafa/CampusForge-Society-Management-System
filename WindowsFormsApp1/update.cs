using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class update : Form
    {
        public update()
        {
            InitializeComponent();

            // Populate comboBox1 with approved societies
            PopulateApprovedSocieties();

            // Populate comboBox2 with heads' names
            PopulateHeadsNames();
        }

        private void PopulateApprovedSocieties()
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string query = "SELECT societyname FROM Societies WHERE status = 'Approved';";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["societyname"].ToString());
                }
                reader.Close();
            }
        }

        private void PopulateHeadsNames()
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string query = "SELECT username FROM Heads;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader["username"].ToString());
                }
                reader.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSociety = comboBox1.SelectedItem.ToString();
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string query = $"SELECT societyhead FROM Societies WHERE societyname = '{selectedSociety}';";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    txtsocietyhead.Text = reader["societyhead"].ToString();
                }
                else
                {
                    txtsocietyhead.Text = "";
                }
                reader.Close();
            }
        }


        private void txtsocietyhead_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string selectedSociety = comboBox1.SelectedItem.ToString();
            string newHead = comboBox2.SelectedItem.ToString();
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string query = $"UPDATE Societies SET societyhead = '{newHead}' WHERE societyname = '{selectedSociety}';";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Society head updated successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to update society head.");
                }
            }
            adminMenu loginForm = new adminMenu();
            loginForm.Show();
            this.Hide();
        }


        private void update_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            adminMenu loginForm = new adminMenu();
            loginForm.Show();
            this.Hide();
        }
    }
}
