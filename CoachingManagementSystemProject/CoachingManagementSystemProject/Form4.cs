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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Form15 Attendance = new Form15();
            Attendance.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form16 viewReport= new Form16();
            viewReport.Show();

        }
    }
}
