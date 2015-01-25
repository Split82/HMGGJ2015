using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(Rigidbody2D))]

public class BasicEnemyController : EnemyController {

	public event Action EnemyDidDieEvent;

	public BasicMovement _basicMovement;
	public BasicMovementCheck _basicMovementCheck;	
	public EnemySleepCheck _enemySleepCheck;
	public float _maxHealth;
	public HitDetector _damageReceiveHitDetector;
	public EnemyLifetimeNotifier _enemyLifetimeNotifier;
	public EnemyDeathJumpMovement _enemyDeathJumpMovement;

	private float _health;

	public  bool IsDying {
		get {
			return _health <= 0;
		}
	}

	void Awake() {

		_health = _maxHealth;
	}

	void Start() {
		
		Check.Null(_basicMovement);
		Check.Null(_basicMovementCheck);
		Check.Null(_enemySleepCheck);
		Check.Null(_damageReceiveHitDetector);
		Check.Null(_enemyLifetimeNotifier);
		Check.Null (_enemyDeathJumpMovement);

		_enemyDeathJumpMovement.enabled = false;

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

		_enemySleepCheck.enabled = true;
		_basicMovement.enabled = true;
		_basicMovementCheck.enabled = true;
		_enemyDeathJumpMovement.enabled = false;

		_enemyLifetimeNotifier.EnemyDidSpawn();
	}

	void ApplyDamage(float damageAmount, Vector2 damageForceDirection) {

		_health -= damageAmount;
		if (_health <= 0) {
			StartCoroutine(PrepareForDeath(damageForceDirection));
		}
	}

	void DamageReceiveTriggerDidEnter(Collider2D otherCollider) {

		if (_health > 0) {
			Fireball fireball = otherCollider.gameObject.GetComponent<Fireball> ();
			ApplyDamage (fireball._damage, new Vector2 ((otherCollider.transform.position - transform.position).x < 0 ? 1 : -1, 1));
		}
	}

	IEnumerator PrepareForDeath(Vector2 deathForceDirection) {

		_enemySleepCheck.enabled = false;
		_basicMovement.enabled = false;
		_basicMovementCheck.enabled = false;
		_enemyDeathJumpMovement.enabled = true;
		_enemyDeathJumpMovement.JumpWithDirection (deathForceDirection);

		yield return new WaitForSeconds(0.5f);

		_enemyLifetimeNotifier.EnemyWasKilled();
		gameObject.Recycle();
	}
}
