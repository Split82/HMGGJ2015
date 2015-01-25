using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]

public class BatEnemyMovement : MonoBehaviour {

	private Rigidbody2D _rigidBody2D;

	public float _checkInterval = 0.1f;
	public float _followSpeed = 10f;
	public float _attackSpeed = 50f;
	public float _waitDistance = 50f;

	public enum BatEnemyStateEnum {
		Follow,
		Attack
	}

	private Vector2 _attackLocation;

	public BatEnemyStateEnum _state = BatEnemyStateEnum.Follow;

	public BatEnemyStateEnum State {
		get {
			return _state;
		}
		set {
			_state = value;
		}
	}

	public Vector2 AttackLocation {
		get {
			return _attackLocation;
		}
		set {
			_attackLocation = value;
		}
	}

	void Start() {
		
		_rigidBody2D = GetComponent<Rigidbody2D>();

		StartCoroutine(AttackPositionUpdate());
	}

	IEnumerator AttackPositionUpdate() {

		while (true) {

			if (_state == BatEnemyStateEnum.Follow) {
				_attackLocation = GameplayManager.Instance._playerController._playerTransform.position;
			}
			yield return new WaitForSeconds(_checkInterval);
		}
	}


	void FixedUpdate() {

		switch (_state) {
		case BatEnemyStateEnum.Follow:
			FollowUpdate();
			break;
		case BatEnemyStateEnum.Attack:
			AttackUpdate();
			break;
		}
	}

	void FollowUpdate() {

		Vector2 direction = (_attackLocation - new Vector2(_rigidBody2D.transform.position.x, _rigidBody2D.transform.position.y));
		_rigidBody2D.velocity = direction.normalized * _followSpeed * Time.fixedDeltaTime;		
	}


	void AttackUpdate() {

		Vector2 direction = (_attackLocation - new Vector2(_rigidBody2D.transform.position.x, _rigidBody2D.transform.position.y));		                   
		_rigidBody2D.velocity = direction.normalized * _attackSpeed * Time.fixedDeltaTime;	
	}

	public void Stop() {
		
		_rigidBody2D.velocity = Vector2.zero;
	}
}
