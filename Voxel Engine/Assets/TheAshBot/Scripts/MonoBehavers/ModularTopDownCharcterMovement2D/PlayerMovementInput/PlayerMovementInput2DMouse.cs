using UnityEngine;

namespace TheAshBot.TwoDimentional.TopDownCharcterMovement
{
    [RequireComponent(typeof(IMovePosition2D))]
    public class PlayerMovementInput2DMouse : MonoBehaviour
    {


        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                GetComponent<IMovePosition2D>().SetMovePosition(Mouse2D.GetMousePosition2D());
            }
        }


    }
}