using UnityEngine;
using System.Collections;

public class CardsProperties : Singleton<CardsProperties> {

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
	
	public Card GenereteNewCard() {

		TraitProperties pos = posTraitProperties[Random.Range(0, posTraitProperties.Length)];
		TraitProperties neg = negTraitProperties[Random.Range(0, negTraitProperties.Length)];
		return new Card(pos, neg);
	}

	public TraitProperties TraitForID(string traitID) {

		foreach (TraitProperties prop in posTraitProperties) {
			if (prop.id == traitID) {
				return prop;
			}
		}

		foreach (TraitProperties prop in negTraitProperties) {
			if (prop.id == traitID) {
				return prop;
			}
		}

		return null;
	}
}
