using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToFinalScene_ExpulsionRoomState : ExpulsionRoomState {
	private bool _next_unloaded = false;
	private bool _final_started_loading = false;
	private bool _final_loaded = false;
	private bool _game_started_unloading = false;
	private bool _game_unloaded = false;
	private bool _this_started_unloading = false;

	public override void PrepareBeforeAction(ExpulsionRoomParam param) {
		// param._monoBehaviour.StartCoroutine(SceneLoader.UnloadSceneAndThen(param._next_scene, () => { _next_unloaded = true; }));
		// param._monoBehaviour.StartCoroutine(SceneLoader.UnloadScene(param._next_scene));
		// param._monoBehaviour.StartCoroutine(SceneLoader.SwitchScene(param._final_scene, param._game_scene));
		param._monoBehaviour.StartCoroutine(SceneLoader.LoadSceneReplace(param._final_scene));
	}

	public override void StateAction(ExpulsionRoomParam param) {
	// 	if(_next_unloaded && !_final_started_loading) {
	// 		param._monoBehaviour.StartCoroutine(SceneLoader.LoadSceneAndThen(param._final_scene, () => { _final_loaded = true; }));
	// 		_final_started_loading = true;
	// 	}

	// 	if(_final_loaded && !_game_started_unloading) {
	// 		SceneLoader.SetActiveScene(param._final_scene);
	// 		param._monoBehaviour.StartCoroutine(SceneLoader.UnloadSceneAndThen(param._game_scene, () => { _game_unloaded = true; }));
	// 		_game_started_unloading = true;
	// 	}

	// 	if(_game_unloaded && !_this_started_unloading) {
	// 		SceneLoader.SetActiveScene(param._final_scene);
	// 		param._monoBehaviour.StartCoroutine(SceneLoader.UnloadScene(param._this_scene));
	// 		_this_started_unloading = true;
	// 	}
	}

	public override ExpulsionRoomState Transition(ExpulsionRoomParam param) {
		return this;
	}
}
