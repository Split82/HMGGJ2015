using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TraitsManager : Singleton<TraitsManager> {

	public event Action<string> TraitWasAdded;

	private Dictionary<string, Action> TraitWasAddedEvent;
	private Dictionary<string, int> _activeTraits;

	void Awake () {

		TraitWasAddedEvent = new Dictionary<string, Action>();
		_activeTraits = new Dictionary<string, int>();
	}

	public void AddTrait(string trait) {

		if (!_activeTraits.ContainsKey(trait)) {
			_activeTraits[trait] = 0;
		}
		else {
			_activeTraits[trait] = _activeTraits[trait] + 1;
		}
		if (TraitWasAddedEvent.ContainsKey(trait)) {
			Action action = TraitWasAddedEvent[trait];
			if (action != null) {
				action();
			}
		}

		if (TraitWasAdded != null) {
			TraitWasAdded(trait);
		}
	}

	public void RegisterForTraitWasAddedEvent(string trait, Action action) {
		TraitWasAddedEvent[trait] = action;
	}
}
