using System;
using UnityEngine;
using UnityEngine.Assertions;

public class PercentageIndicator : MonoBehaviour {
	[SerializeField]
	private float minRotation = 0f;

	[SerializeField]
	private float maxRotation = 180f;

	[SerializeField]
	private PercentageDecayManager decayManager;

	void Start() {
		Assert.IsNotNull(decayManager, $"{name} does not have a percentage manager assigned");
		decayManager.OnPercentageChange += UpdateArrowRotation;
	}

	void UpdateArrowRotation(float percentage) {
		float rotation = Mathf.Lerp(minRotation, maxRotation, percentage);
		Vector3 cur = transform.localEulerAngles;
		cur.z = rotation;
		transform.localEulerAngles = cur;
	}
}
