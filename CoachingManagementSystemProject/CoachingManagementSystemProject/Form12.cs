using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CoachingManagementSystemProject
{
    public partial class Form12 : Form
    {
        System.Data.SqlClient.SqlCommand cmd;
        System.Data.SqlClient.SqlConnection conn;
        System.Data.SqlClient.SqlDataAdapter adapter;
        System.Data.SqlClient.SqlDataReader reader;
        string sql;
        public Form12()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");
            LoadData();
            LoadDataIntoComboBox();
            
        }
        public void LoadDataIntoComboBox()
        {
            conn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");

            conn.Open();

            comboBox1.Items.Clear();


            string query = "SELECT course FROM course";

            using (cmd = new SqlCommand(query, conn))
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    comboBox1.Items.Add(reader["course"].ToString());
                }
            }
            conn.Close();
        }

        void LoadData()
        {
            conn.Open();
            sql = "SELECT id FROM TeachDetails ";
            using (cmd = new SqlCommand(sql, conn))
            {

                using (adapter = new SqlDataAdapter(cmd))
                {

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);


                    comboBox3.DataSource = dataSet.Tables[0];
                    comboBox3.ValueMember = "id";
                    comboBox3.DisplayMember = "id";
                }
                conn.Close();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = comboBox3.Text;
            string stime = comboBox2.Text;

            string course = comboBox1.Text;

            sql = "update TeachDetails set course='" + course + "' , stime='" + stime + "' where id='" + id + "'";
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            int r = cmd.ExecuteNonQuery();
            if (r > 0)
            {
                MessageBox.Show("Batch Allothed Course :"+course+" timming :"+stime);
            }
            else
            {
                MessageBox.Show("Error");
            }
            conn.Close();
            cmd.Dispose();
        }
    }
}
