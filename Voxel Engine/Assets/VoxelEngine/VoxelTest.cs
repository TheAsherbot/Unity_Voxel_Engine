using UnityEngine;

using System.Collections;

namespace TheAshBot.VoxelEngine
{
    public class VoxelTest : MonoBehaviour
    {

        private GenericGrid3D<VoxelNode> grid;
        
        private void Start()
        {
            #region Grid

            VoxelRenderer voxelRenderer = new VoxelRenderer(new Vector3(-8, -8, -8));
            grid = voxelRenderer.GetGrid();

            // Every cell

            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    for (int z = 0; z < grid.GetHeight(); z++)
                    {
                        VoxelNode voxelNode = grid.GetGridObject(x, y, z);
                        voxelNode.isFilled = true;
                        voxelNode.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                        grid.SetGridObjectWithoutNotifying(x, y, z, voxelNode);
                    }
                }
            }





            // Every other.
/*
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    for (int z = 0; z < grid.GetDepth(); z++)
                    {
                        VoxelNode voxelNode = grid.GetGridObject(x, y, z);

                        BitArray bits = new BitArray(3);
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
                                voxelNode.isFilled = false;
                            }
                        }
                        else
                        {
                            if ((bits.GetBit(0) == 1 && bits.GetBit(1) == 1) || (bits.GetBit(0) == 0 && bits.GetBit(1) == 0))
                            {
                                voxelNode.isFilled = false;
                            }
                        }

                        grid.SetGridObjectWithoutNotifying(x, y, z, voxelNode);
                    }
                }
            }
*/



            VoxelNode newVoxelNode = grid.GetGridObject(1, 5, 0);
            newVoxelNode.isFilled = true;
            newVoxelNode.color = Color.HSVToRGB(1, 0.5f, 1);
            grid.SetGridObjectWithoutNotifying(1, 5, 0, newVoxelNode);


            grid.TriggerGridObjectChanged(0, 0, 0);

            #endregion

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(CacheVoxelNeighbors());
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                StartCoroutine(EmptyGrid());
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(LogNeighbors());
            }
        }

        private IEnumerator CacheVoxelNeighbors()
        {
            int startTime = (System.DateTime.Now.Second * 1000) + System.DateTime.Now.Millisecond;

            yield return null;

            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    for (int z = 0; z < grid.GetDepth(); z++)
                    {
                        grid.GetGridObject(x, y, z).UpdateNeighbors();
                    }
                }
            }

            int endTime = (System.DateTime.Now.Second * 1000) + System.DateTime.Now.Millisecond;

            Debug.Log("CacheVoxelNeighbors: " + (endTime - startTime));
        }

        private IEnumerator EmptyGrid()
        {
            int startTime = (System.DateTime.Now.Second * 1000) + System.DateTime.Now.Millisecond;

            yield return null;

            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    for (int z = 0; z < grid.GetDepth(); z++)
                    {
                        grid.SetGridObjectWithoutNotifying(x, y, z, new VoxelNode());
                    }
                }
            }

            grid.TriggerGridObjectChanged(0, 0, 0);

            int endTime = (System.DateTime.Now.Second * 1000) + System.DateTime.Now.Millisecond;

            Debug.Log("EmptyGrid: " + (endTime - startTime));
        }

        private IEnumerator LogNeighbors()
        {
            int startTime = (System.DateTime.Now.Second * 1000) + System.DateTime.Now.Millisecond;

            yield return null;

            for (byte x = 0; x < grid.GetWidth(); x++)
            {
                for (byte y = 0; y < grid.GetHeight(); y++)
                {
                    for (byte z = 0; z < grid.GetDepth(); z++)
                    {
                        BitArray bitArray = new BitArray(6);
                        for (byte i = 0; i < 6; i++)
                        {
                            Vector3Int j = new Vector3Int(x + (i == 4 ? 1 : i == 5 ? -1 : 0), y + (i == 0 ? 1 : i == 1 ? -1 : 0), z + (i == 2 ? 1 : i == 3 ? -1 : 0));
                            bitArray.SetBit(i, grid.GetGridObject(x, y, z).neighbors[i].isFilled);
                            Debug.Log(j + ": " + grid.GetGridObject(x, y, z).neighbors[i].isFilled);
                        }
                        Debug.Log(bitArray.GetValue_Byte());
                    }
                }
            }

            int endTime = (System.DateTime.Now.Second * 1000) + System.DateTime.Now.Millisecond;

            Debug.Log("LogNeighbors: " + (endTime - startTime));
        }

    }
}
