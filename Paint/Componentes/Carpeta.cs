using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Componentes
{
    class Carpeta
    {
        private List<Registro> reg;
        private Hoja hoj;
        private Marco btnCI, btnDC, btnDI;

        public Carpeta(List<Registro> r, Hoja h)
        {
            Reg = r;
            Hoj = h;

            btnCI = new Marco('v');
            btnDC = new Marco('h');
            btnDI = new Marco('a');
        }

        internal Hoja Hoj { get => hoj; set => hoj = value; }
        internal List<Registro> Reg { get => reg; set => reg = value; }
        internal Marco BtnCI { get => btnCI; set => btnCI = value; }
        internal Marco BtnDC { get => btnDC; set => btnDC = value; }
        internal Marco BtnDI { get => btnDI; set => btnDI = value; }
    }
}
