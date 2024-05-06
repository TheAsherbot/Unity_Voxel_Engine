using System.Collections.Generic;

using UnityEngine;


namespace TheAshBot.PixelEngine
{
    public class PixelRenderer
    {

        private static readonly float UV_3_QUARTER_PIXEL_OFFSET = 0.015f;
        private static readonly float UV_QUARTER_PIXEL_OFFSET = 0.005f;

        private PixelChunk pixelChunk;
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private MeshCollider meshCollider;

        private List<Vector3> vertices;
        private List<Vector2> uvs;
        private List<int> triangles;


        public Texture2D texture;
        private short textureIndex;


        public PixelRenderer(Vector2 origin)
        {
            Material material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));

            GameObject gameObject = new GameObject("Pixel Grid Mesh");
            meshFilter = gameObject.AddComponent<MeshFilter>();
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshCollider = gameObject.AddComponent<MeshCollider>();

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

        public void RenderPixels()
        {
            textureIndex = 0;
            vertices = new List<Vector3>();
            uvs = new List<Vector2>();
            triangles = new List<int>();
            texture = new Texture2D(50, 50);


            Mesh mesh = new Mesh();
            mesh.name = "Pixel Grid Mesh";
            for (byte x = 0; x < pixelChunk.GetGrid().GetWidth(); x++)
            {
                for (byte y = 0; y < pixelChunk.GetGrid().GetHeight(); y++)
                {
                    if (IsFilled(x, y) == false)
                    {
                        // Empty
                        continue;
                    }

                    AddSquare(pixelChunk.GetGrid().GetWorldPosition(x, y), pixelChunk.GetGrid().GetGridObject(x, y).color);
                }
            }

            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.SetUVs(0, uvs);


            Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            texture.filterMode = FilterMode.Point;
            texture.Apply();
            material.mainTexture = texture;
            material.color = Color.white;
            meshRenderer.material = material;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mesh.RecalculateTangents();

            meshFilter.sharedMesh = mesh;
            meshCollider.sharedMesh = mesh;
        }



        private void Grid_OnValueChanged(int x, int y)
        {
            RenderPixels();
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
            Vector2 origin = new Vector2(Mathf.FloorToInt(textureIndex / 50f), textureIndex % 50);

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
            Vector2 origin = new Vector2(Mathf.FloorToInt(textureIndex / 50f), textureIndex % 50);

            uvs.AddRange(new List<Vector2>
            {
                origin / 50f + new Vector2(UV_QUARTER_PIXEL_OFFSET, UV_QUARTER_PIXEL_OFFSET), // 0
                origin / 50f + new Vector2(UV_QUARTER_PIXEL_OFFSET, UV_3_QUARTER_PIXEL_OFFSET), // 1
                origin / 50f + new Vector2(UV_3_QUARTER_PIXEL_OFFSET, UV_3_QUARTER_PIXEL_OFFSET), // 2
                origin / 50f + new Vector2(UV_3_QUARTER_PIXEL_OFFSET, UV_QUARTER_PIXEL_OFFSET), // 3
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
        }/*

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
