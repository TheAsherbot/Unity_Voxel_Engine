using UnityEngine;

namespace TheAshBot.TwoDimentional.TopDownCharcterMovement
{
    public class TopDownCharcterMovement2D : MonoBehaviour
    {


        public void AddMovementVelocity()
        {
            gameObject.AddComponent<TopDownCharcterMovementVelocity2D>();
        }
        public void AddMovementInput()
        {
            gameObject.AddComponent<TopDownCharcterMovementInput2D>();
        }
        public void AddMovementPosition()
        {
            gameObject.AddComponent<TopDownCharcterMovementPosition2D>();
        }


    }
}
