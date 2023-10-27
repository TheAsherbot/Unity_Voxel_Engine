using TheAshBot.Meshes;

using UnityEngine;

namespace TheAshBot.TwoDimentional.Grids
{
    public class HexGridVisual2D : MonoBehaviour
    {


        private const int TEXTURE_WIDTH = 64;
        private const int TEXTURE_HEIGHT = 1;


        private bool updateMesh;

        private HexGrid2D grid;
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

        public void SetGrid(HexGrid2D grid)
        {
            this.grid = grid;
            updateMesh = true;

            grid.OnGridValueChanged += Grid_OnGridValueChanged;
        }

        private void Grid_OnGridValueChanged(int x, int y)
        {
            updateMesh = true;
        }

        private void UpdateHeatMapVisual()
        {
            HexagonPointedTop[] hexagonArray = new HexagonPointedTop[grid.GetHeight() * grid.GetWidth()];
            HexagonPointedTop[] uvHexagonArray = new HexagonPointedTop[grid.GetHeight() * grid.GetWidth()];

            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    int index = x * grid.GetHeight() + y;

                    float gridValue = grid.GetValueNormalized(x, y);

                    uvHexagonArray[index] = new HexagonPointedTop(new Vector2(Mathf.RoundToInt(gridValue * TEXTURE_WIDTH), 0), 0);

                    hexagonArray[index] = new HexagonPointedTop(grid.GetWorldPosition(x, y), grid.GetCellSize() / 2);
                }
            }

            Mesh newMesh = CreateMesh.MakeHexagonMesh(hexagonArray, uvHexagonArray, 64, 1);

            mesh.vertices = newMesh.vertices;
            mesh.uv = newMesh.uv;
            mesh.triangles = newMesh.triangles;
        }

    }
}