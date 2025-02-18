using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidShoot : MonoBehaviour {
	void Update() {
		Ray ray = new(transform.position, transform.forward);
		Debug.DrawRay(transform.position, 10 * transform.forward);
		if(Physics.Raycast(ray, out RaycastHit hit, 10)) {
			AcidSprayable target = hit.collider.GetComponent<AcidSprayable>();
			if(target != null) target.Drop();
		}
	}
}
