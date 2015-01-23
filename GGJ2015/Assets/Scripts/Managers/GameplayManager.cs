using UnityEngine;
using System.Collections;

public class GameplayManager : MonoBehaviour {

	public PlayerController _playerController;

	void Awake() {

		Check.Null(_playerController);
	}
}
