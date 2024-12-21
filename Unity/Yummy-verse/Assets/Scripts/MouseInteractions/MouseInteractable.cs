using UnityEngine;

public abstract class MouseInteractable : MonoBehaviour {
	protected bool _processing = false;

	virtual protected void OnMouseClick() {}
	virtual protected void OnMouseHold() {}
	virtual protected void OnMouseRelease() {}

	public void StartProcessing() {
		_processing = true;
	}

	void Update() {
		if(!_processing) return;

		if(Input.GetMouseButtonDown(0)) OnMouseClick();
		if(Input.GetMouseButtonUp(0)) OnMouseRelease();
		if(Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0)) OnMouseHold();
	}
}
