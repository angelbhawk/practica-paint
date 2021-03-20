using Paint.Componentes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint.Manejadores
{
    class manejadorGrafico
    {
        private Form frm;

        private List<Registro> rgs;
        private List<Carpeta> crp;

        private Graphics g;
        private SolidBrush s;
        private Pen p;
        private Color c, b = Color.Black;
        private Figuras3D Fig3D;

        private bool pintar = false, select = false;
        private string herramienta = "lapiz";
        private int control = 0, px = 0, py = 0, fx = 0, fy = 0, ch = 0, hs = 0,    
                                 px1 = 0, py1 = 0, px2 = 0, py2 = 0, px3 = 0, py3 = 0;

        public manejadorGrafico() 
        {
            frm = Application.OpenForms.OfType<Form>().FirstOrDefault();

            CargarComponentes();
            AgregarEventos();

            frm.lblTamaño.Text = "Tamaño de la ventana: " + frm.Width + " x " + frm.Height;
            frm.lblLienzo.Text = "| Tamaño del lienzo: " + crp[hs].Hoj.Width + " x " + crp[hs].Hoj.Height;
        }

        // Métodos auxiliares

        #region Métodos auxiliares

        private void AjustarBotones()
        {
            crp[hs].BtnCI.Location = new Point(crp[hs].Hoj.Width / 2 + 10, crp[hs].Hoj.Height + 10);
            crp[hs].BtnDC.Location = new Point(crp[hs].Hoj.Width + 10, crp[hs].Hoj.Height / 2 + 10);
            crp[hs].BtnDI.Location = new Point(crp[hs].Hoj.Width + 10, crp[hs].Hoj.Height + 10);
        }
        private void AgregarEventos() 
        {
            crp[ch].BtnCI.Move += new EventHandler(ActivarBCI);
            crp[ch].BtnDC.Move += new EventHandler(ActivarBDC);
            crp[ch].BtnDI.Move += new EventHandler(ActivarBDI);

            crp[ch].BtnCI.LocationChanged += new EventHandler(btnCI_Move);
            crp[ch].BtnDC.LocationChanged += new EventHandler(btnDC_Move);
            crp[ch].BtnDI.LocationChanged += new EventHandler(btnDI_Move);

            crp[ch].Hoj.MouseUp += new MouseEventHandler(hoja_MouseUp);
            crp[ch].Hoj.MouseDown += new MouseEventHandler(hoja_MouseDown);
            crp[ch].Hoj.MouseMove += new MouseEventHandler(hoja_MouseMove);

            frm.pbxColorSRojo.Click += new EventHandler(tomarColor);
            frm.pbxColorIRojo.Click += new EventHandler(tomarColor);
            frm.pbxColorSMorado.Click += new EventHandler(tomarColor);
            frm.pbxColorIMorado.Click += new EventHandler(tomarColor);
            frm.pbxColorSAmarillo.Click += new EventHandler(tomarColor);
            frm.pbxColorIAmarillo.Click += new EventHandler(tomarColor);
            //frm.pbxColorSNaranja.Click += new EventHandler(tomarColor);
            //frm.pbxColorINaranja.Click += new EventHandler(tomarColor);
            frm.pbxColorSAzul.Click += new EventHandler(tomarColor);
            frm.pbxColorIAzul.Click += new EventHandler(tomarColor);
            frm.pbxColorSVerde.Click += new EventHandler(tomarColor);
            frm.pbxColorIVerde.Click += new EventHandler(tomarColor);
            frm.pbxAlmacen.Click += new EventHandler(tomarColor);

            frm.pbxBorrador.Click += new EventHandler(elegirHerramienta);
            frm.pbxLapiz.Click += new EventHandler(elegirHerramienta);
            frm.pbxCuentagotas.Click += new EventHandler(elegirHerramienta);
            frm.pbxSelector.Click += new EventHandler(elegirHerramienta);
            frm.pbxCubeta.Click += new EventHandler(elegirHerramienta);

            frm.pbxCerrar.Click += new EventHandler(pbxCerrar_Click);
            frm.pbxMinimizar.Click += new EventHandler(pbxMinimizar_Click);
            frm.pbxMaximizar.Click += new EventHandler(pbxMaximizar_Click);

            frm.panelSuperior.MouseMove += new MouseEventHandler(Form_MouseMove);

            frm.pbxPegar.Click += new EventHandler(portapapeles);
            frm.pbxCopiar.Click += new EventHandler(elegirHerramienta);
            frm.pbxCortar.Click += new EventHandler(elegirHerramienta);

            frm.msNuevo.Click += new EventHandler(Añadir);
            frm.tabContenido.Selected += new TabControlEventHandler(SelecionarHoja);
            frm.borrarLienzo.Click += new EventHandler(BorrarHoja);

            frm.msGuardar.Click += new EventHandler(Guardar);
            frm.pbxColor.Click += new EventHandler(SelecionarColor);
            

            frm.pbxCuadro.Click += new EventHandler(elegirFigura);
            frm.pbxPrisma.Click += new EventHandler(elegirFigura);
            frm.pbxTriangulo.Click += new EventHandler(elegirFigura);
            frm.pbxPiramide.Click += new EventHandler(elegirFigura);
            frm.pbxCirculo.Click += new EventHandler(elegirFigura);
            frm.pbxEsfera.Click += new EventHandler(elegirFigura);
            frm.pbxCilindro.Click += new EventHandler(elegirFigura);
            frm.pbxPentagono.Click += new EventHandler(elegirFigura);

            frm.Resize += new EventHandler(Redimensionar);
        }
        private void CargarComponentes()
        {
            c = Color.Black;
            rgs = new List<Registro>();
            crp = new List<Carpeta>();
            CrearHoja();
        }
        private void CrearHoja() 
        {
            // Crea la hoja
            crp.Add(new Carpeta(new List<Registro>(), new Hoja()));

            // Crear pestaña
            TabPage taP = new TabPage();
            frm.tabContenido.TabPages.Add(taP.ImageKey = "lienzo" + ch.ToString(), taP.Name = "Lienzo " + (ch + 1).ToString(), ch);

            hs = ch; frm.tabContenido.SelectedIndex = ch;
            frm.tabContenido.TabPages[ch].AutoScroll = true;


            frm.tabContenido.TabPages[ch].BackColor = Color.FromArgb(52, 73, 94);
            frm.tabContenido.TabPages[ch].Click += new EventHandler(SelecionarHoja);

            crp[hs].Reg.Add(new Registro(0, 0, 400, 250, Color.White, "fondo"));

            crp[ch].BtnCI.Move += new EventHandler(ActivarBCI);
            crp[ch].BtnDC.Move += new EventHandler(ActivarBDC);
            crp[ch].BtnDI.Move += new EventHandler(ActivarBDI);

            crp[ch].BtnCI.LocationChanged += new EventHandler(btnCI_Move);
            crp[ch].BtnDC.LocationChanged += new EventHandler(btnDC_Move);
            crp[ch].BtnDI.LocationChanged += new EventHandler(btnDI_Move);

            crp[ch].Hoj.MouseUp += new MouseEventHandler(hoja_MouseUp);
            crp[ch].Hoj.MouseDown += new MouseEventHandler(hoja_MouseDown);
            crp[ch].Hoj.MouseMove += new MouseEventHandler(hoja_MouseMove);

            
            frm.tabContenido.GetControl(ch).Controls.Add(crp[ch].BtnCI);

            
            frm.tabContenido.GetControl(ch).Controls.Add(crp[ch].BtnDC);

            
            frm.tabContenido.GetControl(ch).Controls.Add(crp[ch].BtnDI);

            AjustarBotones();

            // Agrega una nueva hoja al tabcontrol
            frm.tabContenido.GetControl(ch).Controls.Add(crp[ch].Hoj);

            pintarFiguras();
            crp[ch].Hoj.Refresh();
        }

        #endregion

        // Eventos de la interfaz

        #region Eventos

        private void ActivarBCI(object sender, EventArgs e)
        {
            control = 1;
        }
        private void ActivarBDC(object sender, EventArgs e)
        {
            control = 2;
        }
        private void ActivarBDI(object sender, EventArgs e)
        {
            control = 3;
        }
        private void btnCI_Move(object sender, EventArgs e)
        {
            if (control == 1)
            {
                crp[hs].Hoj.Height = crp[hs].BtnCI.Top - 10;
                crp[hs].Reg[0].H = crp[hs].Hoj.Height;
                AjustarBotones();
                frm.lblLienzo.Text = "| Tamaño del lienzo: " + crp[hs].Hoj.Width + " x " + crp[hs].Hoj.Height;
                frm.tabContenido.Refresh();
            }         
        }
        private void btnDC_Move(object sender, EventArgs e)
        {
            if(control == 2)
            {
                crp[hs].Hoj.Width = crp[hs].BtnDC.Left - 10;
                crp[hs].Reg[0].V = crp[hs].Hoj.Width;
                AjustarBotones();
                frm.lblLienzo.Text = "| Tamaño del lienzo: " + crp[hs].Hoj.Width + " x " + crp[hs].Hoj.Height;
                frm.tabContenido.Refresh();
            }
        }
        private void btnDI_Move(object sender, EventArgs e)
        {
            if(control == 3)
            {
                crp[hs].Hoj.Width = crp[hs].BtnDI.Left - 10;
                crp[hs].Hoj.Height = crp[hs].BtnDI.Top - 10;
                crp[hs].Reg[0].H = crp[hs].Hoj.Width;
                crp[hs].Reg[0].V = crp[hs].Hoj.Height;
                AjustarBotones();
                frm.lblLienzo.Text = "| Tamaño del lienzo: " + crp[hs].Hoj.Width + " x " + crp[hs].Hoj.Height;
                frm.tabContenido.Refresh();
            }
        }
        private void pbxCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { fx = e.X; fy = e.Y; }
            else { frm.Left = frm.Left + (e.X - fx); frm.Top = frm.Top + (e.Y - fy); }
        }
        private void pbxMinimizar_Click(object sender, EventArgs e)
        {
            frm.WindowState = FormWindowState.Minimized;
        }
        private void pbxMaximizar_Click(object sender, EventArgs e)
        {
            if (frm.WindowState.ToString() == "Normal")
            {
                frm.WindowState = FormWindowState.Maximized;
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
            }
        }
        private void portapapeles(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                crp[hs].Hoj.Image = Clipboard.GetImage();
            }
        }
        private void Añadir(object sender, EventArgs e)
        {
            ch++;
            CrearHoja();
        }
        private void SelecionarHoja(object sender, EventArgs e)
        {
            if(frm.tabContenido.TabPages.Count != 0)
            {
                string nmt = frm.tabContenido.SelectedIndex.ToString();
                hs = Convert.ToInt32(nmt);//.Substring(nmt.Length - 1));
                frm.lblLienzo.Text = "| Tamaño del lienzo: " + crp[hs].Hoj.Width + " x " + crp[hs].Hoj.Height;
            }      
        }
        private void BorrarHoja(object sender, EventArgs e) 
        {
            crp.RemoveAt(hs);
            frm.tabContenido.TabPages.Remove(frm.tabContenido.SelectedTab);
            ch--;
        }
        private void Guardar(object sender, EventArgs e)
        {
            SaveFileDialog guardarImagen = new SaveFileDialog();
            guardarImagen.DefaultExt = "jpg";
            guardarImagen.FileName = "Lienzo" + (ch + 1).ToString();
            guardarImagen.Filter = "jpg files (*.jpg)|*.jpg";
            if (guardarImagen.ShowDialog() == DialogResult.OK)
            {
                crp[hs].Hoj.Image.Save(guardarImagen.FileName);
            }
        }
        private void SelecionarColor(object sender, EventArgs e) 
        {
            ColorDialog color = new ColorDialog(); 
            if (color.ShowDialog() == DialogResult.OK)
            {
                frm.pbxAlmacen.BackColor = color.Color;
            }
        }
        private void Redimensionar(object sender, EventArgs e) 
        { 
            frm.lblTamaño.Text = "Tamaño de la ventana: " + frm.Width + " x " + frm.Height;
            frm.lblLienzo.Text = "| Tamaño del lienzo: " + crp[hs].Hoj.Width + " x " + crp[hs].Hoj.Height;
        }

        #endregion

        // Eventos de dibujo

        private void hoja_MouseUp(object sender, MouseEventArgs e)
        {
            pintar = false;

            if (herramienta == "cuadrado") // Dibuja un cuadrado
            {
                crp[hs].Reg.Add(new Registro(px, py, (e.Y - py), (e.X - px), c, "cuadrado"));
            }
            if(herramienta == "selector")
            {
                px1 = px; px1 = py;
                px2 = e.X; py2 = e.Y;
                select = true;
            }
            if (herramienta == "triangulo") // Dibuja un triangulo
            {
                //puntos
                py3 = e.Y;
                px3 = e.X;
                px2 = (px3 + px) / 2;

                crp[hs].Reg.Add(new Registro(px, py3, px2, py, px3, py3, c, "triangulo"));
            }
            if (herramienta == "piramide") // Dibuja una piramide
            {
                //puntos
                py3 = e.Y;
                px3 = e.X;
                px2 = (px3 + px) / 2;

                crp[hs].Reg.Add(new Registro(px, py3, px2, py, px3, py3, c, "triangulo3D"));
            }
            if (herramienta == "circulo") // Dibuja un circulo
            {
                //puntos
                py3 = e.Y;
                py2 = (py + py3) / 2;
                px3 = e.X;
                px2 = (px3 + px) / 2;

                crp[hs].Reg.Add(new Registro(px, py, e.X - px, e.Y - py, c, "circulo"));
            }
            if (herramienta == "esfera") // Dibuja una esfera
            {
                //puntos
                py3 = e.Y;
                py2 = (py + py3) / 2;
                px3 = e.X;
                px2 = (px3 + px) / 2;

                crp[hs].Reg.Add(new Registro(px, py, e.X - px, e.Y - py, c, "esfera"));
            }
            if (herramienta == "prisma") // Dibuja un prisma
            {
                //puntos
                py3 = e.Y;
                py2 = (py + py3) / 2;
                px3 = e.X;
                px2 = (px3 + px) / 2;

                crp[hs].Reg.Add(new Registro(px, py, e.X - px, e.Y - py, c, "prisma"));
            }
            if (herramienta == "cilindro") // Dibuja un cilindro
            {
                //puntos
                py3 = e.Y;
                py2 = e.Y - py;
                px3 = e.X;
                px2 = e.X - px;

                crp[hs].Reg.Add(new Registro(px, py, px2, py2, px3, py3, c, "cilindro"));
            }
            if (herramienta == "pentagono") // Dibuja un pentagono
            {
                //puntos
                py3 = e.Y;
                py2 = e.Y - py;
                px3 = e.X;
                px2 = (e.X - px) / 2;

                crp[hs].Reg.Add(new Registro(px, py, px2, py2, px3, py3, c, "pentagono"));
            }

            pintarFiguras();
            crp[hs].Hoj.Refresh();
        }
        private void hoja_MouseDown(object sender, MouseEventArgs e)
        {
            pintar = true;
            select = false;
            px = e.X; py = e.Y;
            px1 = e.X; py1 = e.Y;

            if (herramienta == "cuentagotas")
            {
                //MessageBox.Show("a");
                c = crp[hs].Hoj.MBits.GetPixel(px, py);
                frm.pbxColorSelecionado.BackColor = c;
                pintar = false;
            }
            if (herramienta == "cubeta")
            {
                b = crp[hs].Hoj.MBits.GetPixel(px, py);
                FloodFill(crp[hs].Hoj.MBits, new Point(px, py), b, c);
            }
        }
        private void hoja_MouseMove(object sender, MouseEventArgs e)
        {
            if (pintar)
            {
                s = new SolidBrush(c);
                p = new Pen(c);

                g = crp[hs].Hoj.CargarGraficos();

                if (herramienta == "lapiz") // Dibuja con el lapiz
                {
                    crp[hs].Reg.Add(new Registro(e.X, e.Y, 10, 10, c, "punto"));
                }
                if (herramienta == "borrador") // Dibuja con el lapiz
                {
                    crp[hs].Reg.Add(new Registro(e.X, e.Y, 10, 10, Color.White, "punto"));
                }

                if (herramienta == "cuadrado") // Dibuja un cuadrado
                {
                    rgs.Add(new Registro(px, py, e.X, e.Y, c, "cuadrado"));
                    g.DrawRectangle(p, new Rectangle(px, py, e.X - px, e.Y - py));
                    //Fig3D = new Figuras3D();
                    //Fig3D.Cuadro(px, py, e.X, e.Y, g, p);
                }
                if (herramienta == "selector") // Dibuja un cuadrado
                {
                    p = new Pen(Color.DodgerBlue);
                    g.DrawRectangle(p, new Rectangle(px, py, e.X - px, e.Y - py));
                }
                if (herramienta == "triangulo") // Dibuja un triangulo
                {
                    Fig3D = new Figuras3D();
                    Fig3D.Triangulo(px, (px + e.X) / 2, e.X, e.Y, py, e.Y, g, p);
                }
                if (herramienta == "piramide") // Dibuja un triangulo 3D
                {
                    Fig3D = new Figuras3D();
                    Fig3D.Triangulo(px, (px + e.X) / 2, e.X, e.Y, py, e.Y, g, p);
                    Fig3D.Triangulo3D(px, (px + e.X) / 2, e.X, e.Y, py, e.Y, g, p);
                }
                if (herramienta == "circulo") // Dibuja un circulo
                {
                    Fig3D = new Figuras3D();
                    Fig3D.Circulo(px, py, e.X - px, e.Y - py, g, p);
                }
                if (herramienta == "esfera") // Dibuja una esfera
                {
                    Fig3D = new Figuras3D();
                    Fig3D.Esfera(px, py, e.X - px, e.Y - py, g, p);
                }
                if (herramienta == "prisma") // Dibuja un prisma
                {
                    Fig3D = new Figuras3D();
                    Fig3D.Prisma(px, py, e.X - px, e.Y - py, g, p);
                }
                if (herramienta == "pentagono") // Dibuja un pentagono
                {
                    Fig3D = new Figuras3D();
                    Fig3D.pentagono(px, (e.X - px) / 2, e.X, py, e.Y - py, e.Y, g, p);
                }
                if (herramienta == "cilindro") // Dibuja un cilindro
                {
                    Fig3D = new Figuras3D();
                    Fig3D.Cilindro(px, e.X - px, e.X, py, e.Y - py, e.Y, g, p);
                }

                pintarFiguras();
                crp[hs].Hoj.Refresh();
            }
        }

        private void pintarFiguras() 
        {
            try 
            {
                foreach (Registro r in crp[hs].Reg) // Comprobación y dibujo de figuras existentes
                {
                    s = new SolidBrush(r.C);
                    p = new Pen(r.C);

                    //if (r.T == "punto")
                    //{
                    //    g.FillEllipse(s, r.X, r.Y, r.V, r.H);
                
                if (r.T == "cuadrado")
                {
                    g.DrawRectangle(p, new Rectangle(r.X, r.Y, r.V, r.H));
                }
                //if (r.T == "fondo")
                //{
                //    g.FillRectangle(s, new Rectangle(r.X, r.Y, r.V, r.H));
                //}

                // Alan figuras

                if (r.T == "punto")
                        g.FillEllipse(s, r.X, r.Y, r.V, r.H);

                    //if (r.T == "cuadrado")
                    //{
                    //    Fig3D = new Figuras3D();
                    //    Fig3D.Cuadro(r.X, r.Y, r.H, r.V, g, p);
                    //}
                    if (r.T == "triangulo")
                    {
                        Fig3D = new Figuras3D();
                        Fig3D.Triangulo(r.X, r.X2, r.X3, r.Y, r.Y2, r.Y3, g, p);
                    }
                    if (r.T == "triangulo3D")
                    {
                        Fig3D = new Figuras3D();
                        Fig3D.Triangulo(r.X, r.X2, r.X3, r.Y, r.Y2, r.Y3, g, p);
                        Fig3D.Triangulo3D(r.X, r.X2, r.X3, r.Y, r.Y2, r.Y3, g, p);
                    }
                    if (r.T == "circulo")
                    {
                        Fig3D = new Figuras3D();
                        Fig3D.Circulo(r.X, r.Y, r.H, r.V, g, p);
                    }
                    if (r.T == "esfera")
                    {
                        Fig3D = new Figuras3D();
                        Fig3D.Esfera(r.X, r.Y, r.H, r.V, g, p);
                    }
                    if (r.T == "prisma")
                    {
                        Fig3D = new Figuras3D();
                        Fig3D.Prisma(r.X, r.Y, r.H, r.V, g, p);
                    }
                    if (r.T == "pentagono")
                    {
                        Fig3D = new Figuras3D();
                        Fig3D.pentagono(r.X, r.X2, r.X3, r.Y, r.Y2, r.Y3, g, p);
                    }
                    if (r.T == "cilindro")
                    {
                        Fig3D = new Figuras3D();
                        Fig3D.Cilindro(r.X, r.X2, r.X3, r.Y, r.Y2, r.Y3, g, p);
                    }
                    if (r.T == "punto")
                    {
                        crp[hs].Hoj.MBits.SetPixel(r.X, r.Y, r.C);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Se acabo la tinta");
            }
            
        }
        private void tomarColor(object sender, EventArgs e)
        {
            var pbx = sender as PictureBox;

            b = c;
            c = pbx.BackColor;
            frm.pbxColorSelecionado.BackColor = c;
        }
        private void elegirHerramienta(object sender, EventArgs e)
        {
            var pbx = sender as PictureBox;

            if (pbx.Name == "pbxBorrador")
            {
                herramienta = "borrador";
            }

            if (pbx.Name == "pbxLapiz")
            {
                herramienta = "lapiz";
            }

            if (pbx.Name == "pbxCuentagotas")
            {
                herramienta = "cuentagotas";
            }

            if (pbx.Name == "pbxSelector")
            {
                herramienta = "selector";
            }

            if (pbx.Name == "pbxCopiar")
            {
                copiarCortar();
            }

            if (pbx.Name == "pbxCortar")
            {
                copiarCortar();
            }

            if (pbx.Name == "pbxCubeta")
            {
                herramienta = "cubeta";
            }
        }
        private void elegirFigura(object sender, EventArgs e)
        {
            var pbx = sender as PictureBox;

            if (pbx.Name == "pbxCuadro")
            {
                herramienta = "cuadrado";
            }
            if (pbx.Name == "pbxTriangulo")
            {
                herramienta = "triangulo";
            }
            if (pbx.Name == "pbxPiramide")
            {
                herramienta = "piramide";
            }
            if (pbx.Name == "pbxPrisma")
            {
                herramienta = "prisma";
            }
            if (pbx.Name == "pbxCirculo")
            {
                herramienta = "circulo";
            }
            if (pbx.Name == "pbxEsfera")
            {
                herramienta = "esfera";
            }
            if (pbx.Name == "pbxPentagono")
            {
                herramienta = "pentagono";
            }
            if (pbx.Name == "pbxCilindro")
            {
                herramienta = "cilindro";
            }
        }
        private void copiarCortar() 
        {

            // Make a bitmap for the selected area's image.
            Rectangle src_rect = new Rectangle(px1, py1, py2 - py1, px2 - px1);
            Bitmap bm = crp[hs].Hoj.MBits;

            // Copy the selected area into the bitmap.
            using (Graphics gr = Graphics.FromImage(bm))
            {
                Rectangle dest_rect = new Rectangle(px1, py1, py2 - py1, px2 - px1);
                gr.DrawImage(bm, dest_rect, src_rect, GraphicsUnit.Pixel);
            }

            // Copy the selection image to the clipboard.
            Clipboard.SetImage(bm);
        }
        private void FloodFill(Bitmap bmp, Point pt, Color targetColor, Color replacementColor)
        {
            targetColor = bmp.GetPixel(pt.X, pt.Y);
            if (targetColor.ToArgb().Equals(replacementColor.ToArgb()))
            {
                return;
            }

            Stack<Point> pixels = new Stack<Point>();

            pixels.Push(pt);
            while (pixels.Count != 0)
            {
                Point temp = pixels.Pop();
                int y1 = temp.Y;
                while (y1 >= 0 && bmp.GetPixel(temp.X, y1) == targetColor)
                {
                    y1--;
                }
                y1++;
                bool spanLeft = false;
                bool spanRight = false;
                while (y1 < bmp.Height && bmp.GetPixel(temp.X, y1) == targetColor)
                {
                    bmp.SetPixel(temp.X, y1, replacementColor);
                    crp[hs].Reg.Add(new Registro(temp.X, y1, replacementColor, "punto"));

                    if (!spanLeft && temp.X > 0 && bmp.GetPixel(temp.X - 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(temp.X - 1, y1));
                        spanLeft = true;
                    }
                    else if (spanLeft && temp.X - 1 == 0 && bmp.GetPixel(temp.X - 1, y1) != targetColor)
                    {
                        spanLeft = false;
                    }
                    if (!spanRight && temp.X < bmp.Width - 1 && bmp.GetPixel(temp.X + 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(temp.X + 1, y1));
                        spanRight = true;
                    }
                    else if (spanRight && temp.X < bmp.Width - 1 && bmp.GetPixel(temp.X + 1, y1) != targetColor)
                    {
                        spanRight = false;
                    }
                    y1++;
                }

            }
            crp[hs].Hoj.Refresh();

        }
    }
}