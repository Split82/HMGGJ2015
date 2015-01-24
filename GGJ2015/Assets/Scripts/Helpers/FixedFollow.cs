using UnityEngine;
using System.Collections;

public class FixedFollow: MonoBehaviour {

	public GameObject followObject;
	public Vector3 offset;
	
	private Transform followTransform;
	private Transform thisTransform;
	
	void Start() {

		thisTransform = this.transform;
		followTransform = followObject.transform;
		thisTransform.position = followTransform.position + offset;	
	}
	
	void LateUpdate() {

		thisTransform.position = followTransform.position + offset;	
	}
}
