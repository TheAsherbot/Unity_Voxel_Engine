#if UNITY_EDITOR

using UnityEditor;

using UnityEngine;

namespace TheAshBot.TwoDimentional.TopDownCharcterMovement.Editors
{
    [CustomEditor(typeof(MovementWaypoints))]
    public class MovementWaypointEditor : Editor
    {

        private void OnSceneGUI()
        {
            MovementWaypoints movementWaypoints = target as MovementWaypoints;

            if (movementWaypoints.waypointArray == null) return;

            for (int i = 0; i < movementWaypoints.waypointArray.Length; i++)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 newWaypointPosition = Handles.PositionHandle(movementWaypoints.waypointArray[i], Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(movementWaypoints, "Change waypoint position");
                    movementWaypoints.waypointArray[i] = newWaypointPosition;
                    serializedObject.Update();
                }

                Handles.Label(newWaypointPosition - new Vector3(0, +0.1f), "Waypoint #" + i);
            }
        }
    }
}

#endif