using System.Drawing;
using System.Windows.Forms;

namespace Flowers_and_frogs
{
    public class Bouquet
    {
        public Color[] Colors;
        public PictureBox PictureBox;

        public Bouquet(Color[] colors, Image image)
        {
            Colors = colors;
            PictureBox = new PictureBox()
            {
                Image = image,
                Bounds = new Rectangle(new Point(900, 470), new Size(64,64)),
                BackColor = Color.Transparent,
                Visible = false
            };
        }

        public void Appear()
        {
            PictureBox.Visible = true;
        }

        public void Disappear()
        {
            PictureBox.Visible = false;
        }

        public void SetColors(Color[] colors)
        {
            Colors = colors;
        }

        public void SetImage(Image image)
        {
            PictureBox.Image = image;
        }
    }
}