using UnityEngine;

namespace TheAshBot.PixelEngine
{
    public class Grid2D
    {


        /// <summary>
        /// This holds all the functions that are called when the grid changes
        /// </summary>
        public OnGridValueChangedEventArgs OnGridValueChanged;
        public delegate void OnGridValueChangedEventArgs(int x, int y);




        protected int width;
        protected int height;
        protected float cellSize;
        protected Vector2 originPosition;

        /// <summary>
        /// This makes a grid that each cell holds a no value
        /// </summary>
        /// <param name="width">This is the width of the grid</param>
        /// <param name="height">THis is the hight of the grid</param>
        /// <param name="cellSize">This is how big the grid objects are</param>
        /// <param name="originPosition">This is the position of the bottum left grid object(AKA the origin</param>
        /// <param name="showDebug">If this is true the it will show the lines of the grid</param>
        /// <param name="parent">This si the parent object of the text(This is only needed if show debug is true)</param>
        public Grid2D(int width, int height, float cellSize, Vector2 originPosition, bool showDebug, Transform parent)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            if (parent == null)
            {
                showDebug = false;
            }

            if (showDebug)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                    }
                }
                Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
            }
        }

        /// /// <summary>
        /// This makes a grid that each cell holds a no value
        /// </summary>
        /// <param name="width">This is the width of the grid</param>
        /// <param name="height">THis is the hight of the grid</param>
        /// <param name="cellSize">This is how big the grid objects are</param>
        /// <param name="originPosition">This is the position of the bottum left grid object(AKA the origin</param>
        public Grid2D(int width, int height, float cellSize, Vector2 originPosition)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public float GetCellSize()
        {
            return cellSize;
        }

        /// <summary>
        /// This gets the world position of a grid object using its x, and y position
        /// </summary>
        /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
        /// <param name="y">This is the number of grid objects above the start grid object</param>
        /// <returns>The world position</returns>
        public Vector2 GetWorldPosition(int x, int y)
        {
            return new Vector2(x, y) * cellSize + originPosition;
        }

        /// <summary>
        /// This gets the x and y position of a grid object using it's world position
        /// </summary>
        /// <param name="worldPosition">This is the world position of the grid object</param>
        /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
        /// <param name="y">This is the number of grid objects above the start grid object</param>
        public void GetXY(Vector2 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        }

        /// <summary>
        /// This snaps a position to the grid
        /// </summary>
        /// <param name="worldPosition">This is the world position of the grid object</param>
        /// <returns>Returns the world position snaped to the grid</returns>
        public Vector2 SnapPositionToGrid(Vector2 worldPosition)
        {
            GetXY(worldPosition, out int x, out int y);
            return GetWorldPosition(x, y);
        }


        /// <summary>
        /// will return true if the grid cell has a value, else return false
        /// </summary>
        /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
        /// <param name="y">This is the number of grid objects above the start grid object</param>
        /// <returns>true if the grid cell has a value, else false</returns>
        public virtual bool HasValue(int x, int y)
        {
            return false;
        }
        /// <summary>
        /// will return true if the grid cell has a value, else return false
        /// </summary>
        /// <param name="worldPosition">This is the world position of the grid object</param>
        /// <returns>true if the grid cell has a value, else false</returns>
        public virtual bool HasValue(Vector2 worldPosition)
        {
            return false;
        }


        #region Helpers

        protected static TextMesh CreateWorldText(Transform parent, string text, Vector2 localPosition, int fontSize, Color color, TextAnchor textAnchor)
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