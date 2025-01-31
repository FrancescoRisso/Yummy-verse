using UnityEngine;

public class PlayerMouseInteract : InteractionManager {
	[SerializeField]
	private float _interact_distance = 10.0f;

	protected override bool ShouldCheckMouseClick { get; set; } = true;
	protected override bool ShouldCheckEkey { get; set; } = true;


	protected override bool MouseClickWithRaycast { get; set; } = true;
	protected override bool EkeyWithRaycast { get; set; } = true;


	protected override float MouseClickRaycastRange() {
		return _interact_distance;
	}
	protected override float EkeyRaycastRange() {
		return _interact_distance;
	}

	protected override void EkeyAction(EkeyInteractable target) {
		target.StartProcessing();
	}

	protected override void MouseClickAction(MouseInteractable target) {
		target.StartProcessing();
	}
}
