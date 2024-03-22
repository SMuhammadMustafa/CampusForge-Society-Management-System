using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {
        private string username;

        public Main(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegisterSociety registerForm = new RegisterSociety(username);
            registerForm.Show();
            this.Hide();
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Delete_Society loginForm = new Delete_Society(username);
            loginForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            head_details_View loginForm = new head_details_View(username);
            loginForm.Show();
            this.Hide();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            textBox1.Text = username;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            firstpage loginForm = new firstpage();
            loginForm.Show();
            this.Hide();
        }
    }
}
