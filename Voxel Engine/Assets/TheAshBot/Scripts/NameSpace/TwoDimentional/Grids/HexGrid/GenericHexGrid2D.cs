using System;

using JetBrains.Annotations;

using UnityEngine;

namespace TheAshBot.TwoDimentional.Grids
{
    public class GenericHexGrid2D<TGridObject> : HexGrid2D
    {


        private TGridObject[,] gridArray;


        /// <summary>
        /// This makes a grid that each cell holds a generic value
        /// </summary>
        /// <param name="width">This is the width of the grid</param>
        /// <param name="height">This is the hight of the grid</param>
        /// <param name="cellSize">This is how big the grid objects are</param>
        /// <param name="originPosition">This is the position of the bottum left grid object(AKA the origin)</param>
        /// <param name="defaultGridObject">This is the the value that all the gid object will default to<code>(GenericGrid grid, int x, int y) => new TGridObject(grid, x, y)</code></param>
        /// <param name="showDebug">If this is true the it will show the lines of the grid</param>
        /// <param name="parent">This is the parent object of the text(This is only needed if show debug is true)</param>
        public GenericHexGrid2D(int width, int height, float cellSize, Vector2 originPosition, Func<GenericHexGrid2D<TGridObject>, int, int, TGridObject> defaultGridObject, bool showDebug, Transform parent)
                         : base(width, height, cellSize, originPosition)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new TGridObject[width, height];

            TextMesh[,] debugTextArray = new TextMesh[width, height];

            if (showDebug)
            {
                OnGridValueChanged += (int x, int y) =>
                {
                    debugTextArray[x, y].text = gridArray[x, y].ToString();
                };
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    gridArray[x, y] = defaultGridObject(this, x, y);
                    if (showDebug == true)
                    {
                        debugTextArray[x, y] = CreateWorldText(parent, gridArray[x, y].ToString(), GetWorldPosition(x, y), 5 * (int)cellSize, Color.white, TextAnchor.MiddleCenter);
                    }
                }
            }
        }
        /// <summary>
        /// This makes a grid that each cell holds a generic value
        /// </summary>
        /// <param name="width">This is the width of the grid</param>
        /// <param name="height">This is the hight of the grid</param>
        /// <param name="cellSize">This is how big the grid objects are</param>
        /// <param name="originPosition">This is the position of the bottum left grid object(AKA the origin)</param>
        /// <param name="defaultGridObject">This is the the value that all the gid object will default to<code>(GenericGrid grid, int x, int y) => new TGridObject(grid, x, y)</code></param>
        public GenericHexGrid2D(int width, int height, float cellSize, Vector2 originPosition, Func<GenericHexGrid2D<TGridObject>, int, int, TGridObject> defaultGridObject)
                         : base(width, height, cellSize, originPosition)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new TGridObject[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    gridArray[x, y] = defaultGridObject(this, x, y);
                }
            }
        }


        /// <summary>
        /// This sets the value of a cell using it's 
        /// </summary>
        /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
        /// <param name="y">This is the number of grid objects above the start grid object</param>
        /// <param name="value">This is the value it is being set to</param>
        public void SetGridObject(int x, int y, TGridObject value)
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
        /// <param name="worldPosition">This is the grid objects world position</param>
        /// <param name="value">This is the value it is being set to</param>
        public void SetGridObject(Vector2 worldPosition, TGridObject value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetGridObject(x, y, value);
        }

        /// <summary>
        /// This gets the value of a cell using it's positon on the grid
        /// </summary>
        /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
        /// <param name="y">This is the number of grid objects above the start grid object</param>
        /// <returns>Returns the grid object</returns>
        public TGridObject GetGridObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridArray[x, y];
            }
            else
            {
                return default;
            }
        }
        /// <summary>
        /// This gets the value of a cell using it's world position
        /// </summary>
        /// <param name="worldPosition">This is the grid objects world position</param>
        /// <returns>This ruterns the grid object</returns>
        public TGridObject GetGridObject(Vector2 worldPosition)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetGridObject(x, y);
        }

        #region Helpers

        public static TextMesh CreateWorldText(Transform parent, string text, Vector2 localPosition, int fontSize, Color color, TextAnchor textAnchor)
        {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            return textMesh;
        }

        #endregion

    }
}
