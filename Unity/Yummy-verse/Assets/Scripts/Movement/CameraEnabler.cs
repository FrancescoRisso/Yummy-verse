using UnityEngine;
using UnityEngine.Assertions;

public class CameraEnabler : MonoBehaviour {
	private Camera _camera;
	private AudioListener _audio_listener;

	private MovementInteractionEnabler _movement_interaction_enabler;

	void Start() {
		_camera = GetComponentInChildren<Camera>();
		_audio_listener = GetComponentInChildren<AudioListener>();
		_movement_interaction_enabler = GetComponentInChildren<MovementInteractionEnabler>();

		Assert.IsNotNull(_camera, $"{name} cannot find the camera");
		Assert.IsNotNull(_audio_listener, $"{name} cannot find the audio listener");
		Assert.IsNotNull(_movement_interaction_enabler, $"{name} cannot find the movement interaction enabler");
	}

	public void Enable() {
		_camera.enabled = true;
		_audio_listener.enabled = true;
		_movement_interaction_enabler.Enable();
	}

	public void Disable() {
		_camera.enabled = false;
		_audio_listener.enabled = false;
		_movement_interaction_enabler.Disable();
	}
}
