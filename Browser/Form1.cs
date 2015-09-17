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
    
    public partial class Form1 : Form
    {
        public string[] lines;
        public string first_page;

        string[] adress;
        string[] name;

        string[] history;

        private void write_date()
        {
            System.IO.StreamReader l_date = new System.IO.StreamReader(@"data\last_date.txt");
            string last_date=l_date.ReadLine();
            l_date.Close();
            if (last_date != DateTime.Now.ToString("dd:MM:yyyy"))
            {

                history = System.IO.File.ReadAllLines(@"data\history.txt");

                System.IO.StreamWriter file = new System.IO.StreamWriter(@"data\history.txt");
                foreach (string line in history)
                {
                    file.WriteLine(line);
                }
                file.WriteLine("\t\t\t" + DateTime.Now.ToString("dd:MM:yyyy"));
                file.Close();

                System.IO.StreamWriter date = new System.IO.StreamWriter(@"data\last_date.txt");
                date.WriteLine(DateTime.Now.ToString("dd:MM:yyyy"));
                date.Close();
            }
        }

        private void add_to_history(string adress_page)
        {
            history = System.IO.File.ReadAllLines(@"data\history.txt");

            System.IO.StreamWriter file = new System.IO.StreamWriter(@"data\history.txt");
            foreach (string line in history)
            {
                file.WriteLine(line);
            }
            file.WriteLine(DateTime.Now.ToString("hh:mm:ss"));
            file.WriteLine(adress_page);
            file.Close();
        }

       public void re_adress()
        {
            try
            {
                name = System.IO.File.ReadAllLines(@"data\adress.txt");
                int x = 0;
                foreach (string l in name)
                {
                    adress[x] = l;
                    x++;
                }

            }
            catch (System.IO.IOException){}

            try
            {
                System.IO.StreamReader file =new System.IO.StreamReader(@"data\name.txt");

                button5.Text = file.ReadLine();
                button6.Text = file.ReadLine();
                button7.Text = file.ReadLine();
                button8.Text = file.ReadLine();
                button9.Text = file.ReadLine();
                button10.Text = file.ReadLine();
                
                file.Close();
            }
            catch (System.IO.IOException){}
        }

        private void navigate(String address)
        {
            if (String.IsNullOrEmpty(address)) return;
            if (address.Equals("about:blank")) return;
            if (!address.StartsWith("http://") &&
                !address.StartsWith("https://"))
            {
                address = "http://" + address;
            }
            try
            {
                webBrowser1.Navigate(new Uri(address));
            }
            catch (System.UriFormatException)
            {
                return;
            }
            webBrowser1.Focus();
            add_to_history(address);
        }

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            write_date();

            this.Text = "Browser";
            webBrowser1.ScriptErrorsSuppressed = true;

            read_from_file();
            navigate(first_page);
            name = new string[6];
            adress = new string[6];
            re_adress();
        }

        public void read_from_file()
        {
            try
            {
                comboBox2.Items.Clear();

                lines = System.IO.File.ReadAllLines(@"data\bookmark.txt");
                int x = 0;
                foreach (string line in lines)
                {
                    if (x == 0)
                    {
                        first_page = line;
                        x = 1;
                    }
                    else
                    {
                        comboBox2.Items.Add(line);
                    }
                }
               
            }
            catch (System.IO.IOException)
            {

            }
        }

        private void write_to_file()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"data\bookmark.txt");
            file.WriteLine(first_page);
            foreach (string line in lines)
            {
                file.WriteLine(line);
            }
            file.Close();
        }

        private void add_to_bookmark(){
            int povt = 0;
            foreach (string line in lines)
            {
                if (line == webBrowser1.Url.ToString())
                {
                    povt++;
                }
            }
            if (povt == 0)
            {
                comboBox2.Items.Add(webBrowser1.Url.ToString());
                String[] l = new String[lines.Length + 1];
                int x = 0;
                foreach (string line in lines)
                {
                    l[x] = line;
                    x++;
                }
                lines = l;
                lines[x] = webBrowser1.Url.ToString();
                write_to_file();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            navigate("www.google.com");
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowSaveAsDialog();
        }

        private void свойстваСтраницыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPropertiesDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                navigate(textBox1.Text);
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            textBox1.Text = webBrowser1.Url.ToString();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            navigate(lines[comboBox2.SelectedIndex+1]);
        }

        private void добавитьВЗакладкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            add_to_bookmark();
        }

        private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Enter)
            {
                webBrowser1.Document.Focus();
            }
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            webBrowser1.Navigate(webBrowser1.StatusText);
            e.Cancel = true;
        }

        private void сделатьСтартовойСтраницейToolStripMenuItem_Click(object sender, EventArgs e)
        {
                first_page = webBrowser1.Url.ToString();
                write_to_file();
        }

        private void двеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Owner = this;
            form2.Show();
        }

        private void печататьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintDialog();
        }

        private void свойстваToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPageSetupDialog();
        }

        private void предпросмотрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintPreviewDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            navigate(adress[0]);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            navigate(adress[1]);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            navigate(adress[2]);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            navigate(adress[3]);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            navigate(adress[4]);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            navigate(adress[5]);
        }

        private void изменитьКнопкиБыстрогоДоступаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Owner = this;
            form3.Show();
        }

        private void историяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            History history = new History();
            history.Owner = this;
            history.Show();
        }
    }
    
}












