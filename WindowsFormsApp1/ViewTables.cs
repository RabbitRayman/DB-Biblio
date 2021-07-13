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
    public partial class ViewTables : Form
    {
        public ViewTables()
        {
            InitializeComponent();
            LoadCount();
        }
        string conn = Form1.Conn.connect;

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
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            Close();
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
    }
}
