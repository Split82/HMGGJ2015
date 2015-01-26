using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnZone : MonoBehaviour {

	[System.Serializable]
	public class EnemyGroupSpawnParams {
		public GameObject[] _enemyPrefabs;
		public float _spawnTime;
		public float _spawnTimeDelta;
	}
	public EnemyGroupSpawnParams[] _enemyGroupsSpawnParams;
	public int _maxEnemies;
	public Rect[] _enemySpawnZoneRects;

	private EnemyManager _enemyManager;
	private int _enemyCount;

	void Start () {
	
		_enemyManager = EnemyManager.Instance;

		for (int i = 0; i < _enemyGroupsSpawnParams.Length; i++) {
			StartCoroutine(SpawnGroup(i));
		}
	}


	IEnumerator SpawnGroup(int groupIndex) {

		while (_enemyGroupsSpawnParams.Length > groupIndex &&  _enemySpawnZoneRects.Length > 0) {
			Vector3 playerPos = GameplayManager.Instance._playerController._playerTransform.position;
			Rect spawnRect = _enemySpawnZoneRects[Random.Range(0, _enemySpawnZoneRects.Length)];
			EnemyGroupSpawnParams spawnParams = _enemyGroupsSpawnParams[groupIndex];
			if (CanSpawnEnemies(spawnParams._enemyPrefabs.Length)) {
				foreach (GameObject enemyPrefab in spawnParams._enemyPrefabs) {
					Vector3 newPos = new Vector3(Random.Range(spawnRect.xMin, spawnRect.xMax), Random.Range(spawnRect.yMin, spawnRect.yMax)) + transform.position;
					if (Vector3.Distance(playerPos, newPos) > 5) {
						GameObject enemy = _enemyManager.CreateEnemyFromPrefab(enemyPrefab);
						enemy.transform.position = newPos;
						EnemyLifetimeNotifier enemyLifetimeNotifier = enemy.GetComponent<EnemyLifetimeNotifier>();
						enemyLifetimeNotifier.EnemyDidSpawnEvent += EnemyDidSpawn;
						enemyLifetimeNotifier.EnemyWasKilledEvent += EnemyWasKilled;
						_enemyManager.AddEnemy(enemy);
						EnemyController enemyController = enemy.GetComponent<EnemyController>();
						enemyController.PrepareForSpawn();
					}
				}
			}
			float waitMul = 1;
			if (GlobalTraits.Instance._lessEnemies) {
				waitMul *= 2; 
			}
			if (GlobalTraits.Instance._moreEnemies) {
				waitMul *= 0.5f; 
			}
			yield return new WaitForSeconds((spawnParams._spawnTime + Random.value * spawnParams._spawnTimeDelta) * waitMul);
		}
	}

	bool CanSpawnEnemies(int count) {

		return _enemyManager.EnemyCount + count <= _enemyManager._maxEnemyCount && _enemyCount + count <= _maxEnemies;
	}

	void OnDrawGizmos() {

		foreach(Rect rect in _enemySpawnZoneRects) {  
			Vector2 pos = (Vector2)transform.position;
			Vector2 bottomLeft = new Vector2(pos.x + rect.xMin, pos.y + rect.yMin);
			Vector2 bottomRight = new Vector2(pos.x + rect.xMax, pos.y + rect.yMin);
			Vector2 topLeft = new Vector2(pos.x + rect.xMin, pos.y + rect.yMax);
			Vector2 topRight = new Vector2(pos.x + rect.xMax, pos.y + rect.yMax);
			Gizmos.DrawLine (bottomLeft, bottomRight);
			Gizmos.DrawLine (bottomRight, topRight);
			Gizmos.DrawLine (topRight, topLeft);
			Gizmos.DrawLine (topLeft, bottomLeft);
			Gizmos.DrawLine (bottomLeft, topRight);
		}
	}

	void EnemyWasKilled(EnemyLifetimeNotifier enemyLifetimeNotifier) {

		enemyLifetimeNotifier.EnemyWasKilledEvent -= EnemyWasKilled;
		_enemyCount--;
	}

	void EnemyDidSpawn(EnemyLifetimeNotifier enemyLifetimeNotifier) {

		enemyLifetimeNotifier.EnemyDidSpawnEvent -= EnemyDidSpawn;
		_enemyCount++;
	}
}
