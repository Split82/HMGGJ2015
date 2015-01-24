using UnityEngine;
using System.Collections;
using System;

public class HitDetector : MonoBehaviour {

	public event Action<GameObject, GameObject> TriggerDidEnterEvent;

	void OnTriggerEnter2D(Collider2D other) {

		if (TriggerDidEnterEvent != null) {
			TriggerDidEnterEvent(gameObject, other.gameObject);
		}
	}
}
