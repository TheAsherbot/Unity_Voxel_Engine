using TheAshBot.ThreeDimentional;

using UnityEngine;
using UnityEngine.UI;

using QFSW.QC;
using TheAshBot.PixelEngine;

namespace TheAshBot.VoxelEngine
{
    public class VoxelTest : MonoBehaviour
    {

        private GenericGrid3D<VoxelNode> grid;



        private VoxelRenderer voxelRenderer;
        [SerializeField] private RawImage rawImage;


        private void Start()
        {
            #region Grid

            voxelRenderer = new VoxelRenderer(new Vector3(-8, -8, -8));
            grid = voxelRenderer.GetGrid();



            ColorVoxels();

            // PerlinNoise();

            // CheckerBoard();

            Half();

            // Full();




            grid.TriggerGridObjectChanged(0, 0, 0);

            #endregion



            rawImage.texture = voxelRenderer.texture;
        }


        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = Mouse3D.GetMousePosition3D();
                VoxelNode voxelNode = grid.GetGridObject(mousePosition);
                voxelNode.isFilled = false;
                grid.SetGridObject(mousePosition, voxelNode);
            }
        }



        [Command]
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

            grid.TriggerGridObjectChanged(0, 0, 0);
        }

        [Command]
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

            rawImage.texture = voxelRenderer.texture;
            grid.TriggerGridObjectChanged(0, 0, 0);
        }

        [Command]
        private void CheckerBoard()
        {
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    for (int z = 0; z < grid.GetDepth(); z++)
                    {
                        VoxelNode voxelNode = grid.GetGridObject(x, y, z);

                        voxelNode.isFilled = true;

                        if (x % 2 == 0)
                        {
                            voxelNode.isFilled = !voxelNode.isFilled;
                        }
                        if (y % 2 == 0)
                        {
                            voxelNode.isFilled = !voxelNode.isFilled;
                        }
                        if (z % 2 == 0)
                        {
                            voxelNode.isFilled = !voxelNode.isFilled;
                        }

                        grid.SetGridObjectWithoutNotifying(x, y, z, voxelNode);
                    }
                }
            }

            grid.TriggerGridObjectChanged(0, 0, 0);
        }

        [Command]
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

            grid.TriggerGridObjectChanged(0, 0, 0);
        }

        [Command]
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

            grid.TriggerGridObjectChanged(0, 0, 0);
        }

        [Command]
        private void Empty()
        {
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    for (int z = 0; z < grid.GetDepth(); z++)
                    {
                        VoxelNode voxelNode = grid.GetGridObject(x, y, z);
                        voxelNode.isFilled = false;
                        grid.SetGridObjectWithoutNotifying(x, y, z, voxelNode);
                    }
                }
            }

            grid.TriggerGridObjectChanged(0, 0, 0);
        }

    }
}
