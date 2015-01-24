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

		Vector3 camCenter = gameObject.transform.parent.position;
		Vector3 bottomLeft = selfCamera.ViewportToWorldPoint (new Vector3 (0, 0, 10));
		Vector3 topRight = selfCamera.ViewportToWorldPoint (new Vector3 (1, 1, 10));

		float unitsPerRealPixel = (topRight.y - bottomLeft.y) / Screen.height;
		camCenter.x = (int)(camCenter.x / unitsPerRealPixel) * unitsPerRealPixel;
		camCenter.y = (int)(camCenter.y / unitsPerRealPixel) * unitsPerRealPixel;

		gameObject.transform.position = camCenter;
	}
}
