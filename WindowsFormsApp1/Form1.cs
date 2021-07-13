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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        public static class Conn
        {
            public static string connect { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox2.Text == ""))
            {
                MessageBox.Show("Введите логин и пароль");
            }
            else
            {
                string connectString =string.Format(@"Data Source=*put_source_here*;database=kurs1; User ID={0};Password={1}", textBox1.Text,textBox2.Text);
                Conn.connect = connectString;
                try
                {
                    using (SqlConnection connection = new SqlConnection(Conn.connect))
                    {
                        connection.Open();
                        Form2 f2 = new Form2();
                        f2.Show();
                        Close();
                    }
                }
                catch (Exception q)
                {
                    MessageBox.Show("Error: " + q.Message);
                }
            }
        }
    }
}
