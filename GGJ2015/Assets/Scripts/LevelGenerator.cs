using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

	public char[,] GenerateLevel() {

		char[,] data = new char[10, 10];

		for (int i = 0; i < 100; i++) {
			for (int j = 0; j < 100; j++) {			
				data[i, j] = ' ';
			}
		}

		return data;
	}
}
