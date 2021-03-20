using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Componentes
{
    class Figuras3D
    {
        private int x;
        private int y;
        private Color c;
        private string t;
        //clase que contiene todas las figuras 2D y 3D restantes en forma de métodos
        public Figuras3D()
        {
        }
        public void Cuadro(int Cx1, int Cy1, int TamH, int TamV, Graphics g, Pen p)
        {
            Point[] C1 = new Point[4];
            C1[0] = new Point(Cx1, Cy1);
            C1[1] = new Point(TamH, Cy1);
            C1[2] = new Point(TamH, TamV);
            C1[3] = new Point(Cx1, TamV);
            g.DrawPolygon(p, C1);

        }
        public void Triangulo(int Cx1, int Cx2, int Cx3, int Cy1, int Cy2, int Cy3, Graphics g, Pen p)
        {
            PointF[] C1;
            C1 = new PointF[4];
            C1[0] = new PointF(Cx1, Cy1);
            C1[1] = new PointF(Cx2, Cy2);
            C1[2] = new PointF(Cx3, Cy3);
            C1[3] = new PointF(Cx1, Cy1);
            g.DrawLines(p, C1);
        }
        public void Triangulo3D(int Cx1, int Cx2, int Cx3, int Cy1, int Cy2, int Cy3, Graphics g, Pen p)
        {
            //if que voltean la piramide
            int c4x = Cx3 + 20, c4y = Cy3 - 30;
            if (Cy3 <= Cy2)
            {
                c4x = Cx3 + 20;
                c4y = Cy3 + 30;
            }
            if (Cx3 < Cx1)
            {
                c4x = Cx3 - 20;
                c4y = Cy3 - 30;
            }
            if (Cx3 < Cx1 & Cy3 <= Cy2)
            {
                c4x = Cx3 - 20;
                c4y = Cy3 + 30;
                PointF[] C2 = new PointF[3];
                C2[0] = new PointF(Cx3, Cy3);
                C2[1] = new PointF(c4x, c4y);
                C2[2] = new PointF(Cx2, Cy2);
                g.DrawLines(p, C2);
            }
            else
            {
                PointF[] C2 = new PointF[3];
                C2[0] = new PointF(Cx3, Cy3);
                C2[1] = new PointF(c4x, c4y);
                C2[2] = new PointF(Cx2, Cy2);
                g.DrawLines(p, C2);
            }


        }
        public void Prisma(int Cx1, int Cy1, int TamH, int TamV, Graphics g, Pen p)
        {
            int px1 = Cx1 + 20, py1 = Cy1 - 30, px2 = px1 + TamH, py2 = py1 + TamV, py3 = Cy1 + 30;
            int Cx2, Cy2;
            PointF[] C2 = new PointF[4];
            PointF[] C1 = new PointF[5];
            Cx2 = Cx1 + TamH;
            Cy2 = Cy1 + TamV;
            if (Cx2 < Cx1 & Cy2 >= Cy1)
            {
                px1 = Cx1 - 20;
                px2 = TamH + px1;
                Cx2 = TamH + Cx1;
                C2[0] = new PointF(Cx1, Cy1);
                C2[1] = new PointF(Cx2, Cy1);
                C2[2] = new PointF(Cx2, Cy2);
                C2[3] = new PointF(Cx1, Cy2);

                C1[0] = new PointF(Cx1, Cy1);
                C1[1] = new PointF(px1, py1);
                C1[2] = new PointF(px2, py1);
                C1[3] = new PointF(px2, py2);
                C1[4] = new PointF(Cx2, Cy2);
                g.DrawPolygon(p, C2);
                g.DrawLines(p, C1);
                g.DrawLine(p, Cx2, Cy1, px2, py1);
            }
            // para y
            if (Cy2 < Cy1 & Cx2 > Cx1)
            {
                Cy2 = TamV + Cy1;
                py2 = py1 + TamV;
                C2[0] = new PointF(Cx1, Cy1);
                C2[1] = new PointF(Cx2, Cy1);
                C2[2] = new PointF(Cx2, Cy2);
                C2[3] = new PointF(Cx1, Cy2);

                C1[0] = new PointF(Cx2, Cy1);
                C1[1] = new PointF(px2, Cy1 - 30);
                C1[2] = new PointF(px2, py2);
                C1[3] = new PointF(px1, py2);
                C1[4] = new PointF(Cx1, Cy2);
                g.DrawPolygon(p, C2);
                g.DrawLines(p, C1);
                g.DrawLine(p, Cx2, Cy2, px2, Cy2 - 30);
            }
            //inversa
            if (Cx2 < Cx1 & Cy2 < Cy1)
            {
                px1 = Cx1 - 20;
                px2 = TamH + px1;
                Cx2 = TamH + Cx1;
                Cy2 = TamV + Cy1;
                py2 = py1 + TamV;
                C2[0] = new PointF(Cx1, Cy1);
                C2[1] = new PointF(Cx2, Cy1);
                C2[2] = new PointF(Cx2, Cy2);
                C2[3] = new PointF(Cx1, Cy2);

                C1[0] = new PointF(Cx2, Cy1);
                C1[1] = new PointF(Cx2 - 20, py1);
                C1[2] = new PointF(Cx2 - 20, Cy2 - 30);
                C1[3] = new PointF(px1, Cy2 - 30);
                C1[4] = new PointF(Cx1, Cy2);
                g.DrawPolygon(p, C2);
                g.DrawLines(p, C1);
                g.DrawLine(p, Cx2, Cy2, Cx2 - 20, Cy2 - 30);
            }
            if (Cx2 >= Cx1 & Cy2 >= Cy1)
            {
                px1 = Cx1 + 20; py1 = Cy1 - 30; px2 = px1 + TamH; py2 = py1 + TamV; py3 = Cy1 + 30;
                C2[0] = new PointF(Cx1, Cy1);
                C2[1] = new PointF(Cx2, Cy1);
                C2[2] = new PointF(Cx2, Cy2);
                C2[3] = new PointF(Cx1, Cy2);

                C1[0] = new PointF(Cx1, Cy1);
                C1[1] = new PointF(px1, py1);
                C1[2] = new PointF(px2, py1);
                C1[3] = new PointF(px2, py2);
                C1[4] = new PointF(Cx2, Cy2);
                g.DrawPolygon(p, C2);
                g.DrawLines(p, C1);
                g.DrawLine(p, Cx2, Cy1, px2, py1);
            }
        }
            public void Circulo(int Cx1, int Cy1, int TamH, int TamV, Graphics g, Pen p)
        {
            Rectangle Rect = new Rectangle(Cx1, Cy1, TamH, TamV);
            g.DrawEllipse(p, Rect);

        }
        public void Esfera(int Cx1, int Cy1, int TamH, int TamV, Graphics g, Pen p)
        {
            Rectangle Rect = new Rectangle(Cx1, Cy1, TamH, TamV);
            Rectangle Rect2 = new Rectangle(Cx1, Cy1 + TamV * 5 / 11, TamH, TamV / 5);
            g.DrawEllipse(p, Rect);
            g.DrawEllipse(p, Rect2);

        }
        public void pentagono(int Cx1, int Cx2, int Cx3, int Cy1, int Cy2, int Cy3, Graphics g, Pen p)
        {
            int Cx4 = (Cx3 - Cx1) * 4 / 5;
            int Cx5 = (Cx3 - Cx1) * 1 / 5;
            int Cy4 = (Cy3 - Cy1) * 2 / 5;
            Point[] C1 = new Point[5];
            C1[0] = new Point(Cx1 + Cx2, Cy1);
            C1[1] = new Point(Cx3, Cy1 + Cy4);
            C1[2] = new Point(Cx1 + Cx4, Cy3);
            C1[3] = new Point(Cx1 + Cx5, Cy3);
            C1[4] = new Point(Cx1, Cy1 + Cy4);
            g.DrawPolygon(p, C1);

        }
        public void Cilindro(int Cx1, int Cx2, int Cx3, int Cy1, int Cy2, int Cy3, Graphics g, Pen p)
        {
            int My = Cy3 / 10;
            Rectangle Rect = new Rectangle(Cx1, Cy1, Cx2, Cy3 / 5);
            Rectangle Rect2 = new Rectangle(Cx1, Cy3, Cx2, Cy3 / 5);
            g.DrawLine(p, Cx1, Cy1 + My, Cx1, Cy3 + My);
            g.DrawLine(p, Cx3, Cy1 + My, Cx3, Cy3 + My);
            g.DrawEllipse(p, Rect);
            g.DrawEllipse(p, Rect2);

        }
        public Color C { get => c; set => c = value; }
        public string T { get => t; set => t = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }
}
