using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Flowers_and_frogs
{
    public class Frog
    {
        public Color Color;
        public PictureBox PictureBox;
        public Point Location;
        public int Time;
        public ProgressBar Bar;
        
        public Frog(Color color, Point location, Image image)
        {
            Color = color;
            PictureBox = new PictureBox()
            {
                Image = image,
                Bounds = new Rectangle(location, new Size(96, 96)),
                BackColor = Color.Transparent
            };
            Location = location;
            Time = 1500;
            Bar = new ProgressBar()
            {
                Bounds = new Rectangle(new Point(location.X, location.Y - 20), new Size(64,10)),
                Maximum = 1500,
                Minimum = 0,
                Value = 1500,
                Visible = true
            };
        }

        public void MoveTo(Point newPoint)
        {
            Location = newPoint;
            PictureBox.Bounds = new Rectangle(newPoint, new Size(96, 96));
            Bar.Bounds = new Rectangle(new Point(newPoint.X, newPoint.Y - 20), new Size(64, 10));
        }

        public void DecreaseTime()
        {
            Time--;
            Bar.Value = Time;
        }

        public void ResetTime()
        {
            Time = 1500;
            Bar.Value = Time;
        }
    }
}