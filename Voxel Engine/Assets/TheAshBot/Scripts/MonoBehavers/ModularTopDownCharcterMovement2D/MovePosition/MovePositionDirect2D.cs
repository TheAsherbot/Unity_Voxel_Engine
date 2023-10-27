using UnityEngine;

namespace TheAshBot.TwoDimentional.TopDownCharcterMovement
{
    [RequireComponent(typeof(IMoveVelocity2D))]
    public class MovePositionDirect2D : MonoBehaviour, IMovePosition2D
    {


        private Vector3 movePosition;


        private void Update()
        {
            if (TryGetComponent(out IMoveVelocity2D moveVelocity))
            {
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


        public void SetMovePosition(Vector3 movePosition)
        {
            this.movePosition = movePosition;
        }


    }
}
