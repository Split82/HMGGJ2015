using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardViewController : MonoBehaviour {

	public RectTransform _rectTransform;
	public RectMoveAnimator _rectMoveAnimator;
	public Image _overlayImage;

	public bool Highlighted {

		set {
			_highlighted = value;
			_overlayImage.enabled = _highlighted;
		}
		get {
			return _highlighted;
		}
	}

	private bool _highlighted;

	void Awake() {

		Check.Null(_rectTransform);
		Check.Null(_rectMoveAnimator);
		Check.Null(_overlayImage);
	}
}
