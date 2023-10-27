using System;

using UnityEditor;

using UnityEngine;



[AttributeUsage(AttributeTargets.Field)]
public class ReadOnlyTextAttribute : PropertyAttribute 
{
    public int height;
    public int yOffset;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyTextAttribute))]
public class ReadOnlyTextDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUIStyle style = new GUIStyle("Label");
        style.wordWrap = true;
        style.fixedHeight = (attribute as ReadOnlyTextAttribute).height;
        position.y -= (attribute as ReadOnlyTextAttribute).yOffset;

        GUI.enabled = false;
        EditorGUI.TextField(position, property.stringValue, style);
        GUI.enabled = true;
    }
}
#endif