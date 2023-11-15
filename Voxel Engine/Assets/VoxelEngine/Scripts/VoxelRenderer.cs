using System.Collections.Generic;

using UnityEngine;


namespace TheAshBot.VoxelEngine
{
    public class VoxelRenderer
    {

        private static readonly float UV_3_QUARTER_PIXEL_OFFSET = 0.015f;
        private static readonly float UV_QUARTER_PIXEL_OFFSET = 0.005f;

        private static BitArray FRONT_FACE
        {
            get
            {
                return new BitArray(3);
            }
        }
        private static BitArray BACK_FACE
        {
            get
            {
                BitArray bitArray = new BitArray(3);
                bitArray.SetBit(0, 1);
                return bitArray;
            }
        }
        private static BitArray LEFT_FACE
        {
            get
            {
                BitArray bitArray = new BitArray(3);
                bitArray.SetBit(1, 1);
                return bitArray;
            }
        }
        private static BitArray RIGHT_FACE
        {
            get
            {
                BitArray bitArray = new BitArray(3);
                bitArray.SetBit(0, 1);
                bitArray.SetBit(1, 1);
                return bitArray;
            }
        }
        private static BitArray TOP_FACE
        {
            get
            {
                BitArray bitArray = new BitArray(3);
                bitArray.SetBit(2, 1);
                return bitArray;
            }
        }
        private static BitArray BOTTOM_FACE
        {
            get
            {
                BitArray bitArray = new BitArray(3);
                bitArray.SetBit(0, 1);
                bitArray.SetBit(2, 1);
                return bitArray;
            }
        }


        private VoxelChunk voxelChunk;
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private MeshCollider meshCollider;

        private List<Vector3> vertices;
        private List<Vector2> uvs;
        private List<int> triangles;


        public Texture2D texture;
        private short textureIndex;


        public VoxelRenderer(Vector3 origin)
        {
            Material material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));

            GameObject gameObject = new GameObject("Voxel Grid Mesh");
            meshFilter = gameObject.AddComponent<MeshFilter>();
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshCollider = gameObject.AddComponent<MeshCollider>();

            voxelChunk = new VoxelChunk(1, origin);
            voxelChunk.GetGrid().OnGridValueChanged += Grid_OnValueChanged;

