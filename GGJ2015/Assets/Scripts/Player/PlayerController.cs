using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Rigidbody2D _rigidbody2D;

	void Awake() {

		Check.Null(_rigidbody2D);
	}
}
