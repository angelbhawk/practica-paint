using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Componentes
{
    class Registro
    {
        private int x, y, h, v, x2, y2, x3, y3; // caracteristicas
        private Color c; // color
        private Image i; // imagen
        private string t; // tipo de figura

        // Figuras simples
        public Registro(int corX, int corY, int tamH, int tamV, Color col, string tipo) 
        {
            X = corX;
            Y = corY;
            H = tamH;
            V = tamV;
            C = col;
            T = tipo;
        }

        // Figuras complejas
        public Registro(int corX, int corY, int corX2, int corY2, int corX3, int corY3, Color col, string tipo)
        {
            X = corX;
            Y = corY;
            C = col;
            T = tipo;
            X2 = corX2;
            Y2 = corY2;
            X3 = corX3;
            Y3 = corY3;
        }

        // Imagenes
        public Registro(int corX, int corY, int tamH, int tamV, Image img, string tipo)
        {
            X = corX;
            Y = corY;
            H = tamH;
            V = tamV;
            I = img;
            T = tipo;
        }

        // Punto
        public Registro(int corX, int corY, Color col, string tipo)
        {
            X = corX;
            Y = corY;
            C = col;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int H { get => h; set => h = value; }
        public int V { get => v; set => v = value; }
        public Color C { get => c; set => c = value; }
        public string T { get => t; set => t = value; }
        public Image I { get => i; set => i = value; }
        public int X2 { get => x2; set => x2 = value; }
        public int Y2 { get => y2; set => y2 = value; }
        public int X3 { get => x3; set => x3 = value; }
        public int Y3 { get => y3; set => y3 = value; }
    }
}