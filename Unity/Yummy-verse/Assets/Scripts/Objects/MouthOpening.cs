using UnityEngine;
using UnityEngine.Assertions;


public class MouthOpening : MonoBehaviour {
	private float _baseAngle = -90;

	[SerializeField]
	private float _maxAngle = 14;

	[SerializeField]
	PercentageDecayManager _percentageManager;

	void Start() {
		Assert.IsNotNull(_percentageManager, "La bocca non ha assegnato il percentage manager");
		_percentageManager.OnPercentageChange += rotate;
	}

	void rotate(float percentage) {
		float angle = _maxAngle * percentage;
		transform.rotation = Quaternion.Euler(new Vector3(_baseAngle + angle, 0, 0));
	}
}
