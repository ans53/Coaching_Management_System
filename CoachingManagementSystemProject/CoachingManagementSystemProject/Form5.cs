using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace CoachingManagementSystemProject
{
    public partial class Form5 : Form
    {
        System.Data.SqlClient.SqlCommand cmd;
        System.Data.SqlClient.SqlConnection conn;

        public Form5()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form10 TeacherRegistration = new Form10();
            TeacherRegistration.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form11 BatchAllotment = new Form11();
            BatchAllotment.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form10 TeacherRegistration = new Form10();
            TeacherRegistration.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form14 FeeManagement = new Form14();
            FeeManagement.Show();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Form12 Courses = new Form12();
            Courses.Show();
        }
        private void rgigisterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void teacToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form10 TeacherRegistration = new Form10();
            TeacherRegistration.Show();
        }

        private void studentRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form9 StudentRegistration = new Form9();
            StudentRegistration.Show();
        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form11 StudentBatchAllotment = new Form11();
            StudentBatchAllotment.Show();

        }

        private void teacherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form12 Courses = new Form12();
            Courses.Show();

        }

        private void feeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form14 FeeManagement = new Form14();
            FeeManagement.Show();

        }

        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void defaultorsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form20 defaultors= new Form20();
            defaultors.Show();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            
            

        }

            private void addStudentDataToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }

            private void studentToolStripMenuItem2_Click(object sender, EventArgs e)
            {

            }

            private void studentToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                Form18 changeStudentRecord = new Form18();
                changeStudentRecord.Show();
            }

            private void teacherToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                Form19 changeTeacherRecord = new Form19();
                changeTeacherRecord.Show();
            }

            private void updateToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }

        private void addCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form21 changecourse= new Form21();
            changecourse.Show();
        }
    }
    }

