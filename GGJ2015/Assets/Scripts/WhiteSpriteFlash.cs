using UnityEngine;
using System.Collections;

public class WhiteSpriteFlash : MonoBehaviour {

	public SpriteRenderer[] _spriteRenderers;
	public Material _whiteMaterial;
	public Material _defaultMaterial;

	private bool _canFlashAgain;

	void Awake() {

		Check.Array(_spriteRenderers, 1, false);
		Check.Null(_whiteMaterial);
		Check.Null(_defaultMaterial);
		_canFlashAgain = true;
	}

	public void Flash(float duration) {

		if (!_canFlashAgain) {
			return;
		}

		_canFlashAgain = false;

		StartCoroutine(FlashCoroutine(duration));
	}

	private IEnumerator FlashCoroutine(float duration) {

		for (int i = 0; i < _spriteRenderers.Length; i++) {
			_spriteRenderers[i].sharedMaterial = _whiteMaterial;
		}

		yield return new WaitForSeconds(duration);

		for (int i = 0; i < _spriteRenderers.Length; i++) {
			_spriteRenderers[i].sharedMaterial = _defaultMaterial;
		}

		_canFlashAgain = true;
	}
}
