using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]

public class BasicEnemyController : EnemyController {
	
	public BasicMovement _basicMovement;
	public BasicMovementCheck _basicMovementCheck;	
	public EnemySleepCheck _enemySleepCheck;
	public float _maxHealth;
	public HitDetector _damageReceiveHitDetector;
	public EnemyLifetimeNotifier _enemyLifetimeNotifier;

	private float _health;

	void Awake() {

		_health = _maxHealth;
	}

	void Start() {
		
		Check.Null(_basicMovement);
		Check.Null(_basicMovementCheck);
		Check.Null(_enemySleepCheck);
		Check.Null(_damageReceiveHitDetector);
		Check.Null(_enemyLifetimeNotifier);

		_damageReceiveHitDetector.TriggerDidEnterEvent += DamageReceiveTriggerDidEnter;

		_basicMovementCheck.CanNotMoveInTheSameDirectionEvent += () => {
			_basicMovement.Direction = DirectionClass.Opposite(_basicMovement.Direction);
			_basicMovementCheck.Direction = _basicMovement.Direction;
		};

		_enemySleepCheck.CanSleepValueHasChangedEvent += () => {

			if (_enemySleepCheck.CanSleep) {
				_basicMovement.Stop();
				_basicMovement.enabled = false;
				_basicMovementCheck.enabled = false;
			}
			else {
				_basicMovement.enabled = true;
				_basicMovementCheck.enabled = true;
			}
		};
	}

	public override void PrepareForSpawn() {

		_enemyLifetimeNotifier.EnemyDidSpawn();
	}

	void ApplyDamage(float damageAmount) {

		_health -= damageAmount;
		if (_health <= 0) {
			StartCoroutine(PrepareForDeath());
		}
	}

	void DamageReceiveTriggerDidEnter(Collider2D otherCollider) {

		Fireball fireball = otherCollider.gameObject.GetComponent<Fireball>();
		ApplyDamage (fireball._damage);
	}

	IEnumerator PrepareForDeath() {
		
		yield return new WaitForSeconds(1.5f);

		_enemyLifetimeNotifier.EnemyWasKilled();
		gameObject.Recycle();
	}
}
