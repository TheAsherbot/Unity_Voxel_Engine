#if UNITY_EDITOR
using UnityEditor;

using UnityEngine;

namespace TheAshBot.TwoDimentional.TopDownCharcterMovement.Editors
{
    [CustomEditor(typeof(TopDownCharcterMovement2D))]
    public class TopDownCharcterMovement2DEditor : Editor
    {


        TopDownCharcterMovement2D topDownCharcterMovement;



        private void OnEnable()
        {
            topDownCharcterMovement = (TopDownCharcterMovement2D)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (!topDownCharcterMovement.TryGetComponent(out IMoveVelocity2D moveVelocity))
            {
                if (GUILayout.Button("Add Movement Velocity"))
                {
                    topDownCharcterMovement.AddMovementVelocity();
                }
            }
            if (!topDownCharcterMovement.TryGetComponent(out IPlayerMovementInput2D playerMovementInput))
            {
                if (GUILayout.Button("Add Movement Input"))
                {
                    topDownCharcterMovement.AddMovementInput();
                }
            }
            if (!topDownCharcterMovement.TryGetComponent(out IMovePosition2D movePosition))
            {
                if (GUILayout.Button("Add Movement Position"))
                {
                    topDownCharcterMovement.AddMovementPosition();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }


    }
}
#endif