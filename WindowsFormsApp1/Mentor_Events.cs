using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Mentor_Events : Form
    {
        string username;

        private string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";

        public Mentor_Events(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void Mentor_Events_Load(object sender, EventArgs e)
        {
            comboBox2.Items.AddRange(new string[] { "Approve", "Completed" });
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            int mentorId = GetMentorId(username);

            if (mentorId == -1 || comboBox2.SelectedItem == null)
                return;

            string selectedStatus = comboBox2.SelectedItem.ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string societyNameQuery = "SELECT societyname FROM Societies WHERE societymentor = @mentorId";
                SqlCommand societyNameCmd = new SqlCommand(societyNameQuery, connection);
                societyNameCmd.Parameters.AddWithValue("@mentorId", mentorId);
                string societyName = (string)societyNameCmd.ExecuteScalar();

                string eventsQuery = "SELECT eventname FROM Eventss WHERE statuss = @status AND societyname = @societyName";
                SqlCommand eventsCmd = new SqlCommand(eventsQuery, connection);
                eventsCmd.Parameters.AddWithValue("@status", selectedStatus);
                eventsCmd.Parameters.AddWithValue("@societyName", societyName);
                SqlDataReader reader = eventsCmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["eventname"].ToString());
                }
                reader.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;

            string eventName = comboBox1.SelectedItem.ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string eventDetailsQuery = "SELECT societyname, eventbudget, eventdate FROM Eventss WHERE eventname = @eventName";
                SqlCommand eventDetailsCmd = new SqlCommand(eventDetailsQuery, connection);
                eventDetailsCmd.Parameters.AddWithValue("@eventName", eventName);

                SqlDataReader reader = eventDetailsCmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox3.Text = reader["societyname"].ToString();
                    textBox1.Text = eventName;
                    textBox2.Text = reader["eventbudget"].ToString();
                    textBox4.Text = reader["eventdate"].ToString();
                }
                reader.Close();
            }
        }


        private int GetMentorId(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id FROM mentors WHERE username = @username";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", username);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                return -1;
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

        private void button2_Click(object sender, EventArgs e)
        {
            mentorMenu log = new mentorMenu(username);
            log.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
