using UnityEngine;

public class BeforeEntering_LargeIntestinState : LargeIntestinState {
	private bool _entered = false;
	private LargeIntestinParam _param;

	public override void PrepareBeforeAction(LargeIntestinParam param) {
		_param = param;
		param._enter_trigger.Triggered += OnRoomEnter;
	}

	private void OnRoomEnter() {
		_param._enter_trigger.Triggered -= OnRoomEnter;
		_entered = true;
		_param._monoBehaviour.StartCoroutine(SceneLoader.LoadScene(_param._next_scene));
	}

	public override void StateAction(LargeIntestinParam param) {}

	public override LargeIntestinState Transition(LargeIntestinParam param) {
		if(_entered) return new Traversing_LargeIntestinState();
		return this;
	}
}
