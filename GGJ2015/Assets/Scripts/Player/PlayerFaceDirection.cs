using UnityEngine;
using System.Collections;
using System;

public class PlayerFaceDirection : MonoBehaviour {

	public event Action DirectionDidChangeEvent;

	public enum DirectionEnum {
		Left,
		Right
	}
	
	public DirectionEnum Direction {
		get {
			return _direction;
		}
	}
	
	private DirectionEnum _direction = DirectionEnum.Right;
	private GameControlsManager _gameControlsManager;

	void Start() {

		_gameControlsManager = GameControlsManager.Instance;
	}

	void Update() {

		DirectionEnum _prevDirection = _direction;

		// Change face direction only if not shooting
		if (!_gameControlsManager.FireButonIsActive) {
			if (_gameControlsManager.LeftButtonIsActive) {
				_direction = DirectionEnum.Left;
			}
			else if (_gameControlsManager.RightButtonIsActive) {
				_direction = DirectionEnum.Right;
			}
		}

		if (_prevDirection != _direction) {

			if (DirectionDidChangeEvent != null) {
				DirectionDidChangeEvent();
			}
		}
	}
}
