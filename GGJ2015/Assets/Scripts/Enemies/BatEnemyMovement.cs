using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]

public class BatEnemyMovement : MonoBehaviour {

	private Rigidbody2D _rigidBody2D;

	void Start() {
		
		_rigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate() {
		_rigidBody2D.velocity = GameplayManager.Instance._playerController._playerTransform.position - _rigidBody2D.transform.position;
	}

	public void Stop() {
		
		_rigidBody2D.velocity = Vector2.zero;
	}
}
