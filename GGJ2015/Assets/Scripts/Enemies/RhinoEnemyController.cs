using UnityEngine;
using System.Collections;

public class RhinoEnemyController : MonoBehaviour {

	public BasicMovement _basicMovement;
	public BasicMovementCheck _basicMovementCheck;	
	public EnemySleepCheck _enemySleepCheck;
	public EnemySeePlayerCheck _enemySeePlayerCheck;

	void Start() {

		Check.Null(_enemySeePlayerCheck);		
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
	
		
		_enemySeePlayerCheck.IsPlayerVisibleHorizontalValueHasChangedEvent += MovementToPlayer;

		_enemySeePlayerCheck.SimpleDirectionToPlayerValueHasChangedEvent += MovementToPlayer;
	}

	void MovementToPlayer() {
		if (_enemySeePlayerCheck.IsPlayerVisibleHorizontal) {
			_basicMovement.Direction = _enemySeePlayerCheck.SimpleDirectionToPlayer;
			_basicMovementCheck.Direction = _basicMovement.Direction;
		}
	}
}
