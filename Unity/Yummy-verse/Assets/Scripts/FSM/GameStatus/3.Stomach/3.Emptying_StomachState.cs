

public class Emptying_StomachState : StomachState {
	private bool _emptied = false;

	public override void PrepareBeforeAction(StomachParameter param) {
		param._acid_plane.StartEmptying.Invoke();
		param._acid_plane.OnEmptied += () => _emptied = true;
	}

	public override void StateAction(StomachParameter param) {}

	public override StomachState Transition(StomachParameter param) {
		if(_emptied) return new DoorOpening_StomachState();
		return this;
	}
}
