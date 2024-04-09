using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WindowsFormsApp1
{
    public partial class addmembers : Form
    {
        string username;
        public addmembers(string user)
        {
            username = user;    
            InitializeComponent();

            PopulateApprovedSocieties();


        }
        private void PopulateApprovedSocieties()
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string query = $"SELECT societyname FROM Societies WHERE status = 'Approved' AND societyhead = '{username}';";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["societyname"].ToString());
                }
                reader.Close();
            }
        }



        private void addmembers_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text) ||
                dateTimePicker1.Value == null ||
                dateTimePicker2.Value == null ||
                (!checkBox1.Checked && !Secretarycheckbox.Checked && !MemberCheckbox.Checked))
            {
                MessageBox.Show("Please fill all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime selectedTime = dateTimePicker1.Value.Date + dateTimePicker2.Value.TimeOfDay;
            if (selectedTime <= DateTime.Now)
            {
                selectedTime = selectedTime.AddDays(1); 
            }
            TimeSpan timeToWait = selectedTime - DateTime.Now;

            Timer timer = new Timer();
            timer.Interval = (int)timeToWait.TotalMilliseconds;
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                DeleteFromTable();
            };
            timer.Start();

            InsertIntoTable();
        }


        private void DeleteFromTable()
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string societyName = comboBox1.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = $"DELETE FROM societyrecruitment WHERE societyname = @societyName";
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@societyName", societyName);
                int rowsAffected = command.ExecuteNonQuery();
               
            }
        }


        private void InsertIntoTable()
        {
            string societyName = comboBox1.Text;
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string checkTableQuery = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'societyrecruitment') " +
                            "CREATE TABLE societyrecruitment (" +
                            "societyname NVARCHAR(100) PRIMARY KEY NOT NULL," +
                            "secretary BIT," +
                            "member BIT," +
                            "Dept_Head BIT," +
                            "date DATETIME NOT NULL," +
                            "FOREIGN KEY (societyname) REFERENCES Societies(societyname)" +
                            ");";


                SqlCommand createTableCommand = new SqlCommand(checkTableQuery, connection);
                createTableCommand.ExecuteNonQuery();

                DateTime date = dateTimePicker1.Value.Date + dateTimePicker2.Value.TimeOfDay;

                string insertQuery = "INSERT INTO societyrecruitment (societyname, secretary, member, Dept_Head, date) VALUES (@societyname, @secretary, @member, @Dept_Head, @date)";
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@societyname", societyName);
                command.Parameters.AddWithValue("@date", date);

                if (Secretarycheckbox.Checked)
                    command.Parameters.AddWithValue("@secretary", 1);
                else
                    command.Parameters.AddWithValue("@secretary", 0);

                if (MemberCheckbox.Checked)
                    command.Parameters.AddWithValue("@member", 1);
                else
                    command.Parameters.AddWithValue("@member", 0);

                if (checkBox1.Checked)
                    command.Parameters.AddWithValue("@Dept_Head", 1);
                else
                    command.Parameters.AddWithValue("@Dept_Head", 0);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Inserted The Recruitment value.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to Insert the Recruitment value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627) 
                    {
                        MessageBox.Show("A form for this society already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }




        private void button2_Click(object sender, EventArgs e)
        {
            Main login = new Main(username);
            login.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
