using QFSW.QC;

using TheAshBot.TwoDimentional;

using UnityEngine;
using UnityEngine.UI;

namespace TheAshBot.PixelEngine
{
    public class PixelTest : MonoBehaviour
    {

        private GenericGrid2D<PixelNode> grid;



        private PixelRenderer pixelRenderer;
        [SerializeField] private RawImage rawImage;


        private void Start()
        {
            #region Grid

            pixelRenderer = new PixelRenderer(new Vector2(-8, -8));
            grid = pixelRenderer.GetGrid();



            ColorVoxelsToHSVSpectrum();

            // ColorVoxelsRandomly();

            // PerlinNoise();

            // CheckerBoard();

            // Half();

            Full();




            grid.TriggerGridObjectChanged(0, 0);

            #endregion



            rawImage.texture = pixelRenderer.texture;
        }


        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePosition = Mouse2D.GetMousePosition2D();
                PixelNode pixelNode = grid.GetGridObject(mousePosition);
                pixelNode.isFilled = false;
                grid.SetGridObject(mousePosition, pixelNode);
            }
        }



        [Command]
        private void PerlinNoise()
        {
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    bool isFilled = Mathf.RoundToInt(Mathf.PerlinNoise(x / 16f, y / 16f) * 16) > 0.5f ? true : false;

                    PixelNode pixelNode = grid.GetGridObject(x, y);
                    pixelNode.isFilled = isFilled;
                    pixelNode.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                    grid.SetGridObjectWithoutNotifying(x, y, pixelNode);
                }
            }

            grid.TriggerGridObjectChanged(0, 0);
        }

        [Command]
        private void ColorVoxelsToHSVSpectrum()
        {
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    
                    PixelNode pixelNode = grid.GetGridObject(x, y);
                    pixelNode.color = Color.HSVToRGB((float)x / grid.GetWidth(), (float)y / grid.GetHeight(), 1);
                    grid.SetGridObjectWithoutNotifying(x, y, pixelNode);
                }
            }

            rawImage.texture = pixelRenderer.texture;
            grid.TriggerGridObjectChanged(0, 0);
        }

        [Command]
        private void ColorVoxelsRandomly()
        {
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    
                    PixelNode pixelNode = grid.GetGridObject(x, y);
                    pixelNode.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                    grid.SetGridObjectWithoutNotifying(x, y, pixelNode);
                }
            }

            rawImage.texture = pixelRenderer.texture;
            grid.TriggerGridObjectChanged(0, 0);
        }

        [Command]
        private void CheckerBoard()
        {
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    PixelNode pixelNode = grid.GetGridObject(x, y);

                    pixelNode.isFilled = true;

                    if (x % 2 == 0)
                    {
                        pixelNode.isFilled = !pixelNode.isFilled;
                    }
                    if (y % 2 == 0)
                    {
                        pixelNode.isFilled = !pixelNode.isFilled;
                    }

                    grid.SetGridObjectWithoutNotifying(x, y, pixelNode);
                }
            }

            grid.TriggerGridObjectChanged(0, 0);
        }

        [Command]
        private void Half()
        {
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight() / 2; y++)
                {
                    PixelNode pixelNode = grid.GetGridObject(x, y);
                    pixelNode.isFilled = true;
                    grid.SetGridObjectWithoutNotifying(x, y, pixelNode);
                }
            }

            grid.TriggerGridObjectChanged(0, 0);
        }

        [Command]
        private void Full()
        {
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    PixelNode pixelNode = grid.GetGridObject(x, y);
                    pixelNode.isFilled = true;
                    grid.SetGridObjectWithoutNotifying(x, y, pixelNode);
                }
            }

            grid.TriggerGridObjectChanged(0, 0);
        }

        [Command]
        private void Empty()
        {
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    PixelNode pixelNode = grid.GetGridObject(x, y);
                    pixelNode.isFilled = false;
                    grid.SetGridObjectWithoutNotifying(x, y,pixelNode);
                }
            }

            grid.TriggerGridObjectChanged(0, 0);
        }

    }
}
