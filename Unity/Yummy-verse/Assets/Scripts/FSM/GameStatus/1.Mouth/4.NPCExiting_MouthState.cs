using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCExiting_MouthState : MouthState {
	public override void PrepareBeforeAction(MouthParameter param) {
		// param._NPC.ExecMovement(Movimenti.ascensore2bocca);
	}

	private float _time = 0;

	public override void StateAction(MouthParameter param) {
		_time += Time.deltaTime;
	}

	public override MouthState Transition(MouthParameter param) {
		// if(_time > param._door_open_time) {
		// 	param._lift_doors._toggle.Invoke();
		// 	return new LoadingFood_MouthState();
		// }
		// return this;
		return new LoadingFood_MouthState();
	}
}
