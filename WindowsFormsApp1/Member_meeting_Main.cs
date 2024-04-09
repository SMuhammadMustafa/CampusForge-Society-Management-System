using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Member_meeting_Main : Form
    {
        string username;

        public Member_meeting_Main(string uin)
        {
            this.username = uin;
            InitializeComponent();
        }

        private void Member_meeting_Main_Load(object sender, EventArgs e)
        {
            PopulateMeetingsDataGridView();
        }

        private void PopulateMeetingsDataGridView()
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string query = "SELECT Meetings.societyname, Meetings.person, Meetings.date, Meetings.time " +
                           "FROM Members JOIN Meetings ON Members.position = Meetings.person " +
                           "WHERE Members.username = @username AND Members.statuss = 'approve'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            // Implement hover effect if needed
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            // Implement hover effect if needed
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell content click event if needed
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
