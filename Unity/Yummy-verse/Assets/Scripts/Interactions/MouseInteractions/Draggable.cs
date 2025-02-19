using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]
public class Draggable : MouseInteractable {
	protected Rigidbody _rigidbody;
	protected CharacterViewPitch _character_pitch;
	protected Transform _parent;

	void Start() {
		_rigidbody = GetComponent<Rigidbody>();
		Assert.IsNotNull(_rigidbody, $"{name} cannot find its rigidbody");

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Assert.IsNotNull(player, $"{name} cannot find the player");

		_character_pitch = player.GetComponentInChildren<CharacterViewPitch>();
		Assert.IsNotNull(_character_pitch, $"{name} cannot find the player pitch");
	}

	protected override void OnMouseClick() {
		// Abilita il movimento cinematico per disattivare il movimento fisico
		BeforeDragging();
		_rigidbody.isKinematic = true;

		// Salva il parent precedente della gerarchia, e settati come figlio della camera
		_parent = gameObject.transform.parent;
		transform.SetParent(_character_pitch.transform, true);
	}
	protected override void OnMouseHold() {}
	protected override void OnMouseRelease() {
		// Disabilita il movimento cinematico per ri-attivare il movimento fisico
		_rigidbody.isKinematic = false;

		// Ritorna nella posizione originale della gerarchia
		transform.SetParent(_parent, true);

		// Smetti di considerare gli input
		_processing = false;
	}

	public void StartDraggingByChild() {
		OnMouseClick();
	}

	public void StopDraggingByChild() {
		OnMouseRelease();
	}

	protected virtual void BeforeDragging() {}
}
