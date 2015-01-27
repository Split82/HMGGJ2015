using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class Fireball : MonoBehaviour {

	#region Unity params

	public float _damage = 1.0f;
	public float _lifeTime = 1.0f;

	#endregion

	
	#region Properties

	public float Damage {
		get {
			return _damage;
		}
	}

	#endregion


	#region Private vars

	private Transform _transform;
	private Rigidbody2D _rigidBody2D;
	private float _elapsedTime;

	#endregion


	#region Unity callbacks

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

	void OnCollisionEnter2D(Collision2D col) {

		GlobalEffects.Instance.EmitExplosion0Particle(_transform.position, 4);
		gameObject.Recycle();
	}

	void OnTriggerEnter2D(Collider2D other) {

		GlobalEffects.Instance.EmitExplosion0Particle(_transform.position, 4);
		gameObject.Recycle();
	}

	#endregion


	#region Actions

	public void Fire(Vector3 pos, Vector3 direction, float speed) {
		
		_elapsedTime = 0.0f;
		
		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		
		_transform.position = pos;
		_rigidBody2D.velocity = direction.normalized * speed;
	}

	#endregion
}
