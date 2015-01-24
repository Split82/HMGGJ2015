using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardsHandManager : MonoBehaviour {

	public RectTransform[] _cardSlotsRectTransforms;
	public Object _cardPrefab;
	public Vector3 _startPosition;
	public Vector3 _idleCardsOffset = new Vector3(10.0f, -120.0f, 0.0f);
	public Vector3 _pickingLayoutOffset = new Vector3(10.0f, -20.0f, 0.0f);
	public float _cardWidth = 140.0f;

	private const int kMaxNumberOfCards = 5;
	private List<CardViewController> _cardViewControllers;

	void Start() {

		Check.Array(_cardSlotsRectTransforms, kMaxNumberOfCards);	
		SpawnRandomCards();
		LayoutForIdle();
		//LayoutCardsForPicking();
	}

	public void SpawnRandomCards() {

		_cardViewControllers = new List<CardViewController>();
		for (int i = 0; i < kMaxNumberOfCards; i++) {
			GameObject go = (GameObject)Instantiate(_cardPrefab);
			go.transform.SetParent(transform);
			go.transform.localScale = Vector3.one;
			CardViewController cardViewController = go.GetComponent<CardViewController>();
			_cardViewControllers.Add(cardViewController);
			cardViewController._rectTransform.localPosition = _startPosition;
		}
	}

	public void LayoutForIdle() {

		for (int i = 0; i < _cardViewControllers.Count; i++) {
			
			Vector3 pos = _cardSlotsRectTransforms[i].localPosition + _idleCardsOffset;
			_cardViewControllers[i]._rectMoveAnimator.MoveToPosition(pos, 1.5f, null);
		}
	}

	public void LayoutCardsForPicking() {
	
		float posX = -(_cardWidth * (_cardViewControllers.Count - 1)) * 0.5f;

		for (int i = 0; i < _cardViewControllers.Count; i++) {

			Vector3 pos = new Vector3(posX, _cardSlotsRectTransforms[i].localPosition.y, 0.0f) + _pickingLayoutOffset;
			_cardViewControllers[i]._rectMoveAnimator.MoveToPosition(pos, 1.5f, null);
			posX += _cardWidth;
		}
	}

	public void HighlightCard(int cardNum) {

		for (int i = 0; i < _cardViewControllers.Count; i++) {
			_cardViewControllers[i].Highlighted = i == cardNum;
		}
	}

	public void DiscardCard(int cardNum) {

		CardViewController cardViewController = _cardViewControllers[cardNum];

		cardViewController._rectMoveAnimator.MoveToPosition(cardViewController._rectTransform.position + new Vector3(0.0f, 500.0f, 0.0f), 1.0f, () => {
			Destroy(cardViewController.gameObject);
		});

		_cardViewControllers[cardNum] = null;
		_cardViewControllers.RemoveAll(item => item == null);
	}

	void Update() {

		if (GameControlsManager.Instance.LeftButtonPressed) {
			DiscardCard(0);

		}

		if (GameControlsManager.Instance.RightButtonPressed) {
			HighlightCard(0);
			LayoutCardsForPicking();
		}

	}
}
