using UnityEngine;
using System.Collections;
using System;

public class Altar : MonoBehaviour {

	public GameObject _cardOnAltar;

	public event Action CardWasPickedUpEvent;

	public bool HasCard {
		set {
			_hasCard = value;
			_cardOnAltar.SetActive(_hasCard);
		}
		get {
			return _hasCard;
		}
	}

	private bool _hasCard;

	void OnEnable () {

		Check.Null(_cardOnAltar);

		AltarManager.Instance.RegisterAltar(this);
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (HasCard) {
			HasCard = false;

			if (CardWasPickedUpEvent != null) {
				CardWasPickedUpEvent();
			}
		}	
	}
}
