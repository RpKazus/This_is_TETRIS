using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class S_Right : Figure
    {
        public override Color c
        {
            get
            {
                return Color.Blue;
            }
        }
        public override List<Point> FillPoints
        {
            get
            {
                switch (rotation)
                {
                    case 0:
                        List<Point> result = new List<Point>();
                        result.Add(location);
                        result.Add(new Point(location.X, location.Y + r));
                        result.Add(new Point(location.X + r, location.Y + r));
                        result.Add(new Point(location.X + r, location.Y + r + r));
                        return result;
                        break;
                    case 2:
                        List<Point> result3 = new List<Point>();
                        result3.Add(location);
                        result3.Add(new Point(location.X, location.Y + r));
                        result3.Add(new Point(location.X + r, location.Y + r));
                        result3.Add(new Point(location.X + r, location.Y + r + r));
                        return result3;
                        break;
                    case 1:
                        List<Point> result1 = new List<Point>();
                        result1.Add(location);
                        result1.Add(new Point(location.X + r, location.Y));
                        result1.Add(new Point(location.X, location.Y + r));
                        result1.Add(new Point(location.X - r, location.Y + r));
                        return result1;
                        break;
                    case 3:
                        List<Point> result2 = new List<Point>();
                        result2.Add(location);
                        result2.Add(new Point(location.X, location.Y + r));
                        result2.Add(new Point(location.X + r, location.Y + r));
                        result2.Add(new Point(location.X + r, location.Y + r + r));
                        return result2;
                        break;
                    default:
                        List<Point> result0 = new List<Point>();
                        result0.Add(location);
                        return result0;
                        break;
                }
            }
        }
    }
}
