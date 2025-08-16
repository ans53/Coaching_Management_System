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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CoachingManagementSystemProject
{
    public partial class Form13 : Form
    {
        System.Data.SqlClient.SqlCommand cmd;
        System.Data.SqlClient.SqlConnection conn;
        System.Data.SqlClient.SqlConnection connection;
        System.Data.SqlClient.SqlDataAdapter adapter;
        System.Data.SqlClient.SqlDataReader reader;

        public Form13()
        {
            InitializeComponent();
        }
        void InsertAttendance(string id, string attendance, string date)
        {
            connection = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");
            string checkQuery = "SELECT COUNT(*) FROM Attendace WHERE id = @id AND course = @course AND stime = @stime AND AtteDate = @date";

            connection.Open();

            using (cmd = new SqlCommand(checkQuery, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@course", comboBox2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@stime", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@date", date);

                int existingRecords = (int)cmd.ExecuteScalar();

                if (existingRecords == 0)
                {
                  
                    string insertQuery = "INSERT INTO Attendace (id, stime, course, AtteDate, fullname, attendace) " +
                                         "SELECT @id, @stime, @course, @date, s.fullname, @attendace " +
                                         "FROM stuDetails s " +
                                         "WHERE s.id = @id";

                    using (cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@course", comboBox2.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@stime", comboBox1.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@attendace", attendance);

                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Update the existing record for the current date
                    string updateQuery = "UPDATE Attendace SET attendace = @attendace WHERE id = @id AND course = @course AND stime = @stime AND AtteDate = @date";

                    using (cmd = new SqlCommand(updateQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@course", comboBox2.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@stime", comboBox1.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@attendace", attendance);

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            connection.Close();
            cmd.Dispose();
        }

        public void LoadDataIntoComboBox()
        {
            conn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");

           conn.Open();
        
            comboBox2.Items.Clear();

            
            string query = "SELECT course FROM course";

            using ( cmd = new SqlCommand(query, conn))
            using ( reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    
                    comboBox2.Items.Add(reader["course"].ToString());
                }
            }
            conn.Close();
        }
        private void Form13_Load(object sender, EventArgs e)
        {
            LoadDataIntoComboBox();
            string date = DateTime.Today.ToString("dd/MM/yyyy");
            conn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");
            textBox1.Text = date;

            conn.Open();

            
            string attendanceInsertQuery = @"
            INSERT INTO attendace (id, stime, course, fullname)
            SELECT s.id, s.stime, s.course, s.fullname
            FROM stuDetails s
            WHERE NOT EXISTS (
                SELECT 1
                FROM attendace a
                WHERE a.id = s.id
            )";

            using (SqlCommand cmd = new SqlCommand(attendanceInsertQuery, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Fields are Empty");
            }
            else
            {
                string selectedCourse = comboBox2.SelectedItem.ToString();
                string Batch = comboBox1.SelectedItem.ToString();
                string date = textBox1.Text;

                conn.Open();

             
                string query = "SELECT id, fullname FROM stuDetails WHERE course = @course AND stime = @stime ";

                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@course", selectedCourse);
                    cmd.Parameters.AddWithValue("@stime", Batch);
                 

                    using (adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                       
                        dataGridView1.DataSource = null;
                        dataGridView1.Columns.Clear();

                        DataGridViewCheckBoxColumn attendanceColumn = new DataGridViewCheckBoxColumn();
                        attendanceColumn.HeaderText = "Attendance";
                        dataGridView1.Columns.Add(attendanceColumn);

                        dataGridView1.DataSource = dt;
                    }
                }
                conn.Close();
            }
        }
        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }
    

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    string studentID = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                    DataGridViewCheckBoxCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;

                    if (cell != null)
                    {
                        bool isChecked = (bool)cell.EditedFormattedValue;
                        string date = textBox1.Text;

                        if (isChecked)
                        {
                            InsertAttendance(studentID, "Present", date);
                        }
                        else
                        {
                            InsertAttendance(studentID, "Abscent", date);
                        }
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

