using UnityEngine;

public class Exiting_SmallIntestinState : SmallIntestinState {
	private SmallIntestinParam _param;

	public override void PrepareBeforeAction(SmallIntestinParam param) {
		param._NPC_start.NextAnimation();
		_param = param;
		param._exit_trigger.Triggered += OnRoomExit;
		param._before_tube_trigger.Triggered += ComingCloseToTube;
	}

	private void ComingCloseToTube() {
		_param._before_tube_trigger.Triggered -= ComingCloseToTube;
		_param._NPC_end.NextAnimation();
	}

	private void OnRoomExit() {
		_param._mono_behaviour.StartCoroutine(SceneLoader.UnloadScene(_param._prev_scene));
		_param._fsm.enabled = false;
		_param._audio.Pause();
		_param._exit_trigger.Triggered -= OnRoomExit;
	}

	public override void StateAction(SmallIntestinParam param) {}

	public override SmallIntestinState Transition(SmallIntestinParam param) {
		return this;
	}
}
