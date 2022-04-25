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
            BackgroundImage= Image.FromFile(@"..\..\..\Pictures\back.jpg");
            MessageBox.Show("К тебе будут приходить злые жабы. " +
                            "Ты можешь дать им цветочек такого же цвета, как и жаба, " +
                            "тогда они обрадуются и уйдут", "Про игру", MessageBoxButtons.OK);
            
            var model = new Model();
            CreateFlowers(model);
            SpawnFlowers(model);
            
            var buttonCollectBouquet = new Button()
            {
                Location = new Point(700,450),
                Size = new Size(80,80),
                Text = "собрать\nбукет",
                BackColor = Color.Moccasin
                
            };
            Controls.Add(buttonCollectBouquet);

            var buttonThrowCollectedFlowers = new Button()
            {
                Location = new Point(200, 450),
                Size = new Size(80, 80),
                Text = "бросить\nцветы",
                BackColor = Color.Moccasin
            };
            buttonThrowCollectedFlowers.Click += new EventHandler(model.ThrowCollectedFlowers);
            Controls.Add(buttonThrowCollectedFlowers);

            var frog1 = new PictureBox()
            {
                Image = Image.FromFile(@"..\..\..\Pictures\Frog1.png"),
                Bounds = new Rectangle(new Point(850,200), new Size(96,96))
            };
            Controls.Add(frog1);
        }

        private static void SpawnFlowers(Model model)
        {
            var timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += ((sender, args) => { model.TrySpawnFlower(model); });
            timer.Start();
        }

        private void CreateFlowers(Model model)
        {
            for (int i = 0; i < 9; i++) //create flowers
            {
                var color = Color.Empty;
                Image image = null;

                if (i / 3 == 0)
                {
                    color = Color.Pink;
                    image = Image.FromFile(@"..\..\..\Pictures\PinkFlower.png");
                }

                if (i / 3 == 1)
                {
                    color = Color.Blue;
                    image = Image.FromFile(@"..\..\..\Pictures\BlueFlower.png");
                }

                if (i / 3 == 2)
                {
                    color = Color.Orange;
                    image = Image.FromFile(@"..\..\..\Pictures\OrangeFlower.png");
                }

                var flower = new Flower(color, new Point(-100, -100), image);
                model.FlowersArray[i] = flower;

                flower.PictureBox.Click += ((sender, args) =>
                {
                    if (model.CollectedFlowers.Count < 3 && !model.CollectedFlowers.Contains(flower))
                    {
                        flower.Location = model.CollectedFlowersPositions[model.CollectedFlowers.Count];
                        flower.PictureBox.Bounds = new Rectangle(flower.Location, new Size(64, 64));
                        model.CollectedFlowers.Add(flower);
                    }
                });
                Controls.Add(flower.PictureBox);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.FillRectangle(Brushes.Coral, new Rectangle(470,470,64,64));
            graphics.FillRectangle(Brushes.Coral, new Rectangle(400,470,64,64));
            graphics.FillRectangle(Brushes.Coral, new Rectangle(330,470,64,64));
            graphics.FillRectangle(Brushes.Moccasin, new Rectangle(900,470,64,64));
        }
    }
}