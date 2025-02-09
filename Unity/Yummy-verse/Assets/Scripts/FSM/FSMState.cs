abstract public class FSMState<StateType, ParameterType>
	where StateType : FSMState<StateType, ParameterType> {
	abstract public StateType Transition(ParameterType param);
	abstract public void StateAction(ParameterType param);

	virtual public void PrepareBeforeAction(ParameterType param) {}
}
