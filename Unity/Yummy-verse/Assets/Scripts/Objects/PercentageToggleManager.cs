using System;
using UnityEngine;

public enum Operation { Stopped, Increasing, Decreasing }

static class OperationMethods {
	public static Operation GetOpposite(Operation op) {
		switch(op) {
			case Operation.Decreasing: return Operation.Increasing;
			case Operation.Increasing: return Operation.Decreasing;
			case Operation.Stopped: return Operation.Stopped;
			default: return Operation.Stopped;
		}
	}

	public static int GetSign(Operation op) {
		switch(op) {
			case Operation.Decreasing: return -1;
			case Operation.Increasing: return 1;
			case Operation.Stopped: return 0;
			default: return 0;
		}
	}
}

public class PercentageToggleManager : MonoBehaviour {
	[SerializeField]
	[Range(0, 1)]
	private float _initialPercentage;

	private float _percentage;

	public Action _toggle;

	public float _speed;

	[SerializeField]
	private Operation _initialOperation = Operation.Stopped;

	[SerializeField]
	private Operation _firstMovement = Operation.Increasing;

	private Operation _current;
	private Operation _previous;

	public Action<float> OnPercentageChange;

	void Start() {
		_toggle += OnToggle;
		_percentage = _initialPercentage;
		_current = _initialOperation;
		_previous = OperationMethods.GetOpposite(_firstMovement);
		OnPercentageChange?.Invoke(_percentage);
	}

	void Update() {
		int sign = OperationMethods.GetSign(_current);
		if(sign == 0) return;

		_percentage += sign * _speed * Time.deltaTime;
		_percentage = Math.Clamp(_percentage, 0, 1);
		OnPercentageChange?.Invoke(_percentage);

		if(_percentage == 0 || _percentage == 1) _toggle.Invoke();
	}

	private void OnToggle() {
		switch(_current) {
			case Operation.Increasing:
			case Operation.Decreasing:
				_previous = _current;
				_current = Operation.Stopped;
				break;

			case Operation.Stopped:
				_current = OperationMethods.GetOpposite(_previous);
				_previous = Operation.Stopped;
				break;
		}
	}

	public Operation CurrentStatus() {
		return _current;
	}
}
