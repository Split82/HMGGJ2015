using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(Transform))]
public class EnemySeeHorizontalPlayerCheck : MonoBehaviour {
	
	private Transform _transform;
	private DirectionClass.DirectionEnum _simpleDirectionToPlayer;
	private bool _isPlayerVisibleHorizontal;
	
	public LayerMask _playerLayerMask;
	
	public event Action IsPlayerVisibleHorizontalValueHasChangedEvent;
	public event Action SimpleDirectionToPlayerValueHasChangedEvent;

	public bool IsPlayerVisibleHorizontal {
		get {
			return _isPlayerVisibleHorizontal;
		}
	}

	public DirectionClass.DirectionEnum SimpleDirectionToPlayer {
		get {
			return _simpleDirectionToPlayer;
		}
	}
	
	void Start() {

		_transform = GetComponent<Transform>();
	}
	
	IEnumerator PlayerVisibilityCheck() {
		
		while (true) {
			bool oldIsPlayerVisibleHorizontal = _isPlayerVisibleHorizontal;
			DirectionClass.DirectionEnum oldSimpleDirectionToPlayer = _simpleDirectionToPlayer;
			
			UpdateIsPlayerVisible();

			if (_isPlayerVisibleHorizontal != oldIsPlayerVisibleHorizontal) {
				if (IsPlayerVisibleHorizontalValueHasChangedEvent != null) {
					IsPlayerVisibleHorizontalValueHasChangedEvent();
				}
			}

			if (_simpleDirectionToPlayer != oldSimpleDirectionToPlayer) {
				if (SimpleDirectionToPlayerValueHasChangedEvent != null) {
					SimpleDirectionToPlayerValueHasChangedEvent();
				}
			}
			
			yield return new WaitForSeconds(0.05f);
		}
	}

	void UpdateDirectionToPlayer() {

		Vector3 direction = (GameplayManager.Instance._playerController._playerTransform.position - _transform.position);
		_simpleDirectionToPlayer = direction.x > 0 ? DirectionClass.DirectionEnum.Right : DirectionClass.DirectionEnum.Left;
	}

	void UpdateIsPlayerVisible() {

		UpdateDirectionToPlayer();

		RaycastHit2D[] hits = new RaycastHit2D[1];
	
		// right
		int count = Physics2D.RaycastNonAlloc(_transform.position, new Vector2(1f, 0f), hits, float.MaxValue, _playerLayerMask);
		// left
		count += Physics2D.RaycastNonAlloc(_transform.position, new Vector2(-1f, 0f), hits, float.MaxValue, _playerLayerMask);
		_isPlayerVisibleHorizontal = (count > 0);
	}

	public void StartWorking() {
		
		StartCoroutine(PlayerVisibilityCheck());
	}
}
