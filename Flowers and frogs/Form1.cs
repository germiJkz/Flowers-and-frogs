using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowers_and_frogs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            DoubleBuffered = true;
            InitializeComponent();
            Height = 600;
            Width = 1000;
            BackgroundImage= Image.FromFile(@"..\..\..\..\Pictures\back.jpg");
            MessageBox.Show("К тебе будут приходить злые жабы. " +
                            "Ты можешь дать им цветочек такого же цвета, как и жаба, " +
                            "тогда они обрадуются и уйдут", "Про игру", MessageBoxButtons.OK);
            
            
            var buttonCollectBouquet = new Button()
            {
                Location = new Point(700,450),
                Size = new Size(80,80),
                Text = "собрать\nбукет",
                BackColor = Color.Moccasin
                
            };
            
            var collectedFlowers = new List<PictureBox>();
            var flower2 = new PictureBox()
            {
                Image = Image.FromFile(
                    @"..\..\..\..\Pictures\flower1.png"),
                Bounds = new Rectangle(new Point(900,200), new Size(64, 64))
            };
            
            Controls.Add(buttonCollectBouquet);
            flower2.Click += ((sender, args) =>
            {
                if (collectedFlowers.Count != 3)
                {
                    Controls.Remove(flower2);
                    
                    collectedFlowers.Add(new PictureBox()
                    {
                        Image = flower2.Image,
                        Bounds = new Rectangle(new Point(330,470), new Size(64,64))// нее всешда такая позиция
                    });
                    Controls.Add(collectedFlowers[0]);//не всегда 0
                }
            });
            Controls.Add(flower2);

        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.FillRectangle(Brushes.Coral, new Rectangle(470,470,64,64));
            graphics.FillRectangle(Brushes.Coral, new Rectangle(400,470,64,64));
            graphics.FillRectangle(Brushes.Coral, new Rectangle(330,470,64,64));
            graphics.FillRectangle(Brushes.Moccasin, new Rectangle(900,470,64,64));
            //graphics.DrawImage(Image.FromFile(
                //@"C:\Users\Ольга\Documents\моя 2.0\прога 2\ИГРА\Flowers and frogs\flower1.png"), new Point(300, 250));
            
        }
    }
}