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

namespace WindowsFormsApp1
{
    public partial class Quer : Form
    {
        public Quer()
        {
            InitializeComponent();
            Combo1();
            Combo2();
        }
        string conn = Form1.Conn.connect;
        private void button1_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection connection = new SqlConnection(conn))
            {
                try
                {
                    string publ = "SELECT * FROM publ_pages()";
                    var publCmd = new SqlCommand(publ, connection);
                    DataTable publtab = new DataTable();
                    SqlDataAdapter publadap = new SqlDataAdapter(publCmd);
                    publadap.Fill(publtab);
                    dataGridView1.DataSource = publtab;
                }
                catch (Exception q)
                {
                    MessageBox.Show("Ошибка: " + q.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox2.Text == ""))
            {
                MessageBox.Show("Заполните поля");
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    try
                    {
                        string publ = (@"SELECT * FROM publ_language1(@lang,@num)");
                        var publCmd = new SqlCommand(publ, connection);
                        publCmd.Parameters.AddWithValue("@lang", textBox1.Text);
                        publCmd.Parameters.AddWithValue("@num", textBox2.Text);
                        DataTable publtab = new DataTable();
                        SqlDataAdapter publadap = new SqlDataAdapter(publCmd);
                        publadap.Fill(publtab);
                        dataGridView1.DataSource = publtab;
                    }
                    catch (Exception q)
                    {
                        MessageBox.Show("Ошибка: " + q.Message);
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                try
                {
                    string publ = "SELECT * FROM auth_publs()";
                    var publCmd = new SqlCommand(publ, connection);
                    DataTable publtab = new DataTable();
                    SqlDataAdapter publadap = new SqlDataAdapter(publCmd);
                    publadap.Fill(publtab);
                    dataGridView1.DataSource = publtab;
                }
                catch (Exception q)
                {
                    MessageBox.Show("Ошибка: " + q.Message);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                try
                {
                    connection.Open();
                    var sqlCmd = new SqlCommand("coun_lang_tr", connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@coun", textBox3.Text);
                    sqlCmd.Parameters.AddWithValue("@lang", textBox4.Text);
                    int q = sqlCmd.ExecuteNonQuery();
                    if (q > 0) MessageBox.Show("Записи добавлены");
                    else MessageBox.Show("Записи не добавлены");
                }
                catch (Exception q)
                {
                    MessageBox.Show("Ошибка: " + q.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                try
                {
                    string publ = string.Format(@"SELECT * FROM Publi ORDER BY {0}", comboBox1.SelectedIndex + 1);
                    var publCmd = new SqlCommand(publ, connection);
                    DataTable publtab = new DataTable();
                    SqlDataAdapter publadap = new SqlDataAdapter(publCmd);
                    publadap.Fill(publtab);
                    dataGridView1.DataSource = publtab;
                }
                catch (Exception q)
                {
                    MessageBox.Show("Ошибка: " + q.Message);
                }
            }
        }
        private void Combo1()
        {
            comboBox1.Items.Add("Название");
            comboBox1.Items.Add("Тип");
            comboBox1.Items.Add("Год выпуска");
            comboBox1.Items.Add("Издатель");
            comboBox1.Items.Add("Кол-во страниц");
            comboBox1.Items.Add("Язык");
        }
        private void Combo2()
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                try
                {
                    string publ = "SELECT publisher_id 'publid',publisher_name 'publname' FROM publishers";
                    var publCmd = new SqlCommand(publ, connection);
                    DataTable publtab = new DataTable();
                    SqlDataAdapter publadap = new SqlDataAdapter(publCmd);
                    publadap.Fill(publtab);
                    comboBox2.DataSource = publtab;
                    comboBox2.DisplayMember = "publname";
                    comboBox2.ValueMember = "publid";
                }
                catch (Exception q)
                {
                    MessageBox.Show("Ошибка: " + q.Message);
                }
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                try
                {
                    connection.Open();
                    var sqlCmd = new SqlCommand("proc1", connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@id", comboBox2.SelectedValue);
                    sqlCmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception q)
                {
                    MessageBox.Show("Ошибка: " + q.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                try
                {
                    string publ = "SELECT * FROM publish_numb()";
                    var publCmd = new SqlCommand(publ, connection);
                    DataTable publtab = new DataTable();
                    SqlDataAdapter publadap = new SqlDataAdapter(publCmd);
                    publadap.Fill(publtab);
                    dataGridView1.DataSource = publtab;
                }
                catch (Exception q)
                {
                    MessageBox.Show("Ошибка: " + q.Message);
                }
            }
        }
    }
}
