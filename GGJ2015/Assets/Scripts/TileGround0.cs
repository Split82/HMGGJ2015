using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class TileGround0 : MonoBehaviour {

	public Sprite[] _sprites;
	
	void Start () {

		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
	}
}
