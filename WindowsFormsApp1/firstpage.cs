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
    public partial class firstpage : Form
    {
        string username;
        public firstpage()
        {
            InitializeComponent();
            DeleteExpiredRecruitments();
        }

        private void DeleteExpiredRecruitments()
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string checkTableQuery = "IF OBJECT_ID('societyrecruitment', 'U') IS NOT NULL SELECT 1 ELSE SELECT 0";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand checkTableCommand = new SqlCommand(checkTableQuery, connection);
                connection.Open();
                int tableExists = (int)checkTableCommand.ExecuteScalar();

                if (tableExists == 1)
                {
                    string query = "SELECT date FROM societyrecruitment";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime date = (DateTime)reader["date"];
                        DateTime now = DateTime.Now;

                        if (date.Date < now.Date || (date.Date == now.Date && date.TimeOfDay <= now.TimeOfDay))
                        {
                            DeleteRecord(date);
                        }
                    }
                }
            }
        }


        private void DeleteRecord(DateTime date)
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string query = "DELETE FROM societyrecruitment WHERE date = @date";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", date);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
            }
        }





        private void button1_Click_1(object sender, EventArgs e)
        {
            username = "admin";
            Formlogin l = new Formlogin(username);
            l.Show();
            this.Hide();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            username = "head";
            Formlogin l = new Formlogin(username);
            l.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void firstpage_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            username = "mentor";
            Formlogin l = new Formlogin(username);
            l.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            username = "member";
            Formlogin  l = new Formlogin(username);
            l.Show();
            this.Hide();
        }
    }
}
