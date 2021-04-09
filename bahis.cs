using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class bahis : Form
    {
        public bahis()
        {
            InitializeComponent();
            this.Hide();
        }
        public void BackFrom() {
            this.Close();
        }
        public static int chip = 0; 
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                Form1.station = false;
                chip = 5*2;
                BackFrom();
            }
            else if (radioButton2.Checked == true)
            {
                Form1.station = false;
                chip = 10*2;
                BackFrom();
            }
            else if (radioButton3.Checked == true)
            {
                Form1.station = false;
                chip = 25*2;
                BackFrom();
            }
            else if (radioButton4.Checked == true)
            {
                Form1.station = false;
                chip = 50*2;
                BackFrom();
            }
            else 
            {
                Form1.station = false;
                chip = 100*2;
                BackFrom();
            }
            
        }

      
    }
}
