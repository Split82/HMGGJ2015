using UnityEngine;
using System.Collections;

public class GlobalTraits : Singleton<GlobalTraits> {

	public bool _shieldsOff;
	public bool _rearGun;
	public bool _wallsOff;
	public bool _doubleBullets;
	public bool _lessEnemies;
	public bool _zoomOut;
	public bool _slowEnemies;
	public bool _moreEnemies;

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

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("krox", () => {
			_moreEnemies = true;
		});

	}
}
