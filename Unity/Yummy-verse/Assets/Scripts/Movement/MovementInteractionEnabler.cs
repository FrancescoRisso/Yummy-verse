using UnityEngine;
using UnityEngine.Assertions;

public class MovementInteractionEnabler : MonoBehaviour {
	private CharacterViewYaw _yaw_controller;
	private CharacterViewPitch _pitch_controller;
	private PlayerMouseInteract _mouse_interact;
	private CharacterMovement _movement;

	void Start() {
		_yaw_controller = GetComponentInChildren<CharacterViewYaw>();
		_pitch_controller = GetComponentInChildren<CharacterViewPitch>();
		_mouse_interact = GetComponentInChildren<PlayerMouseInteract>();
		_movement = GetComponentInChildren<CharacterMovement>();

		Assert.IsNotNull(_yaw_controller, $"{name} cannot find the yaw controller");
		Assert.IsNotNull(_pitch_controller, $"{name} cannot find the pitch controller");
		Assert.IsNotNull(_movement, $"{name} cannot find the movement controller");
	}

	public void Enable() {
		_yaw_controller.enabled = true;
		_pitch_controller.enabled = true;
		if(_mouse_interact) _mouse_interact.enabled = true;
		_movement.enabled = true;
	}

	public void Disable() {
		_yaw_controller.enabled = false;
		_pitch_controller.enabled = false;
		if(_mouse_interact) _mouse_interact.enabled = false;
		_movement.enabled = false;
	}
}
