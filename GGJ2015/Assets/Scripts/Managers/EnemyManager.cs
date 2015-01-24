using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : Singleton<EnemyManager> {

	public int EnemyCount {
		get {
			return _enemies.Count;
		}
	}
	public int _maxEnemyCount = 10;


	private List<GameObject> _enemies;

	void Awake() {

		_enemies = new List<GameObject> ();
	}

	void Start () {
	
	}
	
	public void AddSpawnZone(EnemySpawnZone spawnZone) {
	}

	public void RemoveSpawnZone(EnemySpawnZone spawnZone) {
	}

	public void AddEnemy(GameObject enemy) {

		_enemies.Add (enemy);
		EnemyLifetimeNotifier enemyLifetimeNotifier = enemy.GetComponent<EnemyLifetimeNotifier>();
		enemyLifetimeNotifier.EnemyWasKilledEvent += EnemyWasKilled;
	}
	
	private void EnemyWasKilled(EnemyLifetimeNotifier enemyLifetimeNotifier) {
	
		_enemies.Remove (enemyLifetimeNotifier.gameObject);
		enemyLifetimeNotifier.EnemyWasKilledEvent -= EnemyWasKilled;
	}

}
