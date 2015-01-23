using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(GroundedCheck))]
[RequireComponent (typeof(Rigidbody2D))]

public class PlayerJump : MonoBehaviour {

	public float _startJumpEnergy = 10.0f;
	public float _jumpVelocity = 4.0f;
	public float _jumpEnergyDepletionMul = 0.5f;

	public event Action<int> PlayerDidJumpEvent; // Parameter is number of jumps since player hit the ground. 0 - from ground jump, 1 - second jump

	public int NumberOfJumpsSinceGrounded {
		get {
			return _numberOfJumpsSinceGrounded;
		}
	}

	private float _jumpEnergy;
	private float _canGroundTimer;
	private int _numberOfJumpsSinceGrounded;
	private bool _nextJumpWillBeJumpStart;
	private bool _didReleaseJumpButton;
	private GroundedCheck _groundCheck;

	private Rigidbody2D _rigidbody2D;

	private const float kCanGroundTimeInterval = 0.05f;

	void Awake() {

		_groundCheck = GetComponent<GroundedCheck>();
		_rigidbody2D = rigidbody2D;
	}
	
	void FixedUpdate() {

		// Ground Timer
		_canGroundTimer -= Time.fixedDeltaTime;
		if (_canGroundTimer < 0.0f) {
			_canGroundTimer = 0.0f;
		}
		
		// Check ground
		if (_canGroundTimer == 0.0f) {

			if (_groundCheck.IsGrounded) {
				_numberOfJumpsSinceGrounded = 0;
			}
		}
		
		// Jump
		if (GameControlsManager.Instance.JumpButtonIsActive) {
			
			// Start of the jump or second jump
			if (_didReleaseJumpButton && _numberOfJumpsSinceGrounded < 2) {

				if (PlayerDidJumpEvent != null) {
					PlayerDidJumpEvent(_numberOfJumpsSinceGrounded);
				}

				_numberOfJumpsSinceGrounded++;
				
				_jumpEnergy = _startJumpEnergy;
				
				_canGroundTimer = kCanGroundTimeInterval;
				Vector2 velocity = _rigidbody2D.velocity;
				velocity.y = _jumpVelocity;
				_rigidbody2D.velocity = velocity;

			}
			// Continuing jump
			else {
				_rigidbody2D.AddForce(Vector2.up * _jumpEnergy);
			}
			
			// Deplete energy in the air
			_jumpEnergy *= _jumpEnergyDepletionMul;
			
			_didReleaseJumpButton = false;
		}
		else {
			_jumpEnergy = 0.0f;
			_didReleaseJumpButton = true;
		}
	}
}
