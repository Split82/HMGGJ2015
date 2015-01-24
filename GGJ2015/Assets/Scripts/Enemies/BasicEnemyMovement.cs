using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]

public class BasicEnemyMovement : MonoBehaviour {

	public float _groundForce = 200.0f;
	public float _groundFriction = 0.9f;
	public float _airFriction = 0.95f;
	public Vector2 _maxVelocity;
	public GroundedCheck _groundedCheck;


	public GroundedCheck _movementWallCheck;
	public GroundedCheck _movementFallCheck;
	public Transform _movementCheckerTransform;

	private Rigidbody2D _rigidBody2D;

	private int _direction = 0;


	void Awake() {

		_direction = Random.Range (0, 2) == 1 ? -1 : 1;
	}


	void Start() {
		
		Check.Null(_groundedCheck);
		Check.Null(_movementWallCheck);
		Check.Null(_movementFallCheck);
		Check.Null(_movementCheckerTransform);
		_rigidBody2D = GetComponent<Rigidbody2D>();
		_movementCheckerTransform.localScale = new Vector3 (_direction, 1, 1);
		StartCoroutine (MovementCheck());
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
			force = 0;
		}
		
		// Add velocity
		if (_direction == -1) {
			velocity -= new Vector2 (1.0f, 0.0f) * force * Time.fixedDeltaTime;
		}
		else if (_direction == 1) {
			velocity += new Vector2 (1.0f, 0.0f) * force * Time.fixedDeltaTime;
		}
		else {
			velocity.x *= friction;
		}

		velocity.x = Mathf.Clamp(velocity.x, -_maxVelocity.x, _maxVelocity.x);
		velocity.y = Mathf.Clamp(velocity.y, -_maxVelocity.y, _maxVelocity.y);
		
		_rigidBody2D.velocity = velocity;
	}

	IEnumerator MovementCheck() {

		while (true) {
			if (!_movementFallCheck.IsGrounded || _movementWallCheck.IsGrounded) {
				_direction = -_direction;
				_movementCheckerTransform.localScale = new Vector3(_direction, 1, 1);
			}
			yield return new WaitForSeconds(0.05f);
		}
	}
}
