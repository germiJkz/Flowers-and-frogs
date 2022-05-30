using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Timers;

namespace Flowers_and_frogs
{
    public class Model
    {
        public Point[] FlowersPositions;
        public Point[] FrogsPositions;
        public Point[] CollectedFlowersPositions;
        public Bouquet Bouquet;
        public List<Flower> CollectedFlowers;
        public Flower[] FlowersArray;
        public Frog[] FrogsArray;
        public int Money;
        public int Lives;
        

        public Model()
        {
            FlowersPositions = new Point[9]
            {
                new Point(200, 150), new Point(300, 150), new Point(400, 150), new Point(500, 150),
                new Point(600, 150), new Point(250, 250), new Point(350, 250), new Point(450, 250),
                new Point(550, 250)
            };
            FrogsPositions = new Point[] {new Point(850, 80), new Point(850, 200), new Point(850, 320)};
            CollectedFlowersPositions = new Point[3]
                {new Point(330, 470), new Point(400, 470), new Point(470, 470)};
            Bouquet = null;
            CollectedFlowers = new List<Flower>();
            FlowersArray = new Flower[21]
            {
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                null, null, null, null
            };
            FrogsArray = new Frog[9] {null, null, null, null, null, null, null, null, null};
            Money = 0;
            Lives = 3;
        }
        
        public  void TrySpawnFlower(Model model)
        {
            var random = new Random();
            
            var unusedFlowers = model.FlowersArray.Where(x => x.Location == new Point(-100, -100)).ToList();
            if (unusedFlowers.Count == 0) 
                return;
            var newFlower = unusedFlowers[random.Next(unusedFlowers.Count)];
            
            var unusedPoints = new List<Point>(model.FlowersPositions);
            
            foreach (var flower in FlowersArray)
            {
                if (!unusedFlowers.Contains(flower)) 
                    unusedPoints.Remove(flower.Location);
            }
            if (unusedPoints.Count == 0)
                return;
            var newPoint = unusedPoints[random.Next(unusedPoints.Count)];
            
            newFlower.MoveTo(newPoint);
        }

        public void TrySpawnFrogs(Model model)
        {
            var random = new Random();
            
            var unusedFrogs = model.FrogsArray.Where(x => x.Location == new Point(-100, -100)).ToList();
            if (unusedFrogs.Count == 0) 
                return;
            var newFrog = unusedFrogs[random.Next(unusedFrogs.Count)];
            
            var unusedPoints = new List<Point>(model.FrogsPositions);
            
            foreach (var frog in FrogsArray)
            {
                if (!unusedFrogs.Contains(frog)) 
                    unusedPoints.Remove(frog.Location);
            }
            if (unusedPoints.Count == 0)
                return;
            var newPoint = unusedPoints[random.Next(unusedPoints.Count)];
            
            newFrog.MoveTo(newPoint);
        }
        
        public void ThrowCollectedFlowers(object sender, System.EventArgs e)
        {
            foreach (var flower in CollectedFlowers)
            {
                flower.MoveTo(new Point(-100, -100));
            }
            CollectedFlowers = new List<Flower>();
        }

        public void CollectBouquet(object sender, System.EventArgs e)
        {
            if (CollectedFlowers.Count == 3)
            {
                var colors = CollectedFlowers.Select(x => x.Color).ToList();
                Image image = null;
                //выбор изображения:
                if (colors.Where(x => x == Color.Orange).Count() == 3)
                {
                    image = Image.FromFile(@"..\..\..\Pictures\OrangeOrangeOrangeBouquet.png");
                }
                else if (colors.Where(x => x == Color.Blue).Count() == 3)
                {
                    image = Image.FromFile(@"..\..\..\Pictures\BlueBlueBlueBouquet.png");
                }
                else if (colors.Where(x => x == Color.Pink).Count() == 3)
                {
                    image = Image.FromFile(@"..\..\..\Pictures\PinkPinkPinkBouquet.png");
                }
                else if (colors.Contains(Color.Orange) && colors.Contains(Color.Blue) && colors.Contains(Color.Pink))
                {
                    image = Image.FromFile(@"..\..\..\Pictures\BluePinkOrangeBouquet.png");
                }
                else if (colors.Where(x => x == Color.Orange).Count() == 2 && colors.Contains(Color.Blue)) 
                {
                    image = Image.FromFile(@"..\..\..\Pictures\OrangeOrangeBlueBouquet.png");
                }
                else if (colors.Where(x => x == Color.Orange).Count() == 2 && colors.Contains(Color.Pink)) 
                {
                    image = Image.FromFile(@"..\..\..\Pictures\OrangeOrangePinkBouquet.png");
                }
                else if (colors.Where(x => x == Color.Blue).Count() == 2 && colors.Contains(Color.Orange)) 
                {
                    image = Image.FromFile(@"..\..\..\Pictures\BlueBlueOrangeBouquet.png");
                }
                else if (colors.Where(x => x == Color.Blue).Count() == 2 && colors.Contains(Color.Pink)) 
                {
                    image = Image.FromFile(@"..\..\..\Pictures\BlueBluePinkBouquet.png");
                }
                else if (colors.Where(x => x == Color.Pink).Count() == 2 && colors.Contains(Color.Blue)) 
                {
                    image = Image.FromFile(@"..\..\..\Pictures\PinkPinkBlueBouquet.png");
                }
                else if (colors.Where(x => x == Color.Pink).Count() == 2 && colors.Contains(Color.Orange)) 
                {
                    image = Image.FromFile(@"..\..\..\Pictures\PinkPinkOrangeBouquet.png");
                }
                else //не понадобиться. но оставлю как дефоолт вариант
                {
                    image = Image.FromFile(@"..\..\..\Puctures\BluePinkOrangeBouquet.png");
                }
                
                Bouquet.Appear();
                Bouquet.SetColors(colors.ToArray());
                Bouquet.SetImage(image);
                foreach (var flower in CollectedFlowers)
                {
                    flower.MoveTo(new Point(-100,-100));
                }
                CollectedFlowers = new List<Flower>();
            }
        }
    }
}