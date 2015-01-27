using UnityEngine;
using System.Collections;

public class EnemyLifetimeNotifier : MonoBehaviour {

	public event System.Action<EnemyLifetimeNotifier> EnemyDidSpawnEvent;
	public event System.Action<EnemyLifetimeNotifier> EnemyWasKilledEvent;
	
	public void EnemyDidSpawn() {

		if (EnemyDidSpawnEvent != null) {
			EnemyDidSpawnEvent(this);
		}
	}

	public void EnemyWasKilled() {

		if (EnemyWasKilledEvent != null) {
			EnemyWasKilledEvent(this);
		}
	}
}
