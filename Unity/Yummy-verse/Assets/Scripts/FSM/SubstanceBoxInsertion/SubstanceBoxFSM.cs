using System;
using UnityEngine;
using UnityEngine.Assertions;

public class SubstanceBoxParam {
	public MonoBehaviour _mono_behaviour;
	public GameObject _game_object;
	public Action<Action<SubstanceBoxLights>> _addInsertIntoBoxListener;
	public float _position_preparing_time;
	public float _insertion_time;
	public Shape _shape;
	public Action _delete_trigger;
	public Action _notify_box;

	public SubstanceBoxParam(MonoBehaviour mono_behaviour, GameObject game_object, Action<Action<SubstanceBoxLights>> addInsertIntoBoxListener,
		float position_preparing_time, float insertion_time, Shape shape, Action delete_trigger, Action notify_box) {
		_mono_behaviour = mono_behaviour;
		_game_object = game_object;
		_addInsertIntoBoxListener = addInsertIntoBoxListener;
		_position_preparing_time = position_preparing_time;
		_insertion_time = insertion_time;
		_shape = shape;
		_delete_trigger = delete_trigger;
		_notify_box = notify_box;
	}
}

public abstract class SubstanceBoxState : FSMState<SubstanceBoxState, SubstanceBoxParam> {}

public class SubstanceBoxFSM : FSM<SubstanceBoxState, SubstanceBoxParam> {
	[SerializeField]
	private Shape _shape;

	public Action<SubstanceBoxLights> InsertIntoBox;

	[SerializeField]
	[Range(0.1f, 3)]
	private float _position_preparing_time;

	[SerializeField]
	[Range(0.1f, 3)]
	private float _insertion_time;

	private Action _delete_trigger;

	private SubstanceBoxLights _notify_box;

	public void SetDeleteTrigger(Action action) {
		_delete_trigger = action;
	}

	private void AddInsertIntoBoxListener(Action<SubstanceBoxLights> handler) {
		InsertIntoBox += handler;
	}

	protected override SubstanceBoxState GetInitialState() {
		InsertIntoBox += (SubstanceBoxLights box) => {
			_notify_box = box;
			Assert.IsNotNull(_notify_box, $"{name} does not have a box to notify");
		};

		return new NotInserting_SubstanceBoxState();
	}

	protected override SubstanceBoxParam GetParams() {
		return new SubstanceBoxParam(this, gameObject, AddInsertIntoBoxListener, _position_preparing_time, _insertion_time, _shape, _delete_trigger,
			_notify_box?.InsertSubstance);
	}

	public bool IsSameShape(Shape s) {
		return s == _shape;
	}
}
