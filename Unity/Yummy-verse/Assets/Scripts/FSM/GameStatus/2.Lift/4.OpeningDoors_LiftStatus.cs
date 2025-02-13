using UnityEngine;

public class OpeningDoors_LiftStatus : LiftState {
	// private bool _finished = false;

	public override void PrepareBeforeAction(LiftProps param) {
		param._porte._toggle.Invoke();
		param._audio.Stop();

		MonoBehaviour.FindObjectOfType<StomachFSM>()._liftArrived.Invoke();
		// param._porte.OnPercentageChange += (float perc) => {
		// 	if(perc == 0) {
		// 		_finished = true;
		// 		param._ascensore.LeavingFaringe();
		// 	}
		// };
	}

	public override void StateAction(LiftProps param) {}

	public override LiftState Transition(LiftProps param) {
		// if(_finished) throw new System.NotImplementedException();
		return this;
	}
}
