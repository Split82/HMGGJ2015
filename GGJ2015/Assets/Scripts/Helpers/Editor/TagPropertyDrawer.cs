using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
	
[CustomPropertyDrawer(typeof(TagPropertyAttribute))]
public class TagPropertyDrawer : PropertyDrawer {
	
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

		EditorGUI.BeginProperty(position, label, property);
		property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
		EditorGUI.EndProperty();
	}
}
	
