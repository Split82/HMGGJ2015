using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(RectTransform))]

public class RectMoveAnimator : MonoBehaviour {

	public AnimationCurve _animationCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);

	private RectTransform _rectTransform;

	void Awake() {

		_rectTransform = GetComponent<RectTransform>();
	}
	
	public void MoveToPosition(Vector3 pos, float duration, Action animationDidFinish) {

		StopAllCoroutines();
		StartCoroutine(MoveToPositionCoroutine(pos, duration, animationDidFinish));
	}

	private IEnumerator MoveToPositionCoroutine(Vector3 pos, float duration, Action animationDidFinish) {

		Vector3 startPos = _rectTransform.localPosition;
		float elapsedTime = 0.0f;
		while (elapsedTime < duration) {

			_rectTransform.localPosition = Vector3.Lerp(startPos, pos, _animationCurve.Evaluate(elapsedTime / duration));

			yield return new WaitForEndOfFrame();
			elapsedTime += Time.deltaTime;
		}

		_rectTransform.localPosition = pos;

		if (animationDidFinish != null) {
			animationDidFinish();
		}
	}
}
