public class BeforeEntering_SmallIntestinState : SmallIntestinState {
	private bool _entered = false;

	public override void PrepareBeforeAction(SmallIntestinParam param) {
		param._enter_trigger.Triggered += () => _entered = true;
	}

	public override void StateAction(SmallIntestinParam param) {}

	public override SmallIntestinState Transition(SmallIntestinParam param) {
		if(_entered) return new FillingBoxes_SmallIntestinState();
		return this;
	}
}
