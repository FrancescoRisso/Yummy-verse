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

		if(Input.GetMouseButtonDown(0))
			OnMouseClick();
		else if(Input.GetMouseButtonUp(0))
			OnMouseRelease();
		else if(Input.GetMouseButton(0))
			OnMouseHold();
	}
}
