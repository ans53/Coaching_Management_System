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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CoachingManagementSystemProject
{
    public partial class Form3 : Form
    {
        System.Data.SqlClient.SqlCommand cmd;
        System.Data.SqlClient.SqlConnection conn;
        System.Data.SqlClient.SqlDataReader dr;
        string sql;
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string id = textBox1.Text;
            string spassword= textBox2.Text;


            string sql = "select * from StuDetails where id='"+id+"' AND spassword='"+spassword+"'";
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            
            dr = cmd.ExecuteReader(); 
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    this.Hide();
                    Form4 StudentPage=new Form4();
                    StudentPage.Show();
                }
            }
            else
            {
                MessageBox.Show("No record found");
            }

            conn.Close();
            dr.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");
        }
    }
}
