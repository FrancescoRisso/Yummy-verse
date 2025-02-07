using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMouth_MainMenuState : MainMenuState {
	public override void StateAction(MainMenuParameter param) {
		SceneManager.LoadScene(param._next_scene.SceneName, LoadSceneMode.Additive);
		SceneManager.UnloadSceneAsync(param._this_scene.SceneName);
	}

	public override MainMenuState Transition(MainMenuParameter param) {
		return this;
	}
}
