using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableOnCart : Draggable {
	private Transform _original_parent;
	private bool _on_cart = false;

	public void Drag(Transform parent) {
		_original_parent = transform.parent;
		transform.SetParent(parent, true);
		_on_cart = true;
	}

	public void Undrag() {
		transform.SetParent(_original_parent, true);
		_on_cart = false;
	}

	protected override void BeforeDragging() {
		if(_on_cart) transform.SetParent(_original_parent, true);
	}
}
