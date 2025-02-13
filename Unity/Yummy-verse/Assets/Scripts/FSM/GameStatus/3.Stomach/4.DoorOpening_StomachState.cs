public class DoorOpening_StomachState : StomachState {
	private bool _done = false;

	public override void PrepareBeforeAction(StomachParameter param) {
		param._doors._toggle.Invoke();
		param._doors.OnPercentageChange += OnDoorPercentageChange;
		param._monoBehaviour.StartCoroutine(SceneLoader.LoadScene(param._next_scene));
	}

	public override void StateAction(StomachParameter param) {}

	public override StomachState Transition(StomachParameter param) {
		if(_done) return new CartFilling_StomachState();
		return this;
	}

	private void OnDoorPercentageChange(float val) {
		if(val == 0) _done = true;
	}
}
