using UnityEngine;
using System.Collections;

public class StayForeverCamera : MonoBehaviour {
	
	private int _numberOfRenderedFrames;
	
	void Start() {
		
		camera.clearFlags = CameraClearFlags.SolidColor;
	}
	
	void Update() {
		
		_numberOfRenderedFrames++;
		
		if (_numberOfRenderedFrames > 2) {
			Debug.Log ("asdasd");
			camera.clearFlags = CameraClearFlags.Depth;
			enabled = false;
		}
	}
}
