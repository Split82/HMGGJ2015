using UnityEngine;
using System.Collections;

public class GameTraits : Singleton<GameTraits> {

	public float damageMultiplier = 1f;
	public int numberOfJumps = 2;

	public bool swapControls = false;

	void Start () {

		TraitsManager.Instance.RegisterForTraitWasAddedEvent(TraitsManager.Trait.FasterGame, () => {
			Time.timeScale = 1.5f;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent(TraitsManager.Trait.StrongerGun, () => {
			damageMultiplier = 5f;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent(TraitsManager.Trait.UnlimitedJumps, () => {
			numberOfJumps = int.MaxValue;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent(TraitsManager.Trait.NoDoubleJump, () => {
			numberOfJumps = 1;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent(TraitsManager.Trait.SwapControls, () => {
			swapControls = !swapControls;
		});
	}
}
