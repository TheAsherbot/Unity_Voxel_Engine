using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

namespace TheAshBot.VoxelEngine
{
    public class VoxelRenderer
    {

        private static readonly float UV_PIXEL_OFFSET = 0.0625f;

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
        private List<Color32> textureColorList;

        private Camera camera;





        public VoxelRenderer(Vector3 origin, Camera camera)
        {
            GameObject gameObject = new GameObject("Voxel Grid Mesh");
            meshFilter = gameObject.AddComponent<MeshFilter>();
            meshRenderer = gameObject.AddComponent<MeshRenderer>();

            voxelChunk = new VoxelChunk(1, origin);
            voxelChunk.GetGrid().OnGridValueChanged += Grid_OnValueChanged;

            textureColorList = new List<Color32>();
        }
        public VoxelRenderer(Vector3 origin)
        {
            GameObject gameObject = new GameObject("Voxel Grid Mesh");
            meshFilter = gameObject.AddComponent<MeshFilter>();
            meshRenderer = gameObject.AddComponent<MeshRenderer>();

            voxelChunk = new VoxelChunk(1, origin);
            voxelChunk.GetGrid().OnGridValueChanged += Grid_OnValueChanged;

            textureColorList = new List<Color32>();

            camera = Camera.main;
        }





        public GenericGrid3D<VoxelNode> GetGrid()
        {
            return voxelChunk.GetGrid();
        }

