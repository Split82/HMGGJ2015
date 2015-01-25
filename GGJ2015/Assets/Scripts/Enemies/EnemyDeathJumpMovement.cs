using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]

public class EnemyDeathJumpMovement : MonoBehaviour {

	public float _jumpForceMul = 7.0f;
	public float _deccelerationMul = 0.9f;

	private Rigidbody2D _rigidBody2D;

	void Awake() {

		_rigidBody2D = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		
		Vector2 velocity = _rigidBody2D.velocity;
		velocity.x *= _deccelerationMul;
		_rigidBody2D.velocity = velocity;
	}

	public void JumpWithDirection(Vector2 jumpDirection) {

		_rigidBody2D.velocity += jumpDirection * _jumpForceMul;
	}
}
