using UnityEngine;
using System.Collections;
using UnityEditor;

public class SceneUtils : MonoBehaviour {

	[MenuItem ("File/Load Scene [Additive]")]
	static void LoadSceneAdditive() {

		var strScenePath = AssetDatabase.GetAssetPath(Selection.activeObject);
		if (strScenePath == null) {
			EditorUtility.DisplayDialog("Select Scene", "You Must Select a Scene first!", "Ok");
			return;
		}
		
		Debug.Log("Opening " + strScenePath + " additively");
		EditorApplication.OpenSceneAdditive(strScenePath);
	}
}