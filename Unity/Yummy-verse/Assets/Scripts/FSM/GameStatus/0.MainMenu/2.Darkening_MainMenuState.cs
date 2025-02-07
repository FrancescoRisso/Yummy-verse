using System;
using UnityEngine;

public class Darkening_MainMenuState : MainMenuState {
	private float _darkness_perc = 0;

	public override void StateAction(MainMenuParameter param) {
		_darkness_perc = Math.Clamp(_darkness_perc + param._darkening_speed * Time.deltaTime, 0, 1);
		// TODO far scurire lo schermo
		Debug.Log($"Diventa buioooo ({100 * _darkness_perc}%)");
	}

	public override MainMenuState Transition(MainMenuParameter param) {
		if(_darkness_perc == 1) return new GoToMouth_MainMenuState();

		return this;
	}
}
