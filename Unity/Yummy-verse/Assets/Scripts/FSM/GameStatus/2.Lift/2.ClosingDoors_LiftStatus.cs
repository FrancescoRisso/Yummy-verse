public class ClosingDoors_LiftStatus : LiftState {
	private bool _finished = false;

	public override void PrepareBeforeAction(LiftProps param) {
		param._porte._toggle.Invoke();
		param._porte.OnPercentageChange += (float perc) => {
			if(perc == 1) {
				_finished = true;
				param._ascensore.LeavingFaringe();
			}
		};
	}

	public override void StateAction(LiftProps param) {}

	public override LiftState Transition(LiftProps param) {
		if(_finished) return new Descending_LiftStatus();
		return this;
	}
}
