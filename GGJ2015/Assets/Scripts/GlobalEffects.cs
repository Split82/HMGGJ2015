using UnityEngine;
using System.Collections;

public class GlobalEffects : Singleton<GlobalEffects> {

	public ParticleSystem _smoke0PS;
	public ParticleSystem _explosionPS;

	void Awake() {
		Check.Null(_smoke0PS);
	}

	public void EmitSmoke0Particle(Vector3 pos, int count) {

		_smoke0PS.transform.position = pos;
		_smoke0PS.Emit(count);
	}

	public void EmitExplosion0Particle(Vector3 pos, int count) {
		
		_explosionPS.transform.position = pos;
		_explosionPS.Emit(count);
	}
}
