using UnityEngine;

public class Falling_ExpulsionRoomState : ExpulsionRoomState {
	private bool _video_finished = false;

	public override void PrepareBeforeAction(ExpulsionRoomParam param) {
		param._player.AddComponent<Gravity>();

		param._video.Play();
		param._video.VideoFinished += () => _video_finished = true;

		param._sciacquone.Play();
	}

	public override void StateAction(ExpulsionRoomParam param) {}

	public override ExpulsionRoomState Transition(ExpulsionRoomParam param) {
		if(_video_finished) return new Black_ExpulsionRoomState();
		return this;
	}
}
