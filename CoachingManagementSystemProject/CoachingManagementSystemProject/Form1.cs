using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoachingManagementSystemProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string v = comboBox1.Text;
            if (v == "Admin")
            {
                this.Hide();
                Form2 Admin =new Form2();
                Admin.Show();
            }
            else
            {
                this.Hide();
                Form6 User = new Form6();
                User.Show();
            }
        }
    }
}
