using UnityEngine;
using System.Collections;

public class GameplayManager : Singleton<GameplayManager> {

	public PlayerController _playerController;

	void Awake() {
		Check.Null(_playerController);
	}
}
