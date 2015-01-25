using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AltarManager : Singleton<AltarManager> {

	private List<Altar> _altars;

	public Altar AltarWithCard {
		get {
			return _altarWithCard;
		}
	}

	public event Action CardHasSpawnedOnAltarEvent;
	public event Action CardWasPickedFromAltarEvent;

	private Altar _altarWithCard;

	void Start() {

		SpawnCardOnRandomAltarDifferentFromAltar(null);
	}
	
	public void RegisterAltar(Altar altar) {

		if (_altars == null) {
			_altars = new List<Altar>();
		}
		_altars.Add(altar);

		altar.CardWasPickedUpEvent += () => {
			if (CardWasPickedFromAltarEvent != null) {
				CardWasPickedFromAltarEvent();
			}
			SpawnCardOnRandomAltarDifferentFromAltar(altar);
		};
	}

	public void SpawnCardOnRandomAltarDifferentFromAltar(Altar altar) {

		Altar newAltar;
		do {
			newAltar = _altars[UnityEngine.Random.Range(0, _altars.Count)];
		} while (newAltar == altar);

		_altarWithCard = newAltar;
		_altarWithCard.HasCard = true;

		if (CardHasSpawnedOnAltarEvent != null) {
			CardHasSpawnedOnAltarEvent();
		}
	}

}
