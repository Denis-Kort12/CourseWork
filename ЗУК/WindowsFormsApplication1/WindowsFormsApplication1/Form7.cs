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
    public partial class Form7 : Form
    {

        public Book[] book = new Book[30];

        int i = 1;

        public Form7()
        {
            InitializeComponent();
        }


        public struct Book
        {
          public string puti;
          public string name;
        }

        private void Form7_Activated(object sender, EventArgs e)
        {
            string line;

            StreamReader sr = new StreamReader(@"zadachi.txt");
            while ((line = sr.ReadLine()) != null)
            {
                book[i].name = line;
                i++;
            }

            sr.Close();

            i = 1;

            StreamReader sr1 = new StreamReader(@"put.txt"); 
            while ((line = sr1.ReadLine()) != null)
            {
                book[i].puti = line;
                i++;    
            }

            sr1.Close();           

            Form2 frm = (Form2)this.Owner;

           int kol = frm.Num() + 1; //MyFunc - это функция основной формы.

            label1.Text = book[kol].name;
            label2.Text = book[kol].puti;

        }
    }
}
