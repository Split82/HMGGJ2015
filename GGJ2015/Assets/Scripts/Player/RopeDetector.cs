using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(Rigidbody2D))]
public class RopeDetector : MonoBehaviour {

	public float _velocityXThreshold = 0.1f;
	public float _velocityYThreshold = -0.1f;
	public LayerMask _ropeLayerMask;

	public event Action PlayerDidEnterRopeEvent;
	public event Action PlayerDidExitRopeEvent;

	public bool PlayerIsInsideRope {
		get {
			return _playerIsInsideTheRope;
		}
	}

	private bool _playerIsInsideTheRope;
	private Rigidbody2D _rigidbody2D;
	private GameControlsManager _gameControlsManager;

	void Awake() {

		_rigidbody2D = rigidbody2D;
		_gameControlsManager = GameControlsManager.Instance;
	}

	void OnTriggerStay2D(Collider2D other) {

		if ((_ropeLayerMask.value & 1 << other.gameObject.layer) == 0) {
			return;
		}

		if (_playerIsInsideTheRope) {
			return;
		}

		Vector2 velocity = _rigidbody2D.velocity;
		if (Mathf.Abs(velocity.x) < _velocityXThreshold && velocity.y < _velocityYThreshold && !_gameControlsManager.LeftButtonIsActive && !_gameControlsManager.RightButtonIsActive) {

			_playerIsInsideTheRope = true;
			if (PlayerDidEnterRopeEvent != null) {
				PlayerDidEnterRopeEvent();
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {

		if ((_ropeLayerMask.value & 1 << other.gameObject.layer) == 0) {
			return;
		}

		StopAllCoroutines();

		_playerIsInsideTheRope = false;
		if (PlayerDidExitRopeEvent != null) {
			PlayerDidExitRopeEvent();
		}
	}

	public void CanHitRopeAgainAfterDelay() {

		StartCoroutine(ResetPlayerInsideTheRopeAfterDelay());
	}

	private IEnumerator ResetPlayerInsideTheRopeAfterDelay() {

		yield return new WaitForSeconds(0.05f);
		_playerIsInsideTheRope = false;
	}
}
