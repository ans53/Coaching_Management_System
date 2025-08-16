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
    public partial class Form14 : Form
    {
        System.Data.SqlClient.SqlCommand cmd;
        System.Data.SqlClient.SqlConnection conn;
        System.Data.SqlClient.SqlDataReader dr;
        System.Data.SqlClient.SqlDataAdapter adapter;
        string sql;
        public Form14()
        {
            InitializeComponent();
        }
        void LoadData()
        {

            conn.Open();

            sql = "SELECT id FROM StuDetails ";
            cmd = new SqlCommand(sql, conn);

            using (adapter = new SqlDataAdapter(cmd))
            {
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                comboBox1.Items.Add("");

                comboBox1.DataSource = dataSet.Tables[0];
                comboBox1.ValueMember = "id";
                comboBox1.DisplayMember = "id";
            }

            conn.Close();
        }
        void LoadInto()
        {
            conn.Open();

            sql = "INSERT INTO Fee (StudentID, MonthPayment, Course, Fee, Paid, Due, PaymentDate) " +
                  "SELECT sd.id, f.MonthPayment, f.Course, f.Fee, f.Paid, f.Due, f.PaymentDate " +
                  "FROM StuDetails sd " +
                  "LEFT JOIN Fee f ON sd.id = f.StudentID " +
                  "WHERE f.StudentID IS NULL";

            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        private void Form14_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");
            string date = DateTime.Today.ToString("dd/MM/yyyy");
            textBox6.Text = date;
            LoadData();
            LoadInto();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string id = comboBox1.Text;
            string course = textBox1.Text;
            string monthPayment;

            int fee = Convert.ToInt32(textBox2.Text);

            if (DateTime.TryParseExact(textBox6.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime paymentDate))
            {
                monthPayment = paymentDate.ToString("MMMM");
            }
            else
            {
                MessageBox.Show("Invalid date format. Please use the format dd/MM/yyyy.");
                conn.Close();
                return;
            }

            sql = "SELECT paid, due FROM Fee WHERE StudentID = '" + id + "' AND Course = '" + course + "' AND MonthPayment = '" + monthPayment + "'";
            cmd = new SqlCommand(sql, conn);

            using (dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        textBox3.Text = dr["paid"].ToString();
                        textBox4.Text = dr["due"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("No Record Found");
                }
            }

            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string id = comboBox1.Text;
            string course = textBox1.Text;
            string monthPayment = string.Empty;
            string PayDate = textBox6.Text;

            if (DateTime.TryParseExact(textBox6.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime paymentDate))
            {
                monthPayment = paymentDate.ToString("MMMM");
            }
            else
            {
                MessageBox.Show("Invalid date format. Please use the format dd/MM/yyyy.");
                conn.Close();
                return;
            }

            sql = "INSERT INTO Fee (StudentID, MonthPayment, Course, Fee, Paid, Due, PaymentDate) " +
                  "VALUES ('" + id + "', '" + monthPayment + "', '" + course + "', " + textBox2.Text + ", " + textBox3.Text + ", " + textBox4.Text + ", '" + PayDate + "')";

            using (cmd = new SqlCommand(sql, conn))
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Payment Done");
                }
                else
                {
                    MessageBox.Show("Payment Failed");
                }
            }

            conn.Close();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

            if (int.TryParse(textBox3.Text, out int pay) && int.TryParse(textBox2.Text, out int fee))
            {
                if (pay <= fee)
                {
                    int due = fee - pay;
                    textBox4.Text = due.ToString();
                }
                else
                {
                    MessageBox.Show("Amount is Invalid");
                }
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter valid numbers for payment.");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string id = comboBox1.Text;
            sql = "SELECT course FROM StuDetails WHERE id = '" + id + "'";
            cmd = new SqlCommand(sql, conn);

            using (dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        textBox1.Text = dr["course"].ToString();
                    }
                }
            }

            string course = textBox1.Text;
            string feeQuery = "SELECT fee FROM course WHERE course = '"+course+"'";

            using (SqlCommand cmd = new SqlCommand(feeQuery, conn))
            {
              

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            textBox2.Text = dr["fee"].ToString();
                        }
                    }
                }
            }
        }
    


     
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            string course = textBox1.Text;

            sql = "SELECT fee FROM course WHERE course = '" + course + "'";

            if (conn.State == ConnectionState.Closed)
            {
                using (SqlConnection newConn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True"))
                {
                    newConn.Open();
                    using (SqlCommand newCmd = new SqlCommand(sql, newConn))
                    {
                       
                        using (SqlDataReader newDr = newCmd.ExecuteReader())
                        {
                            if (newDr.HasRows)
                            {
                                while (newDr.Read())
                                {
                                    textBox2.Text = newDr["fee"].ToString();
                                }
                            }
                            
                        }
                    }
                    newConn.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            if (int.TryParse(textBox3.Text, out int updatedDueAmount))
            {
              
                string id = comboBox1.Text;
                string course = textBox1.Text;
                string monthPayment = string.Empty;
                if (DateTime.TryParseExact(textBox6.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime paymentDate))
                {
                    monthPayment = paymentDate.ToString("MMMM");
                } 

                sql = "UPDATE Fee SET Due = " + updatedDueAmount +
                      " WHERE StudentID = '" + id + "' AND Course = '" + course + "' AND MonthPayment = '" + monthPayment + "'";

                using (SqlCommand updateDueCmd = new SqlCommand(sql, conn))
                {
                    int rowsAffected = updateDueCmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Due amount updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to update due amount.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid input for the due amount. Please enter a valid number.");
            }

            conn.Close();
        }
    }
}

