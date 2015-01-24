using UnityEngine;
using System.Collections;

public class PlayerAnimatorController : MonoBehaviour {

	public Animator _animator;
	public PlayerFaceDirection _playerFaceDirection;
	public GroundedCheck _groundedCheck;
	public PlayerMovement _playerMovement;
	public PlayerController _playerController;
	public PlayerRopeMovement _playerRopeMovement;

	private int _groundedAnimatorParam;
	private int _movingAnimatorParam;
	private int _leftAnimatorParam;
	private int _onTheRopeAnimatorParam;
	private int _climbingAnimatorParam;
	
	void Start () {

		Check.Null(_animator);
		Check.Null(_playerFaceDirection);
		Check.Null(_groundedCheck);
		Check.Null(_playerMovement);
		Check.Null(_playerController);
		Check.Null(_playerRopeMovement);

		_groundedAnimatorParam = Animator.StringToHash("Grounded");
		_movingAnimatorParam = Animator.StringToHash("Moving");
		_leftAnimatorParam = Animator.StringToHash("Left");
		_onTheRopeAnimatorParam = Animator.StringToHash("OnTheRope");
		_climbingAnimatorParam = Animator.StringToHash("Climbing");
	}

	void Update () {
	
		_animator.SetBool(_leftAnimatorParam, _playerFaceDirection.Direction == PlayerFaceDirection.DirectionEnum.Left);
		_animator.SetBool(_groundedAnimatorParam, _groundedCheck.IsGrounded);
		_animator.SetBool(_movingAnimatorParam, _playerMovement.IsRunning);
		_animator.SetBool(_onTheRopeAnimatorParam, _playerController.State == PlayerController.StateEnum.OnTheRope);
		_animator.SetBool(_climbingAnimatorParam, _playerRopeMovement.IsClimbing);
	}
}
