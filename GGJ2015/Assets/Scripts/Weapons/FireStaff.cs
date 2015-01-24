using UnityEngine;
using System.Collections;

public class FireStaff : MonoBehaviour {
	
	public Fireball _fireballPrefab;
	public Transform _firePointTransform;
	public PlayerFaceDirection _playerFaceDirection;
	public float _shotsPerSecond = 5.0f;
	public float _bulletSpeed = 100.0f;
	public float _randomness = 0.2f;
	
	private float _timeSinceLastShot;
	private GameControlsManager _gameControlsManager;
	private Transform _transform;
	private Vector3 _shootDirection;

	void Awake() {
		
		Check.Null(_fireballPrefab);		
		_fireballPrefab.CreatePool(30);

		_transform = transform;
		_gameControlsManager = GameControlsManager.Instance;

		_playerFaceDirection.DirectionDidChangeEvent += () => {

			if (_playerFaceDirection.Direction == PlayerFaceDirection.DirectionEnum.Left) {
				_transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
				_shootDirection = new Vector3(-1.0f, 0.0f, 0.0f);
			}
			else {
				_transform.localScale = Vector3.one;
				_shootDirection = new Vector3(1.0f, 0.0f, 0.0f);
			}
		};

		_shootDirection = new Vector3(1.0f, 0.0f, 0.0f);
	}

	void Update() {	

		_timeSinceLastShot += Time.deltaTime;

		if (_gameControlsManager.FireButonIsActive) {
			Fire(_firePointTransform.position, _shootDirection);
		}
	}
	
	// Returns true if bullet was fired
	public bool Fire(Vector3 pos, Vector3 direction) {
		
		if (_timeSinceLastShot < 1.0f / _shotsPerSecond) {
			return false;
		}	
		
		Vector3 normalizedDirection = direction.normalized;
		
		Fireball bullet = _fireballPrefab.Spawn(pos);
		SoundManager.Instance.PlayShot();
		bullet.Fire(pos, normalizedDirection + (Vector3)Random.insideUnitCircle * _randomness, _bulletSpeed);	
		
		_timeSinceLastShot = 0.0f;
		return true;
	}
}
