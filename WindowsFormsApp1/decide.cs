using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class decide : Form
    {
        private string _societyName;
        private string connectionString = "Data Source=DESKTOP-BUNDG75\\SQLEXPRESS01;Initial Catalog=users;Integrated Security=True";

        public string Decision { get; private set; }

       public  decide(string societyName)
{
    InitializeComponent();
    _societyName = societyName;

    comboBox1.Items.Add("Approve");
    comboBox1.Items.Add("Not Approve");

    comboBox1.SelectedIndex = 0;

    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        conn.Open();

         string selectQuery = @"
        SELECT DISTINCT m.id AS mentorID, m.username AS mentorName
        FROM mentors m
        WHERE NOT EXISTS (
            SELECT 1
            FROM Societies s
            WHERE s.societymentor = m.id
            AND s.status = 'Approved'
        )";

        List<string> availableMentorNames = new List<string>();

        using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
        {
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string mentorName = reader["mentorName"].ToString();
                    availableMentorNames.Add(mentorName);
                }
            }
        }

        foreach (var mentorName in availableMentorNames)
        {
            if (!comboBox2.Items.Contains(mentorName))
            {
                comboBox2.Items.Add(mentorName);
            }
        }
    }
}





        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select a mentor.");
                return;
            }

            Decision = comboBox1.SelectedItem.ToString();

            string mentorName = comboBox2.SelectedItem.ToString();

            int mentorId = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT id FROM mentors WHERE username = @mentorName";
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@mentorName", mentorName);
                object result = selectCommand.ExecuteScalar();
                if (result != null)
                {
                    mentorId = Convert.ToInt32(result);
                }
                else
                {
                    MessageBox.Show("Selected mentor not found in the database.");
                    return;
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE Societies SET status = @status, societymentor = @mentorId WHERE societyname = @societyName";
                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@status", Decision == "Approve" ? "Approved" : "Not Approved");
                updateCommand.Parameters.AddWithValue("@societyName", _societyName);
                updateCommand.Parameters.AddWithValue("@mentorId", mentorId);
                updateCommand.ExecuteNonQuery();
            }

            // Close the form
            DialogResult = DialogResult.OK;
            Close();

            adminMenu login = new adminMenu();
            login.Show();
            this.Hide();
        }



        private void txtsocietyRules_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtsocietycontact_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtsocietywise_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtsocietyhead_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSocietyname_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void decide_Load(object sender, EventArgs e)
        {
           
        }

     
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Decide_pending log = new Decide_pending();
            log.Show();
            this.Hide();
        }
    }
}
