using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]

public class GameCameraFollow : MonoBehaviour {

	public Rigidbody2D _targetRigidBody2D;
	public PlayerFaceDirection _playerFaceDirection;
	public Vector3 _offset;
	public float _directionOffset;
	public float _friction = 0.9f;
	public Vector2 _elasticity = new Vector2(100.0f, 60.0f);

	private Rigidbody2D _rigidBody2D;
	private Vector2 _destinationPos;

	void Start() {

		Check.Null(_targetRigidBody2D);
		Check.Null(_playerFaceDirection);
		
		_rigidBody2D = rigidbody2D;
		transform.position = (Vector3)_targetRigidBody2D.position + _offset;
		_destinationPos = _targetRigidBody2D.position + (Vector2)_offset;
	}
	
	void FixedUpdate() {

		_destinationPos = _targetRigidBody2D.position + (Vector2)_offset;
		_destinationPos.x += _playerFaceDirection.Direction == PlayerFaceDirection.DirectionEnum.Left ? -_directionOffset : _directionOffset;

		Vector2 velocity = _rigidBody2D.velocity;

		Vector2 diff = _destinationPos - _rigidBody2D.position;
		velocity.x += diff.x * _elasticity.x * Time.fixedDeltaTime;
		velocity.y += diff.y * _elasticity.y * Time.fixedDeltaTime;

		velocity *= _friction;

		_rigidBody2D.velocity = velocity;
	}
}