            for (int x = 0; x < GetGrid().GetWidth(); x++)
            {
                for (int y = 0; y < GetGrid().GetHeight(); y++)
                {
                    for (int z = 0; z < GetGrid().GetDepth(); z++)
                    {
                        GetGrid().GetGridObject(x, y, z).UpdateNeighbors();
                    }
                }
            }
        }


        public GenericGrid3D<VoxelNode> GetGrid()
        {
            return voxelChunk.GetGrid();
        }

        public void RenderVoxels()
        {
            textureIndex = 0;
            vertices = new List<Vector3>();
            uvs = new List<Vector2>();
            triangles = new List<int>();
            texture = new Texture2D(50, 50);


            Mesh mesh = new Mesh();
            mesh.name = "Voxel Grid Mesh";
            for (byte x = 0; x < voxelChunk.GetGrid().GetWidth(); x++)
            {
                for (byte y = 0; y < voxelChunk.GetGrid().GetHeight(); y++)
                {
                    for (byte z = 0; z < voxelChunk.GetGrid().GetDepth(); z++)
                    {
                        if (IsFilled(x, y, z) == false)
                        {
                            // Empty
                            continue;
                        }

                        BitArray neighbors = new BitArray(6);

                        // Front
                        if (!ShouldRenderFace(x, y, z, FRONT_FACE))
                        {
                            neighbors.SetBit(0, 1);
                        }
                        // Back
                        if (!ShouldRenderFace(x, y, z, BACK_FACE))
                        {
                            neighbors.SetBit(1, 1);
                        }
                        // Left
                        if (!ShouldRenderFace(x, y, z, LEFT_FACE))
                        {
                            neighbors.SetBit(2, 1);
                        }
                        // Right
                        if (!ShouldRenderFace(x, y, z, RIGHT_FACE))
                        {
                            neighbors.SetBit(3, 1);
                        }
                        // Top
                        if (!ShouldRenderFace(x, y, z, TOP_FACE))
                        {
                            neighbors.SetBit(4, 1);
                        }
                        // Bottom
                        if (!ShouldRenderFace(x, y, z, BOTTOM_FACE))
                        {
                            neighbors.SetBit(5, 1);
                        }

                        AddCube(voxelChunk.GetGrid().GetWorldPosition(x, y, z), neighbors, voxelChunk.GetGrid().GetGridObject(x, y, z).color);
                    }
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



        private void Grid_OnValueChanged(int x, int y, int z)
        {
            RenderVoxels();
        }

        /// <summary>
        /// will test to see if a face of a voxel should be rendered
        /// </summary>
        /// <param name="x">x position of the voxel on the voxel grid</param>
        /// <param name="y">y position of the voxel on the voxel grid</param>
        /// <param name="z">z position of the voxel on the voxel grid</param>
        /// <param name="face">is a bit array with 3 bits. 0 = front; 1 = back; 2 = left; 3 = right; 4 = top; 5 = bottom;</param>
        /// <param name="cameraForward">Is the cameras forward direction</param>
        private bool ShouldRenderFace(byte x, byte y, byte z, BitArray face)
        {
            // Is empty
            if (!IsFilled(x, y, z))
            {
                return false;
            }

            if (IsFaceCovered(x, y, z, face))
            {
                return false;
            }

            return true;
        }

        private bool IsFaceCovered(byte x, byte y, byte z, BitArray face)
        {
            if (IsFilled(x + (face.GetValue_Byte() == 2 ? -1 : face.GetValue_Byte() == 3 ? 1 : 0),
                                     y + (face.GetValue_Byte() == 4 ? 1 : face.GetValue_Byte() == 5 ? -1 : 0),
                                     z + (face.GetValue_Byte() == 0 ? 1 : face.GetValue_Byte() == 1 ? -1 : 0)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This will add values for a cube onto a mesh.
        /// </summary>
        /// <param name="origin">Is the bottom left back corner of the cube relative to the position of the GamObject with the mesh renderer.</param>
        /// <param name="neighbors">Needs 6 bits. 0 = Render Face. 1 = Do not render facer. index: 0 = Front, 1 = Back, 2 = Left, 4 = Right, 5 = Top, 6 = Bottom</param>
        private void AddCube(Vector3 origin, BitArray neighbors, Color32 color)
        {
            if (neighbors.GetValue_Byte() >= 63)
            {
                return;
            }

            float cellSize = voxelChunk.GetGrid().GetCellSize();

            int startVertexIndex = vertices.Count;


            short uvIndex = AddColorToTexture(color);

            // Front
            if (neighbors.GetBit(0) == 0)
            {
                vertices.AddRange(new List<Vector3>
                {
                    origin + new Vector3(cellSize, 0, cellSize), // 0
                    origin + new Vector3(cellSize, cellSize, cellSize), // 1
                    origin + new Vector3(0, cellSize, cellSize), // 2
                    origin + new Vector3(0, 0, cellSize), // 3
                });
                AddUV(uvIndex);
                AddTriangles(ref triangles, startVertexIndex);
                startVertexIndex += 4;
            }
            // Back
            if (neighbors.GetBit(1) == 0)
            {
                vertices.AddRange(new List<Vector3>
                {
                    origin + new Vector3(0, 0, 0), // 0
                    origin + new Vector3(0, cellSize, 0), // 1
                    origin + new Vector3(cellSize, cellSize, 0), // 2
                    origin + new Vector3(cellSize, 0, 0), // 3
                });
                AddUV(uvIndex);
                AddTriangles(ref triangles, startVertexIndex);
                startVertexIndex += 4;
            }
            // Left
            if (neighbors.GetBit(2) == 0)
            {
                vertices.AddRange(new List<Vector3>
                {
                    origin + new Vector3(0, 0, cellSize), // 0
                    origin + new Vector3(0, cellSize, cellSize), // 1
                    origin + new Vector3(0, cellSize, 0), // 2
                    origin + new Vector3(0, 0, 0), // 3
                });
                AddUV(uvIndex);
                AddTriangles(ref triangles, startVertexIndex);
                startVertexIndex += 4;
            }
            // Right
            if (neighbors.GetBit(3) == 0)
            {
                vertices.AddRange(new List<Vector3>
                {
                    origin + new Vector3(cellSize, 0, 0), // 0
                    origin + new Vector3(cellSize, cellSize, 0), // 1
                    origin + new Vector3(cellSize, cellSize, cellSize), // 2
                    origin + new Vector3(cellSize, 0, cellSize), // 3
                });
                AddUV(uvIndex);
                AddTriangles(ref triangles, startVertexIndex);
                startVertexIndex += 4;
            }
            // Top
            if (neighbors.GetBit(4) == 0)
            {
                vertices.AddRange(new List<Vector3>
                {
                    origin + new Vector3(0, cellSize, 0), // 0
                    origin + new Vector3(0, cellSize, cellSize), // 1
                    origin + new Vector3(cellSize, cellSize, cellSize), // 2
                    origin + new Vector3(cellSize, cellSize, 0), // 3
                });
                AddUV(uvIndex);
                AddTriangles(ref triangles, startVertexIndex);
                startVertexIndex += 4;
            }
            // Bottom
            if (neighbors.GetBit(5) == 0)
            {
                vertices.AddRange(new List<Vector3>
                {
                    origin + new Vector3(0, 0, cellSize), // 0
                    origin + new Vector3(0, 0, 0), // 1
                    origin + new Vector3(cellSize, 0, 0), // 2
                    origin + new Vector3(cellSize, 0, cellSize), // 3
                });
                AddUV(uvIndex);
                AddTriangles(ref triangles, startVertexIndex);
            }
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

        private bool IsFilled(int x, int y, int z)
        {
            if (x >= 0 && x < voxelChunk.GetGrid().GetWidth() &&
                y >= 0 && y < voxelChunk.GetGrid().GetHeight() &&
                z >= 0 && z < voxelChunk.GetGrid().GetDepth())
            {
                return voxelChunk.GetGrid().GetGridObject(x, y, z).isFilled;
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
