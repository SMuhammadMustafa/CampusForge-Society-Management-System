using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Add_Event : Form
    {
        string username;
        string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";

        public Add_Event(string a)
        {
            username = a;
            InitializeComponent();
            PopulateSocietyComboBox();
        }

        private void PopulateSocietyComboBox()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT societyname FROM Societies WHERE societyhead = @username", conn);
                cmd.Parameters.AddWithValue("@username", username);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["societyname"].ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string eventName = textBox1.Text;
            DateTime eventDate = dateTimePicker1.Value;

            if (eventDate < DateTime.Now.Date)
            {
                MessageBox.Show("Event date cannot be in the past.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Eventss WHERE eventname = @eventName", conn);
                cmd.Parameters.AddWithValue("@eventName", eventName);

                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Event with the same name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Eventss (eventname, societyname, eventbudget, eventdate) VALUES (@eventName, @societyName, @eventBudget, @eventDate)", conn);
                cmd.Parameters.AddWithValue("@eventName", eventName);
                cmd.Parameters.AddWithValue("@societyName", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@eventBudget", textBox2.Text);
                cmd.Parameters.AddWithValue("@eventDate", dateTimePicker1.Value);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Event added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to add event.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        { 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main log = new Main(username);
            log.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Add_Event_Load(object sender, EventArgs e)
        {

        }
    }
}
