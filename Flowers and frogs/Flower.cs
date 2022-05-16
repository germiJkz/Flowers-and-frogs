using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Flowers_and_frogs
{
    public class Flower
    {
        public Color Color;//стоит ли сделать private  написать методы для изменения этих полей?
        public PictureBox PictureBox;
        public Point Location;

        public Flower(Color color, Point location, Image image)
        {
            Color = color;
            PictureBox = new PictureBox()
            {
                Image = image,
                Bounds = new Rectangle(location, new Size(64, 64)),
                BackColor = Color.Transparent
            };
            Location = location;
        }

        public void MoveTo(Point newPoint)
        {
            Location = newPoint;
            PictureBox.Bounds = new Rectangle(newPoint, new Size(64, 64));
        }
    }
}