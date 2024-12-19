using System;
using UnityEngine;

public class CharacterViewYaw : MonoBehaviour {
	private float yaw = 0.0f;

	public Action<float> RotateYaw;

	void Update() {
		RotateYaw?.Invoke(Input.GetAxisRaw("Mouse X"));

		yaw += Input.GetAxisRaw("Mouse X");
		transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
	}
}
