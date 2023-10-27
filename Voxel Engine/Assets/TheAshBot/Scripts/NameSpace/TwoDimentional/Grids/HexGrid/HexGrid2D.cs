using System.Collections.Generic;

using UnityEngine;

namespace TheAshBot.TwoDimentional.Grids
{
    public class HexGrid2D
    {

        
        /// <summary>
        /// This holds all the functions that are called when the grid changes
        /// </summary>
        public OnGridValueChangedEventArgs OnGridValueChanged;
        public delegate void OnGridValueChangedEventArgs(int x, int y);


        protected const float HEX_VERTICAL_OFFSET_MULTIPLIER = 0.75f;


        protected int width;
        protected int height;
        protected float cellSize;
        protected Vector2 originPosition;


        /// <summary>
        /// This makes a grid that each cell holds a boolean value
        /// </summary>
        /// <param name="width">This is the width of the grid</param>
        /// <param name="height">THis is the hight of the grid</param>
        /// <param name="cellSize">This is how big the grid objects are</param>
        /// <param name="originPosition">This is the position of the bottum left grid object(AKA the origin</param>
        public HexGrid2D(int width, int height, float cellSize, Vector2 originPosition)
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
            return 
                new Vector2(x, 0) * cellSize + 
                new Vector2(0, y) * cellSize * HEX_VERTICAL_OFFSET_MULTIPLIER + 
                ((y % 2) == 1 ? new Vector2(1, 0) * cellSize * 0.5f : Vector2.zero) + 
                originPosition;
        }

        /// <summary>
        /// This gets the x and y position of a grid object using it's world position
        /// </summary>
        /// <param name="worldPosition">This is the world position of the grid object</param>
        /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
        /// <param name="y">This is the number of grid objects above the start grid object</param>
        public void GetXY(Vector2 worldPosition, out int x, out int y)
        {
            int roughX = Mathf.RoundToInt((worldPosition - originPosition).x / cellSize);
            int roughY = Mathf.RoundToInt((worldPosition - originPosition).y / cellSize * HEX_VERTICAL_OFFSET_MULTIPLIER);

            Vector2Int roughXY = new Vector2Int(roughX, roughY);

            bool isOddRow = roughY % 2 == 1;

            List<Vector2Int> neighbourXYList = new List<Vector2Int>
            {
                roughXY + new Vector2Int(-1, 0),
                roughXY + new Vector2Int(+1, 0),
                 
                roughXY + new Vector2Int(isOddRow ? + 1: - 1, +1),
                roughXY + new Vector2Int(+0, +1),

                roughXY + new Vector2Int(isOddRow ? + 1: - 1 , -1),
                roughXY + new Vector2Int(+0, -1),
            };

            Vector2Int closestXY = roughXY;

            foreach (Vector2Int neighbourXY in neighbourXYList)
            { 
                if (Vector2.Distance(worldPosition, GetWorldPosition(neighbourXY.x, neighbourXY.y)) < Vector2.Distance(worldPosition, GetWorldPosition(closestXY.x, closestXY.y)))
                {
                    // neighbourXY is closer then closestXY
                    closestXY = neighbourXY;
                }
            }

            x = closestXY.x;
            y = closestXY.y;
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
        public virtual float GetValueNormalized(int x, int y)
        {
            return 0;
        }
        /// <summary>
        /// will return true if the grid cell has a value, else return false
        /// </summary>
        /// <param name="worldPosition">This is the world position of the grid object</param>
        /// <returns>true if the grid cell has a value, else false</returns>
        public virtual float GetValueNormalized(Vector2 worldPosition)
        {
            return 0;
        }

    }
}
