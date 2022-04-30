﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Flowers_and_frogs
{
    public class Frog
    {
        public Color Color;
        public PictureBox PictureBox;
        public Point Location;
        
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
        }
    }
}