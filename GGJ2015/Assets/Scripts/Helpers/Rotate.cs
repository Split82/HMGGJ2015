using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public Vector3 _rotationVector = new Vector3(0.0f, 1.0f, 0.0f);
	public float _speed = 1.0f;

	private Transform _transform;
	private Vector3 _startRotationAngles;
	
	void Awake() {

		_transform = transform;
		_startRotationAngles = _transform.localEulerAngles;

		if (GetComponent<MeshRenderer>() != null) {
			enabled = false;
		}
	}

	void OnBecameVisible() {

		enabled = true;
	}

	void OnBecameInvisible() {

		enabled = false;
	}

	void Update() {

		_transform.localEulerAngles = _startRotationAngles + _rotationVector * _speed * Time.timeSinceLevelLoad;
	}

}
