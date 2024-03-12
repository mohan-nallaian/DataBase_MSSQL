using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Data.SqlClient;
using System.Reflection;

namespace Database
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-JG9LES2S\SQLEXPRESS;Initial Catalog=MY_DB_1;Integrated Security=True;");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();

            String Sql = "insert into Info(Name, Mobile, Username, Password) values('" + Namebox.Text + "','" + Numberbox.Text + "','" + Username.Text + "','" + Password.Text + "')";
            SqlCommand cmd = new SqlCommand(Sql, con);
            int row_count= cmd.ExecuteNonQuery();

            if (row_count > 0)
            {
                MessageBox.Show("Insert Successfully", "Output", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Load_table();
                Namebox.Clear();
                Numberbox.Clear();
                Username.Clear();
                Password.Clear();
            }
            else {
                MessageBox.Show("Error in Uploading");
            }

            con.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
              Load_table();
        }

        private void Load_table()
        {
            string sql = "select * from Info";
            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;

            Namebox.Text = dataGridView1.Rows[RowIndex].Cells[1].Value.ToString();
            Numberbox.Text = dataGridView1.Rows[RowIndex].Cells[2].Value.ToString();
            Username.Text = dataGridView1.Rows[RowIndex].Cells[3].Value.ToString();
            Password.Text = dataGridView1.Rows[RowIndex].Cells[4].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" Do you want change anything", "UPDATES", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                String Sql = "update Info set Name = '" + Namebox.Text + "',Mobile = '" + Numberbox.Text + "',Username='" + Username.Text + "'Where Mobile='" + Numberbox.Text + "'";
                SqlCommand cmd = new SqlCommand(Sql, con);

                int Row_count = cmd.ExecuteNonQuery();

                if (Row_count > 0)
                {
                    MessageBox.Show("Updated Successfully");
                    Load_table();

                }
                else
                {
                    MessageBox.Show("Update failed");
                }
                con.Close();
            }
        }
    }
}
