using UnityEngine;
using System.Collections;

public class DebrisSpawner : MonoBehaviour {

	#region Unity params

	public LayerMask _collisionLayerMask;
	public GameObject _debrisPrefab;
	public int _count = 1;
	public int _countRandomness = 0;
	public float _speed = 1.0f;
	public float _speedRandomness = 0.2f;
	public float _angleRandomness = 20.0f;
	public float _angularVelocityRandomness = 200.0f;

	#endregion


	#region Unity callbacks

	void Awake() {

		Check.Null(_debrisPrefab.GetComponent<Rigidbody2D>());
	}

	void OnCollisionEnter2D(Collision2D col) {

		if (!_collisionLayerMask.ContainsLayer(col.collider.gameObject.layer)) {
			return;
		}
		
		Vector3 pos = (Vector3)col.contacts[0].point;
		Vector3 normal = (Vector3)col.contacts[0].normal;

		SpawnDebris(pos, normal);
	}
	
	#endregion


	#region Helpers

	private void SpawnDebris(Vector3 pos, Vector3 normal) {

		int count = _count + Random.Range(-_countRandomness, _countRandomness);
		for (int i = 0; i < count; i++) {
			GameObject go = ObjectPool.Spawn(_debrisPrefab);
			go.transform.position = pos;
			go.rigidbody2D.velocity = Quaternion.Euler(0.0f, 0.0f, Random.Range(-_angleRandomness, _angleRandomness)) * normal * (_speed + Random.Range(-_speedRandomness, _speedRandomness));
			go.rigidbody2D.angularVelocity = Random.Range(-_angularVelocityRandomness, _angularVelocityRandomness);
		}
	}

	#endregion
}
