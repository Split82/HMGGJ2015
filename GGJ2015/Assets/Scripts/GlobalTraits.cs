using UnityEngine;
using System.Collections;

public class GlobalTraits : Singleton<GlobalTraits> {
	
	public bool _rearGun;
	public bool _doubleBullets;
	public bool _lessEnemies;
	public bool _zoomOut;
	public bool _slowEnemies;
	public bool _moreEnemies;

	void Start () {
	
		TraitsManager.Instance.RegisterForTraitWasAddedEvent(TraitsManager.Trait.RearGun, () => {
			_rearGun = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent(TraitsManager.Trait.DoubleBullets, () => {
			_doubleBullets = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent(TraitsManager.Trait.LessEnemies, () => {
			_lessEnemies = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent(TraitsManager.Trait.ZoomOut, () => {
			_zoomOut = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent(TraitsManager.Trait.SlowEnemies, () => {
			_slowEnemies = true;
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent(TraitsManager.Trait.MoreEnemies, () => {
			_moreEnemies = true;
		});

	}
}
