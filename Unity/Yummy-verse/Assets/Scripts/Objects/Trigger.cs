using System;
using UnityEngine;

public class Trigger : MonoBehaviour {
	public Action Triggered;
	void OnTriggerEnter() {
		Triggered?.Invoke();
	}
}
