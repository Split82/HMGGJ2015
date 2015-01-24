using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]

public class BasicEnemyController : MonoBehaviour {
	
	public BasicMovement _basicMovement;
	public BasicMovementCheck _basicMovementCheck;	
	public EnemySleepCheck _enemySleepCheck;
	public float _maxHealth;
	public HitDetector _fireballHitDetector;

	private float _health;

	void Awake() {

		_health = _maxHealth;
	}

	void Start() {
		
		Check.Null(_basicMovement);
		Check.Null(_basicMovementCheck);
		Check.Null(_enemySleepCheck);
		Check.Null(_fireballHitDetector);

		_fireballHitDetector.TriggerDidEnterEvent += FireballTriggerDidEnter;

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

	void ApplyDamage(float damageAmount) {

		_health -= damageAmount;
		if (_health <= 0) {
			gameObject.Recycle();
		}
	}

	void FireballTriggerDidEnter(GameObject triggerDeObject, GameObject triggeredObject) {

		Fireball fireball = triggeredObject.GetComponent<Fireball>();
		ApplyDamage (fireball._damage);
	}
}
