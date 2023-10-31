
using System.Collections.Generic;

using UnityEngine;

namespace TheAshBot.Meshes
{
    public struct MeshHelper
    {



        /// <summary>
        /// This assigns the mesh vertices, uv, and triangles to the vertices, uv, and triangle variables
        /// </summary>
        public static Mesh AssignVerticesUvAndTrianglesToMesh(Vector3[] vertices, Vector2[] uv, int[] triangles)
        {
            Mesh mesh = new Mesh
            {
                // Assign the vertices, the uv, and the triangles to the mesh
                vertices = vertices,
                uv = uv,
                triangles = triangles
            };
            return mesh;
        }

        /// <summary>
        /// This makes the UV numbers the same as the vertex numbers
        /// </summary>
        /// <param name="vertices">are the vertices values that are going to be assigned to the UV's</param>
        public static Vector2[] AssignUvsFromVertices(Vector3[] vertices)
        {
            return AssignUvsFromVertices(vertices, Vector3.zero);
        }

        /// <summary>
        /// This makes the UV numbers the same as the vertex numbers
        /// </summary>
        /// <param name="vertices">This is the number of UVs</param>
        /// <param name="offset">is the offset of all of the uvs</param>
        public static Vector2[] AssignUvsFromVertices(Vector3[] vertices, Vector3 offset)
        {
            int UvNumbers = vertices.Length;
            Vector2[] uv = new Vector2[UvNumbers];
            for (int uvAndVertexNumber = 0; uvAndVertexNumber > UvNumbers; uvAndVertexNumber++)
            {
                uv[uvAndVertexNumber] = vertices[uvAndVertexNumber] + offset;
            }
            return uv;
        }

        /// <summary>
        /// This makes triangles and assigns triangle vertices
        /// </summary>
        /// <param name="startTriangleNumber">This is the first triangle number that gets assigned</param>
        /// <param name="firstTriangleValue">This is the value that the first triangle number is getting assigned to</param>
        /// <param name="secondTriangleValue">This is the value that the second triangle number is getting assigned to</param>
        /// <param name="thirdTriangleValue">This is the value that the third triangle number is getting assigned to</param>
        public static void MakeTriangle(ref int[] triangles, int startTriangleNumber, int firstTriangleValue, int secondTriangleValue, int thirdTriangleValue)
        {
            triangles[startTriangleNumber] = firstTriangleValue;
            startTriangleNumber++;
            triangles[startTriangleNumber] = secondTriangleValue;
            startTriangleNumber++;
            triangles[startTriangleNumber] = thirdTriangleValue;
        }
        /// <summary>
        /// This makes triangles and assigns triangle vertices
        /// </summary>
        /// <param name="firstTriangleValue">This is the value that the first triangle number is getting assigned to</param>
        /// <param name="secondTriangleValue">This is the value that the second triangle number is getting assigned to</param>
        /// <param name="thirdTriangleValue">This is the value that the third triangle number is getting assigned to</param>
        public static void MakeTriangle(ref List<int> triangles, int firstTriangleValue, int secondTriangleValue, int thirdTriangleValue)
        {
            triangles.Add(firstTriangleValue);
            triangles.Add(secondTriangleValue);
            triangles.Add(thirdTriangleValue);
        }


        /// <summary>
        /// convers a point on a texture to UV Coordinates
        /// </summary>
        /// <param name="x">is the x position as pixel on the texture</param>
        /// <param name="y">is the y position as pixel on the texture</param>
        /// <param name="textureWidth">is the width of the texture</param>
        /// <param name="textureHeight">is the heigh of the texture</param>
        /// <returns>UV Coordinates for the point on the texture</returns>
        public static Vector2 ConvertPixelsToUVCoordinates(int x, int y, int textureWidth, int textureHeight)
        {
            return new Vector2((float)x / textureWidth, (float)y / textureHeight);
        }

        /// <summary>
        /// will make all the Vertices, Uv, and triangle arrays according to number of quads specified. (4 vertices/uvs per 2 triangle)
        /// </summary>
        /// <param name="quadCount">is the number of quads that the mesh will have</param>
        /// <param name="vertices">is an out variable that contains a empty Vector3 array with 4 time the number of vertices as quads</param>
        /// <param name="uvs">is an out variable that contains a empty Vector2 array with 4 time the number of vertices as quads</param>
        /// <param name="triangles">is an out variable that contains a empty int array with 6 time the number of vertices as quads</param>
        public static void CreateEmptyMeshArray(int quadCount, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles)
        {
            vertices = new Vector3[4 * quadCount];
            uvs = new Vector2[vertices.Length];
            triangles = new int[6 * quadCount];
        }



        #region Me Not Understand
        public static void AddToMeshArrays(Vector3[] vertices, Vector2[] uvs, int[] triangles, int index, Vector3 pos, float rotation, Vector3 baseSize, Vector2 uv00, Vector2 uv11)
        {
            //Relocate vertices
            int vIndex = index * 4;
            int vIndex0 = vIndex;
            int vIndex1 = vIndex + 1;
            int vIndex2 = vIndex + 2;
            int vIndex3 = vIndex + 3;

            baseSize *= .5f;

            bool skewed = baseSize.x != baseSize.y;
            if (skewed)
            {
                vertices[vIndex0] = pos + GetQuaternionEuler(rotation) * new Vector3(-baseSize.x, baseSize.y);
                vertices[vIndex1] = pos + GetQuaternionEuler(rotation) * new Vector3(-baseSize.x, -baseSize.y);
                vertices[vIndex2] = pos + GetQuaternionEuler(rotation) * new Vector3(baseSize.x, -baseSize.y);
                vertices[vIndex3] = pos + GetQuaternionEuler(rotation) * baseSize;
            }
            else
            {
                vertices[vIndex0] = pos + GetQuaternionEuler(rotation - 270) * baseSize;
                vertices[vIndex1] = pos + GetQuaternionEuler(rotation - 180) * baseSize;
                vertices[vIndex2] = pos + GetQuaternionEuler(rotation - 90) * baseSize;
                vertices[vIndex3] = pos + GetQuaternionEuler(rotation - 0) * baseSize;
            }

            //Relocate UVs
            uvs[vIndex0] = new Vector2(uv00.x, uv11.y);
            uvs[vIndex1] = new Vector2(uv00.x, uv00.y);
            uvs[vIndex2] = new Vector2(uv11.x, uv00.y);
            uvs[vIndex3] = new Vector2(uv11.x, uv11.y);

            //Create triangles
            int tIndex = index * 6;

            triangles[tIndex + 0] = vIndex0;
            triangles[tIndex + 1] = vIndex3;
            triangles[tIndex + 2] = vIndex1;

            triangles[tIndex + 3] = vIndex1;
            triangles[tIndex + 4] = vIndex3;
            triangles[tIndex + 5] = vIndex2;
        }


        private static Quaternion[] cachedQuaternionEulerArray;
        private static void CacheQuaternionEuler()
        {
            if (cachedQuaternionEulerArray != null) return;
            cachedQuaternionEulerArray = new Quaternion[360];
            for (int i = 0; i < 360; i++)
            {
                cachedQuaternionEulerArray[i] = Quaternion.Euler(0, 0, i);
            }
        }
        private static Quaternion GetQuaternionEuler(float rotationFloat)
        {
            int rotation = Mathf.RoundToInt(rotationFloat);
            rotation %= 360;
            if (rotation < 0) rotation += 360;
            //if (rotation >= 360) rotation -= 360;
            if (cachedQuaternionEulerArray == null) CacheQuaternionEuler();
            return cachedQuaternionEulerArray[rotation];
        }

        #endregion

    }
}
