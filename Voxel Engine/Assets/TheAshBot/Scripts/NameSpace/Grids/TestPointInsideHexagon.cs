using TheAshBot.TwoDimentional;

using UnityEngine;

namespace TheAshBot.Grids
{
    public class TestPointInsideHexagon : MonoBehaviour
    {


        [SerializeField] private MeshRenderer mouseMeshRenderer;
        [SerializeField] private Material greenMaterial;
        [SerializeField] private Material redMaterial;


        private HexagonPointedTop hexagon;


        private void Start()
        {
            hexagon = new HexagonPointedTop(new Vector2(0, 0), 0.5f);
        }

        private void Update()
        {
            // Change this to "Mouse3D" for a 3D game
            Vector2 testPosition = Mouse2D.GetMousePosition2D();

            mouseMeshRenderer.material = redMaterial;

            if (testPosition.x < hexagon.upperRightCorner.x &&
                testPosition.x > hexagon.upperLeftCorner.x)
            {
                // Inside the horizonal bounds
                if (testPosition.y < hexagon.upperCorner.y &&
                    testPosition.y > hexagon.lowerCorner.y)
                {
                    // Inside the vertical bounds

                    Vector3 directionFromUpperRightCornerToUpperCorner = hexagon.upperCorner - hexagon.upperRightCorner;
                    Vector3 dotDirectionUpperRightCorner = Quaternion.Euler(0, 0, -90) * directionFromUpperRightCornerToUpperCorner;
                    Vector3 directionFromUpperRightCornerToTestPoint = testPosition - hexagon.upperRightCorner;
                    float dotUpperRightCorner = Vector3.Dot(dotDirectionUpperRightCorner.normalized, directionFromUpperRightCornerToTestPoint.normalized);

                    Vector3 directionFromUpperLeftCornerToUpperCorner = hexagon.upperCorner - hexagon.upperLeftCorner;
                    Vector3 dotDirectionUpperLeftCorner = Quaternion.Euler(0, 0, +90) * directionFromUpperLeftCornerToUpperCorner;
                    Vector3 directionFromUpperLeftCornerToTestPoint = testPosition - hexagon.upperLeftCorner;
                    float dotUpperLeftCorner = Vector3.Dot(dotDirectionUpperLeftCorner.normalized, directionFromUpperLeftCornerToTestPoint.normalized);

                    Vector3 directionFromLowerRightCornerToLowerCorner = hexagon.lowerCorner - hexagon.lowerRightCorner;
                    Vector3 dotDirectionLowerRightCorner = Quaternion.Euler(0, 0, +90) * directionFromLowerRightCornerToLowerCorner;
                    Vector3 directionFromLowerRightCornerToTestPoint = testPosition - hexagon.lowerRightCorner;
                    float dotLowerRightCorner = Vector3.Dot(dotDirectionLowerRightCorner.normalized, directionFromLowerRightCornerToTestPoint.normalized);

                    Vector3 directionFromLowerLeftCornerToLowerCorner = hexagon.lowerCorner - hexagon.lowerLeftCorner;
                    Vector3 dotDirectionLowerLeftCorner = Quaternion.Euler(0, 0, -90) * directionFromLowerLeftCornerToLowerCorner;
                    Vector3 directionFromLowerLeftCornerToTestPoint = testPosition - hexagon.lowerLeftCorner;
                    float dotLowerLeftCorner = Vector3.Dot(dotDirectionLowerLeftCorner.normalized, directionFromLowerLeftCornerToTestPoint.normalized);



                    if (dotUpperRightCorner < 0 && dotUpperLeftCorner < 0 && dotLowerRightCorner < 0 && dotLowerLeftCorner < 0)
                    {
                        mouseMeshRenderer.material = greenMaterial;
                    }
                }
            }

            // Change this to "Mouse3D" for a 3D game
            transform.position = Mouse2D.GetMousePosition2D();
        }


    }
}