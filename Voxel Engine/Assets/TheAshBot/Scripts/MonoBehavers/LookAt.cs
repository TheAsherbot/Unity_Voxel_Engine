using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheAshBot.MonoBehaviours
{
    public class LookAt : MonoBehaviour
    {


        public enum Mode
        {
            LookAtFallow,
            LookAtFallowInverted,
            FallowForward,
            FallowForwardInverted,
        }


        public Mode mode;
        public Transform lootAt;


        private void LateUpdate()
        {
            switch (mode)
            {
                case Mode.LookAtFallow:
                    transform.LookAt(lootAt);
                    break;
                case Mode.LookAtFallowInverted:
                    Vector3 directionFromCamera = transform.position - lootAt.position;
                    transform.LookAt(transform.position + directionFromCamera);
                    break;
                case Mode.FallowForward:
                    transform.forward = lootAt.forward;
                    break;
                case Mode.FallowForwardInverted:
                    transform.forward = -lootAt.forward;
                    break;
            }
        }


    }
}
