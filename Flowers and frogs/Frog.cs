using System.Collections.Generic;
using System.Drawing;
namespace Flowers_and_frogs
{
    public class Frog
    {
        private Color Color;
        //private Image Image;
        private Point Location;
        
        public Frog(Color color, Point location)
        {
            Color = color;
            //Image = Image.FromFile(@"..\..\..\..\Pictures\Frog1.png");
            Location = location;
        }
    }
}