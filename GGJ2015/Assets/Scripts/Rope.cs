using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider2D))]

public class Rope : MonoBehaviour {

	public Transform _topTransform;
	public Transform _bottomTransform;
	public Object _ropeMiddlePrefab;

	void Awake() {

		Check.Null(_topTransform);
		Check.Null(_bottomTransform);
		Check.Null(_ropeMiddlePrefab);
	
		float posX = transform.position.x;

		int startY = Mathf.RoundToInt(_bottomTransform.position.y);
		int endY = Mathf.RoundToInt(_topTransform.position.y);

		for (int i = startY + 1; i < endY; i++) {

			GameObject go = (GameObject)Instantiate(_ropeMiddlePrefab, new Vector3(posX, i, 0.0f), Quaternion.identity);
			go.transform.parent = transform;
		}

		BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
		Vector2 size = boxCollider2D.size;
		size.y = endY - startY + 1;
		boxCollider2D.size = size;

		Vector2 center = boxCollider2D.center;
		center.y = -size.y * 0.5f + 0.5f;
		boxCollider2D.center = center;
	}
}
