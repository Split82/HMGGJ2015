using UnityEngine;
using System.Collections;

public class FireballTraits : MonoBehaviour {

	[LayerPropertyAttribute]
	public int _fireballLayer;

	[LayerPropertyAttribute]
	public int _shieldsLayer;

	[LayerPropertyAttribute]
	public int _wallsLayer;

	void Start() {

		Check.Zero(_fireballLayer);
		Check.Zero(_shieldsLayer);
		Check.Zero(_wallsLayer);

		TraitsManager.Instance.RegisterForTraitWasAddedEvent(TraitsManager.Trait.ShieldsOff, () => {

			Physics2D.IgnoreLayerCollision(_fireballLayer, _shieldsLayer);
		});

		TraitsManager.Instance.RegisterForTraitWasAddedEvent(TraitsManager.Trait.WallsOff, () => {
			
			Physics2D.IgnoreLayerCollision(_fireballLayer, _wallsLayer);
		});
	}
}
