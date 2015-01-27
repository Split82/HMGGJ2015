using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardViewController : MonoBehaviour {

	public RectTransform _rectTransform;
	public RectMoveAnimator _rectMoveAnimator;
	public Image _overlayImage;
	public Text _topText;
	public Text _bottomText;
	public Text _posText;
	public Text _negText;
	public Image _image0;
	public Image _image1;

	public CardsProperties.Card CurrentCard {
		get {
			return _currentCard;
		}
		set {
			_currentCard = value;
			_topText.text = _currentCard.pos.text;
			_bottomText.text = _currentCard.neg.text;
			_posText.text = _currentCard.pos.id;
			_negText.text = _currentCard.neg.id;
			_image0.sprite = _currentCard.pos.icon;
			_image1.sprite = _currentCard.neg.icon;
		}
	}
	

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
	private CardsProperties.Card _currentCard;

	void Awake() {

		Check.Null(_rectTransform);
		Check.Null(_rectMoveAnimator);
		Check.Null(_overlayImage);
		Check.Null(_topText);
		Check.Null(_bottomText);
		Check.Null(_posText);
		Check.Null(_negText);
		Check.Null(_image0);
		Check.Null(_image1);
	}
}
