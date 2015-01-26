using UnityEngine;
using System.Collections;

public class AltarGuidingArrow : MonoBehaviour {

	public Transform _playerTransform;
	public SpriteRenderer _arrowSpriteRenderer;
	public float _radius = 0.8f;	
	public float _minVisibleDistance = 10.0f;
	public float _fadeOutDistance = 20.0f;
	public Color _color = Color.white;

	private Transform _altarWithCardTransform;
	private Transform _transform;

	void Start() {

		Check.Null(_playerTransform);
		Check.Null(_arrowSpriteRenderer);

		_transform = transform;

		Altar altarWithCard = AltarManager.Instance.AltarWithCard;
		if (altarWithCard) {
			_altarWithCardTransform = altarWithCard.transform;
		}
		AltarManager.Instance.CardHasSpawnedOnAltarEvent += () => {
			_altarWithCardTransform = AltarManager.Instance.AltarWithCard.transform;
		};
	}

	void Update() {
	
		if (_altarWithCardTransform == null) {
			return;
		}

		Vector3 altarDir = _altarWithCardTransform.position - _playerTransform.position;
		float sqrDistance = altarDir.sqrMagnitude;
		if (sqrDistance < _minVisibleDistance * _minVisibleDistance) {
			_arrowSpriteRenderer.color = Color.clear;
			return;
		}

		float distance = Mathf.Sqrt(sqrDistance);

		Color color = _color;
		color.a *= Mathf.Clamp01((distance - _minVisibleDistance) / (_fadeOutDistance - _minVisibleDistance));
		_arrowSpriteRenderer.color = color;

		_transform.localPosition = _radius * altarDir / distance;
		float angle = Vector3.Angle(Vector3.right, altarDir);
		if (altarDir.y < 0.0f) {
			angle *= -1.0f;
		}
		_transform.localEulerAngles = new Vector3(0.0f, 0.0f, angle);
	}
}
