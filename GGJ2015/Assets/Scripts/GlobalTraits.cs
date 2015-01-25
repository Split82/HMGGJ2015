using UnityEngine;
using System.Collections;

public class GlobalTraits : Singleton<GlobalTraits> {

	public bool _shieldsOff = true;
	public bool _rearGun = true;
	public bool _wallsOff = true;
	public bool _doubleBullets = true;
	public bool _lessEnemies = true;
	public bool _zoomOut = true;
	public bool _slowEnemies = true;

	void Start () {
	
		TraitsManager.Instance.RegisterForTraitWasAddedEvent("pira", () => {
			_shieldsOff = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("kohu", () => {
			_rearGun = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("qedi", () => {
			_wallsOff = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("azan", () => {
			_doubleBullets = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("kvep", () => {
			_lessEnemies = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("oko", () => {
			_zoomOut = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("oko", () => {
			_slowEnemies = true;
		});

	}
}
