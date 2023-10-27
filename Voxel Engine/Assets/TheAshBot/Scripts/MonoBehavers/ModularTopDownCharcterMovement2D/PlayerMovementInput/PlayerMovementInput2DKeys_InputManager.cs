using UnityEngine;

namespace TheAshBot.TwoDimentional.TopDownCharcterMovement
{
    [RequireComponent(typeof(IMoveVelocity2D))]
    public class PlayerMovementInput2DKeys_InputManager : MonoBehaviour
    {
#if ENABLE_LEGACY_INPUT_MANAGER

        private void Update()
        {
            float moveX = 0f;
            float moveY = 0f;

            if (Input.GetKey(KeyCode.W)) moveY = 1;
            if (Input.GetKey(KeyCode.A)) moveX = -1;
            if (Input.GetKey(KeyCode.S)) moveY = -1;
            if (Input.GetKey(KeyCode.D)) moveX = 1;

            Vector3 moveVector = new Vector3(moveX, moveY).normalized;

            GetComponent<IMoveVelocity2D>().SetVelocity(moveVector);
        }

#endif
    }
}
