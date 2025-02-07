using UnityEngine;

public class DarknessFadingOut_MouthState : MouthState {
	private float _perc = 0;

	public override void PrepareBeforeAction(MouthParameter param) {
		param._player_enabler.Disable();
	}

	public override void StateAction(MouthParameter param) {
		_perc += param._fading_duration * Time.deltaTime;
	}

	public override MouthState Transition(MouthParameter param) {
		if(_perc > 1) return new Chewing_MouthState();
		return this;
	}
}
