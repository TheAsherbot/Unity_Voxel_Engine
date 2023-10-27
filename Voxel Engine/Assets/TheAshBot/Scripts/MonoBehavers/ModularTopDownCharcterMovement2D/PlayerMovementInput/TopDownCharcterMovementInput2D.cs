using UnityEngine;

namespace TheAshBot.TwoDimentional.TopDownCharcterMovement
{
    public class TopDownCharcterMovementInput2D : MonoBehaviour, IPlayerMovementInput2D
    {


        public enum InputType
        {
            InputSystem,
            InputManager,
        }

        public enum MovementControlType
        {
            WASD,
            ArrowKeys,
            Mouse,
        }


        [SerializeField] private InputType inputType;
        [SerializeField] private MovementControlType movementControlType;


        private void Update()
        {
            HandelInputType();
        }


        private void HandelInputType()
        {
            switch (movementControlType)
            {
                case MovementControlType.WASD:
                    HandelWASDInput();
                    break;
                case MovementControlType.ArrowKeys:
                    HandelArrowKeysInput();
                    break;
                case MovementControlType.Mouse:
                    HandelMouseInput();
                    break;
            }
        }


        private void HandelWASDInput()
        {
            switch (inputType)
            {
                case InputType.InputSystem:
                    break;
                case InputType.InputManager:
                    Vector3 moveVectorNormalized = GetMovementVectorFromKeys(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D).normalized;

                    if (!TryGetComponent(out IMoveVelocity2D moveVelocity))
                    {
                        moveVelocity = gameObject.AddComponent<TopDownCharcterMovementVelocity2D>();
                        (moveVelocity as TopDownCharcterMovementVelocity2D).SetMovementSpeed(5);
                    }
                    moveVelocity.SetVelocity(moveVectorNormalized);
                    break;
            }
        }

        private void HandelArrowKeysInput()
        {
            switch (inputType)
            {
                case InputType.InputSystem:
                    break;
                case InputType.InputManager:
                    Vector3 moveVectorNormalized = GetMovementVectorFromKeys(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow).normalized;

                    if (!TryGetComponent(out IMoveVelocity2D moveVelocity))
                    {
                        moveVelocity = gameObject.AddComponent<TopDownCharcterMovementVelocity2D>();
                        (moveVelocity as TopDownCharcterMovementVelocity2D).SetMovementSpeed(5);
                    }
                    moveVelocity.SetVelocity(moveVectorNormalized);
                    break;
            }
        }

        private void HandelMouseInput()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (!TryGetComponent(out IMovePosition2D movePosition2D))
                {
                    movePosition2D = gameObject.AddComponent<TopDownCharcterMovementPosition2D>();
                }

                movePosition2D.SetMovePosition(Mouse2D.GetMousePosition2D());
                //                             Mouse 2D handles the diferent input type.
            }
        }


        private Vector3 GetMovementVectorFromKeys(KeyCode up, KeyCode down, KeyCode left, KeyCode right)
        {
            float moveX = 0f;
            float moveY = 0f;

            if (Input.GetKey(up)) moveY += 1;
            if (Input.GetKey(down)) moveY -= 1;

            if (Input.GetKey(left)) moveX -= 1;
            if (Input.GetKey(right)) moveX += 1;

            return new Vector3(moveX, moveY);
        }


    }
}
