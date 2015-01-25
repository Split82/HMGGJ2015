using UnityEngine;
using System.Collections;

public abstract class EnemyController : MonoBehaviour {
	
	public EnemySleepCheck _enemySleepCheck;
	public float _maxHealth;
	public HitDetector _damageReceiveHitDetector;
	public EnemyLifetimeNotifier _enemyLifetimeNotifier;
	public EnemyDeathJumpMovement _enemyDeathJumpMovement;
	public WhiteSpriteFlash _whiteSpriteFlash;
	public GameObject _enemySprite;
	public bool _stayForever;
	public Collider2D _dealDamageCollider;

	public float _health;

	public abstract void PrepareForSpawn();
	protected abstract void PrepareForSleep();
	protected abstract void PrepareFromSleep();
	protected abstract void PrepareForDeathJump();
	
	protected void CommmonStart() {

		_health = _maxHealth;

		Check.Null(_enemySleepCheck);
		Check.Null(_damageReceiveHitDetector);
		Check.Null(_enemyLifetimeNotifier);
		Check.Null(_enemyDeathJumpMovement);
		Check.Null(_whiteSpriteFlash);
		Check.Null(_dealDamageCollider);

		_enemyDeathJumpMovement.enabled = false;
		
		_damageReceiveHitDetector.TriggerDidEnterEvent += DamageReceiveTriggerDidEnter;
		_enemySleepCheck.CanSleepValueHasChangedEvent += () => {
			
			if (_enemySleepCheck.CanSleep) {
				PrepareForSleep();
			}
			else {
				PrepareFromSleep();
			}
		};
	}
	
	protected void CommonPrepareForSpawn() {
		
		_enemySleepCheck.enabled = true;
		_enemyDeathJumpMovement.enabled = false;
		_dealDamageCollider.enabled = true;
		_health = _maxHealth;
		
		_enemyLifetimeNotifier.EnemyDidSpawn();
	}
	
	void ApplyDamage(float damageAmount, Vector2 damageForceDirection) {
		
		_health -= damageAmount * GameTraits.Instance.damageMultiplier;
		if (_health <= 0) {
			PrepareForDeathJump();
			_dealDamageCollider.enabled = false;
			_enemySleepCheck.enabled = false;
			_enemyDeathJumpMovement.enabled = true;
			_enemyDeathJumpMovement.JumpWithDirection (damageForceDirection);

			StartCoroutine(PrepareForDeath(damageForceDirection));
		}
	}
	
	void DamageReceiveTriggerDidEnter(Collider2D otherCollider) {
		
		if (_health > 0) {
			_whiteSpriteFlash.Flash(0.1f);
			Fireball fireball = otherCollider.gameObject.GetComponent<Fireball> ();
			ApplyDamage (fireball._damage, new Vector2 ((otherCollider.transform.position - transform.position).x < 0 ? 1 : -1, 1));
		}
	}
	
	IEnumerator PrepareForDeath(Vector2 deathForceDirection) {
		
		yield return new WaitForSeconds(1.5f);
		
		_enemyLifetimeNotifier.EnemyWasKilled();

		if (_stayForever) {
			StayForeverManager.Instance.StayForever(new GameObject[1] {_enemySprite}, () => {
				gameObject.Recycle();
			});
		}
		else {
			gameObject.Recycle();
		}
	}
}
