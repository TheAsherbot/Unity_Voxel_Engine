using UnityEngine;

namespace TheAshBot.TwoDimentional.Grids
{
    public class BoolGrid2D : Grid2D
    {


        private bool[,] gridArray;

        /// <summary>
        /// This makes a grid that each cell holds a boolean value
        /// </summary>
        /// <param name="width">This is the width of the grid</param>
        /// <param name="height">THis is the hight of the grid</param>
        /// <param name="cellSize">This is how big the grid objects are</param>
        /// <param name="originPosition">This is the position of the bottum left grid object(AKA the origin</param>
        /// <param name="showDebug">If this is true the it will show the lines of the grid</param>
        /// <param name="parent">This si the parent object of the text(This is only needed if show debug is true)</param>
        public BoolGrid2D(int width, int height, float cellSize, Vector2 originPosition, bool showDebug, Transform parent)
                : base (width, height, cellSize, originPosition, showDebug, parent)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new bool[width, height];

            if (parent == null)
            {
                showDebug = false;
            }

            if (showDebug)
            {
                TextMesh[,] debugTextArray = new TextMesh[width, height];

                for (int x = 0; x < gridArray.GetLength(0); x++)
                {
                    for (int y = 0; y < gridArray.GetLength(1); y++)
                    {
                        debugTextArray[x, y] = CreateWorldText(parent, gridArray[x, y].ToString(), GetWorldPosition(x, y) + new Vector2(cellSize, cellSize) * .5f, 5 * (int)cellSize, Color.white, TextAnchor.MiddleCenter);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                    }
                }
                Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

                OnGridValueChanged += (int x, int y) => { debugTextArray[x, y].text = gridArray[x, y].ToString(); };
            }
        }
        /// <summary>
        /// This makes a grid that each cell holds a boolean value
        /// </summary>
        /// <param name="width">This is the width of the grid</param>
        /// <param name="height">THis is the hight of the grid</param>
        /// <param name="cellSize">This is how big the grid objects are</param>
        /// <param name="originPosition">This is the position of the bottum left grid object(AKA the origin</param>
        public BoolGrid2D(int width, int height, float cellSize, Vector2 originPosition)
                : base (width, height, cellSize, originPosition)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new bool[width, height];
        }

        
        /// <summary>
        /// This sets the value of a cell using it's 
        /// </summary>
        /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
        /// <param name="y">This is the number of grid objects above the start grid object</param>
        /// <param name="value">This is the value it is being set to</param>
        public void SetValue(int x, int y, bool value)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                gridArray[x, y] = value;
                OnGridValueChanged?.Invoke(x, y);
            }
        }
        /// <summary>
        /// This sets the vaue of a cell using it's world position
        /// </summary>
        /// <param name="worldPosition">This is the grid objects world position<</param>
        /// <param name="value">This is the value it is being set to</param>
        public void SetValue(Vector2 worldPosition, bool value)
        {
            int x;
            int y;
            GetXY(worldPosition, out x, out y);
            SetValue(x, y, value);
        }

        /// <summary>
        /// This sets the value of a call to the oposite value using it's position on the grid
        /// </summary>
        /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
        /// <param name="y">This is the number of grid objects above the start grid object</param>
        public void SetOppositeValue(int x, int y)
        {
            bool value = !GetValue(x, y);
            SetValue(x, y, value);
        }
        /// <summary>
        /// This sets the value of a call to the oposite value using it's world position
        /// </summary>
        /// <param name="worldPosition">This is the grid objects world position</param>
        public void SetOppositeValue(Vector2 worldPosition)
        {
            int x;
            int y;
            GetXY(worldPosition, out x, out y);
            SetOppositeValue(x, y);
        }

        /// <summary>
        /// This gets the value of a cell using it's positon on the grid
        /// </summary>
        /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
        /// <param name="y">This is the number of grid objects above the start grid object</param>
        /// <returns>Returns the grid object</returns>
        public bool GetValue(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridArray[x, y];
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// This gets the value of a cell using it's world position
        /// </summary>
        /// <param name="worldPosition">This is the grid objects world position</param>
        /// <returns>This ruterns the grid object</returns>
        public bool GetValue(Vector2 worldPosition)
        {
            int x;
            int y;
            GetXY(worldPosition, out x, out y);
            return GetValue(x, y);
        }


        public override bool HasValue(int x, int y)
        {
            return GetValue(x, y);
        }
        public override bool HasValue(Vector2 worldPosition)
        {
            return GetValue(worldPosition);
        }

    }
}
