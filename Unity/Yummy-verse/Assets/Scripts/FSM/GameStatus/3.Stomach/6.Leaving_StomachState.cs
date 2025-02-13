public class Leaving_StomachState : StomachState {
	private bool _exited = false;

	public override void PrepareBeforeAction(StomachParameter param) {
		param._exit_trigger.Triggered += () => _exited = true;
	}
	public override void StateAction(StomachParameter param) {}

	public override StomachState Transition(StomachParameter param) {
		if(_exited) return new Left_StomachState();
		return this;
	}
}
