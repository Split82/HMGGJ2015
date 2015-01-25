using UnityEngine;
using System.Collections;

public class CardScreensAnimatorController : MonoBehaviour {

	public Animator _animator;
	
	private int _pickedCardAnimatorParam;
	private int _chooseCardAnimatorParam;
	
	void Start () {
		
		Check.Null(_animator);
		
		_pickedCardAnimatorParam = Animator.StringToHash("PickedCard");
		_chooseCardAnimatorParam = Animator.StringToHash("ChooseCard");
	}

	public void ShowPickedCardScreen() {

		_animator.SetBool(_pickedCardAnimatorParam, true);
	}

	public void ShowChooseCardScreen() {
		
		_animator.SetBool(_chooseCardAnimatorParam, true);
	}

	public void HideAll() {
		_animator.SetBool(_pickedCardAnimatorParam, false);
		_animator.SetBool(_chooseCardAnimatorParam, false);
	}
}
