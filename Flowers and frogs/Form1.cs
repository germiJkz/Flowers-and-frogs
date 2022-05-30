using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
            MessageBox.Show("К тебе будут приходить злые жабы. Ты можешь забодрить их букетами с цветами"+
                            "(нажми на жабу, чтобы отдать ей собранный букет)." +
                            " За цветы, что по цвету совпадают с цветом лягушки, ты будешь получать от них монетки." +
                            " Если ты не дашь жабе нужного цветка или она устанет ждать, то она тебя укусит." +
                            "\nПопробуй завоевать жабью любовь!", "Про игру", MessageBoxButtons.OK);

            var model = new Model();
            CreateFlowers(model);//нужно ли эти методы распихать по своим классам или оставить их тут?
            CreateFrogs(model);
            CreateBouquet(model);
            SpawnFlowers(model);
            SpawnFogs(model);

            var display = new Label()
            {
                Text = "0/20",
                Location = new Point(810, 20)
            };
            Controls.Add(display);

            var livesBox = new PictureBox()
            {
                Image = Image.FromFile(@"..\..\..\Pictures\heart3.png"),
                BackColor = Color.Transparent,
                Location = new Point(25, 25),
                AutoSize = true,
                Visible = true
            };
            Controls.Add(livesBox);
            
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
            
            Update(display, livesBox, model);
        }

        private static void Update(Label display, PictureBox livesBox, Model model)
        {
            var timer = new Timer();
            timer.Interval = 10;
            timer.Tick += ((sender, args) =>
            {
                display.Text = model.Money.ToString() + "/20";
                foreach (var frog in model.FrogsArray.Where(frog => frog.Location != new Point(-100, -100))) 
                {
                    frog.DecreaseTime();
                }
                
                var angryFrogs = model.FrogsArray
                    .Where(frog => frog.Location != new Point(-100, -100) && frog.Time <= 0)
                    .ToArray();
                foreach (var frog in angryFrogs)
                {
                    model.Lives--;//добавить краснение краёв экрана
                    
                    frog.MoveTo(new Point(-100,-100));
                    frog.ResetTime();
                }
                
                if (model.Lives <= 0)
                {
                    timer.Stop();
                    livesBox.Visible = false;
                    MessageBox.Show("К сожалению, ты был съеден жабами...", "Проигрыш(", MessageBoxButtons.OK);
                    Application.Exit();
                }
                else if (model.Lives == 1)
                {
                    livesBox.Image=Image.FromFile(@"..\..\..\Pictures\heart.png");
                }
                else if (model.Lives == 2)
                {
                    livesBox.Image=Image.FromFile(@"..\..\..\Pictures\heart2.png");
                }
                
                if (model.Money >= 20)
                {
                    timer.Stop();
                    MessageBox.Show("Поздравляю, ты завоевал жабью любовь. Теперь они оставят тебя в покое",
                        "Победа!", MessageBoxButtons.OK);
                    Application.Exit();
                }
            });
            timer.Start();
        }

        private  void CreateBouquet(Model model)
        {
            var bouquet = new Bouquet(new[] {Color.Pink, Color.Orange, Color.Blue},
                Image.FromFile(@"..\..\..\Pictures\BluePinkOrangeBouquet.png"));
            model.Bouquet = bouquet;
            Controls.Add(bouquet.PictureBox);
        }

        private void CreateFrogs(Model model)
        {
            for (int i = 0; i < 9; i++)
            {
                Color color;
                Image image;
                switch (i/3)
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
                            model.Bouquet.Disappear();
                            frog.MoveTo(new Point(-100,-100));
                            frog.ResetTime();
                        }
                        else if (model.Bouquet.Colors.Where(x => x == frog.Color).Count() == 2)
                        {
                            model.Money += 2;
                            model.Bouquet.Disappear();
                            frog.MoveTo(new Point(-100,-100));
                            frog.ResetTime();
                        }
                        else if (model.Bouquet.Colors.Where(x => x == frog.Color).Count() == 1)
                        {
                            model.Money += 1;
                            model.Bouquet.Disappear();
                            frog.MoveTo(new Point(-100,-100));
                            frog.ResetTime();
                        }
                        else //нет совпадающих цветов лягушка злится
                        {
                            model.Lives--;
                            model.Bouquet.Disappear();
                            frog.MoveTo(new Point(-100,-100));
                            frog.ResetTime();
                        }
                    }
                };
                
                Controls.Add(frog.PictureBox);
                Controls.Add(frog.Bar);
            }
        }

        private static void SpawnFlowers(Model model)
        {
            var timer = new Timer();
            timer.Interval = 3500;
            timer.Tick += ((sender, args) => { model.TrySpawnFlower(model); });
            timer.Start();
        }
        
        private static void SpawnFogs(Model model)
        {
            var timer = new Timer();
            timer.Interval = 10000;
            timer.Tick += ((sender, args) => { model.TrySpawnFrogs(model); });
            timer.Start();
        }

        private void CreateFlowers(Model model)
        {
            for (int i = 0; i < 21; i++)
            {
                Color color;
                Image image;
                switch (i/7)
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
                        flower.MoveTo(model.CollectedFlowersPositions[model.CollectedFlowers.Count]);
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
            graphics.DrawImage(Image.FromFile(@"..\..\..\Pictures\coin.png"), new Point(930, 15));
        }
    }
}