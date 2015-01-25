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

	private Altar _altarWithCard;

	void Start() {

		SpawnCardOnRandomAltar();
	}
	
	public void RegisterAltar(Altar altar) {

		if (_altars == null) {
			_altars = new List<Altar>();
		}
		_altars.Add(altar);
	}

	public void SpawnCardOnRandomAltar() {

		_altarWithCard = _altars[UnityEngine.Random.Range(0, _altars.Count)];
		_altarWithCard.HasCard = true;

		if (CardHasSpawnedOnAltarEvent != null) {
			CardHasSpawnedOnAltarEvent();
		}
	}

}
