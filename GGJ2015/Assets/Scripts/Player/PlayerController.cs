using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Rigidbody2D _rigidbody2D;
	public Transform _playerTransform;
	public RopeDetector _ropeDetector;
	public PlayerJump _playerJump;
	public PlayerMovement _playerMovement;
	public PlayerRopeMovement _playerRopeMovement;
	public FireStaff _fireStaff;
	public GroundedCheck _groundedCheck;

	public enum StateEnum {
		Undefined,
		Playing,
		OnTheRope,
	}

	public StateEnum State {
		get {
			return _state;
		}
		private set {

			if (_state == value) {
				return;
			}

			switch (value) {

			case StateEnum.Playing:
				_rigidbody2D.gravityScale = _defaultGravityScale;
				_playerMovement.enabled = true;
				_fireStaff.enabled = true;
				_playerJump.enabled = true;
				_playerRopeMovement.enabled = false;
				break;

			case StateEnum.OnTheRope:
				_rigidbody2D.gravityScale = 0.0f;
				_playerMovement.enabled = false;
				_playerJump.enabled = false;
				_fireStaff.enabled = false;
				_playerRopeMovement.enabled = true;
				break;
			}

			_state = value;
		}
	}

	private StateEnum _state;
	private float _defaultGravityScale;

	public HitDetector _receiveDamageHitDetector;

	void Awake() {

		_defaultGravityScale = _rigidbody2D.gravityScale;

		Check.Null(_rigidbody2D);
		Check.Null(_playerTransform);
		Check.Null(_ropeDetector);
		Check.Null(_playerJump);
		Check.Null(_playerMovement);
		Check.Null(_fireStaff);
		Check.Null(_playerRopeMovement);
		Check.Null(_groundedCheck);
		Check.Null(_receiveDamageHitDetector);
	}

	void Start() {

		State = StateEnum.Playing;

		_ropeDetector.PlayerDidEnterRopeEvent += () => {
			if (!_groundedCheck.IsGrounded) {
				State = StateEnum.OnTheRope;
			}
			else {
				_ropeDetector.CanHitRopeAfterDelay();
			}
		};

		_ropeDetector.PlayerDidExitRopeEvent += () => {
			State = StateEnum.Playing;
		};	

		_playerRopeMovement.PlayerDidExitRopeWithJumpEvent += (bool jump) => {
			_ropeDetector.CanHitRopeAfterDelay();
			State = StateEnum.Playing;
			if (jump) {
				_playerJump.ForcedJump();
			}
		};

		_receiveDamageHitDetector.TriggerDidEnterEvent += ReceiveDamageTriggerDidEnter;
	}

	void ReceiveDamageTriggerDidEnter(GameObject triggerObject, GameObject triggeredObject) {

		switch (triggerObject.tag) {
			case "BasicEmeny" : {
				break;
			}
		}
	}
}
