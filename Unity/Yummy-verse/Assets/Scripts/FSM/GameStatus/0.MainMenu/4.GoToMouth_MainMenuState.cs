using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMouth_MainMenuState : MainMenuState {
	public override void PrepareBeforeAction(MainMenuParameter param) {
		param._monoBehaviour.StartCoroutine(SceneLoader.LoadSceneAndThen(param._next_scene, () => {
			param._camera.GetComponent<Camera>().enabled = false;
			param._camera.GetComponent<AudioListener>().enabled = false;
			SceneLoader.SetActiveScene(param._next_scene);
			param._monoBehaviour.StartCoroutine(SceneLoader.UnoadScene(param._this_scene));
		}));
	}

	public override void StateAction(MainMenuParameter param) {}

	public override MainMenuState Transition(MainMenuParameter param) {
		return this;
	}
}
