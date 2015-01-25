using UnityEngine;
using System.Collections;

public class BasicEnemyAnimatorController : MonoBehaviour {

	public Animator _animator;
	public FaceDirection _faceDirection;
	public EnemyController _enemyController;
	
	private int _leftAnimatorParam;
	private int _deadAnimatorParam;
	
	void Start () {
		
		Check.Null(_animator);
		Check.Null(_faceDirection);

		_leftAnimatorParam = Animator.StringToHash("Left");
		_deadAnimatorParam = Animator.StringToHash("Dead");

	}
	
	void Update () {

		_animator.SetBool(_deadAnimatorParam, _enemyController._health <= 0);
		_animator.SetBool(_leftAnimatorParam, _faceDirection.Direction == FaceDirection.DirectionEnum.Left);
	}
}
