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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text=="Student")
            {
                this.Hide();
                Form3 Student = new Form3();
                Student.Show();

            }
            else
            {
                this.Hide();
                Form7 Teacher = new Form7();
                Teacher.Show();
            }
        }
    }
}
