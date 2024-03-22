using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Decide_Pending_delete : Form
    {
        private string societyN;
        private string connectionString = "Data Source=Strix-15\\SQLEXPRESS;Initial Catalog=users;Integrated Security=True";

        public Decide_Pending_delete(string societyName)
        {
            InitializeComponent();
            societyN = societyName;
            comboBox1.Items.Add("Approve");
            comboBox1.Items.Add("Not Approve");
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if the selected value is "Approve"
            if (comboBox1.SelectedItem.ToString() == "Approve")
            {
                // Perform deletion from 'deleted_societies' table
                DeleteFromDeletedSocieties();

                // Perform deletion from 'Societies' table
                DeleteFromSocieties();

                MessageBox.Show("Deletion completed.");

               
            }
            else
            {
                DeleteFromDeletedSocieties();

                MessageBox.Show("Deletion Not Approved.");
            }
            adminMenu login = new adminMenu();
            login.Show();
            this.Hide();
        }

        private void DeleteFromDeletedSocieties()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM deleted_societies WHERE societyname = @societyName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@societyName", societyN);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting row from 'deleted_societies' table: " + ex.Message);
                }
            }
        }

        private void DeleteFromSocieties()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Societies WHERE societyname = @societyName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@societyName", societyN);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting row from 'Societies' table: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            adminMenu login = new adminMenu();
            login.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            // Handle label click event if needed
        }

        private void txtsocietyname_TextChanged(object sender, EventArgs e)
        {
            // Handle text changed event if needed
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Handle label click event if needed
        }

        private void txtReason_TextChanged(object sender, EventArgs e)
        {
            // Handle text changed event if needed
        }

        private void txtHeadname_TextChanged(object sender, EventArgs e)
        {
            // Handle text changed event if needed
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Handle label click event if needed
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Handle label click event if needed
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Handle label click event if needed
        }

        private void Decide_Pending_delete_Load(object sender, EventArgs e)
        {
            // Handle form load event if needed
        }

    }
}
