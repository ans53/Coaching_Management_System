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
    public partial class Form17 : Form
    {
        System.Data.SqlClient.SqlConnection conn;
        System.Data.SqlClient.SqlCommand cmd;
        System.Data.SqlClient.SqlDataReader dr;
        System.Data.SqlClient.SqlDataReader reader;
        System.Data.SqlClient.SqlDataAdapter adapter;
        string sql;
        public Form17()
        {
            InitializeComponent();
        }
        
        void FilterByCourse()
        {
            string course=comboBox2.SelectedValue.ToString();
            
            conn.Close();
            conn.Open();
            sql = "SELECT id FROM StuDetails where course='"+course+"'";
           
            using (cmd = new SqlCommand(sql, conn))
            {

                using (adapter = new SqlDataAdapter(cmd))
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
        void LoadCourse()
        {
            conn.Open();
            sql = "SELECT course FROM course ";
            using (cmd = new SqlCommand(sql, conn))
            {

                using (adapter = new SqlDataAdapter(cmd))
                {

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);


                    comboBox2.DataSource = dataSet.Tables[0];
                    comboBox2.ValueMember = "course";
                    comboBox2.DisplayMember = "course";
                }
                conn.Close();
                
            }

        }
        public void LoadDataIntoComboBox()
        {
            conn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");

            conn.Open();

            comboBox2.Items.Clear();


            string query = "SELECT course FROM course";

            using (cmd = new SqlCommand(query, conn))
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    comboBox2.Items.Add(reader["course"].ToString());
                }
            }
            conn.Close();
        }
        private void Form17_Load(object sender, EventArgs e)
        {
            LoadDataIntoComboBox();
            string checkQuery = "SELECT COUNT(*) FROM Report";
            conn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");
            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
            {
                conn.Open();
                int count = (int)checkCmd.ExecuteScalar();


                if (count == 0)
                {

                    string attendanceInsertQuery = "INSERT INTO Report (id) SELECT id FROM stuDetails";

                    cmd = new SqlCommand(attendanceInsertQuery, conn);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            LoadCourse();
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id=comboBox1.Text;
            int TestMarks = Convert.ToInt32(textBox1.Text);
            int Practical = Convert.ToInt32(textBox2.Text);
            int Project = Convert.ToInt32(textBox3.Text);
           
            string grade= "Not Available";
            if (int.TryParse(textBox1.Text, out TestMarks) && int.TryParse(textBox2.Text, out Practical) && int.TryParse(textBox3.Text, out Project))
            {
                if (TestMarks > 100 || Practical > 100 || Project > 100)
                {

                    MessageBox.Show("Marks in all 3 subjects cannot be more than 100 ");


                }
                if (TestMarks < 0 || Practical < 0 || Project < 0)
                {
                    MessageBox.Show("Marks in all 3 subjects cannot be less than 0 ");
                }
                int totalMarks = TestMarks + Practical + Project;
                double cal = (totalMarks / 300.0) * 100;
                double avg=Math.Round(cal, 2, MidpointRounding.AwayFromZero);
                
                if (avg >= 80)
                {
                    grade = "Grade A+:" + avg + "%";
                }
                else if (avg >= 70 && avg <= 80)
                {
                    grade = "Grade A:" + avg + "%";
                }
                else if (avg >= 60 && avg <= 70)
                {
                    grade = "Grade B+:" + avg + "%";
                }
                else if (avg >= 50 && avg <= 60)
                {
                    grade = "Grade B:" + avg + "%";
                }
                else if (avg >= 40 && avg <= 50)
                {
                    grade = "Grade C:" + avg + "%";
                }
                else
                {
                    grade = "You have failed with percentage less 40%";
                }
            }
            else
            {
                MessageBox.Show("Int value is required");
            }
          

            sql = "update report set textMarks="+ TestMarks + ",practical=" + Practical + ",project=" + Project + ",grade='" + grade + "' where id='"+id+"'";
            conn.Open();
            using (cmd = new SqlCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
                label3.Text=grade;
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
               

            }
            conn.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterByCourse();
        }
    }
}
