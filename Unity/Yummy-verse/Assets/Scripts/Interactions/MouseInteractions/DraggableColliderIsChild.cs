using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DraggableColliderIsChild : MouseInteractable {
	private Draggable _parent;

	void Start() {
		_parent = GetComponentInParent<Draggable>();
		Assert.IsNotNull(_parent, $"{name} cannot find its draggable parent");
	}

	protected override void OnMouseClick() {
		_parent.StartDraggingByChild();
	}

	protected override void OnMouseRelease() {
		_parent.StopDraggingByChild();
	}
}
