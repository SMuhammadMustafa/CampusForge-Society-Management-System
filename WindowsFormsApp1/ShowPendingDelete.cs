﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ShowPendingDelete : Form
    {
        private string connectionString = "Data Source=Strix-15\\SQLEXPRESS;Initial Catalog=users;Integrated Security=True";

        
        public ShowPendingDelete()
        {
            InitializeComponent();
            PopulateDataGridView();

            // Manually bind the dataGridView1_CellContentClick event handler
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        }

        private void ShowPendingDelete_Load(object sender, EventArgs e)
        {
            // Method implementation
        }


        private void PopulateDataGridView()
        {
            dataGridView1.Columns.Clear(); // Clear existing columns before populating

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT societyname AS Society_Name, status AS Status FROM deleted_Societies WHERE status = 'pending' OR status = 'not approved'";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();

                adapter.Fill(table);

                // Add a button column for review if not already added
                if (dataGridView1.Columns["ReviewButton"] == null)
                {
                    DataGridViewButtonColumn reviewButtonColumn = new DataGridViewButtonColumn();
                    reviewButtonColumn.Name = "ReviewButton";
                    reviewButtonColumn.HeaderText = "Review";
                    reviewButtonColumn.Text = "Review";
                    reviewButtonColumn.UseColumnTextForButtonValue = true;
                    dataGridView1.Columns.Add(reviewButtonColumn);
                }

                // Bind the data to the DataGridView
                dataGridView1.DataSource = table;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "ReviewButton")
            {
                string societyName = dataGridView1.Rows[e.RowIndex].Cells["Society_Name"].Value.ToString();

                // Open the review form
                Decide_Pending_delete reviewForm = new Decide_Pending_delete(societyName);

                // Populate the form with society information
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT s.societyname, s.head, s.reason " +
                                   "FROM deleted_societies s " +
                                   "WHERE s.societyname = @societyName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@societyName", societyName);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        // Populate the form with the fetched data
                        reviewForm.txtsocietyname.Text = reader["societyname"].ToString();
                        reviewForm.txtHeadname.Text = reader["head"].ToString();
                        reviewForm.txtReason.Text = reader["reason"].ToString();
                    }
                }

                reviewForm.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            adminMenu login = new adminMenu();
            login.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }
    }
}