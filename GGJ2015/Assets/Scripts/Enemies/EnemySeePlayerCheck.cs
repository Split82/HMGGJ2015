using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(Transform))]
public class EnemySeePlayerCheck : MonoBehaviour {
	
	private Transform _transform;
	private Vector2 _directionToPlayer;
	private DirectionClass.DirectionEnum _simpleDirectionToPlayer;
	private bool _isPlayerVisible;
	private bool _isPlayerVisibleHorizontal;
	
	public LayerMask _groundLayerMask;
	public LayerMask _playerLayerMask;

	public event Action IsPlayerVisibleValueHasChangedEvent;
	public event Action IsPlayerVisibleHorizontalValueHasChangedEvent;
	public event Action DirectionToPlayerValueHasChangedEvent;
	public event Action SimpleDirectionToPlayerValueHasChangedEvent;
	
	public bool IsPlayerVisible {
		get {
			return _isPlayerVisible;
		}
	}

	public bool IsPlayerVisibleHorizontal {
		get {
			return _isPlayerVisibleHorizontal;
		}
	}

	public Vector2 DirectionToPlayer {
		get {
			return _directionToPlayer;
		}
	}

	public DirectionClass.DirectionEnum SimpleDirectionToPlayer {
		get {
			return _simpleDirectionToPlayer;
		}
	}
	
	void Start() {

		_transform = GetComponent<Transform>();

		StartCoroutine(PlayerVisibilityCheck());
	}
	
	IEnumerator PlayerVisibilityCheck() {
		
		while (true) {
			bool oldIsPlayerVisible = _isPlayerVisible;
			bool oldIsPlayerVisibleHorizontal = _isPlayerVisibleHorizontal;
			DirectionClass.DirectionEnum oldSimpleDirectionToPlayer = _simpleDirectionToPlayer;
			Vector2 oldDirectionToPlayer = _directionToPlayer;
			
			UpdateIsPlayerVisible();

			if (_isPlayerVisible != oldIsPlayerVisible) {
				if (IsPlayerVisibleValueHasChangedEvent != null) {
					IsPlayerVisibleValueHasChangedEvent();
				}
			}

			if (_directionToPlayer != oldDirectionToPlayer) {
				if (DirectionToPlayerValueHasChangedEvent != null) {
					DirectionToPlayerValueHasChangedEvent();
				}
			}

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
		_directionToPlayer =  new Vector2(direction.x, direction.y);
		_simpleDirectionToPlayer = _directionToPlayer.x > 0 ? DirectionClass.DirectionEnum.Right : DirectionClass.DirectionEnum.Left;
	}

	void UpdateIsPlayerVisible() {

		UpdateDirectionToPlayer();

		RaycastHit2D[] hits = new RaycastHit2D[1];
		int count = Physics2D.RaycastNonAlloc(_transform.position, _directionToPlayer, hits, _directionToPlayer.magnitude, _groundLayerMask);
		_isPlayerVisible = (count < 1);

		// right
		count = Physics2D.RaycastNonAlloc(_transform.position, new Vector2(1f, 0f), hits, 1.5f * _directionToPlayer.magnitude, _playerLayerMask);
		// left
		count += Physics2D.RaycastNonAlloc(_transform.position, new Vector2(-1f, 0f), hits, 1.5f * _directionToPlayer.magnitude, _playerLayerMask);
		_isPlayerVisibleHorizontal = (count > 0);
	}

}
