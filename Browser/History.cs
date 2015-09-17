using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Браузер
{
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"data\history.txt");
            file.Close();
            textBox1.Text = "";
        }

        private void History_Load(object sender, EventArgs e)
        {
            string his = System.IO.File.ReadAllText(@"data\history.txt");

            this.Text = "История";

            textBox1.Text = his;
            
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Сохранить";
            saveFileDialog1.Filter = "Файлы блокнота|*.txt";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                saveFileDialog1.ShowDialog();
                System.IO.File.WriteAllText(saveFileDialog1.FileName, textBox1.Text);
            }
        }

        private void очиститьИсториюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"data\history.txt");
            file.Close();
            textBox1.Text = "";
        }

    }
}
