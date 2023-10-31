using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

public class VoxelRenderer
{

    private GenericGrid3D<VoxelNode> grid;
    private MeshFilter meshFilter;

    public VoxelRenderer(Material material, Vector3 origin)
    {
        GameObject gameObject = new GameObject("Voxel Grid Mesh");
        meshFilter = gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>().material = material;

        int size = 16;
        grid = new GenericGrid3D<VoxelNode>(size, size, size, 1, origin + new Vector3(-size / 2, -size / 2, -size / 2), (GenericGrid3D<VoxelNode> grid, int x, int y, int z) => new VoxelNode(grid, x, y, z));
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
                    BitArray neighbors = new BitArray(6);
                    // Front
                    if (GetVoxelType(x, y, z + 1) == 1)
                    {
                        neighbors.SetBit(0, 1);
                    }
                    // Back
                    if (GetVoxelType(x, y, z - 1) == 1)
                    {
                        neighbors.SetBit(1, 1);
                    }
                    // Left
                    if (GetVoxelType(x - 1, y, z) == 1)
                    {
                        neighbors.SetBit(2, 1);
                    }
                    // Right
                    if (GetVoxelType(x + 1, y, z) == 1)
                    {
                        neighbors.SetBit(3, 1);
                    }
                    // Top
                    if (GetVoxelType(x, y + 1, z) == 1)
                    {
                        neighbors.SetBit(4, 1);
                    }
                    // Bottom
                    if (GetVoxelType(x, y - 1, z) == 1)
                    {
                        neighbors.SetBit(5, 1);
                    }


                    AddCube(ref mesh, grid.GetWorldPosition(x, y, z), neighbors);
                }
            }
        }
        meshFilter.mesh = mesh;
    }




    private void Grid_OnValueChanged(int x, int y, int z)
    {
        RenderVoxels();
    }


    /// <summary>
    /// This will add values for a cube onto a mesh.
    /// </summary>
    /// <param name="mesh">is the mash that the cube is being added to</param>
    /// <param name="origin">Is the bottom left back corner of the cube relative to the position of the GamObject with the mesh renderer.</param>
    /// <param name="neighbors">Needs 6 bits. 0 = Render Face. 1 = Do not render facer. index: 0 = Front, 1 = Back, 2 = Left, 4 = Right, 5 = Top, 6 = Bottom</param>
    private void AddCube(ref Mesh mesh, Vector3 origin, BitArray neighbors)
    {
        if (neighbors.GetValue_Byte() >= 63)
        {
            return;
        }

        float cellSize = grid.GetCellSize();

        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> triangles = new List<int>();

        mesh.GetVertices(vertices);
        mesh.GetUVs(0, uvs);
        mesh.GetTriangles(triangles, 0);

        int startVertexIndex = vertices.Count;

        // Front
        if (neighbors.GetBit(0) == 0)
        {
            vertices.AddRange(new List<Vector3>
            {
                origin + new Vector3(cellSize, 0, cellSize), // 0
                origin + new Vector3(cellSize, cellSize, cellSize), // 1
                origin + new Vector3(0, cellSize, cellSize), // 2
                origin + new Vector3(0, 0, cellSize), // 3
            });
            AddUV(ref uvs);
            AddTriangles(ref triangles, startVertexIndex);
            startVertexIndex += 4;
        }
        // Back
        if (neighbors.GetBit(1) == 0)
        {
            vertices.AddRange(new List<Vector3>
            {
                origin + new Vector3(0, 0, 0), // 0
                origin + new Vector3(0, cellSize, 0), // 1
                origin + new Vector3(cellSize, cellSize, 0), // 2
                origin + new Vector3(cellSize, 0, 0), // 3
            });
            AddUV(ref uvs);
            AddTriangles(ref triangles, startVertexIndex);
            startVertexIndex += 4;
        }
        // Left
        if (neighbors.GetBit(2) == 0)
        {
            vertices.AddRange(new List<Vector3>
            {
                origin + new Vector3(0, 0, cellSize), // 0
                origin + new Vector3(0, cellSize, cellSize), // 1
                origin + new Vector3(0, cellSize, 0), // 2
                origin + new Vector3(0, 0, 0), // 3
            });
            AddUV(ref uvs);
            AddTriangles(ref triangles, startVertexIndex);
            startVertexIndex += 4;
        }
        // Right
        if (neighbors.GetBit(3) == 0)
        {
            vertices.AddRange(new List<Vector3>
            {
                origin + new Vector3(cellSize, 0, 0), // 0
                origin + new Vector3(cellSize, cellSize, 0), // 1
                origin + new Vector3(cellSize, cellSize, cellSize), // 2
                origin + new Vector3(cellSize, 0, cellSize), // 3
            });
            AddUV(ref uvs);
            AddTriangles(ref triangles, startVertexIndex);
            startVertexIndex += 4;
        }
        // Top
        if (neighbors.GetBit(4) == 0)
        {
            vertices.AddRange(new List<Vector3>
            {
                origin + new Vector3(0, cellSize, 0), // 0
                origin + new Vector3(0, cellSize, cellSize), // 1
                origin + new Vector3(cellSize, cellSize, cellSize), // 2
                origin + new Vector3(cellSize, cellSize, 0), // 3
            });
            AddUV(ref uvs);
            AddTriangles(ref triangles, startVertexIndex);
            startVertexIndex += 4;
        }
        // Bottom
        if (neighbors.GetBit(5) == 0)
        {
            vertices.AddRange(new List<Vector3>
            {
                origin + new Vector3(0, 0, cellSize), // 0
                origin + new Vector3(0, 0, 0), // 1
                origin + new Vector3(cellSize, 0, 0), // 2
                origin + new Vector3(cellSize, 0, cellSize), // 3
            });
            AddUV(ref uvs);
            AddTriangles(ref triangles, startVertexIndex);
            startVertexIndex += 4;
        }

        mesh.SetVertices(vertices);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(triangles, 0);

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
    }


    private void AddUV(ref List<Vector2> uvs)
    {
        uvs.AddRange(new List<Vector2>
        {
            new Vector2(0, 0), // 0
            new Vector2(0, 1), // 1
            new Vector2(1, 1), // 2
            new Vector2(1, 0), // 3
        });
    }

    private void AddTriangles(ref List<int> triangles, int startVertexIndex)
    {
        triangles.AddRange(new List<int>
        {
            startVertexIndex, startVertexIndex + 1, startVertexIndex + 2,
            startVertexIndex, startVertexIndex + 2, startVertexIndex + 3,
        });
    }

    private byte GetVoxelType(int x, int y, int z)
    {
        if (x >= 0 && x < grid.GetWidth() &&
            y >= 0 && y < grid.GetHeight() &&
            z >= 0 && z < grid.GetDepth())
        {
            return grid.GetGridObject(x, y, z).type.GetValue_Byte();
        }

        return 0;
    }


}
