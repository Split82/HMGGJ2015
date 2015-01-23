using UnityEngine;
using System.Collections;

public class FlexyFollow: MonoBehaviour {
	
	public GameObject _followObject;
	public float _followSpeed = 2.0f;
	public Vector3 _offset;
	public bool _fixedXOffset;
	public bool _fixedYOffset;
	public bool _fixedZOffset;
	public bool _useLocalPosition = false;
	
	private Transform _followTransform;
	private Transform _transform;
	
	void Start () {
		
		_transform = transform;
		_followTransform = _followObject.transform;
		if (_useLocalPosition) {
			_transform.position = _followTransform.position + _offset;	
		}
		else {
			_transform.localPosition = _followTransform.position + _offset;	
		}
	}
	
	void LateUpdate () {

		Vector3 pos = _useLocalPosition ? _transform.localPosition : _transform.position;

		Vector3 destPos = _followTransform.position + _offset;
		Vector3 newPos = Vector3.Lerp(pos, destPos, Time.deltaTime * _followSpeed);
		if (_fixedXOffset) {
			newPos.x = destPos.x;
		}
		if (_fixedYOffset) {
			newPos.y = destPos.y;
		}
		if (_fixedZOffset) {
			newPos.z = destPos.z;
		}

		if (_useLocalPosition) {
			_transform.localPosition = newPos;
		}
		else {
			_transform.position = newPos;
		}
	}
}
