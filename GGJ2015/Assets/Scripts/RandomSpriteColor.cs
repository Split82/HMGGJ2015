using UnityEngine;
using System.Collections;

public class RandomSpriteColor : MonoBehaviour {

	public SpriteRenderer _spriteRenderer;
	public Color _color0 = Color.white;
	public Color _color1 = Color.black;

	void Start() {

		_spriteRenderer.color = Color.Lerp(_color0, _color1, Random.value);
	}
}
