using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToFinalScene_ExpulsionRoomState : ExpulsionRoomState {
	public override void PrepareBeforeAction(ExpulsionRoomParam param) {
		param._monoBehaviour.StartCoroutine(SceneLoader.LoadSceneReplace(param._final_scene));
	}

	public override void StateAction(ExpulsionRoomParam param) {}

	public override ExpulsionRoomState Transition(ExpulsionRoomParam param) {
		return this;
	}
}
