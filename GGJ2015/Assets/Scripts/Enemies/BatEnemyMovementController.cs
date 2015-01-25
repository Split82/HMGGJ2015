using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]

public class BatEnemyMovementController : MonoBehaviour {

	public BatEnemyMovement _batEnemyMovement;

	public float _checkInterval = 0.1f;
	public float _attackDistance = 50f;

	private Rigidbody2D _rigidBody2D;

	IEnumerator AttackPositionUpdate() {
		
		while (true) {
			
			_batEnemyMovement.AttackLocation = GameplayManager.Instance._playerController._playerTransform.position;
			Debug.Log(_batEnemyMovement.AttackLocation);

			yield return new WaitForSeconds(_checkInterval);
		}
	}

	IEnumerator ChangeState() {

		while (true) {
			Vector2 direction = (_batEnemyMovement.AttackLocation - new Vector2(_rigidBody2D.transform.position.x, _rigidBody2D.transform.position.y));		                   

			if (_batEnemyMovement.State == BatEnemyMovement.BatEnemyStateEnum.Follow && direction.magnitude < _attackDistance) {
				_batEnemyMovement.State = BatEnemyMovement.BatEnemyStateEnum.Attack;
			}
			else if (_batEnemyMovement.State == BatEnemyMovement.BatEnemyStateEnum.Attack && direction.magnitude < 0.1) {
				_batEnemyMovement.State = BatEnemyMovement.BatEnemyStateEnum.Follow;
			}
			
			yield return new WaitForSeconds(_checkInterval);
		}
	}

	public void StartWorking() {
		Check.Null(_batEnemyMovement);
		_rigidBody2D = GetComponent<Rigidbody2D>();
		StartCoroutine(ChangeState());
		StartCoroutine(AttackPositionUpdate());
	}

}
