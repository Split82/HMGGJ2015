using UnityEngine;
using System.Collections;

public class GameplayManager : Singleton<GameplayManager> {

	public PlayerController _playerController;
	public LevelBuilder _levelBuilder;

	void Awake() {

		Check.Null(_levelBuilder);
		Check.Null(_playerController);
	}

	void Start() {
	}
}
