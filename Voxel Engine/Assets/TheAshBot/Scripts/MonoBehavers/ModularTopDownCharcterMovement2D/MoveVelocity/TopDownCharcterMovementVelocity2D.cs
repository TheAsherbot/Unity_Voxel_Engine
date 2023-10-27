using UnityEngine;

namespace TheAshBot.TwoDimentional.TopDownCharcterMovement
{
    public class TopDownCharcterMovementVelocity2D : MonoBehaviour, IMoveVelocity2D
    {


        public enum MovementType
        {
            Transfrom,
            Rigidbody,
        }


        [SerializeField] private MovementType movementType;
        [SerializeField] private float movementSpeed = 5;


        private Vector3 velocityVector;
        private new Rigidbody2D rigidbody;



        private void Update()
        {
            switch (movementType)
            {
                case MovementType.Transfrom:
                    transform.position += velocityVector * movementSpeed * Time.deltaTime;
                    break;
            }
        }

        private void FixedUpdate()
        {
            switch (movementType)
            {
                case MovementType.Rigidbody:
                    if (rigidbody == null) AddSetRigidBody();
                    rigidbody.velocity = velocityVector * movementSpeed;
                    break;
            }
        }


        public void SetVelocity(Vector3 velocityVector)
        {
            this.velocityVector = velocityVector;
        }

        public float GetMovementSpeed()
        {
            return movementSpeed;
        }

        public void SetMovementSpeed(float movementSpeed)
        {
            this.movementSpeed = movementSpeed;
        }


        private void AddSetRigidBody()
        {
            if (!TryGetComponent(out rigidbody))
            {
                rigidbody = gameObject.AddComponent<Rigidbody2D>();
                rigidbody.gravityScale = 0;
                rigidbody.freezeRotation = true;
            }
        }


    }
}
