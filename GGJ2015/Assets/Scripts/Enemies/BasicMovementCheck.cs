using UnityEngine;
using System.Collections;
using System;

public class BasicMovementCheck : MonoBehaviour {

	public GroundedCheck _fallGroundedCheck;
	public GroundedCheck _wallGroundedCheck;
	public float _checkInterval = 0.1f;

	public event Action CanNotMoveInTheSameDirectionEvent;

	public bool CanMove {
		get {
			return _fallGroundedCheck.IsGrounded && !_wallGroundedCheck.IsGrounded;
		}
	}

	public DirectionClass.DirectionEnum Direction {

		set {
			_direction = value;
			if (_direction == DirectionClass.DirectionEnum.Left) {
				transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
			}
			else {
				transform.localScale = Vector3.one;
			}
		}
		get {
			return _direction;
		}
	}

	private DirectionClass.DirectionEnum _direction = DirectionClass.DirectionEnum.Right;

	void Start() {
		Check.Null(_fallGroundedCheck);
		Check.Null(_wallGroundedCheck);
	}

	IEnumerator MovementCheck() {
		
		while (true) {
			if (!CanMove) {
				if (CanNotMoveInTheSameDirectionEvent != null) {
					CanNotMoveInTheSameDirectionEvent();
				}
			}
			yield return new WaitForSeconds(_checkInterval);
		}
	}

	public void StartWorking() {

		StartCoroutine(MovementCheck());
	}
}
