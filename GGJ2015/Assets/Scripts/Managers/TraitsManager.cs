using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TraitsManager : Singleton<TraitsManager> {

	public event Action<Trait> TraitWasAdded;
	
	public enum Trait {
		ShieldsOff,
		RearGun,
		WallsOff,
		DoubleBullets,
		LessEnemies,
		ZoomOut,
		SlowEnemies,
		MoreEnemies,
		FasterGame,
		StrongerGun,
		UnlimitedJumps,
		NoDoubleJump,
		SwapControls,
		ScreenBlur,
		ScreenTwirl,
		ScreenGrayscale
	};

	private Dictionary<Trait, Action> TraitWasAddedEvent;
	private Dictionary<Trait, int> _activeTraits;

	void Awake () {

		TraitWasAddedEvent = new Dictionary<Trait, Action>();
		_activeTraits = new Dictionary<Trait, int>();
	}

	public void AddTrait(Trait trait) {

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

	public void RegisterForTraitWasAddedEvent(Trait trait, Action action) {

		TraitWasAddedEvent[trait] = action;
	}
}
