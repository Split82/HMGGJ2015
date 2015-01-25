using UnityEngine;
using System.Collections;

public class ScreenTraits : MonoBehaviour {

	public BlurEffect _blurEffect;
	public TwirlEffect _twirlEffect;
	public GrayscaleEffect _grayscaleEffect;
	
	void Start () {

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("BARM", () => {
			_blurEffect.enabled = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("AKUP", () => {
			_twirlEffect.enabled = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("KLOK", () => {
			_grayscaleEffect.enabled = true;
		});
	}
}
