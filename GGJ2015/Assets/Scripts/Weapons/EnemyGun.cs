using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour {
	
	public EnemyBullet _bulletPrefab;
	public Transform _firePointTransform;
	public FaceDirection _enemyFaceDirection;
	public float _shotsPerSecond = 5.0f;
	public float _bulletSpeed = 100.0f;
	public float _randomness = 0.2f;
	
	private Transform _transform;

	void Awake() {
		
		Check.Null(_bulletPrefab);		
		_bulletPrefab.CreatePool(30);

		_transform = transform;
	}

	void Start() {

		StartCoroutine(Fire());
	}

	IEnumerator Fire() {

		while (true) {
			Vector3 shootDirection;
			if (_enemyFaceDirection.Direction == FaceDirection.DirectionEnum.Left) {
				_transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
				shootDirection = new Vector3(-1.0f, 0.0f, 0.0f);
			}
			else {
				_transform.localScale = Vector3.one;
				shootDirection = new Vector3(1.0f, 0.0f, 0.0f);
			}

			EnemyBullet bullet = _bulletPrefab.Spawn();
			bullet.Fire(_firePointTransform.position, shootDirection + (Vector3)Random.insideUnitCircle * _randomness, _bulletSpeed);	
			yield return new WaitForSeconds(1 / _shotsPerSecond);
		}
	}
}
