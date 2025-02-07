using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftOpening_MouthState : MouthState {
	private bool _done = false;

	public override void PrepareBeforeAction(MouthParameter param) {
		param._lift_doors._toggle.Invoke();
		param._lift_doors.OnPercentageChange += OnDoorPercentageChange;
	}

	public override void StateAction(MouthParameter param) {}

	public override MouthState Transition(MouthParameter param) {
		if(_done) throw new System.NotImplementedException();
		return this;
	}

	private void OnDoorPercentageChange(float val) {
		if(val == 0) _done = true;
	}
}
