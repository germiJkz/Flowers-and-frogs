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
        public Point BouquetPosition;//добавить такой класс
        public List<Flower> CollectedFlowers;
        public Flower[] FlowersArray;
        public Frog[] FrogsArray;

        public Model()
        {
            FlowersPositions = new Point[9]
            {
                new Point(200, 150), new Point(300, 150), new Point(400, 150), new Point(500, 150),
                new Point(600, 150), new Point(250, 250), new Point(350, 250), new Point(450, 250),
                new Point(550, 250)
            };
            FrogsPositions = new Point[] {new Point(850, 100), new Point(850, 200), new Point(850, 300)};
            CollectedFlowersPositions = new Point[3]
                {new Point(330, 470), new Point(400, 470), new Point(470, 470)};
            BouquetPosition = new Point(900, 470);
            CollectedFlowers = new List<Flower>();
            FlowersArray = new Flower[9] {null, null, null, null, null, null, null, null, null};
            FrogsArray = new Frog[3] {null, null, null};
        }
        
        public  void TrySpawnFlower(Model model)
        {
            var random = new Random();
            
            var unusedFlowers = model.FlowersArray.Where(x => x.Location == new Point(-100, -100)).ToList();
            if (unusedFlowers.Count==0) return;
            var newFlower = unusedFlowers[random.Next(unusedFlowers.Count)];
            
            var unusedPoints = new List<Point>(model.FlowersPositions);
            foreach (var flower in FlowersArray)
            {
                if (!unusedFlowers.Contains(flower)) unusedPoints.Remove(flower.Location);
            }
            var newPoint = unusedPoints[random.Next(unusedPoints.Count)];
            
            newFlower.Location = newPoint;
            newFlower.PictureBox.Bounds = new Rectangle(newPoint, new Size(64, 64));
        }
        
        public void ThrowCollectedFlowers(object sender, System.EventArgs e)
        {
            foreach (var flower in CollectedFlowers)
            {
                flower.Location = new Point(-100, -100);
                flower.PictureBox.Bounds = new Rectangle(flower.Location, new Size(64, 64));
            }
            CollectedFlowers = new List<Flower>();
        }
    }
}