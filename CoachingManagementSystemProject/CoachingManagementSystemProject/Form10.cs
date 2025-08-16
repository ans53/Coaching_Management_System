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
    public partial class Form10 : Form
    {
        System.Data.SqlClient.SqlCommand cmd;
        System.Data.SqlClient.SqlConnection conn;
        public Form10()
        {
            InitializeComponent();
        }
        private string GenerateNewId()
        {
            string prefix = "To";
            string newId = null;

            // Connect to the database


            conn.Open();

            while (true)
            {
                // Generate a new ID
                newId = prefix + GetNextIdFromDatabase();

                
                string checkQuery = "SELECT COUNT(*) FROM Teachdetails WHERE id = '" + newId + "'";
                cmd = new SqlCommand(checkQuery, conn);
                cmd.Parameters.AddWithValue("@newId", newId);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count == 0)
                {
                    
                    break;
                }
            }

            conn.Close();

            return newId;


        }
        private int GetNextIdFromDatabase()
        {
            string query = "SELECT MAX(CAST(SUBSTRING(id, 3, LEN(id) - 2) AS INT)) FROM Teachdetails";
            cmd = new SqlCommand(query, conn);

            object result = cmd.ExecuteScalar();

            if (result != DBNull.Value && int.TryParse(result.ToString(), out int maxId))
            {
                return maxId + 1;
            }
            else
            {
                return 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == ""  || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Empty Fields");
            }

            else
            {
                string id = GenerateNewId();


                string fullname = textBox1.Text;
                string spassword = textBox3.Text;
                long phone = Convert.ToInt64(textBox4.Text);
                string phoneRegex = @"^\d{10}$";
                if (!Regex.IsMatch(textBox4.Text, phoneRegex))
                {
                    MessageBox.Show("Phone number must be 10 digits long.");
                    return;
                }

                bool hasLetters = Regex.IsMatch(textBox3.Text, "[a-zA-Z]");
                bool hasNumbers = Regex.IsMatch(textBox3.Text, @"\d");

                if (!hasLetters || !hasNumbers)
                {
                    MessageBox.Show("Password must contain both letters and numbers.");
                    return;
                }

                string sql = "insert into TeachDetails(fullname,id,spassword,phone) values('" + fullname + "','" + id + "'  , '" + spassword + "' , " + phone + ")";
                conn.Open();
                textBox1.Clear();

                textBox3.Clear();
                textBox4.Clear();
                cmd = new SqlCommand(sql, conn);
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    MessageBox.Show("Registration Complete you id is:" + id);
                }
                else
                {
                    MessageBox.Show("Error");
                }
                conn.Close();
                cmd.Dispose();

            }
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");
        }
    }
}
