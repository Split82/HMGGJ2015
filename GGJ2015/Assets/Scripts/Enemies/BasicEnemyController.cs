using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(Rigidbody2D))]

public class BasicEnemyController : EnemyController {

	public BasicMovement _basicMovement;
	public BasicMovementCheck _basicMovementCheck;	

	void Start() {

		CommmonStart();

		Check.Null(_basicMovement);
		Check.Null(_basicMovementCheck);
		_basicMovementCheck.CanNotMoveInTheSameDirectionEvent += () => {
			_basicMovement.Direction = DirectionClass.Opposite(_basicMovement.Direction);
			_basicMovementCheck.Direction = _basicMovement.Direction;
		};
	}

	public override void PrepareForSpawn() {

		CommonPrepareForSpawn();

		_basicMovement.enabled = true;
		_basicMovementCheck.enabled = true;
		_basicMovementCheck.StartWorking();
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

}
