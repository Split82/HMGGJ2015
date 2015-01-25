using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardViewController : MonoBehaviour {

	public RectTransform _rectTransform;
	public RectMoveAnimator _rectMoveAnimator;
	public Image _overlayImage;
	public Text _topText;
	public Text _bottomText;
	public Image _image0;
	public Image _image1;

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
		Check.Null(_topText);
		Check.Null(_bottomText);
		Check.Null(_image0);
		Check.Null(_image1);
	}
}
