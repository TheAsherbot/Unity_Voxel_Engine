using UnityEngine;

using System.Collections;
using TheAshBot.ThreeDimentional;

namespace TheAshBot.VoxelEngine
{
    public class VoxelTest : MonoBehaviour
    {
/*
        private GenericGrid3D<VoxelNode> grid;
        
        private void Start()
        {
            #region Grid

            VoxelRenderer voxelRenderer = new VoxelRenderer(new Vector3(-8, -8, -8));
            grid = voxelRenderer.GetGrid();

            // Every cell

            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int z = 0; z < grid.GetDepth(); z++)
                {
                    int height = Mathf.RoundToInt(Mathf.PerlinNoise(x / 16f, z / 16f) * 16);
                    for (int y = 0; y < height; y++)
                    {
                        VoxelNode voxelNode = grid.GetGridObject(x, y, z);
                        voxelNode.isFilled = true;
                        voxelNode.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                        grid.SetGridObjectWithoutNotifying(x, y, z, voxelNode);
                    }
                }
            }





            // Every other.

            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    for (int z = 0; z < grid.GetDepth(); z++)
                    {
                        VoxelNode voxelNode = grid.GetGridObject(x, y, z);

                        BitArray bits = new BitArray(2);
                        if (x % 2 == 0)
                        {
                            bits.SetBit(0, 1);
                        }
                        if (y % 2 == 0)
                        {
                            bits.SetBit(1, 1);
                        }
                        if (z % 2 == 0)
                        {
                            if ((bits.GetBit(0) == 0 && bits.GetBit(1) == 1) || (bits.GetBit(0) == 1 && bits.GetBit(1) == 0))
                            {
                                voxelNode.isFilled = true;
                                voxelNode.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                            }
                        }
                        else
                        {
                            if ((bits.GetBit(0) == 1 && bits.GetBit(1) == 1) || (bits.GetBit(0) == 0 && bits.GetBit(1) == 0))
                            {
                                voxelNode.isFilled = true;
                                voxelNode.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                            }
                        }

                        grid.SetGridObjectWithoutNotifying(x, y, z, voxelNode);
                    }
                }
            }



            grid.TriggerGridObjectChanged(0, 0, 0);

            #endregion

        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Debug.Log(grid.GetGridObject(new Vector3(1, 0, 1)).neighbors[0].isFilled);
                VoxelNode voxel = grid.GetGridObject(new Vector3 (1, 1, 1));
                voxel.isFilled = false;
                grid.SetGridObject(new Vector3(1, 1, 1), voxel);
                Debug.Log(grid.GetGridObject(new Vector3(1, 0, 1)).neighbors[0].isFilled);
            }
        }
*/
    }
}
