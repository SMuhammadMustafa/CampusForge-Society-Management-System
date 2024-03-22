using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Delete_Society : Form
    {
        private string _username;
        private string _selectedSociety; // Variable to store the selected society name

        public Delete_Society(string username)
        {
            InitializeComponent();
            _username = username;
            txtHeadname.Text = _username; // Auto-fill the society head name with the username

            // Populate ComboBox with society names associated with the current user
            PopulateSocietyComboBox();
        }

        private void PopulateSocietyComboBox()
        {
            string connectionString = "Data Source=Strix-15\\SQLEXPRESS;Initial Catalog=users;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Select society names associated with the current user (head)
                    string selectQuery = "SELECT societyname FROM Societies WHERE societyhead = @username";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", _username);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string societyName = reader.GetString(0);
                                comboBox1.Items.Add(societyName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while populating the ComboBox: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main login = new Main(_username);
            login.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_selectedSociety))
            {
                MessageBox.Show("Please select a society from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Data Source=Strix-15\\SQLEXPRESS;Initial Catalog=users;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Create the deleted_societies table if it doesn't exist
                    string createTableQuery = @"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'deleted_societies')
                                    BEGIN
                                        CREATE TABLE deleted_societies (
                                            id INT PRIMARY KEY IDENTITY,
                                            head NVARCHAR(100),
                                            reason NVARCHAR(255),
                                            societyname NVARCHAR(100) FOREIGN KEY REFERENCES Societies(societyname),
                                            status NVARCHAR(50),
                                            CONSTRAINT UC_SocietyName UNIQUE (societyname)
                                        )
                                    END";

                    using (SqlCommand cmdCreateTable = new SqlCommand(createTableQuery, conn))
                    {
                        cmdCreateTable.ExecuteNonQuery();
                    }

                    // Check if the society already exists in the deleted_societies table
                    string checkExistingQuery = "SELECT COUNT(*) FROM deleted_societies WHERE societyname = @societyname";

                    using (SqlCommand cmdCheckExisting = new SqlCommand(checkExistingQuery, conn))
                    {
                        cmdCheckExisting.Parameters.AddWithValue("@societyname", _selectedSociety);
                        int count = (int)cmdCheckExisting.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Entry for this society already exists in the deleted_societies table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Check the status of the society
                    string selectStatusQuery = "SELECT status FROM Societies WHERE societyname = @societyname";

                    using (SqlCommand cmdSelect = new SqlCommand(selectStatusQuery, conn))
                    {
                        cmdSelect.Parameters.AddWithValue("@societyname", _selectedSociety);
                        string status = (string)cmdSelect.ExecuteScalar();

                        if (status == "Approved")
                        {
                            // Insert record into deleted_societies table
                            string insertQuery = "INSERT INTO deleted_societies (head, reason, societyname, status) VALUES (@head, @reason, @societyname, @status)";

                            using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                            {
                                cmdInsert.Parameters.AddWithValue("@head", txtHeadname.Text);
                                cmdInsert.Parameters.AddWithValue("@reason", txtReason.Text);
                                cmdInsert.Parameters.AddWithValue("@societyname", _selectedSociety);
                                cmdInsert.Parameters.AddWithValue("@status", "Pending"); // Default status for deleted entry

                                cmdInsert.ExecuteNonQuery();

                                MessageBox.Show("Entry inserted for Approval into deleted_societies table.");
                               
                            }
                        }
                        else
                        {
                            MessageBox.Show("Society status is not Approved. Cannot delete society.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Main loginForm = new Main(_username);
            loginForm.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Store the selected society name
            _selectedSociety = comboBox1.SelectedItem.ToString();
        }

        private void Delete_Society_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
