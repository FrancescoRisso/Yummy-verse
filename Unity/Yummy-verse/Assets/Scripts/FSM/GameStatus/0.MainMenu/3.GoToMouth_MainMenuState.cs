using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMouth_MainMenuState : MainMenuState {
	public override void PrepareBeforeAction(MainMenuParameter param) {
		param._monoBehaviour.StartCoroutine(SceneLoader.SwitchScene(param._next_scene, param._this_scene));
	}

	public override void StateAction(MainMenuParameter param) {}

	public override MainMenuState Transition(MainMenuParameter param) {
		return this;
	}
}
