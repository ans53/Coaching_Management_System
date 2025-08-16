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
    public partial class Form7 : Form
    {
        System.Data.SqlClient.SqlCommand cmd;
        System.Data.SqlClient.SqlConnection conn;
        System.Data.SqlClient.SqlDataReader dr;
        public Form7()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string spassword = textBox2.Text;


            string sql = "select * from TeachDetails where id='" + id + "' AND spassword='" + spassword + "'";
            conn.Open();
            cmd = new SqlCommand(sql, conn);

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    this.Hide();
                    Form8 TeacherPage = new Form8();
                    TeacherPage.Show();
                }
            }
            else
            {
                MessageBox.Show("No record found");
            }

            conn.Close();
            dr.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
