using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]

public class BatEnemyMovement : MonoBehaviour {

	private Rigidbody2D _rigidBody2D;

	public float _checkInterval = 0.1f;
	public float _followSpeed = 10f;
	public float _attackSpeed = 50f;
	public float _waitDistance = 50f;
	public float _waitInterval = 1f;

	public enum BatEnemyStateEnum {
		Follow,
		Wait,
		Attack
	}

	private Vector2 _attackLocation;

	public BatEnemyStateEnum _state = BatEnemyStateEnum.Wait;

	void Start() {
		
		_rigidBody2D = GetComponent<Rigidbody2D>();

		StartCoroutine(AttackPositionUpdate());
		StartCoroutine(ChangeState());
	}

	Vector2 dirrectionTo(Vector2 location) {
		return (location - new Vector2(_rigidBody2D.transform.position.x, _rigidBody2D.transform.position.y));
	}

	IEnumerator AttackPositionUpdate() {

		while (true) {

			if (_state == BatEnemyStateEnum.Follow) {
				_attackLocation = GameplayManager.Instance._playerController._playerTransform.position;
			}
			yield return new WaitForSeconds(_checkInterval);
		}
	}

	IEnumerator ChangeState() {

		while (true) {
			if (_state == BatEnemyStateEnum.Follow && dirrectionTo(_attackLocation).magnitude < _waitDistance) {
				_state = BatEnemyStateEnum.Wait;
				yield return new WaitForSeconds(_waitInterval);
				_state = BatEnemyStateEnum.Attack;
				yield return new WaitForSeconds(_checkInterval);
			}
			if (_state == BatEnemyStateEnum.Attack && dirrectionTo(_attackLocation).magnitude < 0.1) {
				_state = BatEnemyStateEnum.Follow;
				yield return new WaitForSeconds(_checkInterval);
			}

			yield return new WaitForSeconds(_checkInterval);
		}
	}
	
	void FixedUpdate() {

		switch (_state) {
		case BatEnemyStateEnum.Follow:
			FollowUpdate();
			break;
		case BatEnemyStateEnum.Wait:
			WaitUpdate();
			break;
		case BatEnemyStateEnum.Attack:
			AttackUpdate();
			break;
		}
	}

	void FollowUpdate() {

		_rigidBody2D.velocity = dirrectionTo(_attackLocation).normalized * _followSpeed * Time.fixedDeltaTime;		
	}

	void WaitUpdate() {

		_rigidBody2D.velocity = Vector2.zero;	
	}

	void AttackUpdate() {
		
		_rigidBody2D.velocity = dirrectionTo(_attackLocation).normalized * _attackSpeed * Time.fixedDeltaTime;	
	}

	public void Stop() {
		
		_rigidBody2D.velocity = Vector2.zero;
	}
}
