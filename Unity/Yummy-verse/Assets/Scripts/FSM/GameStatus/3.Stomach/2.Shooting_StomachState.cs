
public class Shooting_StomachState : StomachState {
	private bool _chain_pulled = false;

	public override void PrepareBeforeAction(StomachParameter param) {
		param._chain.OnPercentageChange += (float perc) => {
			if(perc == 1) _chain_pulled = true;
		};
		param._audio.Play();
	}

	public override void StateAction(StomachParameter param) {}

	public override StomachState Transition(StomachParameter param) {
		if(!_chain_pulled) return this;
		return new Emptying_StomachState();
	}
}
