using UnityEngine;
using System.Collections;

public class BirdEnemyMovement : MonoBehaviour {

	public float _force = 200.0f;
	public float _maxVelocity;
	public Transform _transform;
	public LayerMask _groundLayerMask;

	private Rigidbody2D _rigidBody2D;
	private bool _isPlayerVisible = false;
	private Vector2 _directionToPlayer;
	
	void Start() {

		Check.Null(_transform);
		_rigidBody2D = GetComponent<Rigidbody2D>();

		StartCoroutine (PlayerVisibilityCheck());
	}

	void FixedUpdate() {

		Vector2 velocity = _rigidBody2D.velocity;
		if (_isPlayerVisible) {
			Vector2 direction = _directionToPlayer.normalized;
			velocity += direction * _force * Time.fixedDeltaTime;
		}

		velocity = Vector2.ClampMagnitude(velocity, _maxVelocity);
		_rigidBody2D.velocity = velocity;
	}

	Vector2 DirectionToPlayer() {
		Vector3 direction = (GameplayManager.Instance._playerController._playerTransform.position - _transform.position);
		return new Vector2(direction.x, direction.y);
	}
	
	bool IsPlayerVisible() {

		RaycastHit2D[] hits = new RaycastHit2D[1];
		int count = Physics2D.RaycastNonAlloc(_transform.position, DirectionToPlayer(), hits, DirectionToPlayer().magnitude, _groundLayerMask);
		return (count < 1);
	}

	IEnumerator PlayerVisibilityCheck() {
	
		while (true) {
			_directionToPlayer = DirectionToPlayer();
			_isPlayerVisible = IsPlayerVisible();
		
			yield return new WaitForSeconds(0.05f);
		}
	}
}
