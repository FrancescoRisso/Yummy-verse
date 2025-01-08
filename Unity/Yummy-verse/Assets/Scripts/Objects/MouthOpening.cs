
using UnityEngine;
using UnityEngine.Assertions;


public class MouthOpening : MonoBehaviour {
	[SerializeField]
	[Range(0, 1)]
	private float _percentage;

	private float _baseAngle = -90;

	[SerializeField]
	private float _maxAngle = 14;

	void Update() {
		float angle = _maxAngle * _percentage;
		transform.rotation = Quaternion.Euler(new Vector3(_baseAngle + angle, 0, 0));
	}
}
