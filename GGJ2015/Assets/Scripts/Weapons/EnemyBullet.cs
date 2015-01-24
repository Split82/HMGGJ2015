using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour {
	
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
	
	void OnCollisionEnter2D(Collision2D coll) {

//		ContactPoint2D contact = coll.contacts[0];
//		SpawnHitParticles(contact.point, contact.normal);
		gameObject.Recycle();
	}
	
	public void Fire(Vector3 pos, Vector3 direction, float speed) {

		_elapsedTime = 0.0f;

		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		
		_transform.position = pos;
		_rigidBody2D.velocity = direction.normalized * speed;
	}
	
//	public void DidHitEnemy(Transform enemyTransform) {
//		
//		Vector3 enemyPos = enemyTransform.position;
//		Vector3 pos = _transform.position;
//		
//		SpawnHitParticles(enemyPos, pos - enemyPos);
//		gameObject.Recycle();
//	}
	
	private void SpawnHitParticles(Vector3 pos, Vector3 normal) {
		
//		PSEffectsManager.Instance._shotHitDebrisPS.transform.position = pos;
//		PSEffectsManager.Instance._shotHitDebrisPS.transform.LookAt(pos + normal);
//		PSEffectsManager.Instance._shotHitDebrisPS.Emit(5);
	}
}
