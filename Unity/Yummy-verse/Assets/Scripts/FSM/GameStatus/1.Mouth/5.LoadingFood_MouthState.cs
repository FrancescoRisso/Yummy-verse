using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingFood_MouthState : MouthState {
	private bool _called = false;

	public override void PrepareBeforeAction(MouthParameter param) {
		param._call_lift.activated += () => { _called = true; };
	}

	public override void StateAction(MouthParameter param) {}

	public override MouthState Transition(MouthParameter param) {
		if(_called) {
			param._NPC.ExecMovement(Movimenti.bocca2ascensore);
			param._lift_doors._toggle.Invoke();
			return new GoingToLift_MouthState();
		}
		return this;
	}
}
