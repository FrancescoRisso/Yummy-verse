using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class LiftArriving_StomachState : StomachState {
	private bool _lift_arrived = false;

	public override void PrepareBeforeAction(StomachParameter param) {
		param._stomachFsm.setActionHandler(() => _lift_arrived = true);
	}

	public override void StateAction(StomachParameter param) {}

	public override StomachState Transition(StomachParameter param) {
		if(_lift_arrived) return new Shooting_StomachState();
		return this;
	}
}
