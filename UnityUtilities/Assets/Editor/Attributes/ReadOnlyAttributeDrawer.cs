﻿using UnityEditor;
using UnityEngine;
using UnityUtilities.Attributes;


namespace UnityUtilities.Attributes
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (var scope = new EditorGUI.DisabledGroupScope(true))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
    }


    [CustomPropertyDrawer(typeof(EndReadOnlyGroupAttribute))]
    public class EndReadOnlyGroupDrawer : DecoratorDrawer
    {
        public override float GetHeight()
        {
            return 0;
        }

        public override void OnGUI(Rect position)
        {
            EditorGUI.EndDisabledGroup();
        }
    }

    [CustomPropertyDrawer(typeof(BeginReadOnlyGroupAttribute))]
    public class BeginReadOnlyGroupDrawer : DecoratorDrawer
    {
        public override float GetHeight()
        {
            return 0;
        }

        public override void OnGUI(Rect position)
        {
            EditorGUI.BeginDisabledGroup(true);
        }
    }
}
