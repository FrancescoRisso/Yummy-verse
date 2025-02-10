using System;
using UnityEngine;

public class Darkening_MainMenuState : MainMenuState {
	private bool _finished = false;

	public override void PrepareBeforeAction(MainMenuParameter param) {
		param._video_player.Play();
		param._video_player.VideoFinished += () => { _finished = true; };
	}

	public override void StateAction(MainMenuParameter param) {}

	public override MainMenuState Transition(MainMenuParameter param) {
		if(_finished == true) return new Dark_MainMenuState();

		return this;
	}
}
