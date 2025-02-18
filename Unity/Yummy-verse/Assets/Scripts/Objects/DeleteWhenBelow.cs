using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWhenBelow : MonoBehaviour {
	[SerializeField]
	private float _threshold_y = -1;

	void Update() {
		if(transform.position.y < _threshold_y) Destroy(gameObject);
	}
}
