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
    public partial class Form2 : Form
    {
        System.Data.SqlClient.SqlCommand cmd;
        System.Data.SqlClient.SqlConnection conn;
        System.Data.SqlClient.SqlDataReader dr;
        string sql;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string spassword = textBox2.Text;


            string sql = "SELECT * FROM AdminLogin WHERE id='" + id + "' AND spassword='" + spassword + "'";
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                
                while (dr.Read())
                {
                    this.Hide();
                    Form5 AdminPage = new Form5();
                    AdminPage.Show();
                   

                }
            }
            else
            {
                MessageBox.Show("No record found");
            }

            conn.Close();
            dr.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
