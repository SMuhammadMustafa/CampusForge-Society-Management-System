using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Decide_pending : Form
    {
        private string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";

        public Decide_pending()
        {
            InitializeComponent();
            PopulateDataGridView();
        }

        private void PopulateDataGridView()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT societyname AS Society_Name, status As Status " +
                               "FROM Societies " +
                               "WHERE status = 'pending' OR status = 'not approved'";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();

                adapter.Fill(table);

                DataGridViewButtonColumn reviewButtonColumn = new DataGridViewButtonColumn();
                reviewButtonColumn.Name = "ReviewButton";
                reviewButtonColumn.HeaderText = "Review";
                reviewButtonColumn.Text = "Review";
                reviewButtonColumn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(reviewButtonColumn);

                dataGridView1.DataSource = table;
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "ReviewButton")
            {
                string societyName = dataGridView1.Rows[e.RowIndex].Cells["Society_Name"].Value.ToString();

                decide reviewForm = new decide(societyName);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT s.societyname, s.societyhead, s.societywise, s.societycontact, s.societyRules " +
                                   "FROM Societies s "  +
                                   "WHERE s.societyname = @societyName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@societyName", societyName);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        reviewForm.txtSocietyname.Text = reader["societyname"].ToString();
                        reviewForm.txtsocietyhead.Text = reader["societyhead"].ToString();
                        reviewForm.txtsocietywise.Text = reader["societywise"].ToString();
                        reviewForm.txtsocietycontact.Text = reader["societycontact"].ToString();
                        reviewForm.txtsocietyRules.Text = reader["societyRules"].ToString();
                    }
                }

                reviewForm.ShowDialog();
            }
        }






        private void Decide_pending_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            adminMenu loginForm = new adminMenu();
            loginForm.Show();
            this.Hide();
        }
    }
}
