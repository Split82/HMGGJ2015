using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	public Text _cardPickTimerText;
	public Text _scoreText;
	public Text _scoreFinishText;

	void Awake () {
	
		Check.Null(_cardPickTimerText);
		Check.Null(_scoreText);
		Check.Null(_scoreFinishText);
	}

	void Update () {

		_cardPickTimerText.text = Mathf.RoundToInt(GameplayManager.Instance.TimeTillNextCardPick).ToString();
		_scoreText.text = GameplayManager.Instance.Score.ToString("00000");
		_scoreFinishText.text = _scoreText.text;
	}
}
