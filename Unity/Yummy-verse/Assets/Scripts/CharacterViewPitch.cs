using UnityEngine;

public class CharacterViewPitch : MonoBehaviour {
	[SerializeField]
	private float MinPitch = -60;

	[SerializeField]
	private float MaxPitch = 60;

	private float pitch = 0.0f;

	void Update() {
		pitch -= Input.GetAxisRaw("Mouse Y");
		pitch = Utilities.Angles.ClampAngle(pitch, MinPitch, MaxPitch);
		transform.localEulerAngles = new Vector3(pitch, 0.0f, 0.0f);
	}
}
