using System;
using UnityEngine;
using UnityEngine.Assertions;

public enum Shape { Square, Circle, Triangle, Star }

public class SubstanceTrigger : MonoBehaviour {
	[SerializeField]
	private Shape _shape;

	[SerializeField]
	private SubstanceBoxLights _box;

	private Action _delete;

	void Start() {
		_box = GetComponentInParent<SubstanceBoxLights>();
		Assert.IsNotNull(_box, $"{name} does not have a box assigned");

		_delete += () => Destroy(this);
	}

	void OnTriggerEnter(Collider collision) {
		SubstanceBoxFSM substance = collision.gameObject.GetComponent<SubstanceBoxFSM>();

		if(substance && substance.IsSameShape(_shape)) {
			substance.SetDeleteTrigger(_delete);
			substance.InsertIntoBox?.Invoke(_box);
		}
	}
}
