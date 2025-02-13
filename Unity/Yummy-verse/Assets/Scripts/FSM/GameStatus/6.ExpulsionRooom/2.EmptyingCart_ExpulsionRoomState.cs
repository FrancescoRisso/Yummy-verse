using System;

public class EmptyingCart_ExpulsionRoomState : ExpulsionRoomState {
	private bool _open_botola = false;
	private ExpulsionRoomParam _param;

	public override void PrepareBeforeAction(ExpulsionRoomParam param) {
		_param = param;
		param._expulsion_button.activated += OnButtonPress;
		param._botola_state.OnPercentageChange += (float perc) => {
			if(perc == 1) _open_botola = true;
		};
	}

	private void OnButtonPress() {
		_open_botola = true;
		_param._botola_state._toggle.Invoke();
		_param._expulsion_button.activated -= OnButtonPress;
	}

	public override void StateAction(ExpulsionRoomParam param) {}

	public override ExpulsionRoomState Transition(ExpulsionRoomParam param) {
		if(_open_botola) throw new NotImplementedException();
		return this;
	}
}
