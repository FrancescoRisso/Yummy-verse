using UnityEngine;

public class CharacterViewYaw : MonoBehaviour {
	private float yaw = 0.0f;

	void Update() {
		yaw += Input.GetAxisRaw("Mouse X");
		transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
	}
}
