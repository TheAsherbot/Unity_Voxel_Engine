using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelGrid<TGridObject> : Grid3D
{


    private TGridObject[,,] gridArray;


    /// <summary>
    /// This makes a grid that each cell holds a generic value
    /// </summary>
    /// <param name="width">This is the width of the grid</param>
    /// <param name="height">THis is the height of the grid</param>
    /// <param name="cellSize">This is how big the grid objects are</param>
    /// <param name="originPosition">This is the position of the bottom left grid object(AKA the origin)</param>
    /// <param name="createGridObject">This is the the value that all the gid object will default to <code>(GenericGrid grid, int x, int y, int z) => new TGridObject(grid, x, y int z)</code></param>
    /// <param name="showDebug">If this is true the it will show the lines of the grid</param>
    /// <param name="parent">This is the parent object of the text(This is only needed if show debug is true)</param>
    public VoxelGrid(int width, int height, int depth, float cellSize, Vector2 originPosition, Func<VoxelGrid<TGridObject>, int, int, int, TGridObject> createGridObject, bool showDebug, Transform parent, int baseFontSize = 20)
                : base(width, height, depth, cellSize, originPosition, showDebug, parent)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height, depth];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                for (int z = 0; z < gridArray.GetLength(2); z++)
                {
                    gridArray[x, y, z] = createGridObject(this, x, y, z);
                }
            }
        }

        if (showDebug)
        {
            TextMesh[,,] debugTextArray = new TextMesh[width, height, depth];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        string nodeToString = gridArray[x, y, z].ToString();
                        debugTextArray[x, y, z] = CreateWorldText(parent, nodeToString, GetWorldPosition(x, y, z) + new Vector3(cellSize, cellSize, cellSize) * .5f,
                            baseFontSize * Mathf.RoundToInt(cellSize), Color.white, TextAnchor.MiddleCenter);
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

            OnGridValueChanged += (int x, int y, int z) =>
            {
                debugTextArray[x, y, z].text = gridArray[x, y, z].ToString();
            };
        }
    }
    /// <summary>
    /// This makes a grid that each cell holds a generic value
    /// </summary>
    /// <param name="width">This is the width of the grid</param>
    /// <param name="height">THis is the height of the grid</param>
    /// <param name="cellSize">This is how big the grid objects are</param>
    /// <param name="originPosition">This is the position of the bottom left grid object(AKA the origin)</param>
    /// <param name="createGridObject">This is the the value that all the gid object will default to<code>(GenericGrid grid, int x, int y, int z) => new TGridObject(grid, x, y, z)</code></param>
    public VoxelGrid(int width, int height, int depth, float cellSize, Vector2 originPosition, Func<VoxelGrid<TGridObject>, int, int, int, TGridObject> createGridObject)
                : base(width, height, depth, cellSize, originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height, depth];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                for (int z = 0; z < gridArray.GetLength(2); z++)
                {
                    gridArray[x, y, z] = createGridObject(this, x, y, z);
                }
            }
        }
    }


    /// <summary>
    /// THis sets the value of a cell using it's 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="value"></param>
    public void SetGridObject(int x, int y, int z, TGridObject value)
    {
        if (x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth)
        {
            if (value == null)
            {
                value = default;
            }

            gridArray[x, y, z] = value;

            TriggerGridObjectChanged(x, y, z);
        }
    }
    /// <summary>
    /// This sets the value of a cell using it's world position
    /// </summary>
    /// <param name="worldPosition">This is the grid objects world position<</param>
    /// <param name="value">This is the value it is being set to</param>
    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        GetXYZ(worldPosition, out int x, out int y, out int z);
        SetGridObject(x, y, z, value);
    }

    /// <summary>
    /// This gets the value of a cell using it's position on the grid
    /// </summary>
    /// <param name="x">This is the number of grid objects to the right of the start grid object</param>
    /// <param name="y">This is the number of grid objects above the start grid object</param>
    /// <returns>Returns the grid object</returns>
    public TGridObject GetGridObject(int x, int y, int z)
    {
        if (x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth)
        {
            return gridArray[x, y, z];
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
    /// <returns>This returns the grid object</returns>
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        GetXYZ(worldPosition, out int x, out int y, out int z);
        return GetGridObject(x, y, z);
    }

    public void TriggerGridObjectChanged(int x, int y, int z)
    {
        OnGridValueChanged?.Invoke(x, y, z);
    }

}
