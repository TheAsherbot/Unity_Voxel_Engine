using UnityEngine;

namespace TheAshBot.TwoDimentional.TopDownCharcterMovement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveVelocityRigidbody2D : MonoBehaviour, IMoveVelocity2D
    {


        [SerializeField] private int movementSpeed;


        private Vector3 velocityVector;
        private new Rigidbody2D rigidbody;


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            rigidbody.linearVelocity = velocityVector * movementSpeed;
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
