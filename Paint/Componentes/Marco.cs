using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint.Componentes
{
     class Marco : Button
     {
        private int px, py; 
        private char dir;

        public Marco(char d) 
        {
            this.FlatStyle = FlatStyle.Standard;
            this.BackColor = Color.FromArgb(171, 178, 185);
            this.Size = new Size(10, 10);

            this.SetStyle(ControlStyles.Selectable, false);
            this.MouseMove += new MouseEventHandler(Form_MouseMove);

            Dir = d;
        }

        public char Dir { get => dir; set => dir = value; }

        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { px = e.X; py = e.Y; }
            else 
            { 
                if (Dir == 'h')
                    this.Left = this.Left + (e.X - px);
                if (Dir == 'v')
                    this.Top = this.Top + (e.Y - py); 
                if (Dir == 'a')
                {
                    this.Left = this.Left + (e.X - px);
                    this.Top = this.Top + (e.Y - py); 
                }

            }
        }
    }
}
