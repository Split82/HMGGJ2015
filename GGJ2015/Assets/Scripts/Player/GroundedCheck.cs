using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GroundedCheck : MonoBehaviour {

	#region Unity params

	public LayerMask _groundLayerMask;
	public Vector2 _offset = new Vector2(0.0f, -1.0f);
	public Vector2 _size = new Vector2(1.0f, 1.0f);
	
	#endregion

	
	#region Vars / Properties
	
	public bool IsGrounded {
		get {
			if (!_isGrounded.HasValue) {

				Vector2 pos = (Vector2)_transform.position + _offset;
				Vector2 posA = pos - _size * 0.5f;
				Vector2 posB = pos + _size * 0.5f;

				_isGrounded = (Physics2D.OverlapAreaNonAlloc(posA, posB, _groundCheckColliders, _groundLayerMask.value) > 0);
			}
			return _isGrounded.Value;
		}
	}

	private Collider2D[] _groundCheckColliders;
	private Transform _transform;
	private bool? _isGrounded;

	#endregion


	#region Unity callbacks

	void Awake() {

		_transform = transform;
		_groundCheckColliders = new Collider2D[10];
	}

	void OnDrawGizmos() {

		Vector2 pos = (Vector2)transform.position + _offset;
		Vector2 posA = pos - _size * 0.5f;
		Vector2 posB = pos + _size * 0.5f;
		Gizmos.DrawLine (posA, posB);
	}

	void FixedUpdate() {
		
		_isGrounded = null;
	}


	#endregion

}
