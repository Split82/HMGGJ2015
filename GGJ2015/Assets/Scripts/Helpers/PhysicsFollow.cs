using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]

public class PhysicsFollow : MonoBehaviour {
	
	public Transform _targetTransform;
	public Vector3 _offset;
	public float _friction = 0.9f;
	public float _elasticity = 10.0f;

	private Rigidbody2D _rigidBody2D;

	void Start() {

		_rigidBody2D = rigidbody2D;
		transform.position = _targetTransform.position + _offset;
	}

	void FixedUpdate() {

		Vector3 velocity = _rigidBody2D.velocity;
		Vector3 targetPosition = _targetTransform.position + _offset;
		velocity += (targetPosition - (Vector3)_rigidBody2D.position) * _elasticity * Time.fixedDeltaTime;
		velocity *= _friction;

		_rigidBody2D.velocity = velocity;
	}
}