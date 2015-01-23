using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
public class HorizontalCameraFov : MonoBehaviour {
		
	public float _horizontalFOV;

	void Awake () {
		camera.fieldOfView = Mathf.Rad2Deg * (2.0f * Mathf.Atan(Mathf.Tan(_horizontalFOV * Mathf.Deg2Rad * 0.5f) / camera.aspect));
	}

}
