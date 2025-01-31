using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionManager : MonoBehaviour {
	protected virtual bool ShouldCheckMouseClick { get; set; } = false;
	protected virtual bool ShouldCheckEkey { get; set; } = false;


	protected virtual bool MouseClickWithRaycast { get; set; } = false;
	protected virtual bool EkeyWithRaycast { get; set; } = false;

	protected virtual float MouseClickRaycastRange() {
		return 0.0f;
	}
	protected virtual float EkeyRaycastRange() {
		return 0.0f;
	}


	protected virtual void MouseClickAction(MouseInteractable target) {}
	protected virtual void EkeyAction(EkeyInteractable target) {}


	void Update() {
		if(ShouldCheckMouseClick && Input.GetMouseButtonDown(0)) {
			if(MouseClickWithRaycast) {
				Ray ray = new(transform.position, transform.forward);
				if(Physics.Raycast(ray, out RaycastHit hit, MouseClickRaycastRange())) {
					MouseInteractable target = hit.collider.GetComponent<MouseInteractable>();
					if(target != null) MouseClickAction(target);
				}
			} else {
				MouseClickAction(null);
			}
		}

		if(ShouldCheckEkey && Input.GetKeyDown(KeyCode.E)) {
			if(EkeyWithRaycast) {
				Ray ray = new(transform.position, transform.forward);
				if(Physics.Raycast(ray, out RaycastHit hit, EkeyRaycastRange())) {
					EkeyInteractable target = hit.collider.GetComponent<EkeyInteractable>();
					if(target != null) EkeyAction(target);
				}
			} else {
				EkeyAction(null);
			}
		}
	}
}
