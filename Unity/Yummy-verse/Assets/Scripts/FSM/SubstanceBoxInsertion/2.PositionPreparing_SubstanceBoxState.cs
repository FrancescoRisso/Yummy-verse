using System;
using UnityEngine;

public class PositionPreparing_SubstanceBoxState : SubstanceBoxState {
	private Vector3 _initial_pos;

	private Vector3 _final_pos;

	private Vector3 _rotation_per_sec;

	private float _elapsed = 0;

	public override void PrepareBeforeAction(SubstanceBoxParam param) {
		_initial_pos = param._game_object.transform.localPosition;

		switch(param._shape) {
			case Shape.Square: _final_pos = new Vector3(-0.0292000007f, 0.107600003f, 0.121699996f); break;
			case Shape.Triangle: _final_pos = new Vector3(0.0401900001f, 0.117409997f, 0.129899994f); break;
			case Shape.Circle: _final_pos = new Vector3(0.0350000001f, 0.0522000007f, 0.119199999f); break;
			case Shape.Star: _final_pos = new Vector3(-0.0349199995f, 0.0500999987f, 0.123099998f); break;
		}

		float rot_x = -param._game_object.transform.localRotation.eulerAngles.x;
		float rot_y = -param._game_object.transform.localRotation.eulerAngles.y;
		float rot_z = -param._game_object.transform.localRotation.eulerAngles.z;

		if(rot_x < -180) rot_x += 360;
		if(rot_y < -180) rot_y += 360;
		if(rot_z < -180) rot_z += 360;

		_rotation_per_sec = new Vector3(rot_x, rot_y, rot_z) / param._position_preparing_time;
	}

	public override void StateAction(SubstanceBoxParam param) {
		_elapsed += Time.deltaTime;
		float perc = _elapsed / param._position_preparing_time;
		param._game_object.transform.localPosition = perc * _final_pos + (1 - perc) * _initial_pos;

		if(_elapsed < param._position_preparing_time)
			param._game_object.transform.Rotate(_rotation_per_sec * Time.deltaTime);
		else
			param._game_object.transform.localEulerAngles = new Vector3(0, 0, 0);
	}

	public override SubstanceBoxState Transition(SubstanceBoxParam param) {
		if(_elapsed >= param._position_preparing_time) return new Insertion_SubstanceBoxState();
		return this;
	}
}
