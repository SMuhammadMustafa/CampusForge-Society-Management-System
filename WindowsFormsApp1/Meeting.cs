using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Meeting : Form
    {
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private string username;

        public Meeting(string un)
        {
            username = un;
            InitializeComponent();
            PopulateSocietyNames();
            PopulateComboBox2();
        }

        private void PopulateSocietyNames()
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string query = "SELECT societyname FROM Societies WHERE societyhead = @username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader["societyname"].ToString());
                        }
                    }
                }
            }
        }

        private void PopulateComboBox2()
        {
            comboBox2.Items.Add("secretary");
            comboBox2.Items.Add("Dept_Head");
            comboBox2.Items.Add("member");
        }

        private void Meeting_Load(object sender, EventArgs e)
        {
            CreateTableIfNotExists();
        }

        private void CreateTableIfNotExists()
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string query = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Meetings') " +
                           "CREATE TABLE Meetings (" +
                           "ID INT IDENTITY(1,1) PRIMARY KEY," +
                           "person NVARCHAR(100) NOT NULL," +
                           "date DATE NOT NULL," +
                           "time TIME NOT NULL," +
                           "societyname NVARCHAR(100) NOT NULL" +
                           ");";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main log = new Main(username);
            log.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string societyName = comboBox1.Text;
            string personType = comboBox2.Text;
            DateTime date = dateTimePicker1.Value.Date;
            TimeSpan time = dateTimePicker2.Value.TimeOfDay;

            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string checkPositionQuery = "SELECT COUNT(*) FROM Members WHERE societyname = @societyname AND position = @person";
                SqlCommand checkPositionCommand = new SqlCommand(checkPositionQuery, connection);
                checkPositionCommand.Parameters.AddWithValue("@societyname", societyName);
                checkPositionCommand.Parameters.AddWithValue("@person", personType);
                int positionCount = (int)checkPositionCommand.ExecuteScalar();

                if (positionCount == 0)
                {
                    MessageBox.Show("There is no position against which you are setting position.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string checkDuplicateQuery = "SELECT COUNT(*) FROM Meetings WHERE societyname = @societyname AND person = @person AND date = @date AND time = @time";
                SqlCommand checkDuplicateCommand = new SqlCommand(checkDuplicateQuery, connection);
                checkDuplicateCommand.Parameters.AddWithValue("@societyname", societyName);
                checkDuplicateCommand.Parameters.AddWithValue("@person", personType);
                checkDuplicateCommand.Parameters.AddWithValue("@date", date);
                checkDuplicateCommand.Parameters.AddWithValue("@time", time);
                int count = (int)checkDuplicateCommand.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Meeting for the same society under the same person, date, and time already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string insertQuery = "INSERT INTO Meetings (person, date, time, societyname) VALUES (@person, @date, @time, @societyname)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@person", personType);
                    insertCommand.Parameters.AddWithValue("@date", date);
                    insertCommand.Parameters.AddWithValue("@time", time);
                    insertCommand.Parameters.AddWithValue("@societyname", societyName);

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Meeting details inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to insert meeting details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



    }
}
    