

using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class update_Event : Form
    {
        string username;
        SqlConnection con = new SqlConnection("Data Source=Strix-15\\SQLEXPRESS;Initial Catalog=UsersSE;Integrated Security=True");

        public update_Event(string username)
        {
            InitializeComponent();
            this.username = username;
            PopulateComboBox1();

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        private void PopulateComboBox1()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT societyname FROM Societies WHERE societyhead = @username", con);
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr["societyname"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void PopulateComboBox2()
        {
            comboBox2.Items.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT eventname FROM Eventss WHERE societyname = @societyname AND statuss = 'approve'", con);
                cmd.Parameters.AddWithValue("@societyname", comboBox1.SelectedItem.ToString());
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr["eventname"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void PopulateTextBox2AndDateTimePicker1()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT eventbudget, eventdate FROM Eventss WHERE eventname = @eventname", con);
                cmd.Parameters.AddWithValue("@eventname", comboBox2.SelectedItem.ToString());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox2.Text = dr["eventbudget"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dr["eventdate"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateComboBox2();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateTextBox2AndDateTimePicker1();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void update_Event_Load(object sender, EventArgs e)
        {
            comboBox3.Items.Add("Completed");
            comboBox3.Items.Add("Withdraw");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd;

                if (comboBox3.SelectedItem != null)
                {
                    cmd = new SqlCommand("UPDATE Eventss SET statuss = @status WHERE eventname = @eventname", con);
                    cmd.Parameters.AddWithValue("@status", comboBox3.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@eventname", comboBox2.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();


                    if (comboBox2.SelectedItem.ToString() == "Completed")
                        MessageBox.Show("Congratulations on completing an Event");
                }

                cmd = new SqlCommand("UPDATE Eventss SET eventbudget = @eventbudget WHERE eventname = @eventname", con);
                cmd.Parameters.AddWithValue("@eventbudget", Convert.ToDecimal(textBox2.Text));
                cmd.Parameters.AddWithValue("@eventname", comboBox2.SelectedItem.ToString());
                cmd.ExecuteNonQuery();

                MessageBox.Show("Event updated successfully!");

                Main log = new Main(username);
                log.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main log = new Main(username);
            log.Show();
            this.Hide();
        }
    }
}

