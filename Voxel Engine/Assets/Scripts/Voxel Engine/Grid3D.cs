using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid3D
{


    /// <summary>
    /// This holds all the functions that are called when the grid changes <code>grid.OnGridValueChanged += Grid_OnValueChanged;</code><code>private void Grid_OnValueChanged(int x, int y, int z)</code>
    /// </summary>
    public OnGridValueChangedEventArgs OnGridValueChanged;
    public delegate void OnGridValueChangedEventArgs(int x, int y, int z);




    protected int width;
    protected int height;
    protected int depth;
    protected float cellSize;
    protected Vector3 originPosition;

    /// <summary>
    /// This makes a grid that each cell holds a no value
    /// </summary>
    /// <param name="width">This is the width of the grid</param>
    /// <param name="height">THis is the height of the grid</param>
    /// <param name="cellSize">This is how big the grid objects are</param>
    /// <param name="originPosition">This is the position of the bottom left grid object(AKA the origin</param>
    /// <param name="showDebug">If this is true the it will show the lines of the grid</param>
    /// <param name="parent">This si the parent object of the text(This is only needed if show debug is true)</param>
    public Grid3D(int width, int height, int depth, float cellSize, Vector3 originPosition, bool showDebug, Transform parent)
    {
        this.width = width;
        this.height = height;
        this.depth = depth;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        if (parent == null)
        {
            showDebug = false;
        }

        if (showDebug)
        {
            for (int x = 0; x <= width; x++)
            {
                for (int y = 0; y <= height; y++)
                {
                    for (int z = 0; z <= depth; z++)
                    {
                        if (x != width)
                        {
                            Debug.DrawLine(GetWorldPosition(x, y, z), GetWorldPosition(x + 1, y, z), Color.white, 100f);
                        }
                        if (y != height)
                        {
                            Debug.DrawLine(GetWorldPosition(x, y, z), GetWorldPosition(x, y + 1, z), Color.white, 100f);
                        }
                        if (z != depth)
                        {
                            Debug.DrawLine(GetWorldPosition(x, y, z), GetWorldPosition(x, y, z + 1), Color.white, 100f);
                        }
                    }
                }
            }
        }
    }

    /// /// <summary>
    /// This makes a grid that each cell holds a no value
    /// </summary>
    /// <param name="width">This is the width of the grid</param>
    /// <param name="height">THis is the height of the grid</param>
    /// <param name="cellSize">This is how big the grid objects are</param>
    /// <param name="originPosition">This is the position of the bottom left grid object(AKA the origin</param>
    public Grid3D(int width, int height, int depth, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.depth = depth;
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
    
    public int GetDepth()
    {
        return depth;
    }

    public float GetCellSize()
    {
        return cellSize;
    }
    public int GetCellSizeInt()
    {
        return Mathf.RoundToInt(cellSize);
    }

    /// <summary>
    /// This gets the world position of a grid object using its x, and y position
    /// </summary>
    /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
    /// <param name="y">This is the number of grid objects above the start grid object</param>
    /// <returns>The world position</returns>
    public Vector3 GetWorldPosition(int x, int y, int z)
    {
        return new Vector3(x, y, z) * cellSize + originPosition;
    }

    /// <summary>
    /// This gets the x and y position of a grid object using it's world position
    /// </summary>
    /// <param name="worldPosition">This is the world position of the grid object</param>
    /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
    /// <param name="y">This is the number of grid objects above the start grid object</param>
    public void GetXYZ(Vector3 worldPosition, out int x, out int y, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
    }

    /// <summary>
    /// This snaps a position to the grid
    /// </summary>
    /// <param name="worldPosition">This is the world position of the grid object</param>
    /// <returns>Returns the world position snaped to the grid</returns>
    public Vector3 SnapPositionToGrid(Vector3 worldPosition)
    {
        GetXYZ(worldPosition, out int x, out int y, out int z);
        return GetWorldPosition(x, y, z);
    }


    /// <summary>
    /// will return true if the grid cell has a value, else return false
    /// </summary>
    /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
    /// <param name="y">This is the number of grid objects above the start grid object</param>
    /// <returns>true if the grid cell has a value, else false</returns>
    public virtual bool HasValue(int x, int y, int z)
    {
        return false;
    }
    /// <summary>
    /// will return true if the grid cell has a value, else return false
    /// </summary>
    /// <param name="worldPosition">This is the world position of the grid object</param>
    /// <returns>true if the grid cell has a value, else false</returns>
    public virtual bool HasValue(Vector3 worldPosition)
    {
        return false;
    }


    #region Helpers

    protected static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor)
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
