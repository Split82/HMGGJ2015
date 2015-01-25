using UnityEngine;
using System.Collections;

public class RhinoEnemyController : EnemyController {

	public BasicMovement _basicMovement;
	public BasicMovementCheck _basicMovementCheck;	
	public EnemySleepCheck _enemySleepCheck;
	public EnemySeeHorizontalPlayerCheck _enemySeePlayerCheck;
	public float _maxHealth;
	public HitDetector _damageReceiveHitDetector;
	
	private float _health;

	void Awake() {

		_health = _maxHealth;
	}

	void Start() {

		Check.Null(_enemySeePlayerCheck);		
		Check.Null(_basicMovement);
		Check.Null(_basicMovementCheck);
		Check.Null(_enemySleepCheck);
		Check.Null(_damageReceiveHitDetector);
		
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
		
		_enemySeePlayerCheck.IsPlayerVisibleHorizontalValueHasChangedEvent += MovementToPlayer;

		_enemySeePlayerCheck.SimpleDirectionToPlayerValueHasChangedEvent += MovementToPlayer;
	}

	public override void PrepareForSpawn() {
	}

	void MovementToPlayer() {
		if (_enemySeePlayerCheck.IsPlayerVisibleHorizontal) {
			_basicMovement.Direction = _enemySeePlayerCheck.SimpleDirectionToPlayer;
			_basicMovementCheck.Direction = _basicMovement.Direction;
		}
	}

	void ApplyDamage(float damageAmount) {
		
		_health -= damageAmount;
		if (_health <= 0) {
			gameObject.Recycle();
		}
	}
	
	void DamageReceiveTriggerDidEnter(Collider2D otherCollider) {
		
		Fireball fireball = otherCollider.gameObject.GetComponent<Fireball>();
		ApplyDamage (fireball._damage);
	}
}
