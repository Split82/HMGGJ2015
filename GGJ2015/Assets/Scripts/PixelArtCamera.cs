using UnityEngine;
using System.Collections;

public class PixelArtCamera : MonoBehaviour {

	public int _pixelMultiplier = 2;
	public int _pixelsPerUnit = 48;

	void Start () {
	
	}
	
	void Update () {

		Camera selfCamera = gameObject.GetComponent<Camera> ();
		selfCamera.fieldOfView = Mathf.Atan (((float)Screen.height / (_pixelsPerUnit * _pixelMultiplier)) / -gameObject.transform.position.z) * Mathf.Rad2Deg;
	}
}
