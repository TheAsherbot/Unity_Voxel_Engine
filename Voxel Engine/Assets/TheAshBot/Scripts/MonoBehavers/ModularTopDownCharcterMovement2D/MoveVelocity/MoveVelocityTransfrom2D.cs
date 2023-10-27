using UnityEngine;

namespace TheAshBot.TwoDimentional.TopDownCharcterMovement
{
    public class MoveVelocityTransfrom2D : MonoBehaviour, IMoveVelocity2D
    {


        [SerializeField] private int movementSpeed;


        private Vector3 velocityVector;


        private void Update()
        {
            transform.position += velocityVector * movementSpeed * Time.deltaTime;
        }


        public void SetVelocity(Vector3 velocityVector)
        {
            this.velocityVector = velocityVector;
        }

        public float GetMovementSpeed()
        {
            return movementSpeed;
        }


    }
}
