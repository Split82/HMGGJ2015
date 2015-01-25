using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class Fireball : MonoBehaviour {
	
	public float _damage = 1.0f;
	public float _lifeTime = 1.0f;

	private Transform _transform;
	private Rigidbody2D _rigidBody2D;
	private float _elapsedTime;

	public float Damage {
		get {
			return _damage;
		}
	}
	
	void Awake() {
		
		_transform = transform;		
		_rigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate() {

		_elapsedTime += Time.fixedDeltaTime;

		if (_elapsedTime > _lifeTime) {
			gameObject.Recycle();
		}
	}

	public void Fire(Vector3 pos, Vector3 direction, float speed) {

		_elapsedTime = 0.0f;

		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		
		_transform.position = pos;
		_rigidBody2D.velocity = direction.normalized * speed;
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.layer == LayerMask.NameToLayer ("EnemyHitBox") && GlobalTraits.Instance._shieldsOff) {
			return;
		}
		if (other.gameObject.layer == LayerMask.NameToLayer ("Ground") && GlobalTraits.Instance._wallsOff) {
			return;
		}
		GlobalEffects.Instance.EmitExplosion0Particle(_transform.position, 4);
		gameObject.Recycle();
	}
}
