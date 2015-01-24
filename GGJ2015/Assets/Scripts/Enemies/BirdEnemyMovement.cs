using UnityEngine;
using System.Collections;

public class BirdEnemyMovement : MonoBehaviour {

	public float _force = 200.0f;
	public float _maxVelocity;
	public Transform _transform;
	public LayerMask _groundLayerMask;
	public bool _isGhost;
	public float _friction = 0.8f;
	public float _atackDistance = 2f;

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
			float distance = Vector2.Distance(transform.position, GameplayManager.Instance._playerController._playerTransform.position);
			velocity += direction * _force * Mathf.Clamp((_atackDistance / Mathf.Max(distance, 0.01f)), 1f, 5f) * Time.fixedDeltaTime;
		}
		velocity *= _friction;

		velocity = Vector2.ClampMagnitude(velocity, _maxVelocity);
		_rigidBody2D.velocity = velocity;
	}

	Vector2 DirectionToPlayer() {
		Vector3 direction = (GameplayManager.Instance._playerController._playerTransform.position - _transform.position);
		return new Vector2(direction.x, direction.y);
	}
	
	bool IsPlayerVisible() {
		if (_isGhost)
			return true;

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
