using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AltarManager : Singleton<AltarManager> {

	private List<Altar> _altars;

	public Altar AltarWithCard {
		get {
			return _altarWithCard;
		}
	}

	private Altar _altarWithCard;

	void Awake() {
		_altars = new List<Altar>();
	}

	void Start() {

		SpawnCardOnRandomAltar();
	}
	
	public void RegisterAltar(Altar altar) {

		_altars.Add(altar);
	}

	public void SpawnCardOnRandomAltar() {

		_altarWithCard = _altars[Random.Range(0, _altars.Count)];
		_altarWithCard.HasCard = true;
	}

}
