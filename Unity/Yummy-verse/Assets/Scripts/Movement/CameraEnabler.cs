using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraEnabler : MonoBehaviour {
	private Camera _camera;
	private AudioListener _audio_listener;
	private CharacterViewYaw _yaw_controller;
	private CharacterViewPitch _pitch_controller;
	private PlayerMouseInteract _mouse_interact;

	void Start() {
		_camera = GetComponentInChildren<Camera>();
		_audio_listener = GetComponentInChildren<AudioListener>();
		_yaw_controller = GetComponentInChildren<CharacterViewYaw>();
		_pitch_controller = GetComponentInChildren<CharacterViewPitch>();
		_mouse_interact = GetComponentInChildren<PlayerMouseInteract>();

		Assert.IsNotNull(_camera, $"{name} cannot find the camera");
		Assert.IsNotNull(_audio_listener, $"{name} cannot find the audio listener");
		Assert.IsNotNull(_yaw_controller, $"{name} cannot find the yaw controller");
		Assert.IsNotNull(_pitch_controller, $"{name} cannot find the pitch controller");
	}

	public void Enable() {
		_camera.enabled = true;
		_audio_listener.enabled = true;
		_yaw_controller.enabled = true;
		_pitch_controller.enabled = true;
		if(_mouse_interact) _mouse_interact.enabled = true;
	}

	public void Disable() {
		_camera.enabled = false;
		_audio_listener.enabled = false;
		_yaw_controller.enabled = false;
		_pitch_controller.enabled = false;
		if(_mouse_interact) _mouse_interact.enabled = false;
	}
}
