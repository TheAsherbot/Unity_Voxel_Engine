using TheAshBot.Meshes;

using UnityEngine;

namespace TheAshBot.TwoDimentional.Grids
{
    public class GridVisual2D : MonoBehaviour
    {


        private bool updateMesh;

        private Grid2D grid;
        private MeshFilter meshFilter;
        private Mesh mesh;


        private void Awake()
        {
            mesh = new Mesh();
            meshFilter = GetComponent<MeshFilter>();

            meshFilter.mesh = mesh;
        }

        private void LateUpdate()
        {
            if (updateMesh)
            {
                updateMesh = false;
                UpdateHeatMapVisual();
            }
        }

        public void SetGrid(Grid2D grid)
        {
            this.grid = grid;
            UpdateHeatMapVisual();

            grid.OnGridValueChanged += Grid_OnGridValueChanged;
        }

        private void Grid_OnGridValueChanged(int x, int y)
        {
            updateMesh = true;
        }

        private void UpdateHeatMapVisual()
        {
            MeshHelper.CreateEmptyMeshArray(grid.GetHeight() * grid.GetWidth(), out Vector3[] vertices, out Vector2[] uvs, out int[] triangles);

            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    int index = x * grid.GetHeight() + y;
                    Vector3 quadSize = Vector2.one * grid.GetCellSize();
                    Vector2 offsetSize = quadSize / 2;

                    int gridValue = grid.HasValue(x, y) ? 1 : 0;

                    Vector2 gridValueUV = new Vector2(gridValue, 0f);
                    MeshHelper.AddToMeshArrays(vertices, uvs, triangles, index, grid.GetWorldPosition(x, y) + offsetSize, 0f, quadSize, gridValueUV, gridValueUV);
                }
            }

            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangles;
        }


    }
}