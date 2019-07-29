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
    public partial class Form2 : Form
    {      

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;

            this.Visible = false;        

            Form3 frm = new Form3();
            frm.Show();
           
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)  Hide();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
             Show();
             WindowState = FormWindowState.Normal;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            e.Cancel = true;
            Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            /*if (File.Exists("zadachi.txt") == false)
            { File.Create("zadachi.txt"); }

            if (File.Exists("put.txt") == false)
            { File.Create("put.txt"); }

            if (File.Exists("timer.txt") == false)
            { File.Create("timer.txt"); }*/

            string line;

            label2.Parent = pictureBox1;
            label2.BackColor = Color.Transparent;

            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;

            
               StreamReader sr = new StreamReader("zadachi.txt");
                while ((line = sr.ReadLine()) != null) listBox1.Items.Add(line);

            sr.Close();


        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {

            /*File.Delete("zadachi.txt");
            File.Delete("put.txt");
            File.Delete("timer.txt");*/

            StreamWriter file = new StreamWriter(@"zadachi.txt");

            file.Write("");
            file.Close();


            StreamWriter file1 = new StreamWriter(@"put.txt");

            file1.Write("");
            file1.Close();

            StreamWriter file2 = new StreamWriter(@"timer.txt");

            file2.Write("");
            file2.Close();

            listBox1.Items.Clear();

            notifyIcon1.Visible = false;

            Environment.Exit(0);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form7 frm = new Form7();
            frm.Owner = this;           

            frm.Show();


        }

        public int Num()
        {
           int number = listBox1.SelectedIndex;//Номер Записи в Listbox
            return (number);
        }

        private void button5_Click(object sender, EventArgs e)
        {

            StreamWriter file = new StreamWriter(@"zadachi.txt");    
                   
             file.Write("");
             file.Close();


            StreamWriter file1 = new StreamWriter(@"put.txt");

             file1.Write("");
             file1.Close();

            StreamWriter file2 = new StreamWriter(@"timer.txt");

             file2.Write("");
             file2.Close();

            listBox1.Items.Clear();

        }

        
    }
}
