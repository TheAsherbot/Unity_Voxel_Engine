using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace TheAshBot.Meshes
{
    public class CreateMesh
    {


        public const int VERTICES_PER_HEXAGON = 7;
        public const int TRIANGLES_PER_HEXAGON = 18;


        /// <summary>
        /// This makes a pyramid mesh
        /// </summary>
        public static Mesh MakeFieldOfViewMesh(float meshHeight, float meshWidth, float meshDepth)
        {
            Mesh mesh;

            Vector3[] vertices = new Vector3[5];
            int[] triangles = new int[18];
            Vector2[] uv = new Vector2[5];

            // This is the Start Vertex
            vertices[0] = new Vector3(0, 0, 0);
            // This is the Top Left Vertex
            vertices[1] = new Vector3( meshWidth / 2,  meshHeight / 2, meshDepth);
            // This is the Top Right Vertex
            vertices[2] = new Vector3(-meshWidth / 2,  meshHeight / 2, meshDepth);
            // This is the Bottom Right Vertex
            vertices[3] = new Vector3(-meshWidth / 2, -meshHeight / 2, meshDepth);
            // This is the Bottom Left Vertex
            vertices[4] = new Vector3( meshWidth / 2, -meshHeight / 2, meshDepth);

            uv = MeshHelper.AssineUvsFromVertices(vertices);

            MeshHelper.MakeTriangle(ref triangles, 0, 0, 1, 0); // Top
            MeshHelper.MakeTriangle(ref triangles, 3, 3, 3, 0); // Bottum
            MeshHelper.MakeTriangle(ref triangles, 6, 6, 1, 4); // Left
            MeshHelper.MakeTriangle(ref triangles, 9, 9, 2, 0); // Right
            MeshHelper.MakeTriangle(ref triangles, 12, 12, 4, 1); // Back Left
            MeshHelper.MakeTriangle(ref triangles, 15, 15, 4, 2); // Back Right

            mesh = MeshHelper.AssineVerticesUvAndTrianglesToMesh(vertices, uv, triangles);

            return mesh;
        }


        #region Make Primitives
        
        /// <summary>
        /// This makes a triangel mesh
        /// </summary>
        public static Mesh MakeEquilateralTriangleMesh()
        {
            Mesh mesh;

            Vector3[] vertices = new Vector3[3];
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = new int[3];

            // Make the vertices, the uv, and the triangles
            vertices[0] = new Vector2(   0,   0.5f);
            vertices[1] = new Vector2( 0.5f, -0.5f);
            vertices[2] = new Vector2(-0.5f, -0.5f);

            MeshHelper.AssignUvsFromVertices(vertices, Vector2.one / 2);

            // Note: if you have the triangle in a counter clockwise order than it will be backwards
            MeshHelper.MakeTriangle(ref triangles, 0, 0, 1, 2);

            mesh = MeshHelper.AssineVerticesUvAndTrianglesToMesh(vertices, uv, triangles);

            return mesh;
        }

        /// <summary>
        /// This makes a quad mesh
        /// </summary>
        public static Mesh MakeQuadMesh()
        {
            Mesh mesh;

            Vector3[] vertices = new Vector3[4];
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = new int[6];

            // Make the vertices, the uv, and the triangles
            vertices[0] = new Vector2(0, 0) - (Vector2.one / 2);
            vertices[1] = new Vector2(0, 1) - (Vector2.one / 2);
            vertices[2] = new Vector2(1, 1) - (Vector2.one / 2);
            vertices[3] = new Vector2(1, 0) - (Vector2.one / 2);

            MeshHelper.AssignUvsFromVertices(vertices, Vector3.one / 2);

            // Note: if you have the triangle in a counter clockwise order than it will be backwards
            MeshHelper.MakeTriangle(ref triangles, 0, 0, 1, 2);

            MeshHelper.MakeTriangle(ref triangles, 3, 3, 2, 3);

            mesh = MeshHelper.AssineVerticesUvAndTrianglesToMesh(vertices, uv, triangles);

            return mesh;
        }

        /// <summary>
        /// This is the random meshes i am makeing
        /// </summary>
        public static Mesh MakeCubeMesh()
        {
            Mesh mesh;

            Vector3[] vertices = new Vector3[8];
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = new int[36];

            #region Make Vertices
            vertices[0] = new Vector3(0, 0, 0) - (Vector3.one / 2);
            vertices[1] = new Vector3(0, 1, 0) - (Vector3.one / 2);
            vertices[2] = new Vector3(1, 1, 0) - (Vector3.one / 2);
            vertices[3] = new Vector3(1, 0, 0) - (Vector3.one / 2);
            vertices[4] = new Vector3(0, 0, 1) - (Vector3.one / 2);
            vertices[5] = new Vector3(0, 1, 1) - (Vector3.one / 2);
            vertices[6] = new Vector3(1, 1, 1) - (Vector3.one / 2);
            vertices[7] = new Vector3(1, 0, 1) - (Vector3.one / 2);
            #endregion

            MeshHelper.AssignUvsFromVertices(vertices, new Vector3(0.5f, 0.5f, 0.5f));

            #region Make Triangles
            // Note: if you have the triangle in a counter clockwise order than it will be backwards
            // Front Left
            MeshHelper.MakeTriangle(ref triangles, 0, 0, 1, 2);

            // Front Right
            MeshHelper.MakeTriangle(ref triangles, 3, 3, 2, 3);

            // Top Left
            MeshHelper.MakeTriangle(ref triangles, 6, 6, 5, 6);

            // Top Right
            MeshHelper.MakeTriangle(ref triangles, 9, 9, 6, 2);

            // Left Front
            MeshHelper.MakeTriangle(ref triangles, 12, 12, 1, 0);

            // Left Back
            MeshHelper.MakeTriangle(ref triangles, 15, 15, 5, 0);

            // Right Front
            MeshHelper.MakeTriangle(ref triangles, 18, 18, 6, 3);

            // Right Back
            MeshHelper.MakeTriangle(ref triangles, 21, 21, 7, 3);

            // Back Left
            MeshHelper.MakeTriangle(ref triangles, 24, 24, 4, 7);

            // Back Right
            MeshHelper.MakeTriangle(ref triangles, 27, 27, 5, 7);

            // Bottom Left
            MeshHelper.MakeTriangle(ref triangles, 30, 30, 3, 4);

            // Bottom Right
            MeshHelper.MakeTriangle(ref triangles, 33, 33, 4, 3);
            #endregion

            mesh = MeshHelper.AssineVerticesUvAndTrianglesToMesh(vertices, uv, triangles);

            return mesh;
        }

        /// <summary>
        /// wil make a
        /// </summary>
        /// <param name="drawnHexagonArray"></param>
        /// <param name="uvHexagonArray"></param>
        /// <param name="textureWidth"></param>
        /// <param name="textureHeight"></param>
        /// <param name="vertices"></param>
        /// <param name="uvs"></param>
        /// <param name="triangles"></param>
        public static Mesh MakeHexagonMesh(HexagonPointedTop[] drawnHexagonArray, HexagonPointedTop[] uvHexagonArray, int textureWidth, int textureHeight)
        {
            Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[drawnHexagonArray.Length * VERTICES_PER_HEXAGON];
            Vector2[] uvs = new Vector2[vertices.Length];
            int[] triangles = new int[drawnHexagonArray.Length * TRIANGLES_PER_HEXAGON];

            if (uvHexagonArray.Length < drawnHexagonArray.Length)
            {
                List<HexagonPointedTop> uvHexagonList = uvHexagonArray.ToList();
                for (int i = uvHexagonArray.Length; i < drawnHexagonArray.Length; i++)
                {
                    uvHexagonList.Add(new HexagonPointedTop(Vector2.zero, 0));
                }
            }

            for (int i = 0; i < drawnHexagonArray.Length; i++)
            {

                #region Vertices
                vertices[i * VERTICES_PER_HEXAGON] = drawnHexagonArray[i].centerPoint;
                vertices[(i * VERTICES_PER_HEXAGON) + 1] = drawnHexagonArray[i].upperCorner;
                vertices[(i * VERTICES_PER_HEXAGON) + 2] = drawnHexagonArray[i].upperRightCorner;
                vertices[(i * VERTICES_PER_HEXAGON) + 3] = drawnHexagonArray[i].lowerRightCorner;
                vertices[(i * VERTICES_PER_HEXAGON) + 4] = drawnHexagonArray[i].lowerCorner;
                vertices[(i * VERTICES_PER_HEXAGON) + 5] = drawnHexagonArray[i].lowerLeftCorner;
                vertices[(i * VERTICES_PER_HEXAGON) + 6] = drawnHexagonArray[i].upperLeftCorner;
                #endregion

                #region Triangles
                // Upper Left
                triangles[i * TRIANGLES_PER_HEXAGON] = i * VERTICES_PER_HEXAGON;
                triangles[(i * TRIANGLES_PER_HEXAGON) + 1] = (i * VERTICES_PER_HEXAGON) + 6;
                triangles[(i * TRIANGLES_PER_HEXAGON) + 2] = (i * VERTICES_PER_HEXAGON) + 1;

                // Upper Right
                triangles[(i * TRIANGLES_PER_HEXAGON) + 3] = i * VERTICES_PER_HEXAGON;
                triangles[(i * TRIANGLES_PER_HEXAGON) + 4] = (i * VERTICES_PER_HEXAGON) + 1;
                triangles[(i * TRIANGLES_PER_HEXAGON) + 5] = (i * VERTICES_PER_HEXAGON) + 2;

                // Right
                triangles[(i * TRIANGLES_PER_HEXAGON) + 6] = i * VERTICES_PER_HEXAGON;
                triangles[(i * TRIANGLES_PER_HEXAGON) + 7] = (i * VERTICES_PER_HEXAGON) + 2;
                triangles[(i * TRIANGLES_PER_HEXAGON) + 8] = (i * VERTICES_PER_HEXAGON) + 3;

                // Lower Right
                triangles[(i * TRIANGLES_PER_HEXAGON) + 9] = i * VERTICES_PER_HEXAGON;
                triangles[(i * TRIANGLES_PER_HEXAGON) + 10] = (i * VERTICES_PER_HEXAGON) + 3;
                triangles[(i * TRIANGLES_PER_HEXAGON) + 11] = (i * VERTICES_PER_HEXAGON) + 4;

                // Lower Left
                triangles[(i * TRIANGLES_PER_HEXAGON) + 12] = i * VERTICES_PER_HEXAGON;
                triangles[(i * TRIANGLES_PER_HEXAGON) + 13] = (i * VERTICES_PER_HEXAGON) + 4;
                triangles[(i * TRIANGLES_PER_HEXAGON) + 14] = (i * VERTICES_PER_HEXAGON) + 5;

                // Lower Right
                triangles[(i * TRIANGLES_PER_HEXAGON) + 15] = i * VERTICES_PER_HEXAGON;
                triangles[(i * TRIANGLES_PER_HEXAGON) + 16] = (i * VERTICES_PER_HEXAGON) + 5;
                triangles[(i * TRIANGLES_PER_HEXAGON) + 17] = (i * VERTICES_PER_HEXAGON) + 6;
                #endregion

                #region Uvs
                HexagonPointedTop uvHexagon = uvHexagonArray[i];

                uvs[i * VERTICES_PER_HEXAGON] = new Vector2(uvHexagon.centerPoint.x / textureWidth, uvHexagon.centerPoint.y / textureHeight);
                uvs[(i * VERTICES_PER_HEXAGON) + 1] = new Vector2(uvHexagon.upperCorner.x / textureWidth, uvHexagon.upperCorner.y / textureHeight);
                uvs[(i * VERTICES_PER_HEXAGON) + 2] = new Vector2(uvHexagon.upperRightCorner.x / textureWidth, uvHexagon.upperRightCorner.y / textureHeight);
                uvs[(i * VERTICES_PER_HEXAGON) + 3] = new Vector2(uvHexagon.lowerRightCorner.x / textureWidth, uvHexagon.lowerRightCorner.y / textureHeight);
                uvs[(i * VERTICES_PER_HEXAGON) + 4] = new Vector2(uvHexagon.lowerCorner.x / textureWidth, uvHexagon.lowerCorner.y / textureHeight);
                uvs[(i * VERTICES_PER_HEXAGON) + 5] = new Vector2(uvHexagon.lowerLeftCorner.x / textureWidth, uvHexagon.lowerLeftCorner.y / textureHeight);
                uvs[(i * VERTICES_PER_HEXAGON) + 6] = new Vector2(uvHexagon.upperLeftCorner.x / textureWidth, uvHexagon.upperLeftCorner.y / textureHeight);
                #endregion

            }

            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangles;

            return mesh;
        }

        #endregion

    }

}
