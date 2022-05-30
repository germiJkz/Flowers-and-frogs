using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;

namespace Flowers_and_frogs
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void MoveFlower()
        {
            var flower = new Flower(Color.Blue, new Point(0, 0), Image.FromFile(@"..\..\..\Pictures\BlueFlower.png"));
            flower.MoveTo(new Point(200,300));
            Assert.AreEqual(new Point(200,300), flower.Location);
            Assert.AreEqual(new Point(200,300), flower.PictureBox.Location);
        }
        
        [Test]
        public void MoveFrog()
        {
            var frog = new Frog(Color.Blue, new Point(0, 0), Image.FromFile(@"..\..\..\Pictures\BlueFrog.png"));
            frog.MoveTo(new Point(200,300));
            Assert.AreEqual(new Point(200,300), frog.Location);
            Assert.AreEqual(new Point(200,300), frog.PictureBox.Location);
            Assert.AreEqual(new Rectangle(new Point(200, 280), new Size(64, 10)), frog.Bar.Bounds);
        }

        [Test]
        public void DecreaseTime()
        {
            var frog = new Frog(Color.Blue, new Point(0, 0), Image.FromFile(@"..\..\..\Pictures\BlueFrog.png"));
            frog.DecreaseTime();
            Assert.AreEqual(1499, frog.Time);
            Assert.AreEqual(1499, frog.Bar.Value);
        }
        
        [Test]
        public void ResetTime()
        {
            var frog = new Frog(Color.Blue, new Point(0, 0), Image.FromFile(@"..\..\..\Pictures\BlueFrog.png"));
            frog.DecreaseTime();
            frog.DecreaseTime();
            frog.DecreaseTime();
            frog.DecreaseTime();
            frog.ResetTime();
            Assert.AreEqual(1500, frog.Time);
            Assert.AreEqual(1500, frog.Bar.Value);
        }
        
        [Test]
        public void BouquetAppear()
        {
            var bouquet = new Bouquet(new Color[] {Color.Blue, Color.Blue, Color.Blue},
                Image.FromFile(@"..\..\..\Pictures\BlueBlueBlueBouquet.png"));
            bouquet.Appear();
            Assert.AreEqual(true, bouquet.PictureBox.Visible);
        }
        
        [Test]
        public void BouquetDisappear()
        {
            var bouquet = new Bouquet(new Color[] {Color.Blue, Color.Blue, Color.Blue},
                Image.FromFile(@"..\..\..\Pictures\BlueBlueBlueBouquet.png"));
            bouquet.Appear();
            bouquet.Disappear();
            Assert.AreEqual(false, bouquet.PictureBox.Visible);
        }
        
        [Test]
        public void BouquetSetColors()
        {
            var bouquet = new Bouquet(new Color[] {Color.Blue, Color.Blue, Color.Blue},
                Image.FromFile(@"..\..\..\Pictures\BlueBlueBlueBouquet.png"));
            bouquet.SetColors(new Color[] {Color.Blue, Color.Blue, Color.Orange});
            Assert.AreEqual(new Color[] {Color.Blue, Color.Blue, Color.Orange}, bouquet.Colors);
        }

        [Test]
        public void ThrowCollectedFlowers()
        {
            var model = new Model();
            model.CollectedFlowers.Add(new Flower(Color.Blue, new Point(100, 200),
                Image.FromFile(@"..\..\..\Pictures\BlueFlower.png")));
            model.CollectedFlowers.Add(new Flower(Color.Blue, new Point(100, 200),
                Image.FromFile(@"..\..\..\Pictures\BlueFlower.png")));
            model.CollectedFlowers.Add(new Flower(Color.Blue, new Point(100, 200),
                Image.FromFile(@"..\..\..\Pictures\BlueFlower.png")));
            model.ThrowCollectedFlowers(new object(), EventArgs.Empty);
            Assert.AreEqual(new List<Color>(), model.CollectedFlowers);
        }

        [Test]
        public void CollectBouquet()
        {
            var model = new Model();
            model.Bouquet = new Bouquet(new Color[] { }, Image.FromFile(@"..\..\..\Pictures\BlueBlueBlueBouquet.png"));
            model.CollectedFlowers.Add(new Flower(Color.Blue, new Point(100, 200),
                Image.FromFile(@"..\..\..\Pictures\BlueFlower.png")));
            model.CollectedFlowers.Add(new Flower(Color.Blue, new Point(100, 200),
                Image.FromFile(@"..\..\..\Pictures\BlueFlower.png")));
            model.CollectedFlowers.Add(new Flower(Color.Blue, new Point(100, 200),
                Image.FromFile(@"..\..\..\Pictures\BlueFlower.png")));
            model.CollectBouquet(new object(), EventArgs.Empty);
            Assert.AreEqual(true, model.Bouquet.PictureBox.Visible);
            Assert.AreEqual(new Color[] {Color.Blue, Color.Blue, Color.Blue}, model.Bouquet.Colors);
            Assert.AreEqual(new List<Flower>(), model.CollectedFlowers);
        }
    }
}