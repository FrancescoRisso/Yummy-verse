using UnityEngine;

public class PlayerMouseInteract : InteractionManager {
	[SerializeField]
	private float _interact_distance = 10.0f;

	protected override bool ShouldCheckMouseClick { get; set; } = true;
	protected override bool ShouldCheckEkey { get; set; } = true;
	protected override float MouseClickRaycastRange() {
		return _interact_distance;
	}
	protected override float EkeyRaycastRange() {
		return _interact_distance;
	}

	protected override void EkeyAction(EkeyInteractable target) {
		target.StartProcessing();
	}

	protected override void MouseClickAction(MouseInteractable target) {
		target.StartProcessing();
	}

	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			Ray ray = new(transform.position, transform.forward);
			if(Physics.Raycast(ray, out RaycastHit hit, _interact_distance)) {
				MouseInteractable target = hit.collider.GetComponent<MouseInteractable>();
				if(target != null) target.StartProcessing();
			}
		}

		if(Input.GetKeyDown(KeyCode.E)) {
			Ray ray = new(transform.position, transform.forward);
			if(Physics.Raycast(ray, out RaycastHit hit, _interact_distance)) {
				EkeyInteractable target = hit.collider.GetComponent<EkeyInteractable>();
				if(target != null) target.StartProcessing();
			}
		}
	}
}
