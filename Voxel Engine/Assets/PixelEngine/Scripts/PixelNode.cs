using UnityEngine;

namespace TheAshBot.PixelEngine
{
    public struct PixelNode
    {

        public PixelNode(GenericGrid2D<PixelNode> grid, byte x, byte y)
        {
            this.grid = grid;
            color = new Color32(0, 0, 0, 0);
            isFilled = false;
            this.x = x;
            this.y = y;

            neighbors = new PixelNode[0];
            UpdateNeighbors();
        }


        public bool isFilled;
        public byte x;
        public byte y;
        public Color32 color;
        private GenericGrid2D<PixelNode> grid;

        /// <summary>
        /// Is the 6 direct neighbors. 0 = Top, 1 = Bottom, 2 = Front, 3 = Back, 4 = Right, 5 = Left
        /// </summary>
        public PixelNode[] neighbors;




        public void UpdateNeighbors()
        {
            neighbors = new PixelNode[]
            {
                grid.GetGridObject(x, y + 1),
                grid.GetGridObject(x, y - 1),
                grid.GetGridObject(x + 1, y),
                grid.GetGridObject(x - 1, y),
            };
        }

        public override string ToString()
        {
            return (isFilled == false ? "" : "Filled");
        }

    }
}
