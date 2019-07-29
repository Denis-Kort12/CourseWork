using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form5 : Form
    {
        public int[] data = new int[3];//Глобальные переменные для подсчета времени
        int[] vrem = new int[3];//Глобальные переменные для подсчета времени
        int[] itog = new int[6];//проба

        long Sum;
        int q; //определение сна,гибернации, либо завершение работы
        int sec = 0;

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

        public Form5()
        {
            InitializeComponent();
        }   

        private void button2_Click(object sender, EventArgs e)
        {
            int n = 0;
            if (dateTimePicker1.Enabled == true) n = 0;
            else if (dateTimePicker1.Enabled == false) n = 1;
           // else if (panel1.Enabled == false) n = 2;

          switch (n)
          {
              case 0:
                    {

                        if (raschet() <= 0) { label2.Text = "Не правильно установлено время"; return; }
                        else
                        {
                            label2.Text = "";

                            dateTimePicker1.Enabled = false;

                            button2.Enabled = false;

                            comboBox1.Visible = true;

                            label3.Visible = true;
                        }

                        break;
                    }
              case 1:
                    {
                        if (comboBox1.Text == "Гибернация") q = 0; //определение сна,гибернации, либо завершение работы
                        if (comboBox1.Text == "Сон") q = 1; //определение сна,гибернации, либо завершение работы
                        if (comboBox1.Text == "Выключить") q = 2; //определение сна,гибернации, либо завершение работы

                        comboBox1.Enabled = false; 
                        button2.Enabled = false;
                        button3.Enabled = true;

                        break;
                    }
          }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;

            this.Visible = false;

            Form3 frm = new Form3();
            frm.Show();
        }


        struct Book
        {
            public int id;
            public string name;
            public string cret;

        }


        private void button3_Click(object sender, EventArgs e)
        {
            Book book; int i = 1;

            notifyIcon1.Visible = false;

            Sum = raschet();//Секунды для таймера
            timer1.Enabled = true;
            timer1.Start();

           Form3 frm = new Form3();

            if (comboBox1.Text == "Гибернация")  book.name = "Гибернация"; 
            else book.name = "Сон"; 
            
            book.cret = Convert.ToString(dateTimePicker1.Value);

            string[] strok = File.ReadAllLines("zadachi.txt");//проверка файла на пустоту

            if (strok.Length == 0)
            {
                book.id = i;              

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

                StreamWriter sw1 = new StreamWriter(@"put.txt"); //для хранения задач и переноса в Listbox

                sw1.Write(Environment.NewLine);

                sw1.Close();

            }
            else
            {

                StreamWriter sw1 = File.AppendText("put.txt");

                sw1.Write(Environment.NewLine);

                sw1.Close();

            }


            frm.Show();
            this.Visible = false;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "") button2.Enabled = false;
            else button2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {          
            sec++; 

            if (q == 0 && sec == Sum)
            {
                bool isHibernate = Application.SetSuspendState(PowerState.Hibernate, false, false);
                if (isHibernate == false) MessageBox.Show("Could not hybernate the system.");
                timer1.Stop();
            }

            if (q == 1 && sec == Sum)
            {
                 bool isSuspend = Application.SetSuspendState(PowerState.Suspend, true, true);
                 if (isSuspend == false) MessageBox.Show("Could not suspend the system.");
                 timer1.Stop();
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists("zadachi.txt") == false)
            { System.IO.File.Create("zadachi.txt"); }

            label2.Text = "";

            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent; 

            label2.Parent = pictureBox1;
            label2.BackColor = Color.Transparent;

            label3.Parent = pictureBox1;
            label3.BackColor = Color.Transparent;
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void Form5_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized) Hide();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
    }
}
