using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour {

	public float _groundForce = 200.0f;
	public float _airForce = 50.0f;
	public float _groundFriction = 0.9f;
	public float _airFriction = 0.95f;
	public Vector2 _maxVelocity;
	public GroundedCheck _groundedCheck;

	public bool IsRunning {
		get {
			return _isRunning;
		}
	}

	private Rigidbody2D _rigidBody2D;
	private GameControlsManager _gameControlsManager;
	private bool _isRunning;

	void Start() {
	
		Check.Null(_groundedCheck);
		_rigidBody2D = GetComponent<Rigidbody2D>();
		_gameControlsManager = GameControlsManager.Instance;
	}

	void FixedUpdate() {
			
		Vector2 velocity = _rigidBody2D.velocity;

		float friction;
		float force;

		if (_groundedCheck.IsGrounded) {
			friction = _groundFriction;
			force = _groundForce;
		}
		else {
			friction = _airFriction;
			force = _airForce;
		}

		// Add velocity
		if (_gameControlsManager.LeftButtonIsActive) {
			_isRunning = true;
			velocity -= new Vector2 (1.0f, 0.0f) * force * Time.fixedDeltaTime;
		}
		else if (_gameControlsManager.RightButtonIsActive) {
			_isRunning = true;
			velocity += new Vector2 (1.0f, 0.0f) * force * Time.fixedDeltaTime;
		}
		else {
			_isRunning = false;
			velocity.x *= friction;
		}

		velocity.x = Mathf.Clamp(velocity.x, -_maxVelocity.x, _maxVelocity.x);
		velocity.y = Mathf.Clamp(velocity.y, -_maxVelocity.y, _maxVelocity.y);

		_rigidBody2D.velocity = velocity;
	}
}
