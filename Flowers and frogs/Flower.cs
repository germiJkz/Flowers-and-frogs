using System.Collections.Generic;
using System.Drawing;

namespace Flowers_and_frogs
{
    public class Flower
    {
        private Color Color;
        private Image Image;
        private Point Location;

        public Flower(Color color, Point location)
        {
            Color = color;
            Image = Image.FromFile(@"..\..\..\..\Pictures\flower1.png");
            Location = location;
        }
    }
}