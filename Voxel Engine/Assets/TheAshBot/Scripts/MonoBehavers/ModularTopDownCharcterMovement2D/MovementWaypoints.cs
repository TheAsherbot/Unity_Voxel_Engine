using UnityEngine;

namespace TheAshBot.TwoDimentional.TopDownCharcterMovement
{
    public class MovementWaypoints : MonoBehaviour
    {


        public Vector3[] waypointArray;


        private int waypointIndex = 0;
        private float nextWaypointDistance = 0.05f;


        private void Update()
        {
            if (Vector3.Distance(waypointArray[waypointIndex], transform.position) < nextWaypointDistance)
            {
                waypointIndex++;
                waypointIndex %= waypointArray.Length;
            }

            Vector3 movementDirection = (waypointArray[waypointIndex] - transform.position).normalized;

            GetComponent<IMoveVelocity2D>().SetVelocity(movementDirection);
        }



    }
}
