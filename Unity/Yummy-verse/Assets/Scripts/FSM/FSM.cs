using UnityEngine;

public abstract class FSM<StateType, ParameterType> : MonoBehaviour
	where StateType : FSMState<StateType, ParameterType> {
	private StateType _state;
	private StateType _prev_state = null;

	protected abstract StateType GetInitialState();
	protected abstract ParameterType GetParams();


	void Start() {
		_state = GetInitialState();
		_state.PrepareBeforeAction(GetParams());
	}

	void Update() {
		_state.StateAction(GetParams());
		_prev_state = _state;
		_state = _state.Transition(GetParams());
		if(_state.GetType() != _prev_state.GetType()) _state.PrepareBeforeAction(GetParams());
	}

	public bool StateIs<S>()
		where S : StateType {
		return _state is S;
	}
}
