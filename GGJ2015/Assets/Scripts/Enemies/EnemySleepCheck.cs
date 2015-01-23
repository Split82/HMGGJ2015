using UnityEngine;
using System.Collections;

public class EnemySleepCheck : MonoBehaviour {

	public Behaviour _enemyMovement;
	public Transform _transform;

	private Rigidbody2D _rigidBody2D;
	private float _sleepDistance = 5;

	void Awake() {

		Check.Null(_transform);
		Check.Null(_enemyMovement);
	}

	void Start () {

		_rigidBody2D = GetComponent<Rigidbody2D>();
		StartCoroutine (PlayerDistanceCheck());	
	}

	IEnumerator PlayerDistanceCheck() {
		
		while (true) {
			if (Vector3.Distance(transform.position, GameplayManager.Instance._playerController._playerTransform.position) > _sleepDistance) {
				_rigidBody2D.velocity = new Vector2 (0.0f, 0.0f);
				_enemyMovement.enabled = false;
			} else {
				_enemyMovement.enabled = true;
			}

			yield return new WaitForSeconds(1f);
		}
	}

}
