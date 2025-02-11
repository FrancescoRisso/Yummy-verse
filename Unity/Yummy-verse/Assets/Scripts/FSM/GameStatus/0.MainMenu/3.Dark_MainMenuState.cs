using UnityEngine;

public class Dark_MainMenuState : MainMenuState {
	private float _time = 0;

	public override void StateAction(MainMenuParameter param) {
		_time += Time.deltaTime;
	}

	public override MainMenuState Transition(MainMenuParameter param) {
		if(_time > param._dark_time) return new GoToMouth_MainMenuState();
		return this;
	}
}
