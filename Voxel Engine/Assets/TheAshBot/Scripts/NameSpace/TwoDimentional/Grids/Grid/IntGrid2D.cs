using UnityEngine;

namespace TheAshBot.TwoDimentional.Grids
{
    public class IntGrid2D : Grid2D
    {


        private int minValue = int.MinValue;
        private int maxValue = int.MaxValue;

        private int[,] gridArray;


        /// <summary>
        /// This makes a grid that each cell holds a boolean value
        /// </summary>
        /// <param name="width">This is the width of the grid</param>
        /// <param name="height">This is the hight of the grid</param>
        /// <param name="cellSize">This is how big the grid objects are</param>
        /// <param name="originPosition">This is the position of the bottum left grid object(AKA the origin)</param>
        /// <param name="minValue">This is the minimum value that a cell can have</param>
        /// <param name="maxValue">This is the maximum value that a cell can have</param>
        /// <param name="showDebug">If this is true the it will show the lines of the grid</param>
        /// <param name="parent">This si the parent object of the text(This is only needed if show debug is true)</param>
        public IntGrid2D(int width, int height, float cellSize, Vector2 originPosition, int minValue, int maxValue, bool showDebug, Transform parent)
                : base(width, height, cellSize, originPosition, showDebug, parent)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
            this.minValue = minValue;
            this.maxValue = maxValue;

            gridArray = new int[width, height];

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

