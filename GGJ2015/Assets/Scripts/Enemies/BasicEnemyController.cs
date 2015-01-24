using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]

public class BasicEnemyController : MonoBehaviour {
	
	public BasicMovement _basicMovement;
	public BasicMovementCheck _basicMovementCheck;	
	public EnemySleepCheck _enemySleepCheck;
	
	void Start() {
		
		Check.Null(_basicMovement);
		Check.Null(_basicMovementCheck);
		Check.Null(_enemySleepCheck);

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
}
