public class LiftProps {}

public abstract class LiftState : FSMState<LiftState, LiftProps> {}

public class LiftFSM : FSM<LiftState, LiftProps> {
	protected override LiftState GetInitialState() {
		throw new System.NotImplementedException();
	}

	protected override LiftProps GetParams() {
		return new LiftProps();
	}
}
