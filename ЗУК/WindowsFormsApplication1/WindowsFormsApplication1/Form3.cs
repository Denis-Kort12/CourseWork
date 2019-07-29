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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;

            this.Visible = false;

            Form6 frm = new Form6();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;

            this.Visible = false;
       
            Form4 frm = new Form4();
            frm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;

            this.Visible = false; 

            Form2 frm = new Form2();
            frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;

            this.Visible = false;

            Form5 frm = new Form5();
            frm.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;

            label3.Parent = pictureBox1;
            label3.BackColor = Color.Transparent;

            label4.Parent = pictureBox1;
            label4.BackColor = Color.Transparent;

        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void Form3_Resize(object sender, EventArgs e)
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
