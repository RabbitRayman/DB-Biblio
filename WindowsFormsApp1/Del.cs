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
    public partial class Del : Form
    {
        public Del()
        {
            InitializeComponent();
            LoadCount();
        }
        string conn = Form1.Conn.connect;
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            Close();
        }

        private void LoadCount()
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                string count = "SELECT country_name 'Название страны' FROM countries";
                var countCmd = new SqlCommand(count, connection);
                DataTable counttab = new DataTable();
                SqlDataAdapter countadap = new SqlDataAdapter(countCmd);
                countadap.Fill(counttab);
                dataGridView1.DataSource = counttab;

                string count1 = "SELECT country_id,country_name FROM countries";
                var countCmd1 = new SqlCommand(count1, connection);
                DataTable counttab1 = new DataTable();
                SqlDataAdapter countadap1 = new SqlDataAdapter(countCmd1);
                countadap1.Fill(counttab1);
                comboBox1.DataSource = counttab1;
                comboBox1.DisplayMember = "country_name";
                comboBox1.ValueMember = "country_id";
            }
        }
        private void LoadLang()
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                string lang = "SELECT language_name 'Язык' FROM languages";
                var langCmd = new SqlCommand(lang, connection);
                DataTable langtab = new DataTable();
                SqlDataAdapter langadap = new SqlDataAdapter(langCmd);
                langadap.Fill(langtab);
                dataGridView1.DataSource = langtab;

                string lang1 = "SELECT language_id,language_name FROM languages";
                var langCmd1 = new SqlCommand(lang1, connection);
                DataTable langtab1 = new DataTable();
                SqlDataAdapter langadap1 = new SqlDataAdapter(langCmd1);
                langadap1.Fill(langtab1);
                comboBox2.DataSource = langtab1;
                comboBox2.DisplayMember = "language_name";
                comboBox2.ValueMember = "language_id";
            }
        }
        private void LoadType()
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                string type = "SELECT type_name 'Тип публикации' FROM publication_type";
                var typeCmd = new SqlCommand(type, connection);
                DataTable typetab = new DataTable();
                SqlDataAdapter typeadap = new SqlDataAdapter(typeCmd);
                typeadap.Fill(typetab);
                dataGridView1.DataSource = typetab;

                string type1 = "SELECT type_id,type_name FROM publication_type";
                var typeCmd1 = new SqlCommand(type1, connection);
                DataTable typetab1 = new DataTable();
                SqlDataAdapter typeadap1 = new SqlDataAdapter(typeCmd1);
                typeadap1.Fill(typetab1);
                comboBox3.DataSource = typetab1;
                comboBox3.DisplayMember = "type_name";
                comboBox3.ValueMember = "type_id";
            }
        }
        private void LoadAuth()
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                string auth = "SELECT a.author_lastname 'Фамилия', a.author_firstname 'Имя',a.author_middlename 'Отчество/Среднее имя', a.birthdate 'Дата рождения', c.country_name 'Страна', a.author_publs 'Количество публикаций' FROM authors a INNER JOIN countries c ON a.author_country=c.country_id";
                var authCmd = new SqlCommand(auth, connection);
                DataTable authtab = new DataTable();
                SqlDataAdapter authadap = new SqlDataAdapter(authCmd);
                authadap.Fill(authtab);
                dataGridView1.DataSource = authtab;

                string auth1 = "SELECT * FROM authors";
                var authCmd1 = new SqlCommand(auth1, connection);
                DataTable authtab1 = new DataTable();
                SqlDataAdapter authadap1 = new SqlDataAdapter(authCmd1);
                authadap1.Fill(authtab1);
                DataColumn comp1 = new DataColumn("col1", typeof(string));
                comp1.Expression = "[author_lastname]+' '+[author_firstname] +' '+[author_middlename]+', '+[birthdate]";
                authtab1.Columns.Add(comp1);
                comboBox4.DataSource = authtab1;
                comboBox4.DisplayMember = "col1";
                comboBox4.ValueMember = "author_id";
            }
        }
        private void LoadPublish()
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                string publish = "SELECT p.publisher_name 'Название издательства', p.open_year 'Год основания',p.publisher_director 'Директор', c.country_name 'Страна' FROM publishers p INNER JOIN countries c ON p.publisher_country=c.country_id";
                var publishCmd = new SqlCommand(publish, connection);
                DataTable publishtab = new DataTable();
                SqlDataAdapter publishadap = new SqlDataAdapter(publishCmd);
                publishadap.Fill(publishtab);
                dataGridView1.DataSource = publishtab;

                string publ = "SELECT p.publisher_id 'publid',p.publisher_name 'publname', c.country_name 'countname' FROM publishers p INNER JOIN countries c ON p.publisher_country=c.country_id";
                var publCmd = new SqlCommand(publ, connection);
                DataTable publtab = new DataTable();
                SqlDataAdapter publadap = new SqlDataAdapter(publCmd);
                publadap.Fill(publtab);
                DataColumn comp = new DataColumn("col1", typeof(string));
                comp.Expression = "[publname]+', '+[countname]";
                publtab.Columns.Add(comp);
                comboBox5.DataSource = publtab;
                comboBox5.DisplayMember = "col1";
                comboBox5.ValueMember = "publid";
            }
        }
        private void LoadPubl()
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                string publ = "SELECT p.publication_name 'Название', pt.type_name 'Тип', p.publication_year 'Год выпуска', CONCAT_WS(', ',ps.publisher_name,c.country_name) 'Издательство', p.pages 'Объём (кол-во страниц)',l.language_name 'Язык издания' FROM publications p INNER JOIN publication_type pt ON p.publication_type=pt.type_id INNER JOIN publishers ps ON p.publisher=ps.publisher_id INNER JOIN languages l ON p.publ_language=l.language_id INNER JOIN countries c ON ps.publisher_country=c.country_id";
                var publCmd = new SqlCommand(publ, connection);
                DataTable publtab = new DataTable();
                SqlDataAdapter publadap = new SqlDataAdapter(publCmd);
                publadap.Fill(publtab);
                dataGridView1.DataSource = publtab;

                string publ1 = "SELECT p.publication_id 'publid', p.publication_name 'name', pt.type_name 'type', l.language_name 'lang', p.publication_year 'year' FROM publications p INNER JOIN publication_type pt ON p.publication_type=pt.type_id INNER JOIN languages l ON p.publ_language=l.language_id";
                var publCmd1 = new SqlCommand(publ1, connection);
                DataTable publtab1 = new DataTable();
                SqlDataAdapter publadap1 = new SqlDataAdapter(publCmd1);
                publadap1.Fill(publtab1);
                DataColumn comp = new DataColumn("col1", typeof(string));
                comp.Expression = "[type]+': '+[name] +'. Язык: '+[lang]+'. Год выпуска: '+[year]";
                publtab1.Columns.Add(comp);
                comboBox6.DataSource = publtab1;
                comboBox6.DisplayMember = "col1";
                comboBox6.ValueMember = "publid";
            }
        }
        private void LoadPublAuth()
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                string publauth = "SELECT CONCAT_WS(' ', a.author_lastname,a.author_firstname,a.author_middlename,a.birthdate) 'Автор', CONCAT_WS('; ',p.publication_name, pt.type_name, l.language_name, p.publication_year) 'Публикация' FROM publication_author pa INNER JOIN authors a ON pa.author=a.author_id INNER JOIN publications p ON pa.publication=p.publication_id INNER JOIN publication_type pt ON p.publication_type=pt.type_id INNER JOIN languages l ON p.publ_language=l.language_id";
                var publauthCmd = new SqlCommand(publauth, connection);
                DataTable publauthtab = new DataTable();
                SqlDataAdapter publauthadap = new SqlDataAdapter(publauthCmd);
                publauthadap.Fill(publauthtab);
                dataGridView1.DataSource = publauthtab;

                string publauth1 = "SELECT pa.id 'id', CONCAT_WS(' ', a.author_lastname,a.author_firstname,a.author_middlename,a.birthdate) 'Auth', CONCAT_WS('; ',p.publication_name, pt.type_name, l.language_name, p.publication_year) 'Publ' FROM publication_author pa INNER JOIN authors a ON pa.author=a.author_id INNER JOIN publications p ON pa.publication=p.publication_id INNER JOIN publication_type pt ON p.publication_type=pt.type_id INNER JOIN languages l ON p.publ_language=l.language_id";
                var publauthCmd1 = new SqlCommand(publauth1, connection);
                DataTable publauthtab1 = new DataTable();
                SqlDataAdapter publauthadap1 = new SqlDataAdapter(publauthCmd1);
                publauthadap1.Fill(publauthtab1);
                DataColumn comp = new DataColumn("col1", typeof(string));
                comp.Expression = "[Auth]+' | '+[Publ]";
                publauthtab1.Columns.Add(comp);
                comboBox7.DataSource = publauthtab1;
                comboBox7.DisplayMember = "col1";
                comboBox7.ValueMember = "id";
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case (0):
                    {
                        LoadCount();
                        break;
                    }
                case (1):
                    {
                        LoadLang();
                        break;
                    }
                case (2):
                    {
                        LoadType();
                        break;
                    }
                case (3):
                    {
                        LoadAuth();
                        break;
                    }
                case (4):
                    {
                        LoadPublish();
                        break;
                    }
                case (5):
                    {
                        LoadPubl();
                        break;
                    }
                case (6):
                    {
                        LoadPublAuth();
                        break;
                    }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                try
                {
                    connection.Open();
                    var sqlCmd = new SqlCommand("countries_delete", connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@id", comboBox1.SelectedValue);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена");
                    LoadCount();
                }
                catch (Exception q)
                {
                    MessageBox.Show("Ошибка: " + q.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                try
                {
                    connection.Open();
                    var sqlCmd = new SqlCommand("languages_delete", connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@id", comboBox2.SelectedValue);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена");
                    LoadLang();
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
                    var sqlCmd = new SqlCommand("publtype_delete", connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@id", comboBox3.SelectedValue);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена");
                    LoadType();
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
                    var sqlCmd = new SqlCommand("authors_delete", connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@id", comboBox4.SelectedValue);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена");
                    LoadAuth();
                }
                catch (Exception q)
                {
                    MessageBox.Show("Ошибка: " + q.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                try
                {
                    connection.Open();
                    var sqlCmd = new SqlCommand("publishers_delete", connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@id", comboBox5.SelectedValue);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена");
                    LoadPublish();
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
                    connection.Open();
                    var sqlCmd = new SqlCommand("publications_delete", connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@id", comboBox6.SelectedValue);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена");
                    LoadPubl();
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
                    connection.Open();
                    var sqlCmd = new SqlCommand("publ_auth_delete", connection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@id", comboBox7.SelectedValue);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена");
                    LoadPublAuth();
                }
                catch (Exception q)
                {
                    MessageBox.Show("Ошибка: " + q.Message);
                }
            }
        }
    }
}
