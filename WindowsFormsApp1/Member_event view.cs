using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Member_event_view : Form
    {
        string username;
        string connectionString = "Data Source=Strix-15\\SQLEXPRESS;Initial Catalog=UsersSE;Integrated Security=True";

        public Member_event_view(string u)
        {
            username = u;
            InitializeComponent();
            PopulateEventComboBox();
        }

        private void PopulateEventComboBox()
        {
            string societyname = GetMemberSocietyName(username);

            if (societyname != "")
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT eventname FROM Eventss WHERE societyname = @societyname AND statuss  = 'approve'", conn);
                    cmd.Parameters.AddWithValue("@societyname", societyname);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["eventname"].ToString());
                    }
                }
            }
            else if (societyname == "")
            {
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT eventname FROM Eventss WHERE statuss = 'approve'", conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["eventname"].ToString());
                    }
                }
            }
        }


        private string GetMemberSocietyName(string username)
        {
            string societyname = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Societyname FROM members WHERE username = @username", conn);
                cmd.Parameters.AddWithValue("@username", username);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    societyname = reader["Societyname"].ToString();
                }
                else
                {
                    societyname = null; // Set societyname to null if username is not found
                }
            }


            return societyname;
        }


        private void Member_event_view_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedEvent = comboBox1.SelectedItem.ToString();
            string societyname = GetMemberSocietyName(username);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd;
                if (string.IsNullOrEmpty(societyname))
                {
                    cmd = new SqlCommand("SELECT societyname, eventdate, eventname FROM Eventss WHERE eventname = @eventName", conn);
                }
                else
                {
                    cmd = new SqlCommand("SELECT societyname, eventdate, eventname FROM Eventss WHERE eventname = @eventName AND societyname = @societyname", conn);
                    cmd.Parameters.AddWithValue("@societyname", societyname);
                }
                cmd.Parameters.AddWithValue("@eventName", selectedEvent);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["eventname"].ToString();
                    textBox3.Text = reader["societyname"].ToString();
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            membermenu log = new membermenu(username);
            log.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

}
