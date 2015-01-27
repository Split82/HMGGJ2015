using UnityEngine;
using System.Collections;
using System;

public class StayForeverManager : Singleton<StayForeverManager> {

	[LayerPropertyAttribute]
	public int _layer;

	public void StayForever(GameObject gameObject, Action finishedMethod) {

		StayForever(new GameObject[] {gameObject}, finishedMethod);
	}

	public void StayForever(GameObject[] gameObjects, Action finishedMethod) {

		int[] originalLayers = new int[gameObjects.Length];

		for (int i = 0; i < gameObjects.Length; i++) {
			originalLayers[i] = gameObjects[i].layer;
			gameObjects[i].layer = _layer;
		}
		StartCoroutine(StayForeverCoroutine(gameObjects, originalLayers, finishedMethod));
	}

	private IEnumerator StayForeverCoroutine(GameObject[] gameObjects, int[] originalLayers, Action finishedMethod) {

		yield return new WaitForEndOfFrame();

		for (int i = 0; i < gameObjects.Length; i++) {
			gameObjects[i].layer = originalLayers[i];
		}

		finishedMethod();
	}
}
