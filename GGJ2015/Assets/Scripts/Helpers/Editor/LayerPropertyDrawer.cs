using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(LayerPropertyAttribute))]

public class LayerPropertyDrawer : PropertyDrawer {
	
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		
		EditorGUI.BeginProperty(position, label, property);
		property.intValue = EditorGUI.LayerField(position, label, property.intValue);
		EditorGUI.EndProperty();		
	}
}
