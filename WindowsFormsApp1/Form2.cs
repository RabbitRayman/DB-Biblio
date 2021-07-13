using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add FAdd = new Add();
            FAdd.Show();
            Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ViewTables FVT = new ViewTables();
            FVT.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Alter FAl = new Alter();
            FAl.Show();
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Del FDel = new Del();
            FDel.Show();
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Quer FQuer = new Quer();
            FQuer.Show();
            Close();
        }
    }
}
