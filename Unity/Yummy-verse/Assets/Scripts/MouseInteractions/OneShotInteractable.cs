using System;
using UnityEngine;

public abstract class OneShotInteractable : MouseInteractable {
	public Action activated;

	protected override void OnMouseClick() {
		activated?.Invoke();
		_processing = false;
		LocalActionOnClick();
	}

	protected abstract void LocalActionOnClick();
}
