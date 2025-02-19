using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidDestroyedCounter : MonoBehaviour {
	private int _num_children;

	private int _num_destroyed = 0;

	public Action AllItemsDestroyed;

	void Start() {
		foreach(AcidSprayable child in GetComponentsInChildren<AcidSprayable>()) {
			_num_children++;
			child._destroyed += () => {
				if(++_num_destroyed == _num_children) AllItemsDestroyed?.Invoke();
			};
		}
	}
}
