using System;
using UnityEngine;
using UnityEngine.Assertions;


public class PercentageDecayManager : MonoBehaviour {
	[SerializeField]
	[Range(0, 1)]
	private float _percentage;

	[SerializeField]
	float _increasePerClick;

	[SerializeField]
	float _decaySpeed;

	[SerializeField]
	OneShotInteractable _activator;

	public Action<float> OnPercentageChange;

	void Start() {
		Assert.IsNotNull(_activator, $"{name} non ha un attivatore");
		_activator.activated += () => _percentage += _increasePerClick;
	}

	void Update() {
		_percentage = Math.Clamp(_percentage, 0, 1);
		OnPercentageChange.Invoke(_percentage);
		_percentage -= _decaySpeed * Time.deltaTime;
	}
}
