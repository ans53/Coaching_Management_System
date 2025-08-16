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
    public partial class Form20 : Form
    {
        System.Data.SqlClient.SqlCommand cmd;
        System.Data.SqlClient.SqlConnection conn;
        System.Data.SqlClient.SqlDataAdapter adapter;

        public Form20()
        {
            InitializeComponent();
        }

        private void Form20_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=LAPTOP-V6TICUNR\\SQLEXPRESS;Initial Catalog=coachingsytem;Integrated Security=True");
            string sql = "SELECT StudentID, fee, paid, due, PaymentDate,MonthPayment FROM Fee WHERE due != 0 ORDER BY StudentID, MonthPayment";
            conn.Open();
            cmd = new SqlCommand(sql, conn);
           
            using (adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();

                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }

            conn.Close();
            cmd.Dispose();
        }
    }
}
