public class BeforeEntering_SmallIntestinState : SmallIntestinState {
	private bool _entered = false;

	public override void PrepareBeforeAction(SmallIntestinParam param) {
		param._enter_trigger.Triggered += () => _entered = true;
		param._mono_behaviour.StartCoroutine(SceneLoader.LoadScene(param._next_scene));
	}

	public override void StateAction(SmallIntestinParam param) {}

	public override SmallIntestinState Transition(SmallIntestinParam param) {
		if(_entered) return new FillingBoxes_SmallIntestinState();
		return this;
	}
}
