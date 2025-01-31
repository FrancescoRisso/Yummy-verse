using UnityEngine;
using UnityEngine.Assertions;

public class EquipShooter : EkeyInteractable {
	private CameraEnabler _player_camera_enabler;

	private CameraEnabler _my_camera_enabler;

	// Start is called before the first frame update
	void Start() {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Assert.IsNotNull(player, $"{name} cannot find the player");

		_player_camera_enabler = player.GetComponentInChildren<CameraEnabler>();
		Assert.IsNotNull(_player_camera_enabler, $"{name} cannot find the player's camera enabler");

		_my_camera_enabler = GetComponentInChildren<CameraEnabler>();
		Assert.IsNotNull(_my_camera_enabler, $"{name} cannot find its own camera enabler");
	}

	public override void StartProcessing() {
		_player_camera_enabler.Disable();
		_my_camera_enabler.Enable();
	}
}
