using UnityEngine;
using System.Collections;
using System;

public class StayForeverManager : Singleton<StayForeverManager> {

	public LayerMask _layer;

	public void StayForever(GameObject[] gameObjects, Action finishedMethod) {

		int[] originalLayers = new int[gameObjects.Length];

		for (int i = 0; i < gameObjects.Length; i++) {
			originalLayers[i] = gameObjects[i].layer;
			gameObjects[i].layer = LayerMask.NameToLayer("StayForever");
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
