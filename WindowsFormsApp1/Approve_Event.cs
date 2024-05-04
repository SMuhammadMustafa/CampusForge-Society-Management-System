using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Approve_Event : Form
    {
        string username;
        string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";

        public Approve_Event(string username)
        {
            this.username = username;
            InitializeComponent();
            PopulateEventComboBox();
            PopulateApprovalComboBox();
        }

        private void PopulateApprovalComboBox()
        {
            comboBox2.Items.Add("Approve");
            comboBox2.Items.Add("Not Approve");
        }


        private void PopulateEventComboBox()
        {
            int mentorId = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT id FROM mentors WHERE username = @username", conn);
                cmd.Parameters.AddWithValue("@username", username);

                mentorId = (int)cmd.ExecuteScalar();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT e.eventname FROM Eventss e JOIN Societies s ON s.societyname = e.societyname WHERE s.societymentor = @mentorId AND e.Statuss IS NULL", conn);
                cmd.Parameters.AddWithValue("@mentorId", mentorId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["eventname"].ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select an event.");
                return;
            }

            string selectedEvent = comboBox1.SelectedItem.ToString();
            string newStatus = comboBox2.SelectedItem.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Eventss SET Statuss = @newStatus WHERE eventname = @eventName", conn);
                cmd.Parameters.AddWithValue("@newStatus", newStatus);
                cmd.Parameters.AddWithValue("@eventName", selectedEvent);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Event status updated successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to update event status.");
                }
            }

            comboBox1.Items.Clear();
            PopulateEventComboBox();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedEvent = comboBox1.SelectedItem.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT e.societyname, e.eventbudget, e.eventdate,e.eventname FROM Eventss e WHERE e.eventname = @eventName", conn);
                cmd.Parameters.AddWithValue("@eventName", selectedEvent);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["eventname"].ToString();
                    textBox3.Text = reader["societyname"].ToString();
                    textBox2.Text = reader["eventbudget"].ToString();
                    textBox4.Text = reader["eventdate"].ToString();
                }
            }
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Approve_Event_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            mentorMenu log = new mentorMenu(username);
            log.Show(); 
            this.Hide();
        }
    }
}
