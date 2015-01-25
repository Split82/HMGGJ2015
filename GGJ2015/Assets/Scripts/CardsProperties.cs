using UnityEngine;
using System.Collections;

public class CardsProperties : MonoBehaviour {

	[System.Serializable]
	public class CardProperties {
		public string id;

		public Sprite icon1;
		public Sprite icon2;

		public string text1;
		public string text2;
	}
	public CardProperties[] cardProperties;
}
