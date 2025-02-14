using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
	private float speed = 0;

	void Update() {
		speed += 9.81f * Time.deltaTime;
		Vector3 pos = transform.position;
		pos.y -= speed * Time.deltaTime;
		transform.position = pos;
	}
}
