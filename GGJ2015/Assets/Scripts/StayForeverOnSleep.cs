using UnityEngine;
using System.Collections;

public class StayForeverOnSleep : MonoBehaviour {

	public Rigidbody2D _rigidbody2D;
	public float _checkInterval = 1.0f;
	public bool _recycle = true;

	void OnEnable() {
	
		StartCoroutine(IntervalUpdate());
	}

	void OnDisable() {

		StopAllCoroutines();
	}

	private IEnumerator IntervalUpdate() {

		while (true) {

			yield return new WaitForSeconds(_checkInterval);
			if (_rigidbody2D.IsSleeping()) {
				StayForeverManager.Instance.StayForever(this.gameObject, () => {
					if (_recycle) {
						ObjectPool.Recycle(this.gameObject);
					}
					else {
						Destroy(this.gameObject);
					}
				});
			}
		}
	}
}
