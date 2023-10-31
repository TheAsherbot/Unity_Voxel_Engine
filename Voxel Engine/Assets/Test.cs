using System.Collections.Generic;

using UnityEngine;

using TheAshBot.VoxelEngine;

public class Test : MonoBehaviour
{

    private GenericGrid3D<VoxelNode> grid;
    [SerializeField] private Transform parent;

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


}
