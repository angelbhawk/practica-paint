using Paint.Componentes;
using Paint.Manejadores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form : System.Windows.Forms.Form
    {
        const int grip = 16;
        const int caption = 40;
        manejadorGrafico Fuente;

        public Form()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                Point p = new Point(m.LParam.ToInt32());
                p = this.PointToClient(p);
                if (p.Y < caption && p.Y >= grip)
                {
                    m.Result = (IntPtr)2;
                    return;
                }
                if (p.X >= this.ClientSize.Width - grip && p.Y >= this.ClientSize.Height - grip)
                {
                    m.Result = (IntPtr)17;
                    return;
                }
                if (p.X <= grip && p.Y >= this.ClientSize.Height - grip)
                {
                    m.Result = (IntPtr)16;
                    return;
                }
                if (p.X <= grip)
                {
                    m.Result = (IntPtr)10;
                    return;
                }
                if (p.X >= ClientSize.Width - grip)
                {
                    m.Result = (IntPtr)11;
                    return;
                }
                if (p.Y <= grip)
                {
                    m.Result = (IntPtr)12;
                    return;
                }
                if (p.Y >= this.ClientSize.Height - grip)
                {
                    m.Result = (IntPtr)15;
                    return;
                }
            }
            base.WndProc(ref m);
        }
        private void Form_Load(object sender, EventArgs e)
        {
            Fuente = new manejadorGrafico();
        }
    }
}
