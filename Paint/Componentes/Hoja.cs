using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint.Componentes
{
    class Hoja : PictureBox
    {
        private Bitmap mBits;

        public Hoja()
        {
            this.Height = 250;
            this.Width = 400;
            this.BackColor = Color.White;
            this.Location = new Point(10, 10);
            DoubleBuffered = true;
        }

        public Bitmap MBits { get => mBits; set => mBits = value; }

        public Graphics CargarGraficos()
        {
            MBits =
                new Bitmap(this.Width, this.Height);
            this.Image = MBits;
            Graphics g = Graphics.FromImage(MBits);
            //g.Clear(Color.Black);
            return g;
        }
    }
}