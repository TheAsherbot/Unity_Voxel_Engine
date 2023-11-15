using TheAshBot.ThreeDimentional;

using Unity.VisualScripting.Dependencies.NCalc;

using UnityEngine;

namespace TheAshBot.VoxelEngine
{
    public class VoxelTest : MonoBehaviour
    {

        private GenericGrid3D<VoxelNode> grid;



        [SerializeField] private Texture texture;


        private void Start()
        {



            #region Grid

            VoxelRenderer voxelRenderer = new VoxelRenderer(new Vector3(-8, -8, -8));
            grid = voxelRenderer.GetGrid();



            ColorVoxels();

            // PerlinNoise();

            CheckerBoard();

            // Half();

            // Full();




            grid.TriggerGridObjectChanged(0, 0, 0);

            #endregion
            texture = voxelRenderer.texture;
        }


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Mouse3D.GetMousePosition3D();
                VoxelNode voxelNode = grid.GetGridObject(mousePosition);
                voxelNode.isFilled = false;
                grid.SetGridObject(mousePosition, voxelNode);
                Mouse3D.DebugLogMousePosition3D();
            }
        }



        private void PerlinNoise()
        {
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
        }

        private void ColorVoxels()
        {
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    for (int z = 0; z < grid.GetDepth(); z++)
                    {
                        VoxelNode voxelNode = grid.GetGridObject(x, y, z);
                        voxelNode.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                        grid.SetGridObjectWithoutNotifying(x, y, z, voxelNode);
                    }
                }
            }
        }

        private void CheckerBoard()
        {
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
                            }
                        }
                        else
                        {
                            if ((bits.GetBit(0) == 1 && bits.GetBit(1) == 1) || (bits.GetBit(0) == 0 && bits.GetBit(1) == 0))
                            {
                                voxelNode.isFilled = true;
                            }
                        }

                        grid.SetGridObjectWithoutNotifying(x, y, z, voxelNode);
                    }
                }
            }
        }

        private void Half()
        {
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight() / 2; y++)
                {
                    for (int z = 0; z < grid.GetDepth(); z++)
                    {
                        VoxelNode voxelNode = grid.GetGridObject(x, y, z);
                        voxelNode.isFilled = true;
                        grid.SetGridObjectWithoutNotifying(x, y, z, voxelNode);
                    }
                }
            }
        }

        private void Full()
        {
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    for (int z = 0; z < grid.GetDepth(); z++)
                    {
                        VoxelNode voxelNode = grid.GetGridObject(x, y, z);
                        voxelNode.isFilled = true;
                        grid.SetGridObjectWithoutNotifying(x, y, z, voxelNode);
                    }
                }
            }
        }


    }
}
