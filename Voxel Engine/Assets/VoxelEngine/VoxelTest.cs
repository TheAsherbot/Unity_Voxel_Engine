using UnityEngine;


namespace TheAshBot.VoxelEngine
{
    public class VoxelTest : MonoBehaviour
    {

        private GenericGrid3D<VoxelNode> grid;
        [SerializeField] private Transform parent;

        private void Start()
        {
            VoxelRenderer voxelRenderer = new VoxelRenderer(new Vector3(0, 0));
            grid = voxelRenderer.GetGrid();

            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    for (int z = 0; z < grid.GetDepth(); z++)
                    {
                        VoxelNode voxelNode = grid.GetGridObject(x, y, z);
                        voxelNode.isEmpty = false;
                        voxelNode.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                        grid.SetGridObjectWithoutNotifying(x, y, z, voxelNode);
                    }
                }
            }
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    for (int z = 0; z < grid.GetDepth(); z++)
                    {
                        VoxelNode voxelNode = grid.GetGridObject(x, y, z);
                        /*if (x % 2 == 0)
                        {
                            if (y % 2 == 0)
                            {
                                voxelNode.isEmpty = true;
                            }
                        }
                        else
                        {
                            if (y % 2 == 1)
                            {
                                voxelNode.isEmpty = true;
                            }
                        }*/
                        BitArray bits = new BitArray(3);
                        if (x % 2 == 0)
                        {
                            bits.SetBit(0, 1);
                        }
                        if (y % 2 == 0)
                        {
                            bits.SetBit(1, 1);
                        }
                        if (z % 2 == 0)
                        {
                            if ((bits.GetBit(0) == 0 && bits.GetBit(1) == 1) || (bits.GetBit(0) == 1 && bits.GetBit(1) == 0))
                            {
                                voxelNode.isEmpty = true;
                            }
                        }
                        else
                        {
                            if ((bits.GetBit(0) == 1 && bits.GetBit(1) == 1) || (bits.GetBit(0) == 0 && bits.GetBit(1) == 0))
                            {
                                voxelNode.isEmpty = true;
                            }
                        }

                        grid.SetGridObjectWithoutNotifying(x, y, z, voxelNode);
                    }
                }
            }



            VoxelNode newVoxelNode = grid.GetGridObject(1, 5, 0);
            newVoxelNode.isEmpty = false;
            newVoxelNode.color = Color.HSVToRGB(1, 0.5f, 1);
            grid.SetGridObjectWithoutNotifying(1, 5, 0, newVoxelNode);


            grid.TriggerGridObjectChanged(0, 0, 0);
        }

        /*
            private void Update()
            {
                int size = 20;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    for (int x = 0; x < size; x++)
                    {
                        for (int y = 0; y < size; y++)
                        {
                            for (int z = 0; z < size; z++)
                            {
                                GameObject newGameObject = new GameObject($"{x}, {y}, {z}", typeof(MeshFilter), typeof(MeshRenderer));
                                newGameObject.transform.parent = parent;
                                newGameObject.transform.position = new Vector3(x, y, z);
                                newGameObject.GetComponent<MeshFilter>().mesh = MakeCube();
                                newGameObject.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
                            }
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    for (int x = 0; x < size; x++)
                    {
                        for (int y = 0; y < size; y++)
                        {
                            for (int z = 0; z < size; z++)
                            {
                                GameObject newGameObject = new GameObject($"{x}, {y}, {z}", typeof(MeshFilter), typeof(MeshRenderer));
                                newGameObject.transform.parent = parent;
                                gameObject.transform.position = new Vector3(x, y, z);
                                newGameObject.GetComponent<MeshFilter>().mesh = MakeCube2();
                                newGameObject.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
                            }
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    while (parent.childCount > 0)
                    {
                        DestroyImmediate(parent.GetChild(0).gameObject);
                    }
                }
            }
        */


    }
}
