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
    public partial class Form1 : Form
    {

        public void Naim()
        {
           label3.Text = Environment.UserName;
        }


        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            Form2 frm = new Form2();
            frm.Show();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Naim();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            label1.Parent = pictureBox1;
           label1.BackColor = Color.Transparent;

            label3.Parent = pictureBox1;
            label3.BackColor = Color.Transparent;

            label2.Parent = pictureBox1;
            label2.BackColor = Color.Transparent;

        }
    }
}
