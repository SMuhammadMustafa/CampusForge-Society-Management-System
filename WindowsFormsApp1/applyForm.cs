using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace WindowsFormsApp1
{
    public partial class applyForm : Form
    {
        string username;
        public applyForm(string username)
        {
            InitializeComponent();
            PopulateComboBox();
            PopulateComboBox2();
            this.username = username;
;
        }

        private void PopulateComboBox()
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string query = "SELECT societyname FROM societyrecruitment";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["societyname"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("No societies found.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    membermenu login = new membermenu(username);
                    login.Show();
                    this.Hide();
                }

                reader.Close();
            }
        }

        private void PopulateComboBox2()
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string query = "SELECT secretary, Dept_Head, member FROM societyrecruitment";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader["secretary"] != DBNull.Value && (bool)reader["secretary"])
                    {
                        comboBox2.Items.Add("Secretary");
                    }

                    if (reader["Dept_Head"] != DBNull.Value && (bool)reader["Dept_Head"])
                    {
                        comboBox2.Items.Add("Department Head");
                    }

                    if (reader["member"] != DBNull.Value && (bool)reader["member"])
                    {
                        comboBox2.Items.Add("Member");
                    }
                }

                reader.Close();
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string selectedSociety = comboBox1.SelectedItem.ToString();
            string selectedPosition = comboBox2.SelectedItem.ToString();
            string experience = textBox1.Text;

            string query = "UPDATE members SET Societyname = @Societyname, position = @Position, experience = @Experience WHERE username = @Username AND position IS NULL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Societyname", selectedSociety);
                command.Parameters.AddWithValue("@Position", selectedPosition);
                command.Parameters.AddWithValue("@Experience", experience);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Row updated successfully.");
                }
                else
                {
                    MessageBox.Show("Your Application Already Exists.");
                }
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void applyForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            membermenu log = new membermenu(username);
            log.Show();
            this.Hide();
        }
    }
}
