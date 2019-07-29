using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        public int[] data = new int[3];//Глобальные переменные для подсчета времени
               int[] vrem = new int[3];//Глобальные переменные для подсчета времени
               Book1[] book1 = new Book1[10];

        int[] itog = new int[6];//проба
        long Sum;

        int sec = 0; int sec1 = 0; int sec2 = 0; int sec3 = 0;
        int sec4 = 0; int sec5 = 0; int sec6 = 0; int sec7 = 0;
        int sec8 = 0;

        int j = 1;

        public long raschet()
        {
            data[0] = Convert.ToInt16(dateTimePicker1.Value.Day);
            data[1] = Convert.ToInt16(dateTimePicker1.Value.Month);
            data[2] = Convert.ToInt16(dateTimePicker1.Value.Year);

            vrem[0] = Convert.ToInt16(dateTimePicker1.Value.Hour);
            vrem[1] = Convert.ToInt16(dateTimePicker1.Value.Minute);
            vrem[2] = Convert.ToInt16(dateTimePicker1.Value.Second);


            System.DateTime date2 = new System.DateTime(data[2], data[1], data[0], vrem[0], vrem[1], vrem[2]);
            System.DateTime date1 = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second);

            System.TimeSpan diff1 = date2.Subtract(date1);

            long day1 = Convert.ToInt32(diff1.Days); //Количестов дней до установленного пользователем время
            int hour1 = Convert.ToInt32(diff1.Hours); //Количестов часов до установленного пользователем время
            int minute1 = Convert.ToInt32(diff1.Minutes); //Количестов минут до установленного пользователем время
            int secund1 = Convert.ToInt32(diff1.Seconds); //Количестов секунд до установленного пользователем время

            Sum = ((day1 * 24 + hour1) * 60 + minute1) * 60 + secund1;

            return Sum;
        }



        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.ShowDialog();
            textBox1.Text = ofd.FileName;

        }

        private void button3_Click(object sender, EventArgs e)//доработать
        {
            int n = 0;

            label5.Text = "";

            if (textBox1.Visible == false) n = 0;
            else if (dateTimePicker1.Visible == false) n = 1; 
                 else if (dateTimePicker1.Visible == true) n = 2; 
            
            
            switch (n)
            {
                case 0:
                {
                        label1.Visible = true;
                        textBox1.Visible = true;
                        button1.Visible = true;

                        textBox2.Enabled = false;
                        button3.Enabled = false;


                   break;
                }

                case 1:
                {
                        label2.Visible = true;
                        dateTimePicker1.Visible = true;

                        button1.Enabled = false;
                        textBox1.Enabled = false;//Поменять!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! false
                 
                        break;

                }

                case 2:
                {
                        if (raschet() <= 0) { label5.Text = "Не правильно установлено время";  break; }
                        else
                        {
                           button3.Enabled = false;

                           dateTimePicker1.Enabled = false;

                            button4.Enabled = true;
                        }

                        break;
                }
                
            }
        }       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "") button3.Enabled = false;         
            else button3.Enabled = true;
        }


        struct Book
        {
            public int id;
            public string name;
            public string cret;
            public string puti;
            

        }

        struct Book1
        {
           public string kol;
        }


        private void button4_Click(object sender, EventArgs e) //Возвращение на Form3 по нажатию кнопки готово 
        {
            notifyIcon1.Visible = false;

            Form3 frm = new Form3();

           Book book; int i = 1;

            Sum = raschet();//Секунды для таймера

            //проверка таймеров

           if (Sum <= 0)
           {
              frm.Show();
              this.Visible = false;
           }
           else
           {
                for (j = 1; j < 10; j++)
                {
                    if (book1[j].kol == "false")
                    {
                        switch (j)
                        {
                            case 1: { timer1.Enabled = true; book1[1].kol = "true"; break; }
                            case 2: { timer2.Enabled = true; book1[2].kol = "true"; break; }
                            case 3: { timer3.Enabled = true; book1[3].kol = "true"; break; }
                            case 4: { timer4.Enabled = true; book1[4].kol = "true"; break; }
                            case 5: { timer5.Enabled = true; book1[5].kol = "true"; break; }
                            case 6: { timer6.Enabled = true; book1[6].kol = "true"; break; }
                            case 7: { timer7.Enabled = true; book1[7].kol = "true"; break; }
                            case 8: { timer8.Enabled = true; book1[8].kol = "true"; break; }
                            case 9: { timer9.Enabled = true; book1[9].kol = "true"; break; }

                        }

                        StreamWriter sw3 = new StreamWriter(@"timer.txt"); //для хранения задач и переноса в Listbox

                        for (j = 1; j < 10; j++) sw3.WriteLine(book1[j].kol, Environment.NewLine);

                        sw3.Close();

                    }
                    else if (j == 9)
                    {
                        MessageBox.Show("Лимит задач привышен(вы можете указать максимум 9 задач в этом разделе!");
                        frm.Show();

                        this.Visible = false;
                    }
                }

             }      
                                  

            string[] strok = File.ReadAllLines("zadachi.txt");//проверка файла на пустоту

            if (strok.Length == 0)
            {
                book.id = i;
                book.name = textBox2.Text;
                book.cret = Convert.ToString(dateTimePicker1.Value);          

                StreamWriter sw = new StreamWriter(@"zadachi.txt"); //для хранения задач и переноса в Listbox

                sw.Write(book.id + ". <<");
                sw.Write(book.name + ">> ");
                sw.WriteLine(book.cret + " ", Environment.NewLine);

                sw.Close();

            }
            else
            { 
                book.id = System.IO.File.ReadAllLines("zadachi.txt").Length + 1;
                book.name = textBox2.Text;
                book.cret = Convert.ToString(dateTimePicker1.Value);

                StreamWriter sw = File.AppendText("zadachi.txt");

                sw.Write(book.id + ". <<");
                sw.Write(book.name + ">> ");
                sw.WriteLine(book.cret + " ", Environment.NewLine);

                sw.Close();

            }


            string[] strok1 = File.ReadAllLines("put.txt");//проверка файла на пустоту

            if (strok1.Length == 0)
            {
                              
                book.puti = textBox1.Text;

                StreamWriter sw1 = new StreamWriter(@"put.txt"); //для хранения задач и переноса в Listbox

                 sw1.WriteLine(book.puti, Environment.NewLine);

                sw1.Close();

            }
            else
            {
                book.puti = textBox1.Text;

                StreamWriter sw1 = File.AppendText("put.txt");

                 sw1.WriteLine(book.puti,Environment.NewLine);

                sw1.Close();

            }

            frm.Show();

                this.Visible = false;
            
            }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "") button3.Enabled = false;
            else button3.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)//Кнопка отмены
        {
            notifyIcon1.Visible = false;

            this.Visible = false;

            Form3 frm = new Form3();
            frm.Show(); 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sec++;

            if (sec == Sum)
            {
                F();

                book1[1].kol = "false";

                StreamWriter sw4 = new StreamWriter(@"timer.txt"); //для хранения задач и переноса в Listbox

                for (j = 1; j < 10; j++) sw4.WriteLine(book1[j].kol, Environment.NewLine);

                sw4.Close();

                F();

                Process.Start(textBox1.Text);
                timer1.Stop();
            }

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            /*if (System.IO.File.Exists("zadachi.txt") == false)
            { System.IO.File.Create("zadachi.txt");  }

            if (System.IO.File.Exists("put.txt") == false)
            { System.IO.File.Create("put.txt"); }

            if (System.IO.File.Exists("timer.txt") == false)
            { System.IO.File.Create("timer.txt"); }*/

            label5.Text = "";

            label2.Parent = pictureBox1;
            label2.BackColor = Color.Transparent;

            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;

            label5.Parent = pictureBox1;
            label5.BackColor = Color.Transparent;

            label4.Parent = pictureBox1;
            label4.BackColor = Color.Transparent;
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void Form4_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized) Hide();
        }

        private void Form4_Activated(object sender, EventArgs e)
        {
            string[] strok2 = File.ReadAllLines("timer.txt");//проверка файла на пустоту

            if (strok2.Length == 0)
            {

                StreamWriter sw2 = new StreamWriter(@"timer.txt"); //для хранения задач и переноса в Listbox

                sw2.WriteLine("false", Environment.NewLine);
                sw2.WriteLine("false", Environment.NewLine);
                sw2.WriteLine("false", Environment.NewLine);
                sw2.WriteLine("false", Environment.NewLine);
                sw2.WriteLine("false", Environment.NewLine);
                sw2.WriteLine("false", Environment.NewLine);
                sw2.WriteLine("false", Environment.NewLine);
                sw2.WriteLine("false", Environment.NewLine);
                sw2.WriteLine("false", Environment.NewLine);

                sw2.Close();

                 string line;

                 StreamReader sr = new StreamReader(@"timer.txt");
                 while ((line = sr.ReadLine()) != null)
                 {
                     book1[j].kol = line;
                     j++;
                 }
                 sr.Close();

                j = 1;
            }
            else
            {
                j = 1;

                string line1;

                StreamReader sr1 = new StreamReader(@"timer.txt");
                while ((line1 = sr1.ReadLine()) != null)
                {
                    book1[j].kol = line1;
                    j++;
                }
                sr1.Close();

                j = 1;

            }
        }

        public void F()
        {
            j = 1;

            string line1;

            StreamReader sr1 = new StreamReader(@"timer.txt");
            while ((line1 = sr1.ReadLine()) != null)
            {
                book1[j].kol = line1;
                j++;
            }
            sr1.Close();

            j = 1;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            sec1++;

            if (sec1 == Sum)
            {
                F();

                book1[2].kol = "false";

                StreamWriter sw5 = new StreamWriter(@"timer.txt"); //для хранения задач и переноса в Listbox

                for (j = 1; j < 10; j++) sw5.WriteLine(book1[j].kol, Environment.NewLine);

                sw5.Close();

                F();

                Process.Start(textBox1.Text);
                timer2.Stop();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {            
            sec2++;

            if (sec2 == Sum)
            {
                F();

                book1[3].kol = "false";

                StreamWriter sw4 = new StreamWriter(@"timer.txt"); //для хранения задач и переноса в Listbox

                for (j = 1; j < 10; j++) sw4.WriteLine(book1[j].kol, Environment.NewLine);

                sw4.Close();

                F();

                Process.Start(textBox1.Text);
                timer3.Stop();
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            sec3++;

            if (sec3 == Sum)
            {
                F();

                book1[4].kol = "false";

                StreamWriter sw4 = new StreamWriter(@"timer.txt"); //для хранения задач и переноса в Listbox

                for (j = 1; j < 10; j++) sw4.WriteLine(book1[j].kol, Environment.NewLine);

                sw4.Close();

                F();

                Process.Start(textBox1.Text);
                timer4.Stop();
            }
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            sec4++;

            if (sec4 == Sum)
            {
                F();

                book1[5].kol = "false";

                StreamWriter sw4 = new StreamWriter(@"timer.txt"); //для хранения задач и переноса в Listbox

                for (j = 1; j < 10; j++) sw4.WriteLine(book1[j].kol, Environment.NewLine);

                sw4.Close();

                F();

                Process.Start(textBox1.Text);
                timer5.Stop();
            }
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            sec5++;

            if (sec5 == Sum)
            {
                F();

                book1[6].kol = "false";

                StreamWriter sw4 = new StreamWriter(@"timer.txt"); //для хранения задач и переноса в Listbox

                for (j = 1; j < 10; j++) sw4.WriteLine(book1[j].kol, Environment.NewLine);

                sw4.Close();

                F();

                Process.Start(textBox1.Text);
                timer6.Stop();
            }
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            sec6++;

            if (sec6 == Sum)
            {
                F();

                book1[7].kol = "false";

                StreamWriter sw4 = new StreamWriter(@"timer.txt"); //для хранения задач и переноса в Listbox

                for (j = 1; j < 10; j++) sw4.WriteLine(book1[j].kol, Environment.NewLine);

                sw4.Close();

                F();

                Process.Start(textBox1.Text);
                timer7.Stop();
            }
        }

        private void timer8_Tick(object sender, EventArgs e)
        {
            sec7++;

            if (sec7 == Sum)
            {
                F();

                book1[8].kol = "false";

                StreamWriter sw4 = new StreamWriter(@"timer.txt"); //для хранения задач и переноса в Listbox

                for (j = 1; j < 10; j++) sw4.WriteLine(book1[j].kol, Environment.NewLine);

                sw4.Close();

                F();

                Process.Start(textBox1.Text);
                timer8.Stop();
            }
        }

        private void timer9_Tick(object sender, EventArgs e)
        {
            sec8++;

            if (sec8 == Sum)
            {
                F();

                book1[9].kol = "false";

                StreamWriter sw4 = new StreamWriter(@"timer.txt"); //для хранения задач и переноса в Listbox

                for (j = 1; j < 10; j++) sw4.WriteLine(book1[j].kol, Environment.NewLine);

                sw4.Close();

                F();

                Process.Start(textBox1.Text);
                timer9.Stop();
            }
        }
    }
}
