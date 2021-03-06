using UnityEngine;
using System.Collections;

public class GameplayManager : Singleton<GameplayManager> {

	public PlayerController _playerController;
	public CardsHandManager _cardsHandManager;
	public AltarManager _altarManager;
	public float _cardPickTimeInterval = 3.0f;

	public float TimeTillNextCardPick {
		get {
			return _timeTillNextCardPick;
		}
	}

	public int Score {
		get {
			return _score;
		}
	}

	private float _timeTillNextCardPick;
	private int _score;

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
					Time.timeScale = 1.0f;
					Application.LoadLevel(Application.loadedLevel);
				});
			}
		};

		_cardsHandManager.CardWasChosenEvent += (CardsProperties.Card card) => {
			TraitsManager.Instance.AddTrait(card.pos.trait);
			TraitsManager.Instance.AddTrait(card.neg.trait);
		};

		EnemyManager.Instance.EnemyWasKilledEvent += () => {
			_score++;
		};

		CardsHandManager.Instance.CardWasPickedEvent += () => {
			_score += 100;
		};

	}

	void Start() {

		StartCoroutine(CardPickCoroutine());
	}

	private IEnumerator CardPickCoroutine() {

		while (true) {

			float startTime = Time.timeSinceLevelLoad;
			while (Time.timeSinceLevelLoad - startTime < _cardPickTimeInterval) {

				_timeTillNextCardPick = _cardPickTimeInterval - (Time.timeSinceLevelLoad - startTime);
				yield return new WaitForSeconds(0.2f);
			}

			_timeTillNextCardPick = 0.0f;

			if (_cardsHandManager.NumberOfCardsInHand <= 1) {
				Time.timeScale = 0.0f;
				_cardsHandManager.PresentGameOver(() => {
					Time.timeScale = 1.0f;
					Application.LoadLevel(Application.loadedLevel);
				});
			}
			else {
				float oldTimeScale = Time.timeScale;
				Time.timeScale = 0.0f;
				_cardsHandManager.PresentChooseCard(() => {
					Time.timeScale = oldTimeScale;
				});
			}
		}
	}

}
