using UnityEngine;

public abstract class InteractionManager : MonoBehaviour {
	protected abstract bool ShouldCheckMouseClick();
	protected abstract bool ShouldCheckEkey();


	protected abstract bool MouseClickWithRaycast();
	protected abstract bool EkeyWithRaycast();

	protected abstract float MouseClickRaycastRange();
	protected abstract float EkeyRaycastRange();


	protected abstract void MouseClickAction(MouseInteractable target);
	protected abstract void EkeyAction(EkeyInteractable target);

	protected virtual void ExtraUpdateAction() {}


	void Update() {
		if(ShouldCheckMouseClick() && Input.GetMouseButtonDown(0)) {
			if(MouseClickWithRaycast()) {
				Ray ray = new(transform.position, transform.forward);
				if(Physics.Raycast(ray, out RaycastHit hit, MouseClickRaycastRange())) {
					MouseInteractable target = hit.collider.GetComponent<MouseInteractable>();
					if(target != null) MouseClickAction(target);
				}
			} else {
				MouseClickAction(null);
			}
		}

		// Debug.Log("Qui");

		if(ShouldCheckEkey() && Input.GetKeyDown(KeyCode.E)) {
			// Debug.Log("Quo");
			if(EkeyWithRaycast()) {
				Ray ray = new(transform.position, transform.forward);
				if(Physics.Raycast(ray, out RaycastHit hit, EkeyRaycastRange())) {
					// Debug.Log(hit.collider);
					EkeyInteractable target = hit.collider.GetComponent<EkeyInteractable>();
					// Debug.Log(target);
					if(target != null) EkeyAction(target);
				}
			} else {
				EkeyAction(null);
			}
		}

		ExtraUpdateAction();
	}
}
