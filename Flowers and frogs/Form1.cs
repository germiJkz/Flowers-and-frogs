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
            CreateFlowers(model);//нужно ли эти методы распихать по своим классам или оставить их тут?
            CreateFrogs(model);
            CreateBouet(model);
            SpawnFlowers(model);
            SpawnFogs(model);

            

            var buttonCollectBouquet = new Button()
            {
                Location = new Point(700,450),
                Size = new Size(80,80),
                Text = "собрать\nбукет",
                BackColor = Color.Moccasin
                
            };
            buttonCollectBouquet.Click += new EventHandler(model.CollectBouquet);
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
        }

        private static void CreateBouet(Model model)
        {
            var bouquet = new Bouquet(new[] {Color.Pink, Color.Orange, Color.Blue},
                Image.FromFile(@"..\..\..\Pictures\BluePinkOrangeBouquet.png"));
            bouquet.PictureBox.Visible = false;
            model.Bouquet = bouquet;
        }

        private void CreateFrogs(Model model)
        {
            for (int i = 0; i < 3; i++)
            {
                Color color;
                Image image;
                switch (i)
                {
                    case 0:
                        color = Color.Orange;
                        image=Image.FromFile(@"..\..\..\Pictures\OrangeFrog.png");
                        break;
                    case 1:
                        color = Color.Blue;
                        image=Image.FromFile(@"..\..\..\Pictures\BlueFrog.png");
                        break;
                    case 2:
                        color = Color.Pink;
                        image=Image.FromFile(@"..\..\..\Pictures\PinkFrog.png");
                        break;
                    default://надо ли?
                        color = Color.Orange;
                        image=Image.FromFile(@"..\..\..\Pictures\OrangeFrog.png");
                        break;
                }

                var frog = new Frog(color, new Point(-100,-100), image);
                model.FrogsArray[i] = frog;
                
                frog.PictureBox.Click += (sender, args) => // нужно ли это писать здесь или вынести куда-либо
                {
                    if (model.Bouquet.PictureBox.Visible) 
                    {
                        if (model.Bouquet.Colors.Where(x => x == frog.Color).Count() == 3)
                        {
                            model.Money += 3;
                            model.Bouquet.PictureBox.Visible = false;
                            frog.Location = new Point(-100, -100);
                            frog.PictureBox.Bounds = new Rectangle(frog.Location, new Size(96, 96));
                        }
                        else if (model.Bouquet.Colors.Where(x => x == frog.Color).Count() == 2)
                        {
                            model.Money += 2;
                            model.Bouquet.PictureBox.Visible = false;
                            frog.Location = new Point(-100, -100);
                            frog.PictureBox.Bounds = new Rectangle(frog.Location, new Size(96, 96));
                        }
                        else if (model.Bouquet.Colors.Where(x => x == frog.Color).Count() == 1)
                        {
                            model.Money += 1;
                            model.Bouquet.PictureBox.Visible = false;
                            frog.Location = new Point(-100, -100);
                            frog.PictureBox.Bounds = new Rectangle(frog.Location, new Size(96, 96));
                        }
                        else //нет совпадающих цветов лягушка злится
                        {
                            
                        }
                    }
                };
                Controls.Add(frog.PictureBox);
            }
        }

        private static void SpawnFlowers(Model model)
        {
            var timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += ((sender, args) => { model.TrySpawnFlower(model); });
            timer.Start();
        }
        
        private static void SpawnFogs(Model model)
        {
            var timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += ((sender, args) => { model.TrySpawnFrogs(model); });
            timer.Start();
        }

        private void CreateFlowers(Model model)
        {
            for (int i = 0; i < 12; i++)
            {
                Color color;
                Image image;
                switch (i/4)
                {
                    case 0: 
                        color = Color.Pink;
                        image = Image.FromFile(@"..\..\..\Pictures\PinkFlower.png");
                        break;
                    case 1:
                        color = Color.Blue;
                        image = Image.FromFile(@"..\..\..\Pictures\BlueFlower.png");
                        break;
                    case 2:
                        color = Color.Orange;
                        image = Image.FromFile(@"..\..\..\Pictures\OrangeFlower.png");
                        break;
                    default://никогда не выйдет сюда, нужно ли оставить?
                        color = Color.Pink;
                        image = Image.FromFile(@"..\..\..\Pictures\PinkFlower.png");
                        break;
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