using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyInInspectorAttribute))]
    public class ReadOnlyInInspectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string valueStr;
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    valueStr = property.intValue.ToString();
                    break;
                case SerializedPropertyType.Boolean:
                    valueStr = property.boolValue.ToString();
                    break;
                case SerializedPropertyType.Float:
                    valueStr = property.floatValue.ToString("0.00000");
                    break;
                case SerializedPropertyType.String:
                    valueStr = property.stringValue;
                    break;
                case SerializedPropertyType.Enum:
                    valueStr = property.enumDisplayNames[property.enumValueIndex];
                    break;
                case SerializedPropertyType.Vector2:
                    valueStr = property.vector2Value.ToString();
                    break;
                case SerializedPropertyType.Vector3:
                    valueStr = property.vector3Value.ToString();
                    break;
                case SerializedPropertyType.ObjectReference:
                    try
                    {
                        valueStr = property.objectReferenceValue.ToString();
                    }
                    catch (NullReferenceException)
                    {
                        valueStr = "None (Game Object)";
                    }
                    break;
                default:
                    valueStr = "(not supported)";
                    break;
            }

            EditorGUI.LabelField(position, label.text, valueStr);
        }
    }
}