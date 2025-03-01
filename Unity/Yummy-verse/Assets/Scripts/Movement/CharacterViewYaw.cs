using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterViewYaw : MonoBehaviour {
	[SerializeField]
	private float MinYaw;

	[SerializeField]
	private float MaxYaw;

	private float InitialYaw;

	[SerializeField]
	private bool _clamp = false;

	private float yaw = 0.0f;

	private bool _initialized = false;

	public void UpdateInitialYaw() {
		if(!_initialized || !_clamp) {
			InitialYaw = transform.eulerAngles.y;
			yaw = InitialYaw;
		}
	}

	void Start() {
		Cursor.lockState = CursorLockMode.Locked;  // keep confined to center of screen
		UpdateInitialYaw();
		_initialized = true;
	}

	void Update() {
		yaw += Input.GetAxisRaw("Mouse X");
		if(_clamp) yaw = Utilities.Angles.ClampAngle(yaw, MinYaw + InitialYaw, MaxYaw + InitialYaw);
		transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
	}
}
