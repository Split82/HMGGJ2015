using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBuilder : MonoBehaviour {

	public LevelGenerator _levelGenerator;
	public GameObject _tileGround0Prefab;

	public Vector2 StartPos {
		get {
			return _startPos;
		}
	}

	private Transform _transform;
	private Vector2 _startPos;

	void Awake() {

		_transform = transform;

		char[,] tiles = _levelGenerator.GenerateLevel();
		for (int j = 0; j < tiles.GetLength(1); j++) {
			for (int i = 0; i < tiles.GetLength(0); i++) {
				char c = tiles[i, j];
				// X S E

				if (c == 'X') {
					GameObject go = (GameObject)Instantiate(_tileGround0Prefab);
					go.transform.parent = _transform;
					go.transform.position = new Vector3(i, j, 0.0f);
				}
				else if (c == 'S') {
					_startPos = new Vector2(i, j);
				}
				
			}
		}
	}
}
