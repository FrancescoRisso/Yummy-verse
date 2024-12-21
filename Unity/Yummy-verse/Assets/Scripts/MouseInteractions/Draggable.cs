using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class Draggable : MouseInteractable {
	private Rigidbody _rigidbody;
	private CharacterMovement _character_movement;
	private CharacterViewPitch _character_pitch;
	private CharacterViewYaw _character_yaw;

	private Vector3 _pos_relative_to_character;

	void Start() {
		_rigidbody = GetComponent<Rigidbody>();
		Assert.NotNull(_rigidbody, $"{name} cannot find its rigidbody");

		_character_movement = FindAnyObjectByType<CharacterMovement>();
		Assert.NotNull(_character_movement, $"{name} cannot find the player movement");

		_character_pitch = FindAnyObjectByType<CharacterViewPitch>();
		Assert.NotNull(_character_pitch, $"{name} cannot find the player pitch");

		_character_yaw = FindAnyObjectByType<CharacterViewYaw>();
		Assert.NotNull(_character_yaw, $"{name} cannot find the player pitch");
	}

	protected override void OnMouseClick() {
		_pos_relative_to_character = transform.position - _character_pitch.gameObject.transform.position;

		_rigidbody.isKinematic = true;

		_character_movement.Movement += OnCharacterPositionChange;
		_character_yaw.RotateYaw += OnCharacterYawChange;
	}
	protected override void OnMouseHold() {}
	protected override void OnMouseRelease() {
		_rigidbody.isKinematic = false;

		_character_movement.Movement -= OnCharacterPositionChange;
		_character_yaw.RotateYaw -= OnCharacterYawChange;

		_processing = false;
	}

	private void OnCharacterPositionChange(Vector3 delta) {
		transform.position += delta;
	}

	private void OnCharacterYawChange(float delta) {
		Vector3 rotation = transform.eulerAngles;
		transform.eulerAngles = new Vector3(rotation.x, rotation.y + delta, rotation.z);

		transform.position -= _pos_relative_to_character;
		_pos_relative_to_character = Quaternion.Euler(0, delta, 0) * _pos_relative_to_character;
		transform.position += _pos_relative_to_character;
	}
}