        public void RenderVoxels()
        {
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
                        if (!ShouldRenderFace(x, y, z, FRONT_FACE, camera.transform) == true)
                        {
                            neighbors.SetBit(0, 1);
                        }
                        // Back
                        if (!ShouldRenderFace(x, y, z, BACK_FACE, camera.transform) == true)
                        {
                            neighbors.SetBit(1, 1);
                        }
                        // Left
                        if (!ShouldRenderFace(x, y, z, LEFT_FACE, camera.transform) == true)
                        {
                            neighbors.SetBit(2, 1);
                        }
                        // Right
                        if (!ShouldRenderFace(x, y, z, RIGHT_FACE, camera.transform) == true)
                        {
                            neighbors.SetBit(3, 1);
                        }
                        // Top
                        if (!ShouldRenderFace(x, y, z, TOP_FACE, camera.transform) == true)
                        {
                            neighbors.SetBit(4, 1);
                        }
                        // Bottom
                        if (!ShouldRenderFace(x, y, z, BOTTOM_FACE, camera.transform) == true)
                        {
                            neighbors.SetBit(5, 1);
                        }

                        /*
                        // Front
                        if (IsFilled(x, y, z + 1) == true)
                        {
                            neighbors.SetBit(0, 1);
                        }
                        // Back
                        if (IsFilled(x, y, z - 1) == true)
                        {
                            neighbors.SetBit(1, 1);
                        }
                        // Left
                        if (IsFilled(x - 1, y, z) == true)
                        {
                            neighbors.SetBit(2, 1);
                        }
                        // Right
                        if (IsFilled(x + 1, y, z) == true)
                        {
                            neighbors.SetBit(3, 1);
                        }
                        // Top
                        if (IsFilled(x, y + 1, z) == true)
                        {
                            neighbors.SetBit(4, 1);
                        }
                        // Bottom
                        if (IsFilled(x, y - 1, z) == true)
                        {
                            neighbors.SetBit(5, 1);
                        }
                        */
                        AddCube(ref mesh, voxelChunk.GetGrid().GetWorldPosition(x, y, z), neighbors, voxelChunk.GetGrid().GetGridObject(x, y, z).color);
                    }
                }
            }



            Material material = new Material(meshRenderer.material);
            Texture2D texture = GetTexture();
            texture.filterMode = FilterMode.Point;
            texture.Apply();
            material.mainTexture = texture;
            material.color = Color.white;
            meshRenderer.material = material;
            meshRenderer.SetMaterials(new List<Material> { material });


            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mesh.RecalculateTangents();

            meshFilter.mesh = mesh;
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
        private bool ShouldRenderFace(byte x, byte y, byte z, BitArray face, Transform cameraTransform)
        {
            // Is empty
            if (!IsFilled(x, y, z))
            {
                return false;
            }

            // Neighbor is filled
            if (IsFilled(x + (face.GetValue_Byte() == 2 ? -1 : face.GetValue_Byte() == 3 ? 1 : 0),
                         y + (face.GetValue_Byte() == 4 ? 1 : face.GetValue_Byte() == 5 ? -1 : 0),
                         z + (face.GetValue_Byte() == 0 ? 1 : face.GetValue_Byte() == 1 ? -1 : 0)))
            {
                return false;
            }

            if (face.GetValue_Byte() == 0)
            {
                // z = 1
                return (GetGrid().GetWorldPosition(x, y, z).z + (GetGrid().GetCellSize() / 2)) < cameraTransform.position.z;
            }
            if (face.GetValue_Byte() == 1)
            {
                // z = -1
                return (GetGrid().GetWorldPosition(x, y, z).z - (GetGrid().GetCellSize() / 2)) > cameraTransform.position.z;
            }
            if (face.GetValue_Byte() == 2)
            {
                // x = -1
                return (GetGrid().GetWorldPosition(x, y, z).x - (GetGrid().GetCellSize() / 2)) > cameraTransform.position.x;
            }
            if (face.GetValue_Byte() == 3)
            {
                // x = 1
                return (GetGrid().GetWorldPosition(x, y, z).x + (GetGrid().GetCellSize() / 2)) < cameraTransform.position.x;
            }
            if (face.GetValue_Byte() == 4)
            {
                // y = 1
                return (GetGrid().GetWorldPosition(x, y, z).y + (GetGrid().GetCellSize() / 2)) < cameraTransform.position.y;
            }
            if (face.GetValue_Byte() == 5)
            {
                // y = -1
                return (GetGrid().GetWorldPosition(x, y, z).y - (GetGrid().GetCellSize() / 2)) > cameraTransform.position.y;
            }

            return true;
        }

        /// <summary>
        /// This will add values for a cube onto a mesh.
        /// </summary>
        /// <param name="mesh">is the mash that the cube is being added to</param>
        /// <param name="origin">Is the bottom left back corner of the cube relative to the position of the GamObject with the mesh renderer.</param>
        /// <param name="neighbors">Needs 6 bits. 0 = Render Face. 1 = Do not render facer. index: 0 = Front, 1 = Back, 2 = Left, 4 = Right, 5 = Top, 6 = Bottom</param>
        private void AddCube(ref Mesh mesh, Vector3 origin, BitArray neighbors, Color32 color)
        {
            if (neighbors.GetValue_Byte() >= 63)
            {
                return;
            }

            float cellSize = voxelChunk.GetGrid().GetCellSize();

            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();
            List<int> triangles = new List<int>();

            mesh.GetVertices(vertices);
            mesh.GetUVs(0, uvs);
            mesh.GetTriangles(triangles, 0);

            int startVertexIndex = vertices.Count;

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
                AddUV(ref uvs, color);
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
                AddUV(ref uvs, color);
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
                AddUV(ref uvs, color);
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
                AddUV(ref uvs, color);
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
                AddUV(ref uvs, color);
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
                AddUV(ref uvs, color);
                AddTriangles(ref triangles, startVertexIndex);
            }

            mesh.SetVertices(vertices);
            mesh.SetUVs(0, uvs);
            mesh.SetTriangles(triangles, 0);
        }

        private void AddUV(ref List<Vector2> uvs, Color32 color)
        {
            byte uvIndex = 0;
            if (textureColorList.Contains(color))
            {
                uvIndex = (byte)textureColorList.FindIndex(foundColor => foundColor.Equals(color));
            }
            else if (textureColorList.Count >= 256)
            {
                uvIndex = (byte)color.GetClosestColorRBG(textureColorList, color);
            }
            else
            {
                // Does not have the color, and has space
                uvIndex = (byte)textureColorList.Count;
                textureColorList.Add(color);
            }

            Vector2 origin = new Vector2(Mathf.Round(uvIndex / 16f), uvIndex % 16);
            uvs.AddRange(new List<Vector2>
        {
            origin / 16f    , // 0
            origin / 16f + new Vector2(0, UV_PIXEL_OFFSET), // 1
            origin / 16f + new Vector2(UV_PIXEL_OFFSET, UV_PIXEL_OFFSET), // 2
            origin / 16f + new Vector2(UV_PIXEL_OFFSET, 0), // 3
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
        }

        private Texture2D GetTexture()
        {
            byte size = 16;
            Texture2D texture = new Texture2D(size, size);

            for (byte x = 0; x < size; x++)
            {
                for (byte y = 0; y < size; y++)
                {
                    texture.SetPixel(x, y, GetTextureColorFromXY(x, y));
                }
            }

            return texture;
        }

        private Color GetTextureColorFromXY(byte x, byte y)
        {
            Color color = default;
            try
            {
                color = textureColorList[x * 16 + y];
            }
            catch (System.Exception)
            {

            }

            if (color == default)
            {
                color = Color.white;
            }
            return color;
        }


    }
}
