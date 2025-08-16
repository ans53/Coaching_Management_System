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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Form13 Attendance= new Form13();
            Attendance.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            Form17 generateReport = new Form17();
            generateReport.Show();
        }
    }
}
