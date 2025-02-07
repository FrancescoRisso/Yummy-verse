using System;
using System.Globalization;
using UnityEngine;

public class Chewing_MouthState : MouthState {
	private bool _all_chewings_done = false;
	private int _num_chewings;

	public override void PrepareBeforeAction(MouthParameter param) {
		param._player_enabler.Enable();
		_num_chewings = param._num_chewings;
		param._chewings_counter.OnNewIteration += NewChewing;
	}

	public override void StateAction(MouthParameter param) {}

	public override MouthState Transition(MouthParameter param) {
		if(_all_chewings_done) return new LiftOpening_MouthState();
		return this;
	}

	private void NewChewing(int num) {
		if(num == _num_chewings) _all_chewings_done = true;
	}
}
