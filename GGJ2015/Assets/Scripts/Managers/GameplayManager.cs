using UnityEngine;
using System.Collections;

public class GameplayManager : Singleton<GameplayManager> {

	public PlayerController _playerController;
	public CardsHandManager _cardsHandManager;
	public AltarManager _altarManager;

	void Awake() {

		Check.Null(_playerController);
		Check.Null(_cardsHandManager);
		Check.Null(_altarManager);

		_altarManager.CardWasPickedFromAltarEvent += () => {
			Time.timeScale = 0.0f;
			_cardsHandManager.PresentNewPickedCard(() => {
				Time.timeScale = 1.0f;
			});
		};
	}

	void Update() {

		if (Input.GetKeyDown(KeyCode.Space)) {
			Time.timeScale = 0.0f;
			_cardsHandManager.PresentChooseCard(() => {
				Time.timeScale = 1.0f;
			});
		}
	}
}
