using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class Button : OneShotInteractable {
	private Component _button_top;

	[SerializeField]
	private float _press_speed;
	[SerializeField]
	private float _release_speed;

	public PercentageToggleManager _perc;

	private Vector3 _original_pos;
	private float _delta_h = 0.01f;
	private Vector3 _pressed_pos;


	void Start() {
		_button_top = Children.FindChild(gameObject, "pulsante");
		Assert.IsNotNull(_button_top, $"{name} cannot find its button to be moved");

		_perc = GetComponent<PercentageToggleManager>();
		Assert.IsNotNull(_button_top, $"{name} cannot find its percentage toggle manager");

		_original_pos = _button_top.transform.position;
		_pressed_pos = _button_top.transform.position - _delta_h * transform.up;
	}

	protected override void LocalActionOnClick() {
		switch(_perc.CurrentStatus()) {
			case Operation.Stopped:
				_perc.OnPercentageChange += UpdatePosition;
				_perc._speed = _press_speed;
				_perc._toggle.Invoke();
				break;

			case Operation.Increasing:
				_perc._speed = _press_speed;
				_perc._toggle.Invoke();
				_perc._toggle.Invoke();
				break;

			case Operation.Decreasing: break;
		}
	}

	private void UpdatePosition(float perc) {
		Vector3 pos = _button_top.transform.position;
		_button_top.transform.position = perc * _original_pos + (1 - perc) * _pressed_pos;

		if(perc == 0) {
			_perc._speed = _release_speed;
			_perc._toggle.Invoke();
		}

		if(perc == 1) _perc.OnPercentageChange -= UpdatePosition;
	}
}
