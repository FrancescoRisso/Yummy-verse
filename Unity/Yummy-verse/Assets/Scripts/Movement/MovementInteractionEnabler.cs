using UnityEngine;
using UnityEngine.Assertions;

public class MovementInteractionEnabler : MonoBehaviour {
	private CharacterViewYaw _yaw_controller;
	private CharacterViewPitch _pitch_controller;
	private PlayerMouseInteract _mouse_interact;
	private CharacterMovement _movement;

	private bool _init_called;

	void Start() {
		_yaw_controller = GetComponentInChildren<CharacterViewYaw>();
		_pitch_controller = GetComponentInChildren<CharacterViewPitch>();
		_mouse_interact = GetComponentInChildren<PlayerMouseInteract>();
		_movement = GetComponentInChildren<CharacterMovement>();

		Assert.IsNotNull(_yaw_controller, $"{name} cannot find the yaw controller");
		Assert.IsNotNull(_pitch_controller, $"{name} cannot find the pitch controller");

		_init_called = true;
	}

	public void Enable() {
		if(!_init_called) Start();

		_yaw_controller.enabled = true;
		_pitch_controller.enabled = true;
		if(_mouse_interact) _mouse_interact.enabled = true;
		if(_movement) _movement.enabled = true;
	}

	public void Disable() {
		if(!_init_called) Start();

		_yaw_controller.enabled = false;
		_pitch_controller.enabled = false;
		if(_mouse_interact) _mouse_interact.enabled = false;
		if(_movement) _movement.enabled = false;
	}
}
