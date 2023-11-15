using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

namespace TheAshBot.ThreeDimentional
{
    public static class Mouse3D
    {

        private static Vector2 MousePosition
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return UnityEngine.InputSystem.Mouse.current.position.ReadValue();
#elif ENABLE_LEGACY_INPUT_MANAGER
                return Input.mousePosition;
#else
                return Vector2.zero;
#endif
            }
        }

        #region GetObjectAtMousePosition

        #region Normal

        /// <summary>
        /// This will try to get a gameobject at the mouse World Position.
        /// </summary>
        /// <param name="camera">This is the camera that it takes the mouse position from.</param>
        /// <param name="hit">This is th gameobject that the mouse is over.</param>
        /// <returns>true if the mosue is over a gameobject.</returns>
        public static bool TryGetObjectAtMousePosition(Camera camera, out GameObject hit)
        {
            Ray ray = camera.ScreenPointToRay(MousePosition);
            UnityEngine.Physics.Raycast(ray, out RaycastHit raycastHit);

            if (raycastHit.transform != null)
            {
                hit = raycastHit.transform.gameObject;
                return true;
            }

            hit = null;
            return false;
        }

        /// <summary>
        /// This will try to get a gameobject at the mouse World Position. 
        /// </summary>
        /// <param name="hit">This is th gameobject that the mouse is over.</param>
        /// <returns>true if the mosue is over a gameobject.</returns>
        public static bool TryGetObjectAtMousePosition(out GameObject hit)
        {
            return TryGetObjectAtMousePosition(Camera.main, out hit);
        }

        #endregion

        #region Ingore Component

        /// <summary>
        /// This will try to get a gameobject at the mouse World Position that does not has a component.
        /// </summary>
        /// <typeparam name="T">This is the compentent it ingores</typeparam>
        /// <param name="camera">This is the camera that it takes the mouse position from.</param>
        /// <param name="hit">This is th gameobject that the mouse is over.</param>
        /// <returns>true if the mosue is over a gameobject.</returns>
        public static bool TryGetObjectAtMousePositionIngoreComponent<T>(Camera camera, out GameObject hit) where T : Component
        {
            Ray ray = camera.ScreenPointToRay(MousePosition);
            RaycastHit[] raycastHitArray = UnityEngine.Physics.RaycastAll(ray);

            foreach (RaycastHit raycastHit in raycastHitArray)
            {
                if (!raycastHit.transform.TryGetComponent(out T t))
                {
                    hit = raycastHit.transform.gameObject;
                    return true;
                }
            }

            hit = null;
            return false;
        }

        /// <summary>
        /// This will try to get a gameobject at the mouse World Position that does not has a component. 
        /// </summary>
        /// <typeparam name="T">This is the compentent it ingores</typeparam>
        /// <param name="hit">This is th gameobject that the mouse is over.</param>
        /// <returns>true if the mosue is over a gameobject.</returns>
        public static bool TryGetObjectAtMousePositionIngoreComponent<T>(out GameObject hit) where T : Component
        {
            return TryGetObjectAtMousePositionIngoreComponent<T>(Camera.main, out hit);
        }

        #endregion

        #region With Component

        /// <summary>
        /// This will try to get a gameobject at the mouse World Position that has a component.
        /// </summary>
        /// <typeparam name="T">This is the component that it has to have</typeparam>
        /// <param name="camera">This is the camera that it takes the mouse position from.</param>
        /// <param name="hit">This is th gameobject that the mouse is over.</param>
        /// <returns>true if the mosue is over a gameobject.</returns>
        public static bool TryGetObjectAtMousePositionWithComponent<T>(Camera camera, out GameObject hit) where T : Component
        {
            Ray ray = camera.ScreenPointToRay(MousePosition);
            RaycastHit[] raycastHitArray = UnityEngine.Physics.RaycastAll(ray);

            foreach (RaycastHit raycastHit in raycastHitArray)
            {
                if (raycastHit.transform.TryGetComponent(out T t))
                {
                    hit = raycastHit.transform.gameObject;
                    return true;
                }
            }

            hit = null;
            return false;
        }

        /// <summary>
        /// This will try to get a gameobject at the mouse World Position that has a component.
        /// </summary>
        /// <typeparam name="T">This is the component that it has to have</typeparam>
        /// <param name="camera">This is the camera that it takes the mouse position from.</param>
        /// <param name="hit">This is th gameobject that the mouse is over.</param>
        /// <returns>true if the mosue is over a gameobject.</returns>
        public static bool TryGetObjectAtMousePositionWithComponent<T>(out GameObject hit) where T : Component
        {
            return TryGetObjectAtMousePositionIngoreComponent<T>(Camera.main, out hit);
        }

        #endregion

        #endregion

        #region Mouse Position Vector3

        #region GetMousePosition
        /// <summary>
        /// This gets the mouse position in 3D using a raycast
        /// </summary>
        /// <param name="camera">This is the camera that the raycasts shoots from</param>
        /// <param name="layerMask">This is the layers that the raycast egnors</param>
        /// <returns>This return the the point that the raycast hits</returns>
        public static Vector3 GetMousePosition3D(Camera camera, LayerMask layerMask)
        {
            Ray ray = camera.ScreenPointToRay(MousePosition);
            Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask);
            return raycastHit.point;
        }

        /// <summary>
        /// This gets the mouse position in 3D using a raycast
        /// </summary>
        /// <param name="camera">This is the camera that the raycasts shoots from</param>
        /// <returns>This return the the point that the raycast hits</returns>
        public static Vector3 GetMousePosition3D(Camera camera)
        {
            Ray ray = camera.ScreenPointToRay(MousePosition);
            Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue);
            return raycastHit.point;
        }

        /// <summary>
        /// This gets the mouse position in 3D using a raycast
        /// </summary>
        /// <param name="layerMask">This is the layers that the raycast egnors</param>
        /// <returns>This return the the point that the raycast hits</returns>
        public static Vector3 GetMousePosition3D(LayerMask layerMask)
        {
            return GetMousePosition3D(Camera.main, layerMask);
        }

        /// <summary>
        /// This gets the mouse position in 3D using a raycast
        /// </summary>
        /// <returns>This return the the point that the raycast hits</returns>
        public static Vector3 GetMousePosition3D()
        {
            return GetMousePosition3D(Camera.main);
        }
        #endregion

        #region DebugLogMousePosition
        /// <summary>
        /// This logs the mouse position in 3D using a raycast
        /// </summary>
        /// <param name="camera">This is the camera that the raycasts shoots from</param>
        /// <param name="layerMask">This is the layers that the raycast egnors</param>
        public static void DebugLogMousePosition3D(Camera camera, LayerMask layerMask)
        {
            Debug.Log("MousePosition3D = " + GetMousePosition3D(camera, layerMask));
        }

        /// <summary>
        /// This logs the mouse position in 3D using a raycast
        /// </summary>
        /// <param name="camera">This is the camera that the raycasts shoots from</param>
        public static void DebugLogMousePosition3D(Camera camera)
        {
            Debug.Log("MousePosition3D = " + GetMousePosition3D(camera));
        }

        /// <summary>
        /// This logs the mouse position in 3D using a raycast
        /// </summary>
        /// <param name="layerMask">This is the layers that the raycast egnors</param>
        public static void DebugLogMousePosition3D(LayerMask layerMask)
        {
            Debug.Log("MousePosition3D = " + GetMousePosition3D(layerMask));
        }

        /// <summary>
        /// This logs the mouse position in 3D using a raycast
        /// </summary>
        public static void DebugLogMousePosition3D()
        {
            Debug.Log("MousePosition3D = " + GetMousePosition3D());
        }
        #endregion

        #endregion

        #region Mouse Position Vector3Int

        #region GetMousePositionInt
        /// <summary>
        /// Using a raycast to gets this the mouse position in 3D and round it to an int
        /// </summary>
        /// <param name="camera">This is the camera that the raycasts shoots from</param>
        /// <param name="layerMask">This is the layers that the raycast egnors</param>
        /// <returns>This return the the point that the raycast hits Rounded to and int</returns>
        public static Vector3Int GetMousePositionInt3D(Camera camera, LayerMask layerMask)
        {
            return Vector3Int.RoundToInt(GetMousePosition3D(camera, layerMask));
        }

        /// <summary>
        /// Using a raycast to gets this the mouse position in 3D and round it to an int
        /// </summary>
        /// <param name="camera">This is the camera that the raycasts shoots from</param>
        /// <returns>This return the the point that the raycast hits Rounded to and int</returns>
        public static Vector3Int GetMousePositionInt3D(Camera camera)
        {
            return Vector3Int.RoundToInt(GetMousePosition3D(camera));
        }

        /// <summary>
        /// Using a raycast to gets this the mouse position in 3D and round it to an int
        /// </summary>
        /// <param name="layerMask">This is the layers that the raycast egnors</param>
        /// <returns>This return the the point that the raycast hits Rounded to and int</returns>
        public static Vector3Int GetMousePositionInt3D(LayerMask layerMask)
        {
            return Vector3Int.RoundToInt(GetMousePosition3D(layerMask));
        }

        /// <summary>
        /// Using a raycast to gets this the mouse position in 3D and round it to an int
        /// </summary>
        /// <returns>This return the the point that the raycast hits Rounded to and int</returns>
        public static Vector3Int GetMousePositionInt3D()
        {
            return Vector3Int.RoundToInt(GetMousePosition3D());
        }
        #endregion

        #region DebugLogMousePositionInt
        /// <summary>
        ///  using a raycast this logs the mouse position in 3D rounded to a vector3 int
        /// </summary>
        /// <param name="camera">This is the camera that the raycasts shoots from</param>
        /// <param name="layerMask">This is the layers that the raycast egnors</param>
        public static void DebugLogMousePositionInt3D(Camera camera, LayerMask layerMask)
        {
            Debug.Log("MousePositionInt3D = " + GetMousePositionInt3D(camera, layerMask));
        }

        /// <summary>
        ///  using a raycast this logs the mouse position in 3D rounded to a vector3 int
        /// </summary>
        /// <param name="camera">This is the camera that the raycasts shoots from</param>
        public static void DebugLogMousePositionInt3D(Camera camera)
        {
            Debug.Log("MousePositionInt3D = " + GetMousePositionInt3D(camera));
        }

        /// <summary>
        ///  using a raycast this logs the mouse position in 3D rounded to a vector3 int
        /// </summary>
        /// <param name="layerMask">This is the layers that the raycast egnors</param>
        public static void DebugLogMousePositionInt3D(LayerMask layerMask)
        {
            Debug.Log("MousePositionInt3D = " + GetMousePositionInt3D(layerMask));
        }

        /// <summary>
        ///  using a raycast this logs the mouse position in 3D rounded to a vector3 int
        /// </summary>
        public static void DebugLogMousePositionInt3D()
        {
            Debug.Log("MousePositionInt3D = " + GetMousePositionInt3D());
        }
        #endregion

        #endregion

        #region IsMouseOverUI

        /// <summary>
        /// This checks to see if the mouse is over UI while ingoring a script.
        /// </summary>
        /// <typeparam name="T">This is the Component that the is being ingnored.</typeparam>
        /// <returns>true if the mouse is over the UI without the script.</returns>
        public static bool IsMouseOverUIIngoreComponent<T>() where T : Component
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = MousePosition;

            List<RaycastResult> raycastResultList = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
            for (int raycastNumber = 0; raycastNumber < raycastResultList.Count; raycastNumber++)
            {
                if (raycastResultList[raycastNumber].gameObject.GetComponent<T>() != null)
                {
                    raycastResultList.RemoveAt(raycastNumber);
                    raycastNumber--;
                }
            }

            return raycastResultList.Count > 0;
        }

        /// <summary>
        /// This checks to see if the mouse is over UI with a script.
        /// </summary>
        /// <typeparam name="T">This is the Component that the UI has to have.</typeparam>
        /// <returns>true if the mouse is over the UI with the script.</returns>
        public static bool IsMouseOverUIWithComponent<T>() where T : Component
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = MousePosition;

            List<RaycastResult> raycastResultList = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
            for (int raycastNumber = 0; raycastNumber < raycastResultList.Count; raycastNumber++)
            {
                if (raycastResultList[raycastNumber].gameObject.GetComponent<T>() != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This checks to see if the mouse is over UI.
        /// </summary>
        /// <returns>true if the mouse is over the UI.</returns>
        public static bool IsMouseOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        #endregion

    }

}
