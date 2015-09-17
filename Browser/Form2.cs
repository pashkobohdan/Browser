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
    public partial class Form2 : Form
    {
        string[] lines;
        DataTable table;
        DataSet in_data;
        int count;
        string firstPage;
        Form1 frmFirst;

        private void read_file()
        {
            
                lines = System.IO.File.ReadAllLines(@"data\bookmark.txt");
                int x = 0;
                foreach (string line in lines)
                {
                    if (x == 0)
                    {
                        firstPage = line;
                        x = 1;
                    }
                    else
                    {
                        table.Rows.Add(line);
                    }
                }
            
        }

        private void write_file()
        {
            string[] l = new string[table.Rows.Count+1];
            l[0] = firstPage;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                l[i+1] = dataGridView1[0, i].Value.ToString();
            }

            lines = null;
            lines = l;


            System.IO.StreamWriter file = new System.IO.StreamWriter(@"data\bookmark.txt");
            foreach (string line in lines)
            {
                file.WriteLine(line);
            }
            file.Close();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            frmFirst = this.Owner as Form1;
            this.Text = "Управление закладками";

            table = new DataTable();
            in_data = new DataSet();

           // dataGridView1.Columns[0].Width += 1 ;


            dataGridView1.DataSource = table;
            table.Columns.Add("Закладки");
            in_data.Tables.Add(table);
            read_file();

            
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                table.Rows.Add();
                ++count;
            }
            catch (System.ArgumentException) { }
            catch (System.InvalidCastException) { }
            catch (System.Data.ConstraintException) { }
            catch (System.Data.NoNullAllowedException) { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //table.Rows.Remove();
            try
            {
                table.Rows[dataGridView1.CurrentRow.Index].Delete();
                --count;
            }
            catch (System.Data.DeletedRowInaccessibleException) { }
            catch (System.IndexOutOfRangeException) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            write_file();
            frmFirst.read_from_file();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
 
    }
}
