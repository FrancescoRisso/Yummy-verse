
public class Shooting_StomachState : StomachState {
	public override void StateAction(StomachParameter param) {}

	public override StomachState Transition(StomachParameter param) {
		// TODO Quando catenella è tirata, passare allo stato emptying
		return new Emptying_StomachState();
	}
}
