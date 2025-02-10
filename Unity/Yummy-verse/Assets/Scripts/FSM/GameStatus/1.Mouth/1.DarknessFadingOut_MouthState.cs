using UnityEngine;

public class DarknessFadingOut_MouthState : MouthState {
	private bool _finished = false;

	public override void PrepareBeforeAction(MouthParameter param) {
		param._player_enabler.Disable();
		param._video_player.VideoFinished += () => { _finished = true; };
	}

	public override void StateAction(MouthParameter param) {}

	public override MouthState Transition(MouthParameter param) {
		if(_finished) return new Chewing_MouthState();
		return this;
	}
}
