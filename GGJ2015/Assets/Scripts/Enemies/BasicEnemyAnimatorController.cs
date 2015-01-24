using UnityEngine;
using System.Collections;

public class BasicEnemyAnimatorController : MonoBehaviour {

	public Animator _animator;
	public FaceDirection _faceDirection;
	
	private int _leftAnimatorParam;
	
	void Start () {
		
		Check.Null(_animator);
		Check.Null(_faceDirection);

		_leftAnimatorParam = Animator.StringToHash("Left");
	}
	
	void Update () {

		_animator.SetBool(_leftAnimatorParam, _faceDirection.Direction == FaceDirection.DirectionEnum.Left);
	}
}
