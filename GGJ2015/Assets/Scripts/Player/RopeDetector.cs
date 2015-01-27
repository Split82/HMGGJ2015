using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(Rigidbody2D))]
public class RopeDetector : MonoBehaviour {

	public float _velocityXThreshold = 0.1f;
	public float _velocityYThreshold = -0.1f;
	public LayerMask _ropeLayerMask;

	public event Action<Transform> PlayerDidEnterRopeEvent; // Rope Transform
	public event Action PlayerDidExitRopeEvent;

	public bool PlayerIsInsideRope {
		get {
			return _playerIsOnTheRope;
		}
	}

	private bool _playerIsOnTheRope;
	private Rigidbody2D _rigidbody2D;
	private GameControlsManager _gameControlsManager;

	void Awake() {

		_rigidbody2D = rigidbody2D;
		_gameControlsManager = GameControlsManager.Instance;
	}

	void OnTriggerStay2D(Collider2D other) {

		if (_playerIsOnTheRope) {
			return;
		}

		if (!_ropeLayerMask.ContainsLayer(other.gameObject.layer)) {
			return;
		}

		Vector2 velocity = _rigidbody2D.velocity;
		if (Mathf.Abs(velocity.x) < _velocityXThreshold && velocity.y < _velocityYThreshold && !_gameControlsManager.LeftButtonIsActive && !_gameControlsManager.RightButtonIsActive) {

			_playerIsOnTheRope = true;

			if (PlayerDidEnterRopeEvent != null) {
				PlayerDidEnterRopeEvent(other.transform);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {

		if (!_ropeLayerMask.ContainsLayer(other.gameObject.layer)) {
			return;
		}

		StopAllCoroutines();

		_playerIsOnTheRope = false;

		if (PlayerDidExitRopeEvent != null) {
			PlayerDidExitRopeEvent();
		}
	}

	public void CanHitRopeAgainAfterDelay() {

		StartCoroutine(ResetPlayerInsideTheRopeAfterDelay());
	}

	private IEnumerator ResetPlayerInsideTheRopeAfterDelay() {

		yield return new WaitForSeconds(0.05f);
		_playerIsOnTheRope = false;
	}
}
