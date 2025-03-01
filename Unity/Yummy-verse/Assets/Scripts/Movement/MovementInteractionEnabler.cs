using UnityEngine;
using UnityEngine.Assertions;

public class MovementInteractionEnabler : MonoBehaviour {
	private CharacterViewYaw _yaw_controller;
	private CharacterViewPitch _pitch_controller;
	private PlayerMouseInteract _mouse_interact;
	private CharacterMovement _movement;
	private CharacterController _controller;

	[SerializeField]
	private bool _has_pitch_controller = true;

	[SerializeField]
	private bool _movement_managers_are_in_parent = false;

	private bool _init_called;

	void Start() {
		if(_movement_managers_are_in_parent) {
			_yaw_controller = GetComponentInParent<CharacterViewYaw>();
			_pitch_controller = GetComponentInParent<CharacterViewPitch>();
			_movement = GetComponentInParent<CharacterMovement>();
			_controller = GetComponentInParent<CharacterController>();
		} else {
			_yaw_controller = GetComponentInChildren<CharacterViewYaw>();
			_pitch_controller = GetComponentInChildren<CharacterViewPitch>();
			_movement = GetComponentInChildren<CharacterMovement>();
			_controller = GetComponentInChildren<CharacterController>();
		}

		_mouse_interact = GetComponentInChildren<PlayerMouseInteract>();

		Assert.IsNotNull(_yaw_controller, $"{name} cannot find the yaw controller");
		if(_has_pitch_controller) Assert.IsNotNull(_pitch_controller, $"{name} cannot find the pitch controller");

		_init_called = true;
	}

	public void Enable() {
		if(!_init_called) Start();

		_yaw_controller.enabled = true;
		_yaw_controller.UpdateInitialYaw();
		if(_pitch_controller) _pitch_controller.enabled = true;
		if(_mouse_interact) _mouse_interact.enabled = true;
		if(_movement) _movement.enabled = true;
		if(_controller) _controller.enabled = true;
	}

	public void Disable() {
		if(!_init_called) Start();

		_yaw_controller.enabled = false;
		if(_pitch_controller) _pitch_controller.enabled = false;
		if(_mouse_interact) _mouse_interact.enabled = false;
		if(_movement) _movement.enabled = false;
		if(_controller) _controller.enabled = false;
	}
}