                OnGridValueChanged += (int x, int y) =>
                {
                    debugTextArray[x, y].text = gridArray[x, y].ToString();
                };
            }
        }
        /// <summary>
        /// This makes a grid that each cell holds a boolean value
        /// </summary>
        /// <param name="width">This is the width of the grid</param>
        /// <param name="height">THis is the hight of the grid</param>
        /// <param name="cellSize">This is how big the grid objects are</param>
        /// <param name="originPosition">This is the position of the bottum left grid object(AKA the origin</param>
        /// <param name="showDebug">If this is true the it will show the lines of the grid</param>
        /// <param name="parent">This si the parent object of the text(This is only needed if show debug is true)</param>
        public IntGrid2D(int width, int height, float cellSize, Vector2 originPosition, bool showDebug, Transform parent)
                : base(width, height, cellSize, originPosition, showDebug, parent)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new int[width, height];

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

                OnGridValueChanged += (int x, int y) =>
                {
                    debugTextArray[x, y].text = gridArray[x, y].ToString();
                };
            }
        }
        /// <summary>
        /// This makes a grid that each cell holds a boolean value
        /// </summary>
        /// <param name="width">This is the width of the grid</param>
        /// <param name="height">THis is the hight of the grid</param>
        /// <param name="cellSize">This is how big the grid objects are</param>
        /// <param name="originPosition">This is the position of the bottum left grid object(AKA the origin</param>
        /// <param name="minValue">This is the minimum value that a cell can have</param>
        /// <param name="maxValue">This is the maximum value that a cell can have</param>
        public IntGrid2D(int width, int height, float cellSize, Vector2 originPosition, int minValue, int maxValue)
                : base(width, height, cellSize, originPosition)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
            this.minValue = minValue;
            this.maxValue = maxValue;

            gridArray = new int[width, height];
        }
        /// <summary>
        /// This makes a grid that each cell holds a boolean value
        /// </summary>
        /// <param name="width">This is the width of the grid</param>
        /// <param name="height">THis is the hight of the grid</param>
        /// <param name="cellSize">This is how big the grid objects are</param>
        /// <param name="originPosition">This is the position of the bottum left grid object(AKA the origin</param>
        public IntGrid2D(int width, int height, float cellSize, Vector2 originPosition)
                : base(width, height, cellSize, originPosition)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new int[width, height];
        }


        /// <summary>
        /// THis sets the value of a cell using it's 
        /// </summary>
        /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
        /// <param name="y">This is the number of grid objects above the start grid object</param>
        /// <param name="value">This is the value it is being set to</param>
        public void SetValue(int x, int y, int value)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                gridArray[x, y] = Mathf.Clamp(value, minValue, maxValue);
                OnGridValueChanged?.Invoke(x, y);
            }
        }
        /// <summary>
        /// This sets the vaue of a cell using it's world position
        /// </summary>
        /// <param name="worldPosition">This is the grid objects world position<</param>
        /// <param name="value">This is the value it is being set to</param>
        public void SetValue(Vector2 worldPosition, int value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetValue(x, y, value);
        }

        /// <summary>
        /// This gets the value of a cell using it's positon on the grid
        /// </summary>
        /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
        /// <param name="y">This is the number of grid objects above the start grid object</param>
        /// <returns>Returns the grid object</returns>
        public int GetValue(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridArray[x, y];
            }
            else
            {
                Debug.Log("DID NOT HIT");
                return 0;
            }
        }
        /// <summary>
        /// This gets the value of a cell using it's world position
        /// </summary>
        /// <param name="worldPosition">This is the grid objects world position</param>
        /// <returns>This ruterns the grid object</returns>
        public int GetValue(Vector2 worldPosition)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetValue(x, y);
        }

        /// <summary>
        /// This adds the to the value of a cell using it's positon on the grid
        /// </summary>
        /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
        /// <param name="y">This is the number of grid objects above the start grid object</param>
        /// <param name="value">This is the value being adding to the previus value</param>
        public void AddValue(int x, int y, int value)
        {
            SetValue(x, y, GetValue(x, y) + value);
        }
        /// <summary>
        /// This adds the to the value of a cell using it's world position
        /// </summary>
        /// <param name="worldPosition">This is the grid objects world position</param>
        /// <param name="value">This is the value being adding to the previus value</param>
        public void AddValue(Vector2 worldPosition, int value)
        {
            SetValue(worldPosition, GetValue(worldPosition) + value);
        }

        /// <summary>
        /// This adds the to the value of multipule cell
        /// </summary>
        /// <param name="worldPosition">This is the grid objects world position</param>
        /// <param name="value">This is the value being adding to the previus value</param>
        /// <param name="fullValueRadius">This is the raduis of the cells that are set to the max value</param>
        /// <param name="radius">This is the raduis of the dimand</param>
        public void AddValueInDimand(Vector2 worldPosition, int value, int fullValueRadius, int radius)
        {
            int lowerValueAmount = Mathf.RoundToInt((float)value / (radius - fullValueRadius));

            GetXY(worldPosition, out int originX, out int originY);

            if (!(originX >= 0 && originY >= 0 && originX < width && originY < height))
            {
                Debug.Log("originX >= 0 &&...");
                return;
            }

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius - x; y++)
                {
                    int _radius = x + y;
                    int addValueAmount = value;
                    if (_radius >= fullValueRadius)
                    {
                        addValueAmount -= lowerValueAmount * (_radius - fullValueRadius);
                    }

                    AddValue(originX + x, originY + y, addValueAmount);

                    if (x != 0)
                    {
                        AddValue(originX - x, originY + y, addValueAmount);
                    }
                    if (y != 0)
                    {
                        AddValue(originX + x, originY - y, addValueAmount);
                        if (x != 0)
                        {
                            AddValue(originX - x, originY - y, addValueAmount);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// This adds the to the value of multipule cell
        /// </summary>
        /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
        /// <param name="y">This is the number of grid objects above the start grid object</param>
        /// <param name="value">This is the value being adding to the previus value</param>
        /// <param name="fullValueRadius">This is the raduis of the cells that are set to the max value</param>
        /// <param name="radius">This is the raduis of the dimand</param>
        public void AddValueInDimand(int x, int y, int value, int fullValueRadius, int radius)
        {
            AddValueInDimand(GetWorldPosition(x, y), value, fullValueRadius, radius);
        }


        public override bool HasValue(int x, int y)
        {
            return GetValue(x, y) != 0;
        }
        public override bool HasValue(Vector2 worldPosition)
        {
            return GetValue(worldPosition) != 0;
        }

    }
}
