using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Mentor_Meeting : Form
    {

        string username;
        public Mentor_Meeting(string uin)
        {
            this.username = uin;
            InitializeComponent();
        }

        private void Mentor_Meeting_Load(object sender, EventArgs e)
        {

            PopulateMeetingsDataGridView();
        }
  
        private void Member_meeting_Main_Load(object sender, EventArgs e)
        {
            PopulateMeetingsDataGridView();
        }

        private void PopulateMeetingsDataGridView()
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";

            string mentorIdQuery = "SELECT id FROM mentors WHERE username = @username";

            string societyNameQuery = "SELECT societyname FROM societies WHERE societymentor = @mentorId";

            string meetingsQuery = "SELECT Meetings.societyname, Meetings.person, Meetings.date, Meetings.time " +
                                    "FROM Meetings " +
                                    "WHERE Meetings.societyname = @societyname";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                int mentorId;
                using (SqlCommand mentorIdCommand = new SqlCommand(mentorIdQuery, connection))
                {
                    mentorIdCommand.Parameters.AddWithValue("@username", username);
                    mentorId = Convert.ToInt32(mentorIdCommand.ExecuteScalar());
                }

                string societyName;
                using (SqlCommand societyNameCommand = new SqlCommand(societyNameQuery, connection))
                {
                    societyNameCommand.Parameters.AddWithValue("@mentorId", mentorId);
                    societyName = societyNameCommand.ExecuteScalar().ToString();
                }

                using (SqlCommand meetingsCommand = new SqlCommand(meetingsQuery, connection))
                {
                    meetingsCommand.Parameters.AddWithValue("@societyname", societyName);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(meetingsCommand))
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
            
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button2_Click_1(object sender, EventArgs e)
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
