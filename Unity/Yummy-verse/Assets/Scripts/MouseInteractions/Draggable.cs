using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Draggable : MouseInteractable {
	private Rigidbody _rigidbody;
	private CharacterMovement _character_movement;

	void Start() {
		_rigidbody = GetComponent<Rigidbody>();
		Assert.NotNull(_rigidbody, $"{name} cannot find its rigidbody");

		_character_movement = FindAnyObjectByType<CharacterMovement>();
		Assert.NotNull(_character_movement, $"{name} cannot find the player movement");
	}

	protected override void OnMouseClick() {
		_rigidbody.isKinematic = true;
		_character_movement.Movement += OnCharacterPositionChange;
	}
	protected override void OnMouseHold() {}
	protected override void OnMouseRelease() {
		_rigidbody.isKinematic = false;
		_character_movement.Movement -= OnCharacterPositionChange;
		_processing = false;
	}

	private void OnCharacterPositionChange(Vector3 delta) {
		transform.position += delta;
	}

	// private void OnCharacterPositionChange(Vector3 delta) {
	// 	transform.position += delta;
	// }
}
