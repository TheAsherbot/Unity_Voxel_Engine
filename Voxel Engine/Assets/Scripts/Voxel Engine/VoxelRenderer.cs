using System.Collections.Generic;

using UnityEngine;

public class VoxelRenderer
{

    private GenericGrid3D<VoxelNode> grid;
    private MeshFilter meshFilter;

    public VoxelRenderer(Material material)
    {
        GameObject gameObject = new GameObject("Voxel Grid Mesh");
        meshFilter = gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>().material = material;

        int size = 16;
        grid = new GenericGrid3D<VoxelNode>(size, size, size, 1, new Vector3(-size / 2, -size / 2, -size / 2), (GenericGrid3D<VoxelNode> grid, int x, int y, int z) => new VoxelNode(grid, x, y, z));
        grid.OnGridValueChanged += Grid_OnValueChanged;
    }


    public GenericGrid3D<VoxelNode> GetGrid()
    {
        return grid;
    }

    public void RenderVoxels()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Voxel Grid Mesh";
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                for (int z = 0; z < grid.GetDepth(); z++)
                {
                    Debug.Log($"{x}, {y}, {z}");
                    AddCube(ref mesh, grid.GetWorldPosition(x, y, z));
                }
            }
        }
        meshFilter.mesh = mesh;
    }




    private void Grid_OnValueChanged(int x, int y, int z)
    {
        RenderVoxels();
    }

    private void AddCube(ref Mesh mesh, Vector3 origin)
    {
        float cellSize = grid.GetCellSize();

        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> triangles = new List<int>();
        
        mesh.GetVertices(vertices);
        mesh.GetUVs(0, uvs);
        mesh.GetTriangles(triangles, 0);

        int startVertexIndex = vertices.Count;

        vertices.AddRange(new List<Vector3>
        {
            // Front 0
            origin + new Vector3(cellSize, 0, cellSize), // 0
            origin + new Vector3(cellSize, cellSize, cellSize), // 1
            origin + new Vector3(0, cellSize, cellSize), // 2
            origin + new Vector3(0, 0, cellSize), // 3
                                
            // Back 4           
            origin + new Vector3(0, 0, 0), // 0
            origin + new Vector3(0, cellSize, 0), // 1
            origin + new Vector3(cellSize, cellSize, 0), // 2
            origin + new Vector3(cellSize, 0, 0), // 3
                                
            // Left 8           
            origin + new Vector3(cellSize, 0, 0), // 0
            origin + new Vector3(cellSize, cellSize, 0), // 1
            origin + new Vector3(cellSize, cellSize, cellSize), // 2
            origin + new Vector3(cellSize, 0, cellSize), // 3
                                
            // Right 12         
            origin + new Vector3(0, 0, cellSize), // 0
            origin + new Vector3(0, cellSize, cellSize), // 1
            origin + new Vector3(0, cellSize, 0), // 2
            origin + new Vector3(0, 0, 0), // 3
                                
            // Top 16           
            origin + new Vector3(0, cellSize, 0), // 0
            origin + new Vector3(0, cellSize, cellSize), // 1
            origin + new Vector3(cellSize, cellSize, cellSize), // 2
            origin + new Vector3(cellSize, cellSize, 0), // 3
                                
            // Bottom 20        
            origin + new Vector3(0, 0, cellSize), // 0
            origin + new Vector3(0, 0, 0), // 1
            origin + new Vector3(cellSize, 0, 0), // 2
            origin + new Vector3(cellSize, 0, cellSize), // 3
        });
        uvs.AddRange(new List<Vector2>
        {
            // Front
            new Vector2(0, 0), // 0
            new Vector2(0, 1), // 1
            new Vector2(1, 1), // 2
            new Vector2(1, 0), // 3

            // Back
            new Vector2(0, 0), // 0
            new Vector2(0, 1), // 1
            new Vector2(1, 1), // 2
            new Vector2(1, 0), // 3

            // Left
            new Vector2(0, 0), // 0
            new Vector2(0, 1), // 1
            new Vector2(1, 1), // 2
            new Vector2(1, 0), // 3

            // Right
            new Vector2(0, 0), // 0
            new Vector2(0, 1), // 1
            new Vector2(1, 1), // 2
            new Vector2(1, 0), // 3

            // Top
            new Vector2(0, 0), // 0
            new Vector2(0, 1), // 1
            new Vector2(1, 1), // 2
            new Vector2(1, 0), // 3

            // Bottom
            new Vector2(0, 0), // 0
            new Vector2(0, 1), // 1
            new Vector2(1, 1), // 2
            new Vector2(1, 0), // 3
        });
        triangles.AddRange(new List<int>
        {
            // Front
            startVertexIndex, startVertexIndex + 1, startVertexIndex + 2,
            startVertexIndex, startVertexIndex + 2, startVertexIndex + 3,

            // Back
            startVertexIndex + 4, startVertexIndex + 5, startVertexIndex + 6,
            startVertexIndex + 4, startVertexIndex + 6, startVertexIndex + 7,

            // Left
            startVertexIndex + 8, startVertexIndex + 9, startVertexIndex + 10,
            startVertexIndex + 8, startVertexIndex + 10,startVertexIndex +  11,

            // Right
            startVertexIndex + 12, startVertexIndex + 13, startVertexIndex + 14,
            startVertexIndex + 12, startVertexIndex + 14, startVertexIndex + 15,

            // Top
            startVertexIndex + 16, startVertexIndex + 17, startVertexIndex + 18,
            startVertexIndex + 16, startVertexIndex + 18, startVertexIndex + 19,

            // Bottom
            startVertexIndex + 20, startVertexIndex + 21, startVertexIndex + 22,
            startVertexIndex + 20, startVertexIndex + 22, startVertexIndex + 23,
        });

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetUVs(0, uvs);

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
    }



}
