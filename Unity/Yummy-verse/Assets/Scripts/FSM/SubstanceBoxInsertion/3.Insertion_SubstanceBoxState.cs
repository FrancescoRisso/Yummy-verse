using System;
using UnityEngine;

public class Insertion_SubstanceBoxState : SubstanceBoxState {
	private Vector3 _initial_pos;

	private Vector3 _final_pos;

	private float _elapsed = 0;

	public override void PrepareBeforeAction(SubstanceBoxParam param) {
		_initial_pos = param._game_object.transform.localPosition;
		_final_pos = _initial_pos;

		switch(param._shape) {
			case Shape.Square: _final_pos.z = 0.0619f; break;
			case Shape.Triangle: _final_pos.z = 0.0624f; break;
			case Shape.Circle: _final_pos.z = 0.0648f; break;
			case Shape.Star: _final_pos.z = 0.0613f; break;
		}
	}

	public override void StateAction(SubstanceBoxParam param) {
		_elapsed += Time.deltaTime;
		float perc = _elapsed / param._insertion_time;
		param._game_object.transform.localPosition = perc * _final_pos + (1 - perc) * _initial_pos;
	}

	public override SubstanceBoxState Transition(SubstanceBoxParam param) {
		if(_elapsed >= param._insertion_time) {
			param._notify_box.Invoke();
			param._delete_trigger.Invoke();
			MonoBehaviour.Destroy(param._mono_behaviour);
		}
		return this;
	}
}
