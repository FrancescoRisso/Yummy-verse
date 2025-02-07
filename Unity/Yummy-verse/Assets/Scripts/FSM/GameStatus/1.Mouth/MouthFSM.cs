using UnityEngine;
using UnityEngine.Assertions;

public class MouthParameter {
	public MouthParameter(MovementInteractionEnabler player_enabler, float fading_duration, int num_chewings,
		PercentageCycleNotifier chewings_counter, PercentageToggleManager lift_doors) {
		_player_enabler = player_enabler;
		_fading_duration = fading_duration;
		_num_chewings = num_chewings;
		_chewings_counter = chewings_counter;
		_lift_doors = lift_doors;
	}

	public MovementInteractionEnabler _player_enabler { set; get; }
	public float _fading_duration { set; get; }
	public int _num_chewings { set; get; }
	public PercentageCycleNotifier _chewings_counter { set; get; }
	public PercentageToggleManager _lift_doors { set; get; }
}

public abstract class MouthState : FSMState<MouthState, MouthParameter> {}


public class MouthFSM : FSM<MouthState, MouthParameter> {
	[SerializeField]
	[Range(0.1f, 5)]
	private float _fading_duration = 1;

	[SerializeField]
	private MovementInteractionEnabler _player_enabler;

	// [SerializeField]
	// private PercentageToggleManager _fading_manager;

	[SerializeField]
	[Range(1, 10)]
	private int _num_chewings = 5;

	[SerializeField]
	private PercentageCycleNotifier _chewings_counter;

	[SerializeField]
	private PercentageToggleManager _lift_doors;

	protected override MouthState GetInitialState() {
		// Assert.IsNotNull(_fading_manager, $"{name} does not have the fading manager");
		Assert.IsNotNull(_player_enabler, $"{name} does not have the player's movement interaction enabler");
		Assert.IsNotNull(_chewings_counter, $"{name} does not have the chewing counter");
		Assert.IsNotNull(_lift_doors, $"{name} does not have the lift doors");

		return new DarknessFadingOut_MouthState();
	}

	protected override MouthParameter GetParams() {
		return new MouthParameter(_player_enabler, _fading_duration, _num_chewings, _chewings_counter, _lift_doors);
	}
}
