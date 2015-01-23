using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {
	
	private Transform mainCamera;
	
	void Start () {
		mainCamera = Camera.main.gameObject.transform;
	}

	void Update () {
		transform.LookAt(mainCamera);
	}
}
