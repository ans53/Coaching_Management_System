using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoachingManagementSystemProject
{
    public partial class Form19 : Form
    {
        System.Data.SqlClient.SqlCommand cmd;
        System.Data.SqlClient.SqlConnection conn;
        public Form19()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            conn.Open();
            string sql = "SELECT id FROM TeachDetails ";
            using (cmd = new SqlCommand(sql, conn))
            {

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);


                    comboBox1.DataSource = dataSet.Tables[0];
                    comboBox1.ValueMember = "id";
                    comboBox1.DisplayMember = "id";
                }
                conn.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Empty Fields");
            }


            else
            {
                string id = comboBox1.Text;

                string fullname = textBox1.Text;

                long phone = 0;
              
                string phonein = textBox2.Text;
                if (fullname.All(c => char.IsLetter(c) && char.IsWhiteSpace(c)))
                {

                    if (long.TryParse(phonein, out phone) && phonein.All(char.IsDigit))
                    {

                        phone = Convert.ToInt64(textBox2.Text);
                    }
                    else
                    {

                        MessageBox.Show("Invalid phone number. Please enter a valid numeric phone number.");
                    }
                }
               
                string phoneRegex = @"^\d{10}$";
                if (!Regex.IsMatch(textBox2.Text, phoneRegex))
                {
                    MessageBox.Show("Phone number must be 10 digits long.");
                    return;
                }



                string sql = "update Teachdetails set fullname='" + fullname + "',phone=" + phone + " where id='" + id + "'";
                conn.Open();


                textBox1.Clear();
                textBox2.Clear();
                cmd = new SqlCommand(sql, conn);
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    MessageBox.Show("Update Complete you id is:" + id);
                }
                else
                {
                    MessageBox.Show("Error");
                }
                conn.Close();
                cmd.Dispose();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void Form19_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");
            LoadData();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
           
        }
    }
    
}
