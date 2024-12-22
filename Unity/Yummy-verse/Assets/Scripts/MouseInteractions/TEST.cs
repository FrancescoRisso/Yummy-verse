using System;
using NUnit.Framework;
using UnityEngine;

public class TEST : MonoBehaviour {
	[SerializeField]
	private OneShotInteractable _trigger;

	void Start() {
		Assert.IsNotNull(_trigger, "Test does not have a trigger");
		_trigger.activated += () => Debug.Log("Triggered");
	}
}
