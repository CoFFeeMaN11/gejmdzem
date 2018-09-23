using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
public class EnumFlagsAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect _position, UnityEditor.SerializedProperty _property, GUIContent _label)
    {
        // Change check is needed to prevent values being overwritten during multiple-selection
        UnityEditor.EditorGUI.BeginChangeCheck();
        int newValue = UnityEditor.EditorGUILayout.MaskField(_property.intValue, _property.enumNames);
        if (UnityEditor.EditorGUI.EndChangeCheck())
        {
            _property.intValue = newValue;
        }
    }
}
