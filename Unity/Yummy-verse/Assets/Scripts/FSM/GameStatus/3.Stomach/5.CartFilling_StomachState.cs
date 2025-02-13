public class CartFilling_StomachState : StomachState {
	public override void StateAction(StomachParameter param) {}

	public override StomachState Transition(StomachParameter param) {
		// TODO capire quando sono tutte piene
		return new Leaving_StomachState();
	}
}
