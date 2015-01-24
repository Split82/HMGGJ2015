using UnityEngine;
using System.Collections;
using System;

public class HitDetector : MonoBehaviour {

	public event Action<Collider2D> TriggerDidEnterEvent;

	void OnTriggerEnter2D(Collider2D other) {

		if (TriggerDidEnterEvent != null) {
			TriggerDidEnterEvent(other);
		}
	}
}
