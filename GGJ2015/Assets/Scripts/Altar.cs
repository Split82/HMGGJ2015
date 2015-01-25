using UnityEngine;
using System.Collections;

public class Altar : MonoBehaviour {

	public GameObject _cardOnAltar;

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

}
