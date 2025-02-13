using System;
using UnityEngine;
using UnityEngine.Assertions;

public enum Shape { Square, Circle, Triangle, Star }

public class SubstanceTrigger : MonoBehaviour {
	[SerializeField]
	private Shape _shape;

	[SerializeField]
	private SubstanceBoxLights _box;

	void Start() {
		_box = GetComponentInParent<SubstanceBoxLights>();
		Assert.IsNotNull(_box, $"{name} does not have a box assigned");

	}

	void OnTriggerEnter(Collider collision) {
		SubstanceBoxFSM substance = collision.gameObject.GetComponent<SubstanceBoxFSM>();

		if(substance && substance.IsSameShape(_shape)) {
			Destroy(this);
			substance.InsertIntoBox?.Invoke(_box);
		}
	}
}
