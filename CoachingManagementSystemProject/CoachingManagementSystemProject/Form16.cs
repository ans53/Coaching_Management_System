using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoachingManagementSystemProject
{
    
    public partial class Form16 : Form
    {
        System.Data.SqlClient.SqlCommand cmd;
        System.Data.SqlClient.SqlConnection conn;
        System.Data.SqlClient.SqlDataReader reader;

        string sql;
        public Form16()
        {
            InitializeComponent();
        }

        private void Form16_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id=textBox1.Text;
            sql = "select*from Report where id='"+id+"'";
            conn.Open();
            cmd=new SqlCommand(sql,conn);
            reader=cmd.ExecuteReader();
            while (reader.Read())
            {
               
                string testMarks = reader["textMarks"].ToString();
                string practical = reader["practical"].ToString();
                string project = reader["project"].ToString();
                string grade = reader["grade"].ToString();
                if (testMarks == "" && practical == "" && project == "" && grade == "")
                {
                    MessageBox.Show("Marks Not been declared Yet");
                }
                else
                {
                    label2.Text = "Your test Marks = " + testMarks;
                    label3.Text = "Your Practical Marks = " + practical;
                    label4.Text = "Your Project Marks = " + project;
                    label5.Text = grade;
                }


            }
            conn.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
