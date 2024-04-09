using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;



namespace WindowsFormsApp1
{
    public partial class Mentor_Member : Form
    {

        private string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";

        string un;
        public Mentor_Member(string usernmae)
        {
            this.un = usernmae;
            InitializeComponent();
            PopulateDataGridView();
        }

        private int getmentorID()
        {
            string query = "SELECT id FROM mentors WHERE username = @un";

            int mentorID;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@un", un);

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    mentorID = Convert.ToInt32(result);
                    return mentorID;
                }
                else
                {
                    return 1;
                }
            }
        }


        private void PopulateDataGridView()
        {
            dataGridView1.Columns.Clear();

            int mentorID = getmentorID();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = "SELECT m.societyname AS Society_Name, m.statuss AS Status " +
                                "FROM members m " +
                                "WHERE m.statuss = 'Approve' OR m.statuss = 'Suspend' AND " +
                                "EXISTS (SELECT s.societyhead FROM societies s " +
                                "WHERE s.societyname = m.societyname " +
                                "AND s.societymentor = @mentorID)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", un);
                command.Parameters.AddWithValue("@mentorID", mentorID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();

                adapter.Fill(table);

                if (dataGridView1.Columns["ReviewButton"] == null)
                {
                    DataGridViewButtonColumn reviewButtonColumn = new DataGridViewButtonColumn();
                    reviewButtonColumn.Name = "ReviewButton";
                    reviewButtonColumn.HeaderText = "Review";
                    reviewButtonColumn.Text = "Review";
                    reviewButtonColumn.UseColumnTextForButtonValue = true;
                    dataGridView1.Columns.Add(reviewButtonColumn);
                }

                dataGridView1.DataSource = table;
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "ReviewButton")
            {
                string societyName = dataGridView1.Rows[e.RowIndex].Cells["Society_Name"].Value.ToString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    int mentorID = getmentorID();

                    string query = "SELECT m.societyname AS Society_Name, m.username As unmae, m.namee AS username, m.position AS position, m.experience AS exp, m.Executive_Council as Ec " +
                                   "FROM members m " +
                                   "WHERE m.statuss = 'Approve'  Or m.statuss = 'Suspend' AND " +
                                   "EXISTS (SELECT s.societyhead FROM societies s WHERE s.societyname = m.societyname AND s.societymentor = @mentorID)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@mentorID", mentorID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            string memberUsername = reader["unmae"].ToString();
                            MemntorMemberDetails reviewForm = new MemntorMemberDetails(un, memberUsername);

                            if (!reader.IsDBNull(reader.GetOrdinal("Ec")))
                            {
                                reviewForm.textBox2.Text = reader["Ec"].ToString();
                            }
                            else
                            {
                                reviewForm.textBox2.Text = "NONE";
                            }
                            reviewForm.textBox1.Text = memberUsername;
                            reviewForm.txtsocietyhead.Text = reader["position"].ToString();
                            reviewForm.txtsocietycontact.Text = reader["exp"].ToString();

                            reviewForm.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mentorMenu log = new mentorMenu(un);
            log.Show();
            this.Hide();
        }

        private void Mentor_Member_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}


