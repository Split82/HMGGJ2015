using UnityEngine;
using System.Collections;

public class BatEnemyController : EnemyController {

	public BatEnemyMovement _batMovement;

	public override void PrepareForSpawn() {

		CommonPrepareForSpawn();

		_batMovement.enabled = true;
	}

	protected override void PrepareForSleep() {
		
		_batMovement.Stop();
		_batMovement.enabled = false;
	}
	
	protected override void PrepareFromSleep() {
		
		_batMovement.enabled = true;
	}
	
	protected override void PrepareForDeathJump() {
		
		_batMovement.enabled = false;
	}
}
