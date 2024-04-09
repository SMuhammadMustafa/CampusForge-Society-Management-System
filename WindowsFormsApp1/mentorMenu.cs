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
    public partial class mentorMenu : Form
    {
        String username;
        public mentorMenu(String un)
        {
            InitializeComponent();
            username = un;
        }

        private void mentorMenu_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            firstpage login = new firstpage();
            login.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mentor_society_view login = new mentor_society_view(username);
            login.Show(); 
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mentor_Member login = new Mentor_Member(username);
            login.Show();
            this.Hide();
        }
    }
}
