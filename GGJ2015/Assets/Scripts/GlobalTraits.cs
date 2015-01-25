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
	
		TraitsManager.Instance.RegisterForTraitWasAddedEvent("PIRA", () => {
			_shieldsOff = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("KOHU", () => {
			_rearGun = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("QEDI", () => {
			_wallsOff = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("AZAN", () => {
			_doubleBullets = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("KVEP", () => {
			_lessEnemies = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("OKO", () => {
			_zoomOut = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("NOHA", () => {
			_slowEnemies = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent("KROX", () => {
			_moreEnemies = true;
		});

	}
}
