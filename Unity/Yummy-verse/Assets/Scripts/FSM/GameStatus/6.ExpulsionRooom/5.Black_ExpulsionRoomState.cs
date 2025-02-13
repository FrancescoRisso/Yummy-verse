using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_ExpulsionRoomState : ExpulsionRoomState {
	public override void PrepareBeforeAction(ExpulsionRoomParam param) {
		param._attractor.enabled = false;
		param._player.GetComponent<Gravity>().enabled = false;
	}

	private float _time = 0;

	public override void StateAction(ExpulsionRoomParam param) {
		_time += Time.deltaTime;
	}

	public override ExpulsionRoomState Transition(ExpulsionRoomParam param) {
		if(_time > param._wait_before_restarting) return new GoToFinalScene_ExpulsionRoomState();
		return this;
	}
}
