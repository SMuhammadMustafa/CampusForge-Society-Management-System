using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;



namespace WindowsFormsApp1
{
    public partial class Final_Approve : Form
    {
        private string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";


        string username = null;

        string musername = null;
        public Final_Approve(string usernmae,string musername)
        {
            username = usernmae;
            this.musername = musername;
            InitializeComponent();

            comboBox1.Items.Add("Approve");
            comboBox1.Items.Add("Not Approve");

            comboBox1.SelectedIndex = 0;
        }

        private void Final_Approve_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Approve_Members login = new Approve_Members(username);
            login.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a status.");
                return;
            }

            string selectedStatus = comboBox1.SelectedItem.ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE members SET statuss = @Status WHERE username = @musername";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status", selectedStatus);
                command.Parameters.AddWithValue("@musername", musername); 
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Member updated successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to update status.");
                }
            }
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
