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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class head_details_View : Form
    {
        private string username;
        public head_details_View(string u)
        {
            username = u;
            InitializeComponent();

            PopulateApprovedSocieties();

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }


        private void head_details_View_Load(object sender, EventArgs e)
        {
        }

        private void PopulateApprovedSocieties()
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string query = $"SELECT societyname FROM Societies WHERE status = 'Approved' AND societyhead = '{username}';";

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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSociety = comboBox1.SelectedItem.ToString();
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string query = $"SELECT societyhead, societycontact, societywise, societyRules, societymentor FROM Societies WHERE societyname = '{selectedSociety}' AND status = 'Approved';";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    txtsocietyhead.Text = reader["societyhead"].ToString();
                    txtsocietycontact.Text = reader["societycontact"].ToString();
                    txtsocietywise.Text = reader["societywise"].ToString();
                    txtsocietyRules.Text = reader["societyRules"].ToString();

                    int mentorId = Convert.ToInt32(reader["societymentor"]);
                    string mentorName = GetMentorName(mentorId, connectionString);
                    txtsocietymentor.Text = mentorName;
                }
                reader.Close();
            }
        }


        private string GetMentorName(int mentorId, string connectionString)
        {
            string query = $"SELECT username FROM mentors WHERE id = {mentorId};";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return reader["username"].ToString();
                }
                return "";
            }
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

        private void admin_Society_details_Load(object sender, EventArgs e)
        {

        }

        private void txtsocietymentor_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main loginForm = new Main(username);
            loginForm.Show();
            this.Hide();
        }

        private void txtsocietyhead_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
