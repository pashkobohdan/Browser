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
    public partial class Form3 : Form
    {

        string[] adres;
        string[] name;
        Form1 frmFirst;

        public void write()
        {
            System.IO.StreamWriter names = new System.IO.StreamWriter(@"data\name.txt");


            try
            {
                names.WriteLine(textBox1.Text);
            }
            catch (System.IO.IOException)
            {
                names.WriteLine(name[0]);
            }


            try
            {
                names.WriteLine(textBox3.Text);
            }
            catch (System.IO.IOException)
            {
                 names.WriteLine(name[1]);
            }

            try
            {
                names.WriteLine(textBox5.Text);
            }
            catch (System.IO.IOException)
            {
                 names.WriteLine(name[2]);
            }


            try
            {
                names.WriteLine(textBox7.Text);
            }
            catch (System.IO.IOException)
            {
                 names.WriteLine(name[3]);
            }

            try
            {
                names.WriteLine(textBox9.Text);
            }
            catch (System.IO.IOException)
            {
                 names.WriteLine(name[4]);
            }

            try
            {
                names.WriteLine(textBox11.Text);
            }
            catch (System.IO.IOException)
            {
                 names.WriteLine(name[5]);
            }

            names.Close();









            System.IO.StreamWriter adress = new System.IO.StreamWriter(@"data\adress.txt");

            try
            {
                adress.WriteLine(textBox2.Text);
            }
            catch (System.IO.IOException)
            {
                 adress.WriteLine(adres[0]);
            }


            try
            {
                adress.WriteLine(textBox4.Text);
            }
            catch (System.IO.IOException)
            {
                 adress.WriteLine(adres[1]);
            }

            try
            {
                adress.WriteLine(textBox6.Text);
            }
            catch (System.IO.IOException)
            {
                 adress.WriteLine(adres[2]);
            }


            try
            {
                adress.WriteLine(textBox8.Text);
            }
            catch (System.IO.IOException)
            {
                 adress.WriteLine(adres[3]);
            }

            try
            {
                adress.WriteLine(textBox10.Text);
            }
            catch (System.IO.IOException)
            {
                 adress.WriteLine(adres[4]);
            }

            try
            {
                adress.WriteLine(textBox12.Text);
            }
            catch (System.IO.IOException)
            {
                 adress.WriteLine(adres[5]);
            }

            adress.Close();
        }

        public void read()
        {
            try
            {
                name = System.IO.File.ReadAllLines(@"data\adress.txt");
                int x = 0;
                foreach (string l in name)
                {
                    adres[x] = l;
                    x++;
                }

                textBox2.Text = adres[0];
                textBox4.Text = adres[1];
                textBox6.Text = adres[2];
                textBox8.Text = adres[3];
                textBox10.Text = adres[4];
                textBox12.Text = adres[5];
                
            }
            catch (System.IO.IOException) { }

            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@"data\name.txt");

                textBox1.Text = file.ReadLine();
                name[0] = textBox1.Text;
                textBox3.Text = file.ReadLine();
                name[1] = textBox3.Text;
                textBox5.Text = file.ReadLine();
                name[2] = textBox5.Text;
                textBox7.Text = file.ReadLine();
                name[3] = textBox7.Text;
                textBox9.Text = file.ReadLine();
                name[4] = textBox9.Text;
                textBox11.Text = file.ReadLine();
                name[5] = textBox11.Text;

                file.Close();
            }
            catch (System.IO.IOException) { }
        }

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            frmFirst = this.Owner as Form1;
            this.Text = "Управление страницами быстрого доступа";

            adres = new string[6];
            read();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            write();
            frmFirst.re_adress();
        }
    }
}
