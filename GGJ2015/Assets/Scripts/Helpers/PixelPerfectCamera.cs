using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PixelPerfectCamera : Singleton<PixelPerfectCamera> {

	public Transform _parentTransform;
	public Camera[] _cameras;
	public int _scale = 4;
	public int pixelsPerUnit = 48;

	private float _offset;

	void Awake() {

		RefreshSizes();
	}

	void LateUpdate() {

		RefreshSizes();
	}

	void RefreshSizes() {

		Vector3 parentPos = _parentTransform.position;
		parentPos.x = (Mathf.Round(parentPos.x * pixelsPerUnit * _scale) + 0.5f) / (pixelsPerUnit * _scale);
		parentPos.y = (Mathf.Round(parentPos.y * pixelsPerUnit * _scale) + 0.5f) / (pixelsPerUnit * _scale);
		
		for (int i = 0; i < _cameras.Length; i++) {
			_cameras[i].orthographicSize = (Screen.height / (float)pixelsPerUnit) / (float)_scale;
			_cameras[i].transform.position = parentPos;
		}
	}
}
