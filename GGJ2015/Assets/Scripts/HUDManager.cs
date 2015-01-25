using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	public Text _cardPickTimerText;

	void Awake () {
	
		Check.Null(_cardPickTimerText);
	}

	void Update () {

		_cardPickTimerText.text = Mathf.RoundToInt(GameplayManager.Instance.TimeTillNextCardPick).ToString();
	}
}
