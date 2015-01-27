using UnityEngine;
using System.Collections;

public class FlashOnHit : MonoBehaviour {

	public WhiteSpriteFlash _whiteSpriteFlash;
	public float _flashDuration = 0.1f;

	void Awake() {

		Check.Null(_whiteSpriteFlash);
	}

	void OnCollisionEnter2D(Collision2D col) {

		_whiteSpriteFlash.Flash(_flashDuration);
	}
}
