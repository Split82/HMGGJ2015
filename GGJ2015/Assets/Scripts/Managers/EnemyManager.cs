using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : Singleton<EnemyManager> {

	public GameObject _basicEmenyPrefab;
	public GameObject _rhinoEmenyPrefab;
	public GameObject _batEmenyPrefab;
	public GameObject _crabEmenyPrefab;

	public event System.Action EnemyWasKilledEvent;

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
		_rhinoEmenyPrefab.CreatePool(10);
		_batEmenyPrefab.CreatePool(10);
		_crabEmenyPrefab.CreatePool(10);
	}

	void Start () {
	
	}

	public GameObject CreateEnemyFromPrefab(GameObject enemyPrefab) {

		GameObject spawPool = null;
		switch (enemyPrefab.tag) {
			case "RhinoEnemy" : {
				spawPool = _rhinoEmenyPrefab;
				break;
			}
			case "BatEnemy" : {
				spawPool = _batEmenyPrefab;
				break;
			}
			case "CrabEnemy" : {
				spawPool = _crabEmenyPrefab;
				break;
			}
			default:
			case "BasicEnemy" : {
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

		if (EnemyWasKilledEvent != null) {
			EnemyWasKilledEvent();
		}
	}

}
