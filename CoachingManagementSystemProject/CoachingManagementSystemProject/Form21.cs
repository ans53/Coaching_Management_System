using System;
using System.Collections;
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
    public partial class Form21 : Form
    {
        System.Data.SqlClient.SqlCommand cmd;
        System.Data.SqlClient.SqlConnection conn;
        System.Data.SqlClient.SqlDataAdapter adapter;
        public Form21()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");
            conn.Open();
            long fee;
            if (long.TryParse(textBox2.Text, out fee))
            {

                string course = textBox1.Text;
               
                string query = "Insert Into course values('" + course + "'," + fee + ")";
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                LoadData();
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter valid numbers for payment.");
            }
            textBox1.Clear();
            textBox2.Clear();
            conn.Close();
        }

        private void Form21_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");
            LoadData();
            
        }
        void LoadData()
        {
            conn.Close ();
            string query = "SELECT * FROM course";
            conn.Open();
            adapter = new SqlDataAdapter(query, conn);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);


            dataGridView1.DataSource = dataTable;
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            long fee;
            if (long.TryParse(textBox2.Text, out fee))
            {
                string course = textBox1.Text;

                string connectionString = "Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True";
                using (conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM course WHERE Course ='"+course+"' AND Fee = "+fee+"";
                    using (cmd = new SqlCommand(query, conn))
                    {
                       
                        cmd.ExecuteNonQuery();
                    }
                    LoadData();
                }
                textBox1.Clear();
                textBox2.Clear();
                conn.Close();
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter valid numbers for fee.");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
