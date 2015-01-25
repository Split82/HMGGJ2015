using UnityEngine;
using System.Collections;

public class ScreenTraits : MonoBehaviour {

	public BlurEffect _blurEffect;
	public TwirlEffect _twirlEffect;
	public GrayscaleEffect _grayscaleEffect;
	
	void Start () {

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("BTS", () => {
			_blurEffect.enabled = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("DTS", () => {
			_twirlEffect.enabled = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("GRS", () => {
			_grayscaleEffect.enabled = true;
		});
	}
}
