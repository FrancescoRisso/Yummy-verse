public class Traversing_LargeIntestinState : LargeIntestinState {
	private LargeIntestinParam _param;

	public override void PrepareBeforeAction(LargeIntestinParam param) {
		param._exit_trigger.Triggered += OnRoomExit;
		// param._audio.Play();
		param._NPC.NextAnimation();
		_param = param;
	}

	private void OnRoomExit() {
		_param._audio.Pause();
		_param._fsm.enabled = false;
		_param._exit_trigger.Triggered -= OnRoomExit;
		_param._monoBehaviour.StartCoroutine(SceneLoader.UnloadScene(_param._prev_scene));
	}

	public override void StateAction(LargeIntestinParam param) {}

	public override LargeIntestinState Transition(LargeIntestinParam param) {
		return this;
	}
}
