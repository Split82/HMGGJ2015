using UnityEngine;
using System.Collections;

public class ScreenTraits : MonoBehaviour {

	public BlurEffect _blurEffect;
	public TwirlEffect _twirlEffect;
	public GrayscaleEffect _grayscaleEffect;
	
	void Start () {

		TraitsManager.Instance.RegisterForTraitWasAddedEvent(TraitsManager.Trait.ScreenBlur, () => {
			_blurEffect.enabled = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent(TraitsManager.Trait.ScreenTwirl, () => {
			_twirlEffect.enabled = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent(TraitsManager.Trait.ScreenGrayscale, () => {
			_grayscaleEffect.enabled = true;
		});
	}
}
