using UnityEngine;

namespace TheAshBot.TwoDimentional.TopDownCharcterMovement
{
    public class TopDownCharcterMovementPosition2D : MonoBehaviour, IMovePosition2D
    {


        public enum MovementType
        {
            Direct,
#if AStarPathFindingPro
        AStarPathFindingPro,
#endif
        }



        [SerializeField] private MovementType movementType = MovementType.Direct;


        private Vector3 movePosition;




        private void Update()
        {
            HandelMovementType();
        }



        public void SetMovePosition(Vector3 movePosition)
        {
            this.movePosition = movePosition;
        }



        private void HandelMovementType()
        {
            switch (movementType)
            {
                case MovementType.Direct:
                    HandleDirectMovement();
                    break;
#if AStarPathFindingPro
            case MovementType.AStarPathFindingPro:

                break;
#endif
            }
        }

        private void HandleDirectMovement()
        {
            if (!TryGetComponent(out IMoveVelocity2D moveVelocity))
            {
                moveVelocity = gameObject.AddComponent<TopDownCharcterMovementVelocity2D>();
                (moveVelocity as TopDownCharcterMovementVelocity2D).SetMovementSpeed(5);
            }

            Vector3 moveDirection = (movePosition - transform.position).normalized;

            float distanceToMovePosition = Vector3.Distance(transform.position, movePosition);

            if (distanceToMovePosition > 0)
            {
                Vector3 positionAfterMoving = transform.position + moveDirection * moveVelocity.GetMovementSpeed() * Time.deltaTime;

                float distanceToMovePositionAfterMoving = Vector3.Distance(transform.position, positionAfterMoving);

                if (distanceToMovePositionAfterMoving > distanceToMovePosition)
                {
                    transform.position = movePosition;
                    moveDirection = Vector3.zero;
                }

                moveVelocity.SetVelocity(moveDirection);
            }
        }


    }
}
