using UnityEngine;
using System.Collections;

public class BasicEnemyMovement : MonoBehaviour {

	public float _speed = 1.0f;

	private Rigidbody2D _rigidbody2D;

	void Awake() {

		_rigidbody2D = rigidbody2D;
	}

	void Start () {
	}
}
