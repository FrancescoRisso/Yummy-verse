using UnityEngine;

public class CartFilling_StomachState : StomachState {
	public override void PrepareBeforeAction(StomachParameter param) {
		// param._NPC.ExecMovementWithCallback(Movimenti.finestra2sotto, () => {
		// 	Transform playerParent = param._player.transform.parent;
		// 	param._cart.transform.SetParent(playerParent);
		// 	param._cart.GetComponentInChildren<CameraEnabler>().Enable();
		// });
	}

	public override void StateAction(StomachParameter param) {}

	public override StomachState Transition(StomachParameter param) {
		// TODO capire quando sono tutte piene
		return new Leaving_StomachState();
	}
}
