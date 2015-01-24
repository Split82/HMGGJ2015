using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(Rigidbody2D))]

public class PlayerRopeMovement : MonoBehaviour {
	
	public float _force;
	public float _friction;
	public float _maxVelocity;
	public float _exitTime = 0.1f;

	public event Action<bool> PlayerDidExitRopeWithJumpEvent;

	public bool IsClimbing {
		get {
			return Mathf.Abs(_rigidBody2D.velocity.y) > 0.1f;
		}
	}
		
	private Rigidbody2D _rigidBody2D;
	private GameControlsManager _gameControlsManager;
	private float _exitTimer;

	void Start() {

		_rigidBody2D = GetComponent<Rigidbody2D>();
		_gameControlsManager = GameControlsManager.Instance;
	}
	
	void FixedUpdate() {
		
		Vector2 velocity = _rigidBody2D.velocity;
		
		// Add velocity
		if (_gameControlsManager.JumpButtonIsActive) {
			velocity += new Vector2 (0.0f, 1.0f) * _force * Time.fixedDeltaTime;
		}
		else if (_gameControlsManager.DownButtonIsActive) {
			velocity -= new Vector2 (0.0f, 1.0f) * _force * Time.fixedDeltaTime;
		}
		else {
			velocity.y *= _friction;
		}

		velocity.x = 0.0f;
		velocity.y = Mathf.Clamp(velocity.y, -_maxVelocity, _maxVelocity);
		
		_rigidBody2D.velocity = velocity;
		Vector2 pos = _rigidBody2D.position; 
		pos.x = Mathf.Round(pos.x);
		_rigidBody2D.position = pos;

		if (_gameControlsManager.LeftButtonIsActive || _gameControlsManager.RightButtonIsActive) {
			_exitTimer += Time.fixedDeltaTime;
			if (_exitTimer > _exitTime && PlayerDidExitRopeWithJumpEvent != null) {
				PlayerDidExitRopeWithJumpEvent(_gameControlsManager.JumpButtonIsActive);
			}
		}
		else {
			_exitTimer = 0.0f;
		}
	}
}
