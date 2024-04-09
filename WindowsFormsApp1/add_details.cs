using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class add_details : Form
    {
        string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";

        string username;
        public add_details(string un)
        {
            InitializeComponent();
            this.username = un;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string value1 = textBox1.Text;
            string value2 = textBox2.Text;

            string query = "UPDATE members SET namee = @value1, batch = @value2, statuss = 'pending' WHERE Username = @username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@value1", value1);
                    command.Parameters.AddWithValue("@value2", value2);
                    command.Parameters.AddWithValue("@username", username);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Details Updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            membermenu log = new membermenu(username);
            log.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void add_details_Load(object sender, EventArgs e)
        {

        }
    }
}
