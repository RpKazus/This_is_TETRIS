using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public void Randomize()
        {
            
            Random rnd = new Random();
            System.Threading.Thread.Sleep(10);
            switch (rnd.Next(0,6))
            {
                case 0:
                    figure = new Square();
                    break;
                case 1:
                    figure = new Line();
                    break;
                case 2:
                    figure = new G_Right();
                    break;
                case 3:
                    figure = new G_Left();
                    break;
                case 4:
                    figure = new S_Right();
                    break;
                case 5:
                    figure = new S_Left();
                    break;
            }
            figure.location = new Point((rnd.Next(1,17) * r), figure.location.Y);
        }
        bool Apressed = false;
        bool Dpressed = false;
        int Score = 0;
        Figure figure = new Line();
        int defaultLoc = 0;
        public Form1()
        {
            InitializeComponent();
        }
        int r = 20;
        List<Point> Fallist = new List<Point>();
        List<OverPoint> ColorList = new List<OverPoint>();
        public void DrawPoint(Graphics gr)
        {
            SolidBrush sb = new SolidBrush(Color.Gold);
            foreach (Point paint in Fallist)
            {
                foreach(OverPoint op in ColorList)
                    if(paint.Equals(new Point(op.X, op.Y)))
                    {
                        sb.Color = op.Color;
                        gr.FillRectangle(sb, paint.X, paint.Y, r, r);
                    }
                gr.DrawRectangle(new Pen(Color.Black, 2), paint.X, paint.Y, r, r);
            }
        }
        public bool CanFall()
        {
            bool boolFall = figure.SNT.Y + figure.r + 1 < panel1.Height;
                foreach (Point falling in figure.FillPoints)
                {
                    foreach (Point falled in Fallist)
                    {
                        if (falled.Equals(new Point(falling.X, falling.Y + 20)))
                        {

                            return false;
                        }
                    }
                }
            return boolFall;
        }
        public bool CanRotate()
        {
            int figurot = figure.rotation;
            figure.Rotate();
            foreach(Point pp in figure.FillPoints)
                if(pp.X >= 0 && pp.Y < panel1.Height && pp.X <= panel1.Width)
                foreach(Point po in Fallist)
                {
                    if (po.Equals(pp))
                    {
                        figure.rotation = figurot;
                        return false;
                    }
                }
                else
                {
                    figure.rotation = figurot;
                    return false;
                }
            figure.rotation = figurot;
            return true;
        }
        public bool CanLeft()
        {
            bool boolMove = figure.SLT.X -1 > 0;
            foreach (Point falling in figure.FillPoints)
            {
                foreach (Point falled in Fallist)
                {
                    if (falled.Equals(new Point(falling.X - r, falling.Y)))
                        return false;
                }
            }
            return boolMove;
        }
        public bool CanRight()
        {
            bool boolMove = figure.SPT.X + r + 1< panel1.Width;
            foreach (Point falling in figure.FillPoints)
            {
                foreach (Point falled in Fallist)
                {
                    if (falled.Equals(new Point(falling.X + (r * 1), falling.Y)))
                        return false;
                }
            }
            return boolMove;
        }
        public void CheckLine()
        {
            for (int y = 0; y < panel1.Height; y += r)
            {
                List<Point> Liest = new List<Point>();
                foreach (Point pp in Fallist)
                    if (pp.Y == y) Liest.Add(pp);
                if (Liest.Count >= 18)
                {
                    foreach (Point pp in Liest)
                        Fallist.Remove(pp);
                    for (int i = 0; i < Fallist.Count; i++)
                        if (Fallist[i].Y < y) Fallist[i] = new Point(Fallist[i].X, Fallist[i].Y + r);
                    Score += 100;
                }
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            figure.DrowFigure(e.Graphics);
            DrawPoint(e.Graphics);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            ScoreList.Text = Score.ToString();
            if (CanFall())
                figure.step();
            else
            {
                if (figure.location.Y == 0)
                {
                    timer1.Enabled = false;
                    MessageBox.Show("Вы проиграли!!");
                    this.Close();
                }                
                Fallist.AddRange(figure.FillPoints);
                //List<OverPoint> TempList = new List<OverPoint>();
                ColorList.Add(new OverPoint(figure.FillPoints[0].X, figure.FillPoints[0].Y, figure.c));
                ColorList.Add(new OverPoint(figure.FillPoints[1].X, figure.FillPoints[1].Y, figure.c));
                ColorList.Add(new OverPoint(figure.FillPoints[2].X, figure.FillPoints[2].Y, figure.c));
                ColorList.Add(new OverPoint(figure.FillPoints[3].X, figure.FillPoints[3].Y, figure.c));
                //label2.Text = Convert.ToString(panel1.Height) + " " + Convert.ToString(figure.location.Y) + " " + Convert.ToString(Fallist[2].Y);
                figure = new Square();
                   Randomize();
                /*if(defaultLoc < 320) defaultLoc += 40;
                else defaultLoc = 0;
                figure.location = new Point(defaultLoc, 320);*/
                CheckLine();
            }
            panel1.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    Apressed = true;
                    System.Threading.Thread.Sleep(50);
                    break;
                case Keys.D:
                    Dpressed = true;
                    System.Threading.Thread.Sleep(50);
                    break;
                case Keys.S:
                    while(CanFall())
                        figure.step();
                    break;
                case Keys.Space:
                    if (CanFall() && CanRotate())
                        figure.Rotate();
                    panel1.Invalidate();
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Randomize();
        }

        private void Motion_Tick(object sender, EventArgs e)
        {
            if(Apressed & CanLeft()) figure.location = new Point(figure.location.X - r, figure.location.Y);
            if (Dpressed & CanRight()) figure.location = new Point(figure.location.X + r, figure.location.Y);
            panel1.Invalidate();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    Apressed = false;
                    break;
                case Keys.D:
                    Dpressed = false;
                    break;
            }
        }
    }
}
