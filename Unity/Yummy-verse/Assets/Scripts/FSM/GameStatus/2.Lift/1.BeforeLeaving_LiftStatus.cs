
using UnityEngine;

public class BeforeLeaving_LiftStatus : LiftState {
	private bool _close_doors = false;

	public override void PrepareBeforeAction(LiftProps param) {
		param._ascensore.OnCorrectButtonPress += () => { _close_doors = true; };
	}

	public override void StateAction(LiftProps param) {}

	public override LiftState Transition(LiftProps param) {
		if(_close_doors) return new ClosingDoors_LiftStatus();
		return this;
	}
}
