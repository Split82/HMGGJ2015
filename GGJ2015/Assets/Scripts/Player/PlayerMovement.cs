using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour {

	public float _force;
	public float _friction = 0.9f;

	private Rigidbody2D _rigidBody2D;
	private GameControlsManager _gameControlsManager;

	void Start() {
	
		_rigidBody2D = GetComponent<Rigidbody2D>();

		_gameControlsManager = GameControlsManager.Instance;
	}

	void FixedUpdate() {
			
		Vector2 velocity = _rigidBody2D.velocity;

		// Add velocity
		if (_gameControlsManager.LeftButtonIsActive) {
			velocity -= new Vector2 (1.0f, 0.0f) * _force * Time.fixedDeltaTime;
		}
		else if (_gameControlsManager.RightButtonIsActive) {
			velocity += new Vector2 (1.0f, 0.0f) * _force * Time.fixedDeltaTime;
		}
		
		velocity.x *= _friction;

		_rigidBody2D.velocity = velocity;
	}
}
