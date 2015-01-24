using UnityEngine;
using System.Collections;
using System;

public class EnemySleepCheck : MonoBehaviour {

	public float _sleepDistance = 50.0f;
	public float _checkInterval = 0.1f;

	public event Action CanSleepValueHasChangedEvent;

	public bool CanSleep {
		get {
			return _canSleep;
		}
	}

	private Transform _transform;	
	private Transform _playerTransform;
	private bool _canSleep;
	
	void Start() {
		_transform = transform;
		_playerTransform = GameplayManager.Instance._playerController._playerTransform;
	}

	IEnumerator CheckSleep() {
		
		while (true) {

			bool oldCanSleep = _canSleep;
			_canSleep = Vector3.Distance(_transform.position, _playerTransform.position) > _sleepDistance;
			if (_canSleep != oldCanSleep) {
				if (CanSleepValueHasChangedEvent != null) {
					CanSleepValueHasChangedEvent();
				}
			}
			yield return new WaitForSeconds(_checkInterval);
		}
	}
}
