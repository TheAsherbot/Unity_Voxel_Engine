using System.Drawing;
using System.Runtime.CompilerServices;

using UnityEngine;

namespace TheAshBot.VoxelEngine
{
    public class VoxelChunk
    {

        private static readonly byte GRID_SIZE = 16;



        private GenericGrid3D<VoxelNode> grid;



        public VoxelChunk(float cellSize, Vector3 originPosition)
        {
            grid = new GenericGrid3D<VoxelNode>(GRID_SIZE, GRID_SIZE, GRID_SIZE, cellSize, originPosition, 
                (GenericGrid3D<VoxelNode> grid, int x, int y, int z) => new VoxelNode(grid, x, y, z));
        }
        public VoxelChunk(GenericGrid3D<VoxelChunk> chunkGrid, int x, int y, int z)
        {
            grid = new GenericGrid3D<VoxelNode>(GRID_SIZE, GRID_SIZE, GRID_SIZE, 1, new Vector3(x, y, z) * 16,
            (GenericGrid3D<VoxelNode> grid, int x, int y, int z) => new VoxelNode(grid, x, y, z));
        }

    }
}
