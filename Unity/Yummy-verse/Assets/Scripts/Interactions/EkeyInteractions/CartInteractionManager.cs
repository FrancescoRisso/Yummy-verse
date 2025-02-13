using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CartInteractionManager : InteractionManager {
	private CameraEnabler _player_camera_enabler;
	private CameraEnabler _my_camera_enabler;
	private Transform _player_original_parent;
	private GameObject _player;

	public void PlayerPushesCart(GameObject player) {
		_player_original_parent = player.transform.parent;
		player.transform.SetParent(gameObject.transform);
		player.transform.localPosition = new Vector3(0.0299999993f, 0.169999999f, -0.370000005f);
		player.transform.localEulerAngles = new Vector3(0, 0, 0);
	}

	protected override bool ShouldCheckEkey() {
		return true;
	}

	protected override bool ShouldCheckMouseClick() {
		return false;
	}

	protected override bool EkeyWithRaycast() {
		return false;
	}

	protected override bool MouseClickWithRaycast() {
		throw new System.NotImplementedException();
	}

	protected override float EkeyRaycastRange() {
		throw new System.NotImplementedException();
	}

	protected override float MouseClickRaycastRange() {
		throw new System.NotImplementedException();
	}

	protected override void EkeyAction(EkeyInteractable target) {
		_player_camera_enabler.Enable();
		_my_camera_enabler.Disable();
		_player.transform.SetParent(_player_original_parent);
		this.enabled = false;
	}

	protected override void MouseClickAction(MouseInteractable target) {
		throw new System.NotImplementedException();
	}

	void Start() {
		_player = GameObject.FindGameObjectWithTag("Player");
		Assert.IsNotNull(_player, $"{name} cannot find the player");

		_player_camera_enabler = _player.GetComponentInChildren<CameraEnabler>();
		Assert.IsNotNull(_player_camera_enabler, $"{name} cannot find the player's camera enabler");

		_my_camera_enabler = GetComponentInChildren<CameraEnabler>();
		Assert.IsNotNull(_my_camera_enabler, $"{name} cannot find its own camera enabler ");
	}
}
