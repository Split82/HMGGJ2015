using UnityEngine;
using System.Collections;

public class RhinoEnemyController : EnemyController {

	public BasicMovement _basicMovement;
	public BasicMovementCheck _basicMovementCheck;	
	public EnemySeeHorizontalPlayerCheck _enemySeePlayerCheck;


	void Start() {

		CommmonStart();

		Check.Null(_enemySeePlayerCheck);		
		Check.Null(_basicMovement);
		Check.Null(_basicMovementCheck);

		_basicMovementCheck.CanNotMoveInTheSameDirectionEvent += () => {
			_basicMovement.Direction = DirectionClass.Opposite(_basicMovement.Direction);
			_basicMovementCheck.Direction = _basicMovement.Direction;
		};
		
		_enemySeePlayerCheck.IsPlayerVisibleHorizontalValueHasChangedEvent += MovementToPlayer;
		_enemySeePlayerCheck.SimpleDirectionToPlayerValueHasChangedEvent += MovementToPlayer;
	}

	public override void PrepareForSpawn() {
		
		CommonPrepareForSpawn();
		
		_basicMovement.enabled = true;
		_basicMovementCheck.enabled = true;
		_basicMovementCheck.StartWorking();
		_enemySeePlayerCheck.StartWorking();
	}
	
	protected override void PrepareForSleep() {
		
		_basicMovement.Stop();
		_basicMovement.enabled = false;
		_basicMovementCheck.enabled = false;
	}
	
	protected override void PrepareFromSleep() {
		
		_basicMovement.enabled = true;
		_basicMovementCheck.enabled = true;
	}
	
	protected override void PrepareForDeathJump() {
		
		_basicMovement.enabled = false;
		_basicMovementCheck.enabled = false;
	}

	void MovementToPlayer() {

		if (_enemySeePlayerCheck.IsPlayerVisibleHorizontal) {
			_basicMovement.Direction = _enemySeePlayerCheck.SimpleDirectionToPlayer;
			_basicMovementCheck.Direction = _basicMovement.Direction;
		}
	}
}
