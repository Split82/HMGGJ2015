using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]

public class FaceDirection : MonoBehaviour {

	public enum DirectionEnum {
		Left,
		Right
	}
	
	public DirectionEnum Direction {
		get {

			float velocityX = _rigidbody2D.velocity.x;
			if (velocityX > 0.01f) {
				_direction = DirectionEnum.Right;
			}
			else if (velocityX < -0.01f) {
				_direction = DirectionEnum.Left;
			}

			return _direction;
		}
	}

	private DirectionEnum _direction = DirectionEnum.Right;
	private Rigidbody2D _rigidbody2D;
	
	void Awake() {
		_rigidbody2D = rigidbody2D;
	}
}
