using UnityEngine;
using System.Collections;

public class FollowLocalRotation : MonoBehaviour {

	public Transform _target;

	private Transform _transform;
	
	void Awake() {

		_transform = transform;
	}

	void Update() {
	
		_transform.localRotation = _target.localRotation;
	}
}
