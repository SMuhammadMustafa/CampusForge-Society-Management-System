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
    public partial class mentor_society_view : Form
    {
        
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
           
        String user;

        public mentor_society_view(string username)
        {
            InitializeComponent();

            this.user = username;


            PopulateApprovedSocieties();
            //    MessageBox.Show(user);
        }

        private void mentor_society_view_Load(object sender, EventArgs e)
        {

        }

        private void PopulateApprovedSocieties()
        {
            int mentorID = GetMentorID();

            if (mentorID == 0)
            {
                MessageBox.Show("Mentor ID not found.");
                return;
            }

            string query = $"SELECT societyname FROM Societies WHERE status = 'Approved' AND societymentor = {mentorID};";

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching societies: {ex.Message}");
            }
        }

        private int GetMentorID()
        {
            int mentorID = 0;

            try
            {
                string query = $"SELECT id FROM mentors WHERE username = '{user}';";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        mentorID = Convert.ToInt32(reader["id"]);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching mentor ID: {ex.Message}");
            }

            return mentorID;
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
                    string mentorName = user;
                    txtsocietymentor.Text = mentorName;
                }
                reader.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mentorMenu login = new mentorMenu(user);
            login.Show();
            this.Hide();
        }
    }
}
