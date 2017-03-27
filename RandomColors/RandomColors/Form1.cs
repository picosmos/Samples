using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomColors
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var rnd = new Random();

            for (var i = 0; i < 58; ++i)
            {
                for (var z = 0; z < 100; ++z)
                {
                    var rR = rnd.Next(0, 256);
                    var rG = rnd.Next(0, 256);
                    var rB = rnd.Next(0, 256);


                    var myColor = Color.FromArgb(rR, rG, rB);
                    graphics.FillRectangle(new SolidBrush(myColor), z * 10, i * 10, 9, 9);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
