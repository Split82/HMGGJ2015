using UnityEngine;
using System.Collections;

public class GameTraits : Singleton<GameTraits> {

	public float damageMultiplier = 1f;
	public int numberOfJumps = 2;

	public bool swapControls = false;

	void Start () {

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("OPPO", () => {
			Time.timeScale = 1.5f;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("WOTA", () => {
			damageMultiplier = 5f;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("FARU", () => {
			numberOfJumps = int.MaxValue;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("NOGO", () => {
			numberOfJumps = 1;
		});
	}
}
