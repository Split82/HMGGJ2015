using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : Singleton<EnemyManager> {

	public GameObject _basicEmenyPrefab;
	public GameObject _birdEmenyPrefab;

	public int EnemyCount {
		get {
			return _enemies.Count;
		}
	}
	public int _maxEnemyCount = 10;


	private List<GameObject> _enemies;

	void Awake() {

		_enemies = new List<GameObject> ();
		_basicEmenyPrefab.CreatePool(10);
		_birdEmenyPrefab.CreatePool(10);
	}

	void Start () {
	
	}

	public GameObject CreateEnemyFromPrefab(GameObject enemyPrefab) {

		GameObject spawPool = null;
		switch (enemyPrefab.tag) {
			case "bird" : {
				spawPool = _birdEmenyPrefab;
				break;
			}
			default:
			case "basic" : {
				spawPool = _basicEmenyPrefab;
				break;
			}
		}
		return spawPool.Spawn();
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
