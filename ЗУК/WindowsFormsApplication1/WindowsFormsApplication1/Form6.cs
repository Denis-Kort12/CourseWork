using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form6 : Form
    {
        public int[] data = new int[3];//Глобальные переменные для подсчета времени
        int[] vrem = new int[3];//Глобальные переменные для подсчета времени
        int[] itog = new int[6];//проба

        long Sum;

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

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateWaitableTimer(IntPtr lpTimerAttributes,
        bool bManualReset, string lpTimerName);

        [DllImport("kernel32.dll")]
        public static extern bool SetWaitableTimer(IntPtr hTimer, [In] ref long
        pDueTime, int lPeriod, IntPtr pfnCompletionRoutine, IntPtr
        lpArgToCompletionRoutine, bool fResume);

        [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
        public static extern Int32 WaitForSingleObject(IntPtr handle, uint
        milliseconds);

        static IntPtr handle;
        public void SetWaitForWakeUpTime()//static
        {
            long duetime = -(Sum*10000000);
            
            handle = CreateWaitableTimer(IntPtr.Zero, true, "MyWaitabletimer"); 
            SetWaitableTimer(handle, ref duetime, 0, IntPtr.Zero, IntPtr.Zero, true); 
            uint INFINITE = 0xFFFFFFFF;
            int ret = WaitForSingleObject(handle, INFINITE);

            SomeMethod();
           
            MessageBox.Show("Здравствуйте)"); 
        }

//вкл. монитора

        [DllImport("user32.dll")] 
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(IntPtr dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        private const int MOVE = 0x0001;
        private const int HWND_BROADCAST = 0xffff;
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MONITORPOWER = 0xF170;

        public void SomeMethod()
        {
            SendMessage((IntPtr)HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, -1);
            mouse_event((IntPtr)MOVE, 0, 0, 0, UIntPtr.Zero);
        }

        //создание потока

        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;

            Form3 frm = new Form3();
            frm.Show();

            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (raschet() <= 0) { label2.Text = "Не правильно установлено время"; return; }
            else
            {
                label2.Text = "";

                dateTimePicker1.Enabled = false;
                Class1 buttons = new Class1();
                buttons.buttonClicked(button2,button3);

            }
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

             Form3 frm = new Form3();


            string[] strok = File.ReadAllLines("zadachi.txt");//проверка файла на пустоту

            if (strok.Length == 0)
            {
                book.id = i;
                book.name = "Пробуждение";
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
                book.name = "Пробуждение";
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

            // SetWaitForWakeUpTime();//ф-ция пробуждения
            Thread thr1 = new Thread(ThreadFunction);
            thr1.Start();

            Thread thr = new Thread(ThreadFunction);
            thr.Start();


        }

            public void ThreadFunction()
            {
               SetWaitForWakeUpTime();
            }

        private void Form6_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists("zadachi.txt") == false)
            { System.IO.File.Create("zadachi.txt"); }

            label2.Text = "";

            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;

            label2.Parent = pictureBox1;
            label2.BackColor = Color.Transparent;

        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void Form6_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized) Hide();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
    }
}
