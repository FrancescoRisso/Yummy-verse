using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingToLift_MouthState : MouthState {
	public override void StateAction(MouthParameter param) {}

	public override MouthState Transition(MouthParameter param) {
		return this;
	}
}
