using System.Collections.Generic;

using UnityEngine;

public class Test : MonoBehaviour
{

    private GenericGrid3D<VoxelNode> grid;
    [SerializeField] private Transform parent;
    [SerializeField] private Material material;

    private void Start()
    {
        VoxelRenderer voxelRenderer = new VoxelRenderer(new Vector3(0, 0));
        grid = voxelRenderer.GetGrid();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                for (int z = 0; z < grid.GetDepth(); z++)
                {
                    VoxelNode voxelNode = grid.GetGridObject(x, y, z);
                    if (x % 2 == 0 && y % 2 == 0 && z % 2 == 0)
                    {
                        voxelNode.type.SetBit(0, 1);
                        voxelNode.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                    }
                    else
                    {
                        voxelNode.type.SetBit(0, 0);
                    }
                    grid.SetGridObjectWithoutNotifying(x, y, z, voxelNode);
                }
            }
        }

        grid.TriggerGridObjectChanged(0, 0, 0);
    }

/*
    private void Update()
    {
        int size = 20;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        GameObject newGameObject = new GameObject($"{x}, {y}, {z}", typeof(MeshFilter), typeof(MeshRenderer));
                        newGameObject.transform.parent = parent;
                        newGameObject.transform.position = new Vector3(x, y, z);
                        newGameObject.GetComponent<MeshFilter>().mesh = MakeCube();
                        newGameObject.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        GameObject newGameObject = new GameObject($"{x}, {y}, {z}", typeof(MeshFilter), typeof(MeshRenderer));
                        newGameObject.transform.parent = parent;
                        gameObject.transform.position = new Vector3(x, y, z);
                        newGameObject.GetComponent<MeshFilter>().mesh = MakeCube2();
                        newGameObject.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            while (parent.childCount > 0)
            {
                DestroyImmediate(parent.GetChild(0).gameObject);
            }
        }
    }
*/

    private Mesh MakeCube()
    {
        Mesh mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>()
        {
            new Vector3(0, 0, 1), // 0
            new Vector3(0, 1, 1), // 1
            new Vector3(1, 1, 1), // 2
            new Vector3(1, 0, 1), // 3
            new Vector3(0, 0, 0), // 4
            new Vector3(0, 1, 0), // 5
            new Vector3(1, 1, 0), // 6
            new Vector3(1, 0, 0), // 7
        };

        List<Vector2> uvs = new List<Vector2>()
        {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
        };

        List<int> triangles = new List<int>()
        {
            // Front
            3, 2, 1,
            3, 1, 0,

            // Back
            4, 5, 6,
            4, 6, 7,

            // Left
            0, 1, 5,
            0, 5, 4,

            // Right
            7, 6, 2,
            7, 2, 3,

            // Top
            5, 1, 2,
            5, 2, 6,

            // Bottom
            0, 4, 7,
            0, 7, 3,
        };

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        return mesh;

    }
    
    private Mesh MakeCube2()
    {
        Mesh mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>()
        {
            // Front 0
            new Vector3(1, 0, 1), // 0
            new Vector3(1, 1, 1), // 1
            new Vector3(0, 1, 1), // 2
            new Vector3(0, 0, 1), // 3

            // Back 4
            new Vector3(0, 0, 0), // 0
            new Vector3(0, 1, 0), // 1
            new Vector3(1, 1, 0), // 2
            new Vector3(1, 0, 0), // 3

            // Left 8
            new Vector3(1, 0, 0), // 0
            new Vector3(1, 1, 0), // 1
            new Vector3(1, 1, 1), // 2
            new Vector3(1, 0, 1), // 3

            // Right 12
            new Vector3(0, 0, 1), // 0
            new Vector3(0, 1, 1), // 1
            new Vector3(0, 1, 0), // 2
            new Vector3(0, 0, 0), // 3

            // Top 16
            new Vector3(0, 1, 0), // 0
            new Vector3(0, 1, 1), // 1
            new Vector3(1, 1, 1), // 2
            new Vector3(1, 1, 0), // 3

            // Bottom 20
            new Vector3(0, 0, 1), // 0
            new Vector3(0, 0, 0), // 1
            new Vector3(1, 0, 0), // 2
            new Vector3(1, 0, 1), // 3
        };
        List<Vector2> uvs = new List<Vector2>()
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
        };
        List<int> triangles = new List<int>()
        {
            // Front
            0, 1, 2,
            0, 2, 3,

            // Back
            4, 5, 6,
            4, 6, 7,

            // Left
            8, 9, 10,
            8, 10, 11,

            // Right
            12, 13, 14,
            12, 14, 15,

            // Top
            16, 17, 18,
            16, 18, 19,

            // Bottom
            20, 21, 22,
            20, 22, 23,
        };

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetUVs(0, uvs);
        
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();

        return mesh;

    }


}
