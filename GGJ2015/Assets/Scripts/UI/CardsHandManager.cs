using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardsHandManager : MonoBehaviour {

	public RectTransform[] _cardSlotsRectTransforms;
	public CardScreensAnimatorController _cardScreensAnimatorController;
	public Object _cardPrefab;
	public Vector3 _startPosition;
	public Vector3 _idleCardsOffset = new Vector3(10.0f, -120.0f, 0.0f);
	public Vector3 _pickingLayoutOffset = new Vector3(10.0f, -20.0f, 0.0f);
	public float _cardWidth = 140.0f;
	
	private const int kMaxNumberOfCards = 5;
	private List<CardViewController> _cardViewControllers;

	void Start() {

		Check.Array(_cardSlotsRectTransforms, kMaxNumberOfCards);	
		Check.Null(_cardScreensAnimatorController);
		Check.Null(_cardPrefab);

		SpawnRandomCards();
		LayoutCardsForIdle();
	}

	private CardViewController CreateNewCard() {
		GameObject go = (GameObject)Instantiate(_cardPrefab);
		go.transform.SetParent(transform);
		go.transform.localScale = Vector3.one;
		CardViewController cardViewController = go.GetComponent<CardViewController>();
		return cardViewController;
	}

	private void SpawnRandomCards() {

		_cardViewControllers = new List<CardViewController>();
		for (int i = 0; i < kMaxNumberOfCards; i++) {
			CardViewController cardViewController = CreateNewCard();
			_cardViewControllers.Add(cardViewController);
			cardViewController._rectTransform.localPosition = _startPosition;
		}
	}

	private void LayoutCardsForIdle() {

		for (int i = 0; i < _cardViewControllers.Count; i++) {
			
			Vector3 pos = _cardSlotsRectTransforms[i].localPosition + _idleCardsOffset;
			_cardViewControllers[i]._rectMoveAnimator.MoveToPosition(pos, 0.5f + i * 0.15f, null);
		}
	}

	private void LayoutCardsForPicking(System.Action finishedDelegate) {
	
		float posX = -(_cardWidth * (_cardViewControllers.Count - 1)) * 0.5f;

		for (int i = 0; i < _cardViewControllers.Count; i++) {

			Vector3 pos = new Vector3(posX, _startPosition.y, 0.0f) + _pickingLayoutOffset;
			int j = i;
			_cardViewControllers[i]._rectMoveAnimator.MoveToPosition(pos, 0.8f + i * 0.15f, () => {
				if (j == 0 && finishedDelegate != null) {
					finishedDelegate();
				}
			});
			posX += _cardWidth;
		}
	}

	private void HighlightCard(int cardNum) {

		for (int i = 0; i < _cardViewControllers.Count; i++) {
			_cardViewControllers[i].Highlighted = i == cardNum;
		}
	}

	private void DiscardCard(int cardNum) {

		CardViewController cardViewController = _cardViewControllers[cardNum];

		cardViewController._rectMoveAnimator.MoveToPosition(cardViewController._rectTransform.position + new Vector3(0.0f, 500.0f, 0.0f), 1.0f, () => {
			Destroy(cardViewController.gameObject);
		});

		_cardViewControllers[cardNum] = null;
		_cardViewControllers.RemoveAll(item => item == null);
	}

	public void PresentNewPickedCard(System.Action finishedDelegate) {

		StartCoroutine(PresentNewPickedCardCoroutine(finishedDelegate));
	}

	public void PresentChooseCard(System.Action finishedDelegate) {
		
		StartCoroutine(PresentChooseCardCoroutine(finishedDelegate));
	}

	private IEnumerator PresentNewPickedCardCoroutine(System.Action finishedDelegate) {

		_cardScreensAnimatorController.ShowPickedCardScreen();

		bool canContinue = false;

		// New Card
		CardViewController cardViewController = CreateNewCard();
		cardViewController._rectTransform.localPosition = _startPosition + new Vector3(0.0f, 500.0f, 0.0f);
		cardViewController._rectMoveAnimator.MoveToPosition(_startPosition, 0.45f, () => {
			canContinue = true;
		});

		while (!canContinue || !GameControlsManager.Instance.FireButonIsActive) {
			yield return new WaitForEndOfFrame();
		}

		if (_cardViewControllers.Count == kMaxNumberOfCards) {
			DiscardCard(Random.Range(0, _cardViewControllers.Count));
		}
		_cardViewControllers.Add(cardViewController);
		LayoutCardsForIdle();
		_cardScreensAnimatorController.HideAll();

		if (finishedDelegate != null) {
			finishedDelegate();
		}
	}

	private IEnumerator PresentChooseCardCoroutine(System.Action finishedDelegate) {
		
		_cardScreensAnimatorController.ShowChooseCardScreen();
		
		bool canContinue = false;

		LayoutCardsForPicking(() => {
			canContinue = true;
		});
			
		int highlightedCardIdx = 0;
		HighlightCard(highlightedCardIdx);

		bool canChangeHighlight = false;

		while (!canContinue || !GameControlsManager.Instance.FireButonIsActive) {

			if (GameControlsManager.Instance.RightButtonIsActive) {
				if (canChangeHighlight) {
					 highlightedCardIdx = (highlightedCardIdx + 1) % _cardViewControllers.Count;
					HighlightCard(highlightedCardIdx);
					canChangeHighlight = false;
				}
			}
			else if (GameControlsManager.Instance.LeftButtonIsActive) {
				if (canChangeHighlight) {
					highlightedCardIdx--;
					if (highlightedCardIdx < 0) {
						highlightedCardIdx = _cardViewControllers.Count - 1;
					}
					HighlightCard(highlightedCardIdx);
					canChangeHighlight = false;
				}
			}
			else {
				canChangeHighlight = true;
			}

			yield return new WaitForEndOfFrame();
		}

		DiscardCard(highlightedCardIdx);

		LayoutCardsForIdle();
		_cardScreensAnimatorController.HideAll();
		
		if (finishedDelegate != null) {
			finishedDelegate();
		}
	}

}
