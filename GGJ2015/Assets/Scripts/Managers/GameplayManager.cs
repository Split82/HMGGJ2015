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
			float oldTimeScale = Time.timeScale;
			Time.timeScale = 0.0f;
			_cardsHandManager.PresentNewPickedCard(() => {
				Time.timeScale = oldTimeScale;
			});
		};

		_playerController.PlayerWasHitEvent += () => {
			_cardsHandManager.DiscardRandomCard();
			if (_cardsHandManager.NumberOfCardsInHand == 0) {
				Time.timeScale = 0.0f;
				_cardsHandManager.PresentGameOver(() => {
				});
			}
		};
	}

	void Update() {

//		if (Input.GetKeyDown(KeyCode.Space)) {
//			float oldTimeScale = Time.timeScale;
//			Time.timeScale = 0.0f;
//			_cardsHandManager.PresentChooseCard(() => {
//				Time.timeScale = oldTimeScale;
//			});
//		}
	}
}
