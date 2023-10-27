using System.Collections.Generic;

using TheAshBot.Meshes;

using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{

    private VoxelGrid<VoxelNode> grid;


    private void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Debug.Log(mesh.name + " has " + mesh.subMeshCount + " sub meshes!");

        /*grid = new VoxelGrid<VoxelNode>(16, 16, 16, 1, Vector3.zero, (VoxelGrid<VoxelNode> grid, int x, int y, int z) => new VoxelNode(grid, x, y, z));

        List<int> triangles = new List<int>();
        List<Vector3> vertices = new List<Vector3>();

        int vertexIndex = 0;
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                for (int z = 0; z < grid.GetDepth(); z++)
                {
                    if (grid.GetGridObject(x, y, z).type.GetValue_Byte() == 1)
                    {
                        MakeCubeVertices(ref vertices, x, y, z);
                        MakeCubeTriangles(ref triangles, vertexIndex);
                        vertexIndex += 8;
                    }
                }
            }
        }

        Mesh mesh = new Mesh();

        for (int i = 0; i < vertices.Count; i++)
        {
            Debug.Log("Vertex " + i + ": " + vertices[i]);
        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();*/

        mesh = MakeCube2();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void MakeCubeVertices(ref List<Vector3> vertices, int x, int y, int z)
    {
        int cellSize = grid.GetCellSizeInt();

        vertices.Add(grid.GetWorldPosition(x, y, z)); // 0
        vertices.Add(grid.GetWorldPosition(x, y + 1, z)); // 1
        vertices.Add(grid.GetWorldPosition(x + 1, y, z)); // 2
        vertices.Add(grid.GetWorldPosition(x + 1, y + 1, z)); // 3
        vertices.Add(grid.GetWorldPosition(x, y, z + 1)); // 4
        vertices.Add(grid.GetWorldPosition(x, y + 1, z + 1)); // 5
        vertices.Add(grid.GetWorldPosition(x + 1, y, z + 1)); // 6
        vertices.Add(grid.GetWorldPosition(x + 1, y + 1, z + 1)); // 7
    }

    private void MakeCubeTriangles(ref List<int> triangles, int startVertexIndex)
    {
        // Front
        MeshHelper.MakeTriangle(ref triangles, startVertexIndex, startVertexIndex + 1, startVertexIndex + 2);
        MeshHelper.MakeTriangle(ref triangles, startVertexIndex + 1, startVertexIndex + 3, startVertexIndex + 2);

        // Back
        MeshHelper.MakeTriangle(ref triangles, startVertexIndex + 6, startVertexIndex + 5, startVertexIndex + 4);
        MeshHelper.MakeTriangle(ref triangles, startVertexIndex + 6, startVertexIndex + 7, startVertexIndex + 5);

        // Top
        MeshHelper.MakeTriangle(ref triangles, startVertexIndex + 1, startVertexIndex + 5, startVertexIndex + 3);
        MeshHelper.MakeTriangle(ref triangles, startVertexIndex + 5, startVertexIndex + 7, startVertexIndex + 3);

        // Bottom
        MeshHelper.MakeTriangle(ref triangles, startVertexIndex, startVertexIndex + 2, startVertexIndex + 4);
        MeshHelper.MakeTriangle(ref triangles, startVertexIndex + 2, startVertexIndex + 6, startVertexIndex + 4);

        // Right
        MeshHelper.MakeTriangle(ref triangles, startVertexIndex + 2, startVertexIndex + 3, startVertexIndex + 7);
        MeshHelper.MakeTriangle(ref triangles, startVertexIndex + 2, startVertexIndex + 7, startVertexIndex + 6);

        // Left
        MeshHelper.MakeTriangle(ref triangles, startVertexIndex + 0, startVertexIndex + 5, startVertexIndex + 1);
        MeshHelper.MakeTriangle(ref triangles, startVertexIndex + 0, startVertexIndex + 4, startVertexIndex + 5);
    }




    private Mesh MakeCube()
    {
        Mesh mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();

        vertices.Add(new Vector3(0, 0, 0)); // 0
        vertices.Add(new Vector3(0, 1, 0)); // 1
        vertices.Add(new Vector3(1, 0, 0)); // 2
        vertices.Add(new Vector3(1, 1, 0)); // 3
        vertices.Add(new Vector3(0, 0, 1)); // 4
        vertices.Add(new Vector3(0, 1, 1)); // 5
        vertices.Add(new Vector3(1, 0, 1)); // 6
        vertices.Add(new Vector3(1, 1, 1)); // 7

        List<Vector2> uvs = new List<Vector2>();

        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(1, 0));
        uvs.Add(new Vector2(1, 1));

        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(1, 0));
        uvs.Add(new Vector2(1, 1));

        List<int> triangles = new List<int>();

        // Front
        MeshHelper.MakeTriangle(ref triangles, 0, 1, 2);
        MeshHelper.MakeTriangle(ref triangles, 1, 3, 2);

        // Back
        MeshHelper.MakeTriangle(ref triangles, 6, 5, 4);
        MeshHelper.MakeTriangle(ref triangles, 6, 7, 5);

        // Top
        MeshHelper.MakeTriangle(ref triangles, 1, 5, 3);
        MeshHelper.MakeTriangle(ref triangles, 5, 7, 3);

        // Bottom
        MeshHelper.MakeTriangle(ref triangles, 0, 2, 4);
        MeshHelper.MakeTriangle(ref triangles, 2, 6, 4);

        // Right
        MeshHelper.MakeTriangle(ref triangles, 2, 3, 7);
        MeshHelper.MakeTriangle(ref triangles, 2, 7, 6);

        // Left
        MeshHelper.MakeTriangle(ref triangles, 0, 5, 1);
        MeshHelper.MakeTriangle(ref triangles, 0, 4, 5);

        mesh.SetVertices(vertices);
        mesh.triangles = triangles.ToArray();
        mesh.SetUVs(0, uvs);

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();

        return mesh;

    }
    
    private Mesh MakeCube2()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        List<Vector3> vertices = new List<Vector3>();
        mesh.GetVertices(vertices);

        for (int i = 0; i < vertices.Count; i++)
        {
            Debug.Log("Vertices " + i + ": " + vertices[i]);
        }

        List<Vector2> uvs = new List<Vector2>();
        mesh.GetUVs(0, uvs);

        for (int i = 0; i < uvs.Count; i++)
        {
            Debug.Log("uvs " + i + ": " + uvs[i]);
        }

        List<int> triangles = new List<int>();
        mesh.GetTriangles(triangles, 0);

        for (int i = 0; i < triangles.Count; i++)
        {
            Debug.Log("triangles " + i + ": " + triangles[i]);
        }


        // List<Vector3> normals = new List<Vector3>();
        // mesh.GetNormals(normals);


        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetUVs(0, uvs);
        // mesh.SetNormals(normals);

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();

        return mesh;

    }


}
