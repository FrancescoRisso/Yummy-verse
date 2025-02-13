public class BeforeEntering_ExpulsionRoomState : ExpulsionRoomState {
	private bool _entered = false;
	private ExpulsionRoomParam _param;

	public override void PrepareBeforeAction(ExpulsionRoomParam param) {
		_param = param;
		param._enter_trigger.Triggered += OnRoomEnter;
	}

	private void OnRoomEnter() {
		_param._audio.Play();
		_entered = true;
		_param._enter_trigger.Triggered -= OnRoomEnter;
	}

	public override void StateAction(ExpulsionRoomParam param) {}

	public override ExpulsionRoomState Transition(ExpulsionRoomParam param) {
		if(_entered) return new EmptyingCart_ExpulsionRoomState();
		return this;
	}
}
