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
    public partial class adminMenu : Form
    {
        public adminMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Decide_pending loginform = new Decide_pending();
            loginform.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowPendingDelete loginform = new ShowPendingDelete();
            loginform.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            update loginform = new update();
            loginform.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            admin_Society_details loginform = new admin_Society_details();
            loginform.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            firstpage login = new firstpage();
            login.Show();
            this.Hide();
        }

        private void adminMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
