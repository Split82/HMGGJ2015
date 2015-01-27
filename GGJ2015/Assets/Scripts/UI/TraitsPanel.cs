using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TraitsPanel : MonoBehaviour {

	public GameObject _traitImagePrefab;

	private float _lastTraitPosX = 5.0f;
	private float _traitWidth = 10.0f;
	private float _separator = 5.0f;

	void Start () {

		Check.Null (_traitImagePrefab);

		TraitsManager.Instance.TraitWasAdded += (trait) => {

			GameObject go = (GameObject)Instantiate(_traitImagePrefab);
			go.transform.SetParent(transform);
			Image image = go.GetComponent<Image>();
			image.rectTransform.localScale = Vector3.one;
			image.rectTransform.localPosition = new Vector3(_lastTraitPosX, -5.0f, 0.0f);
			image.sprite = CardsProperties.Instance.TraitPropertiesForTrait(trait).icon;
			_lastTraitPosX += (_traitWidth + _separator);
		};	
	}
}
