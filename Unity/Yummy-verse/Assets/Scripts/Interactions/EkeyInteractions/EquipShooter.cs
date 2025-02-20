using UnityEngine;
using UnityEngine.Assertions;

public class EquipShooter : EkeyInteractable {
	private CameraEnabler _player_camera_enabler;

	private CameraEnabler _my_camera_enabler;

	private ShooterInteractionManager _shooter_interaction_manager;

	GameObject _player;

	void Start() {
		_player = GameObject.FindGameObjectWithTag("Player");
		Assert.IsNotNull(_player, $"{name} cannot find the player");

		_player_camera_enabler = _player.GetComponentInChildren<CameraEnabler>();
		Assert.IsNotNull(_player_camera_enabler, $"{name} cannot find the player's camera enabler");

		_my_camera_enabler = GetComponentInChildren<CameraEnabler>();
		Assert.IsNotNull(_my_camera_enabler, $"{name} cannot find its own camera enabler");

		_shooter_interaction_manager = GetComponentInChildren<ShooterInteractionManager>();
		Assert.IsNotNull(_shooter_interaction_manager, $"{name} cannot find its shooter interaction manager");
	}

	public override void StartProcessing() {
		_player_camera_enabler.Disable();
		_my_camera_enabler.Enable();
		_shooter_interaction_manager.enabled = true;
		_shooter_interaction_manager.PlayerShoots(_player);
	}
}
