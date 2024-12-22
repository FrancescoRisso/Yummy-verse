using System;
using UnityEngine;

public class CharacterViewYaw : MonoBehaviour {
	private float yaw = 0.0f;

	void Start() {
		Cursor.lockState = CursorLockMode.Locked;  // keep confined to center of screen
	}

	void Update() {
		yaw += Input.GetAxisRaw("Mouse X");
		transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
	}
}
