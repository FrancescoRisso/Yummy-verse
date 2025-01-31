using UnityEngine;
using UnityEngine.Assertions;

public class ShooterInteractionManager : InteractionManager {
	private CameraEnabler _player_camera_enabler;

	private CameraEnabler _my_camera_enabler;

	protected override bool ShouldCheckMouseClick() {
		return true;
	}
	protected override bool ShouldCheckEkey() {
		return true;
	}


	protected override bool MouseClickWithRaycast() {
		return false;
	}
	protected override bool EkeyWithRaycast() {
		return false;
	}


	protected override float MouseClickRaycastRange() {
		throw new System.NotImplementedException();
	}
	protected override float EkeyRaycastRange() {
		throw new System.NotImplementedException();
	}

	protected override void EkeyAction(EkeyInteractable target) {
		_player_camera_enabler.Enable();
		_my_camera_enabler.Disable();
		this.enabled = false;
	}

	protected override void MouseClickAction(MouseInteractable target) {
		// TODO shoot
	}

	void Start() {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Assert.IsNotNull(player, $"{name} cannot find the player");

		_player_camera_enabler = player.GetComponentInChildren<CameraEnabler>();
		Assert.IsNotNull(_player_camera_enabler, $"{name} cannot find the player's camera enabler");

		_my_camera_enabler = GetComponentInChildren<CameraEnabler>();
		Assert.IsNotNull(_my_camera_enabler, $"{name} cannot find its own camera enabler");
	}
}
