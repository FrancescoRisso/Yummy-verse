using UnityEngine;

public class CartFilling_StomachState : StomachState {
	private StomachParameter _param;

	public override void PrepareBeforeAction(StomachParameter param) {
		param._end_of_ramp_trigger.Triggered += StartSecondNPC;
		_param = param;
	}

	private void StartSecondNPC() {
		_param._end_of_ramp_trigger.Triggered += StartSecondNPC;
		_param._NPC_below_speaking.NextAnimation();
	}

	public override void StateAction(StomachParameter param) {}

	public override StomachState Transition(StomachParameter param) {
		// TODO capire quando sono tutte piene
		return new Leaving_StomachState();
	}
}
