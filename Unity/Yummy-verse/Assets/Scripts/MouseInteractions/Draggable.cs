using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class Draggable : MouseInteractable {
	private Rigidbody _rigidbody;
	private CharacterViewPitch _character_pitch;
	private Transform _parent;

	void Start() {
		_rigidbody = GetComponent<Rigidbody>();
		Assert.NotNull(_rigidbody, $"{name} cannot find its rigidbody");

		_character_pitch = FindAnyObjectByType<CharacterViewPitch>();
		Assert.NotNull(_character_pitch, $"{name} cannot find the player pitch");
	}

	protected override void OnMouseClick() {
		// Abilita il movimento cinematico per disattivare il movimento fisico
		_rigidbody.isKinematic = true;

		// Salva il parent precedente della gerarchia, e settati come figlio della camera
		_parent = gameObject.transform.parent;
		transform.SetParent(_character_pitch.transform);
	}
	protected override void OnMouseHold() {}
	protected override void OnMouseRelease() {
		// Disabilita il movimento cinematico per ri-attivare il movimento fisico
		_rigidbody.isKinematic = false;

		// Ritorna nella posizione originale della gerarchia
		transform.SetParent(_parent);

		// Smetti di considerare gli input
		_processing = false;
	}
}
