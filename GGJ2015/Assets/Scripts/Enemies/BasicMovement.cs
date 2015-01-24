using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]

public class BasicMovement : MonoBehaviour {

	public float _force = 200.0f;
	public Vector2 _maxVelocity = new Vector2(10.0f, 10.0f);

	public  DirectionClass.DirectionEnum Direction {
		set {
			_direction = value;
		}
		get {
			return _direction;
		}
	}

	private Rigidbody2D _rigidBody2D;
	private DirectionClass.DirectionEnum _direction = DirectionClass.DirectionEnum.Right;

	void Start() {

		_rigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate() {

		Vector2 velocity = _rigidBody2D.velocity;
		
		// Add velocity
		if (_direction == DirectionClass.DirectionEnum.Left) {
			velocity -= new Vector2 (1.0f, 0.0f) * _force * Time.fixedDeltaTime;
		}
		else if (_direction == DirectionClass.DirectionEnum.Right) {
			velocity += new Vector2 (1.0f, 0.0f) * _force * Time.fixedDeltaTime;
		}
		if (_direction == DirectionClass.DirectionEnum.None) {
			velocity.x = 0.0f;
		}

		velocity.x = Mathf.Clamp(velocity.x, -_maxVelocity.x, _maxVelocity.x);
		velocity.y = Mathf.Clamp(velocity.y, -_maxVelocity.y, _maxVelocity.y);
		
		_rigidBody2D.velocity = velocity;
	}

	public void Stop() {

		_rigidBody2D.velocity = Vector2.zero;
	}
}
