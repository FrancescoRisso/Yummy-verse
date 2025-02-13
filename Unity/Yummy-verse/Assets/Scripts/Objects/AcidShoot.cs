using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidShoot : MonoBehaviour {
	// Start is called before the first frame update
	void Start() {}

	// Update is called once per frame
	void Update() {
		Ray ray = new(transform.position, transform.forward);
		if(Physics.Raycast(ray, out RaycastHit hit, 10)) {
			AcidSprayable target = hit.collider.GetComponent<AcidSprayable>();
			if(target != null) target.Push(0.5f * transform.right);
		}
	}
}
