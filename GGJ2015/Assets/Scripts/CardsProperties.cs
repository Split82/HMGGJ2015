using UnityEngine;
using System.Collections;

public class CardsProperties : MonoBehaviour {

	[System.Serializable]
	public class TraitProperties {
		public string id;

		public Sprite icon;

		public string text;
	}

	public class Card {
		public TraitProperties pos, neg;
		public Card(TraitProperties pos, TraitProperties neg) {
			this.pos = pos;
			this.neg = neg;
		}
	}

	public TraitProperties[] posTraitProperties;
	public TraitProperties[] negTraitProperties;
	
	public Card genereteNewCard() {
		TraitProperties pos = posTraitProperties[Random.Range(0, posTraitProperties.Length)];
		TraitProperties neg = posTraitProperties[Random.Range(0, negTraitProperties.Length)];
		return new Card(pos, neg);
	}
}
