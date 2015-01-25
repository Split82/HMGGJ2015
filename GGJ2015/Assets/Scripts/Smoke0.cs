using UnityEngine;
using System.Collections;

public class Smoke0 : MonoBehaviour {

	private GlobalEffects _globalEffects;
	private Transform _transform;

	void Start () {

		_transform = transform;
		_globalEffects = GlobalEffects.Instance;
	}

	void Update () {
	
		_globalEffects.EmitSmoke0Particle(_transform.position, 1);
	}
}
