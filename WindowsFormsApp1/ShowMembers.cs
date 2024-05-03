using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class ShowMembers : Form
    {
        private string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
        string username;
        public ShowMembers(string username)
        {
            this.username = username;

            InitializeComponent();
            PopulateDataGridView();
        }

        private void PopulateDataGridView()
        {
            dataGridView1.Columns.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT m.societyname AS Society_Name, m.statuss AS Status, m.username AS Username " +
                               "FROM members m " +
                               "WHERE (m.statuss = 'Approve' OR m.statuss = 'Suspend') AND " +
                               "EXISTS (SELECT s.societyhead FROM societies s WHERE s.societyname = m.societyname AND s.societyhead = @username)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
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
                string usernam = dataGridView1.Rows[e.RowIndex].Cells["Username"].Value.ToString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT m.namee AS name, m.position AS position, m.experience AS experience " +
                                   "FROM members m " +
                                   "WHERE m.societyname = @societyName AND m.username = @usernam";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@societyName", societyName);
                    command.Parameters.AddWithValue("@usernam", usernam);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        MemberDetailDisplay reviewForm = new MemberDetailDisplay(username, usernam);
                        reviewForm.textBox1.Text = usernam;
                        reviewForm.txtsocietyhead.Text = reader["position"].ToString();
                        reviewForm.txtsocietycontact.Text = reader["experience"].ToString();
                        reviewForm.ShowDialog();
                    }
                }
            }
        }




        private void ShowMembers_Load(object sender, EventArgs e)
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
    }
}
