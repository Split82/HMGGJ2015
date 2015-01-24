using UnityEngine;
using System.Collections;

public class PlayerAnimatorController : MonoBehaviour {

	public Animator _animator;
	public PlayerFaceDirection _playerFaceDirection;
	public GroundedCheck _groundedCheck;
	public PlayerMovement _playerMovement;

	private int _groundedAnimatorParam;
	private int _movingAnimatorParam;
	private int _leftAnimatorParam;
	
	void Start () {

		Check.Null(_animator);
		Check.Null(_playerFaceDirection);
		Check.Null(_groundedCheck);
		Check.Null(_playerMovement);

		_groundedAnimatorParam = Animator.StringToHash("Grounded");
		_movingAnimatorParam = Animator.StringToHash("Moving");
		_leftAnimatorParam = Animator.StringToHash("Left");
	}

	void Update () {
	
		_animator.SetBool(_leftAnimatorParam, _playerFaceDirection.Direction == PlayerFaceDirection.DirectionEnum.Left);
		_animator.SetBool(_groundedAnimatorParam, _groundedCheck.IsGrounded);
		_animator.SetBool(_movingAnimatorParam, _playerMovement.IsRunning);
	}
}
