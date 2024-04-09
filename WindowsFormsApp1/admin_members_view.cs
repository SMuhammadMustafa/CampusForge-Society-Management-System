using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class admin_members_view : Form
    {
        private string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
        string username;

        public admin_members_view()
        {
            InitializeComponent();
            PopulateComboBox();
            PopulateDataGridView();
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }


        private void PopulateComboBox()
        {
            comboBox1.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT societyname FROM societies WHERE status = 'Approved'";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string societyName = reader["societyname"].ToString();
                    comboBox1.Items.Add(societyName);
                }

            }

           
        }

        private void PopulateDataGridView()
        {
            dataGridView1.Columns.Clear();

            if (comboBox1.SelectedItem == null)
            {
                return;
            }

            string societyName = comboBox1.SelectedItem.ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT m.societyname AS Society_Name, m.statuss AS Status " +
                               "FROM members m " +
                               "WHERE m.societyname = @societyName AND (m.statuss = 'Approve' OR m.statuss = 'Suspend') AND " +
                               "EXISTS (SELECT s.societyhead FROM societies s WHERE s.societyname = m.societyname)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@societyName", societyName);
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

       

        private void admin_members_view_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            adminMenu log = new adminMenu();
            log.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}