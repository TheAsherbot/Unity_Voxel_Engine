using System.Collections.Generic;

using UnityEngine;


namespace TheAshBot.PixelEngine
{
    public class PixelRenderer
    {


        private class DummyClass : MonoBehaviour
        {
            public delegate void OnBeforeRenderDelegate();
            public OnBeforeRenderDelegate OnBeforeRender;

            private void Update()
            {
                OnBeforeRender?.Invoke();
            }
        }



        private static float UV_TOP_RIGHT_OFFSET_AMOUNT = 0.045f;
        private static float UV_BOTTOM_LEFT_OFFSET_AMOUNT = 0.015f;

        private PixelChunk pixelChunk;
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private MeshCollider meshCollider;

        private List<Vector3> vertices;
        private List<Vector2> uvs;
        private List<int> triangles;


        public Texture2D texture;
        private short textureIndex;

        private Material material;
        private Mesh mesh;
        private Matrix4x4 transformRotationScaleMatrix;
        private RenderParams renderParams;


        public PixelRenderer(Vector2 origin)
        {
            // Setup Rendering Stuff
            material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            transformRotationScaleMatrix = new Matrix4x4();
            mesh = new Mesh();
            mesh.name = "Pixel Grid Mesh";
            renderParams = new RenderParams(material);

            // Setup Dummy Object
            GameObject gameObject = new GameObject("Pixel Grid Mesh");
            gameObject.AddComponent<DummyClass>().OnBeforeRender += OnBeforeRender;
            meshFilter = gameObject.AddComponent<MeshFilter>();
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshCollider = gameObject.AddComponent<MeshCollider>();

            // Setup Chunck
            pixelChunk = new PixelChunk(1, origin);
            pixelChunk.GetGrid().OnGridValueChanged += Grid_OnValueChanged;

            for (int x = 0; x < GetGrid().GetWidth(); x++)
            {
                for (int y = 0; y < GetGrid().GetHeight(); y++)
                {
                    GetGrid().GetGridObject(x, y).UpdateNeighbors();
                }
            }
        }


        public GenericGrid2D<PixelNode> GetGrid()
        {
            return pixelChunk.GetGrid();
        }

        public void UpdatePixels()
        {
            GenericGrid2D<PixelNode> grid = pixelChunk.GetGrid();


            textureIndex = 0;
            vertices = new List<Vector3>();
            uvs = new List<Vector2>();
            triangles = new List<int>();

            texture = new Texture2D(grid.GetWidth(), grid.GetHeight());


            for (byte x = 0; x < grid.GetWidth(); x++)
            {
                for (byte y = 0; y < grid.GetHeight(); y++)
                {
                    if (IsFilled(x, y) == false)
                    {
                        // Empty
                        continue;
                    }

                    AddSquare(grid.GetWorldPosition(x, y), grid.GetGridObject(x, y).color);
                }
            }

            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.SetUVs(0, uvs);


            texture.filterMode = FilterMode.Point;
            texture.Apply();
            material.mainTexture = texture;
            material.color = Color.white;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mesh.RecalculateTangents();

            // meshRenderer.material = material;
            // meshFilter.sharedMesh = mesh;
            // meshCollider.sharedMesh = mesh;
            
            renderParams.material = material;

            transformRotationScaleMatrix.SetTRS(grid.GetOriginPosition() + new Vector2(grid.GetWidth() / 2, grid.GetHeight() / 2), Quaternion.identity, Vector3.one);

        }

        public void OnBeforeRender()
        {
            Graphics.RenderMesh(renderParams, mesh, 0, transformRotationScaleMatrix);
        }


        private void Grid_OnValueChanged(int x, int y)
        {
            UpdatePixels();
        }

        /// <summary>
        /// This will add values for a cube onto a mesh.
        /// </summary>
        /// <param name="origin">Is the bottom left back corner of the square relative to the position of the GamObject with the mesh renderer.</param>
        private void AddSquare(Vector2 origin, Color32 color)
        {
            float cellSize = pixelChunk.GetGrid().GetCellSize();


            short uvIndex = AddColorToTexture(color);

            // Front
            vertices.AddRange(new List<Vector3>
            {
                origin + new Vector2(0, 0), // 0
                origin + new Vector2(0, cellSize), // 1
                origin + new Vector2(cellSize, cellSize), // 2
                origin + new Vector2(cellSize, 0), // 3
            });
            AddUV(uvIndex);
            AddTriangles(ref triangles, vertices.Count - 4);
            
        }

        /// <summary>
        /// Adds a color to the UV Texture.
        /// </summary>
        /// <param name="color">The color that is added to the UV texture.</param>
        /// <returns>Texture index for the color for later use.</returns>
        private short AddColorToTexture(Color32 color)
        {
            Vector2 origin = new Vector2(Mathf.FloorToInt(textureIndex / pixelChunk.GetGrid().GetHeight()), textureIndex % pixelChunk.GetGrid().GetHeight());

            texture.SetPixel((int)origin.x, (int)origin.y, color);

            textureIndex++;

            return (short)(textureIndex - 1);
        }
        
        /// <summary>
        /// Get UV for a color.
        /// </summary>
        /// <param name="color">The color that is added to the UV texture.</param>
        /// <returns>UV index for the color for later use.</returns>
        private void AddUV(short textureIndex)
        {
            Vector2 origin = new Vector2(Mathf.FloorToInt(textureIndex / pixelChunk.GetGrid().GetHeight()), textureIndex % pixelChunk.GetGrid().GetHeight());

            uvs.AddRange(new List<Vector2>
            {
                origin / pixelChunk.GetGrid().GetHeight() + new Vector2(UV_BOTTOM_LEFT_OFFSET_AMOUNT, UV_BOTTOM_LEFT_OFFSET_AMOUNT), // 0
                origin / pixelChunk.GetGrid().GetHeight() + new Vector2(UV_BOTTOM_LEFT_OFFSET_AMOUNT, UV_TOP_RIGHT_OFFSET_AMOUNT), // 1
                origin / pixelChunk.GetGrid().GetHeight() + new Vector2(UV_TOP_RIGHT_OFFSET_AMOUNT, UV_TOP_RIGHT_OFFSET_AMOUNT), // 2
                origin / pixelChunk.GetGrid().GetHeight() + new Vector2(UV_TOP_RIGHT_OFFSET_AMOUNT, UV_BOTTOM_LEFT_OFFSET_AMOUNT), // 3
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

        private bool IsFilled(int x, int y)
        {
            if (x >= 0 && x < pixelChunk.GetGrid().GetWidth() &&
                y >= 0 && y < pixelChunk.GetGrid().GetHeight())
            {
                return pixelChunk.GetGrid().GetGridObject(x, y).isFilled;
            }

            return false;
        }
        
        /*

        private Texture2D GetTexture()
        {
            byte size = 50;

            for (byte x = 0; x < size; x++)
            {
                for (byte y = 0; y < size; y++)
                {
                    texture.SetPixel(x, y, textureColorList[x * 50 + y]);
                }
            }

            return texture;
        }*/


    }
}
