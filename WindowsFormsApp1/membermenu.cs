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
    public partial class membermenu : Form
    {
        string username;
        public membermenu(string un)
        {
            username = un;
            InitializeComponent();
        }

        private void membermenu_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";
            string existsQuery = "IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'societyrecruitment') SELECT 1 ELSE SELECT 0";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand existsCommand = new SqlCommand(existsQuery, connection))
                {
                    int exists = (int)existsCommand.ExecuteScalar();

                    if (exists == 1)
                    {
                        string checkQuery = "SELECT COUNT(*) FROM members WHERE statuss IS NULL";
                        using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                        {
                            int nullStatusCount = (int)checkCommand.ExecuteScalar();

                            if (nullStatusCount > 0)
                            {
                                MessageBox.Show("Please fill in complete details before proceeding.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                string societyQuery = "SELECT societyname FROM societyrecruitment";
                                using (SqlCommand societyCommand = new SqlCommand(societyQuery, connection))
                                using (SqlDataReader reader = societyCommand.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        applyForm login = new applyForm(username);
                                        login.Show();
                                        this.Hide();
                                    }
                                    else
                                    {
                                        MessageBox.Show("No societies found.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        membermenu login = new membermenu(username);
                                        login.Show();
                                        this.Hide();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }



        private void button6_Click(object sender, EventArgs e)
        {
            firstpage login = new firstpage();  
            login.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            add_details log = new add_details(username);
            log.Show();
            this.Hide();
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {

        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Orange;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor= Color.Black;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Member_meeting_Main log = new Member_meeting_Main(username);
            log.Show();
            this.Hide();
        }
    }
}
