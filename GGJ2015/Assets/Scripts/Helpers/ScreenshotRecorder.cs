using UnityEngine;
using System.Collections;

public class ScreenshotRecorder : MonoBehaviour {

	public string folder = "ScreenshotFolder";
	public int frameRate = 60;

	void Start() {
		Time.captureFramerate = frameRate;
		System.IO.Directory.CreateDirectory(folder);
	}

	void Update() {
		string name = string.Format("{0}/{1:D04} shot.png", folder, Time.frameCount);
		Application.CaptureScreenshot(name);
	}
}