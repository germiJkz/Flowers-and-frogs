using System.Collections.Generic;
using System.Drawing;
namespace Flowers_and_frogs
{
    public class Model
    {
        private Point[] FlowersPositions;
        private Point[] FrogsPositions;
        private Point[] CollectedFlowersPositions;
        private Point BouquetPosition;//добавить такой класс
        private List<Flower> CollectedFlowers;
        private Flower[] FlowersArray;
        private Frog[] FrogsArray;

        public Model()
        {
            FlowersPositions = new Point[9]
            {
                new Point(200, 150), new Point(300, 150), new Point(400, 150), new Point(500, 150),
                new Point(600, 150), new Point(250, 250), new Point(350, 250), new Point(450, 250),
                new Point(550, 250)
            };
            FrogsPositions = new Point[] {new Point(900, 100), new Point(900, 200), new Point(900, 300)};
            CollectedFlowersPositions = new Point[3]
                {new Point(330, 470), new Point(400, 470), new Point(470, 470)};
            BouquetPosition = new Point(900, 470);
            CollectedFlowers = new List<Flower>();
            FlowersArray = new Flower[9] {null, null, null, null, null, null, null, null, null};
            FrogsArray = new Frog[3] {null, null, null};
        }
    }
}