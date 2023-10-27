using System;
using UnityEngine;

namespace TheAshBot
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// will Copy a component onto another gamobject
        /// </summary>
        /// <typeparam name="T">Is the typr of the component being copyed</typeparam>
        /// <param name="destination">is the gamobject that the component is being copyed to.</param>
        /// <param name="original">is the original component</param>
        /// <returns>the copyed compoent</returns>
        public static T CopyComponent<T>(this GameObject destination, T original) where T : Component
        {
            // get the type of the component;
            Type componentType = original.GetType();

            // adding the component to the gamobject
            Component copy = destination.AddComponent(componentType);

            // Getting the fields from the original component
            System.Reflection.FieldInfo[] fields = componentType.GetFields();

            // cycling through all the field in the original component
            foreach (System.Reflection.FieldInfo field in fields)
            {
                // setting the new copmponent value to equal the same as the original compinent's value for that field
                field.SetValue(copy, field.GetValue(original));
            }

            // returning the new component
            return copy as T;
        }

        /// <summary>
        /// will Copy a component onto another gamobject
        /// </summary>
        /// <param name="destination">is the gamobject that the component is being copyed to.</param>
        /// <param name="original">is the original component</param>
        /// <returns>the copyed compoent</returns>
        public static Component CopyComponent(this GameObject destination, Component original)
        {
            // get the type of the component;
            Type type = original.GetType();

            // adding the component to the gamobject
            Component copy = destination.AddComponent(type);

            // Getting the fields from the original component
            System.Reflection.FieldInfo[] fields = type.GetFields();

            // cycling through all the field in the original component
            foreach (System.Reflection.FieldInfo field in fields)
            {
                // setting the new copmponent value to equal the same as the original compinent's value for that field
                field.SetValue(copy, field.GetValue(original));
            }

            // returning the new component
            return copy;
        }

        /// <summary>
        /// will set the global scale of a transfrom
        /// </summary>
        /// <param name="transform">is the transfrom that the globel scal is being set to</param>
        /// <param name="newGlobalScale">is the globel scale that the transfrom is being set to.</param>
        public static void SetGlobalScale(this Transform transform, Vector3 newGlobalScale)
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(newGlobalScale.x / transform.lossyScale.x, newGlobalScale.y / transform.lossyScale.y, newGlobalScale.z / transform.lossyScale.z);
        }

        /// <summary>
        /// will set the global scale of a transfrom
        /// </summary>
        /// <param name="transform">is the transfrom that the globel scal is being set to</param>
        /// <param name="newGlobalScale">is the globel scale that the transfrom is being set to.</param>
        public static void SetGlobalScale(this Transform transform, Vector2 newGlobalScale)
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(newGlobalScale.x / transform.lossyScale.x, newGlobalScale.y / transform.lossyScale.y, 1);
        }

        /// <summary>
        /// will Rotate the vector3
        /// </summary>
        /// <param name="originalVector">is the vector being rotated</param>
        /// <param name="rotation">is the rotation that will be applied to the vector</param>
        /// <returns>the rotated vector</returns>
        public static Vector3 Rotate(this Vector3 originalVector, Quaternion rotation)
        {
            return rotation * originalVector;
        }
        
        /// <summary>
        /// will Rotate the vector2
        /// </summary>
        /// <param name="originalVector">is the vector being rotated</param>
        /// <param name="rotation">is the rotation that will be applied to the vector</param>
        /// <returns>the rotated vector</returns>
        public static Vector2 Rotate(this Vector2 originalVector, Quaternion rotation)
        {
            return rotation * originalVector;
        }

    }
}
