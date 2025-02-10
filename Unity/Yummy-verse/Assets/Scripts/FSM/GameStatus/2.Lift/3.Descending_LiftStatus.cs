using System.Collections;
using UnityEngine;

public class Descending_LiftStatus : LiftState {
	private bool _arrived;

	public override void PrepareBeforeAction(LiftProps param) {
		param._mono_behaviour.StartCoroutine(SceneLoader.LoadScene(param._next_scene));
		param._mono_behaviour.StartCoroutine(SceneLoader.UnloadScene(param._prev_scene));
	}

	public override void StateAction(LiftProps param) {
		param._mono_behaviour.StartCoroutine(Wait(param._descend_time));
		_arrived = true;
	}

	public override LiftState Transition(LiftProps param) {
		if(_arrived) return new OpeningDoors_LiftStatus();
		return this;
	}

	private IEnumerator Wait(float time) {
		yield return new WaitForSeconds(time);
	}
}
