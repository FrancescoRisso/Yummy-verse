using UnityEngine.Assertions;
using UnityEngine;

public class Left_StomachState : StomachState {
	public override void PrepareBeforeAction(StomachParameter param) {
		param._stomachFsm.enabled = false;
		param._audio.Pause();
		
		param._monoBehaviour.StartCoroutine(SceneLoader.UnloadScene(param._prev_scene));
	}

	public override void StateAction(StomachParameter param) {}

	public override StomachState Transition(StomachParameter param) {
		return this;
	}
}
