using UnityEngine;
using UnityEngine.Assertions;

public class MouthParameter {
	public MouthParameter(MovementInteractionEnabler player_enabler, int num_chewings, PercentageCycleNotifier chewings_counter,
		PercentageToggleManager lift_doors, VideoPlayerManager video_player) {
		_player_enabler = player_enabler;
		_num_chewings = num_chewings;
		_chewings_counter = chewings_counter;
		_lift_doors = lift_doors;
		_video_player = video_player;
	}

	public MovementInteractionEnabler _player_enabler { set; get; }
	public int _num_chewings { set; get; }
	public PercentageCycleNotifier _chewings_counter { set; get; }
	public PercentageToggleManager _lift_doors { set; get; }
	public VideoPlayerManager _video_player { set; get; }
}

public abstract class MouthState : FSMState<MouthState, MouthParameter> {}


public class MouthFSM : FSM<MouthState, MouthParameter> {
	[SerializeField]
	private MovementInteractionEnabler _player_enabler;

	[SerializeField]
	[Range(1, 10)]
	private int _num_chewings = 5;

	[SerializeField]
	private PercentageCycleNotifier _chewings_counter;

	[SerializeField]
	private PercentageToggleManager _lift_doors;

	[SerializeField]
	private VideoPlayerManager _video_player;

	protected override MouthState GetInitialState() {
		// Assert.IsNotNull(_fading_manager, $"{name} does not have the fading manager");
		Assert.IsNotNull(_player_enabler, $"{name} does not have the player's movement interaction enabler");
		Assert.IsNotNull(_chewings_counter, $"{name} does not have the chewing counter");
		Assert.IsNotNull(_lift_doors, $"{name} does not have the lift doors");
		Assert.IsNotNull(_video_player, $"{name} does not have the video player");

		return new DarknessFadingOut_MouthState();
	}

	protected override MouthParameter GetParams() {
		return new MouthParameter(_player_enabler, _num_chewings, _chewings_counter, _lift_doors, _video_player);
	}
}
