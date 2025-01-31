using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterViewYaw : MonoBehaviour {
	[SerializeField]
	private float MinYaw;

	[SerializeField]
	private float MaxYaw;

	[SerializeField]
	private bool _clamp = false;

	private float yaw = 0.0f;

	void Start() {
		Cursor.lockState = CursorLockMode.Locked;  // keep confined to center of screen
	}

	void Update() {
		yaw += Input.GetAxisRaw("Mouse X");
		if(_clamp) yaw = Utilities.Angles.ClampAngle(yaw, MinYaw, MaxYaw);
		transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
	}
}
