using UnityEngine;
using System.Collections;

public class EnemySpawnZoneRect : MonoBehaviour {

	void OnDrawGizmos() {
		
		Vector2 pos = (Vector2)transform.position;
		Vector2 size = (Vector2)transform.localScale;
		Vector2 bottomLeft = new Vector2(pos.x - size.x * 0.5f, pos.y - size.y * 0.5f);
		Vector2 bottomRight = new Vector2(pos.x + size.x * 0.5f, pos.y - size.y * 0.5f);
		Vector2 topLeft = new Vector2(pos.x - size.x * 0.5f, pos.y + size.y * 0.5f);
		Vector2 topRight = new Vector2(pos.x + size.x * 0.5f, pos.y + size.y * 0.5f);
		Gizmos.DrawLine (bottomLeft, bottomRight);
		Gizmos.DrawLine (bottomRight, topRight);
		Gizmos.DrawLine (topRight, topLeft);
		Gizmos.DrawLine (topLeft, bottomLeft);
		Gizmos.DrawLine (bottomLeft, topRight);
	}
}
