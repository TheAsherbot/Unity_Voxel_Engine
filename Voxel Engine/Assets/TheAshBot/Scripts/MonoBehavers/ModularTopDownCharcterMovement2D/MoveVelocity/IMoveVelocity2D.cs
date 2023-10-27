using UnityEngine;

namespace TheAshBot.TwoDimentional.TopDownCharcterMovement
{
    public interface IMoveVelocity2D
    {

        public void SetVelocity(Vector3 velocityVector);

        public float GetMovementSpeed();

    }
}
