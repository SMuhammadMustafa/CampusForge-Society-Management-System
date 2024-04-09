using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Approve_Members : Form
    {
        private string connectionString = "Data Source=Strix-15\\SQLEXPRESS;Initial Catalog=users;Integrated Security=True";
        string username;
        public Approve_Members(string username)
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
                string query = "SELECT m.societyname AS Society_Name, m.username AS Username, m.statuss AS Status " +
                               "FROM members m " +
                               "WHERE m.statuss = 'pending' AND " +
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
                string ree = dataGridView1.Rows[e.RowIndex].Cells["Username"].Value.ToString(); // Assuming the column name containing username is "Username_Column_Name"

                Final_Approve reviewForm = new Final_Approve(username, ree);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT m.societyname AS Society_Name ,m.namee AS usernam, m.position AS position, m.experience AS exp " +
                                   "FROM members m " +
                                   "WHERE m.statuss = 'pending' AND " +
                                   "EXISTS (SELECT s.societyhead FROM societies s WHERE s.societyname = m.societyname AND s.societyhead = @username)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        reviewForm.txtSocietyname.Text = reader["Society_Name"].ToString();
                        reviewForm.textBox1.Text = reader["usernam"].ToString();
                        reviewForm.txtsocietyhead.Text = reader["position"].ToString();
                        reviewForm.txtsocietycontact.Text = reader["exp"].ToString();
                    }
                }

                reviewForm.ShowDialog();
            }
        }


        private void Approve_Members_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }

        private void Approve_Members_Load_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main log = new Main(username);
            log.Show();
            this.Hide();

        }
    }
}
