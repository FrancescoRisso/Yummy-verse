using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class MouthParameter {
	public MouthParameter(int num_chewings, PercentageCycleNotifier chewings_counter, PercentageToggleManager lift_doors,
		VideoPlayerManager video_player, MonoBehaviour mono_behaivour, SceneReference game_scene, GameObject tmp_camera, SceneReference next_scene) {
		_num_chewings = num_chewings;
		_chewings_counter = chewings_counter;
		_lift_doors = lift_doors;
		_video_player = video_player;
		_mono_behaivour = mono_behaivour;
		_game_scene = game_scene;
		_tmp_camera = tmp_camera;
		_next_scene = next_scene;
	}

	public int _num_chewings { set; get; }
	public PercentageCycleNotifier _chewings_counter { set; get; }
	public PercentageToggleManager _lift_doors { set; get; }
	public VideoPlayerManager _video_player { set; get; }
	public MonoBehaviour _mono_behaivour { set; get; }
	public SceneReference _game_scene { set; get; }
	public GameObject _tmp_camera { set; get; }
	public SceneReference _next_scene { set; get; }
}

public abstract class MouthState : FSMState<MouthState, MouthParameter> {}


public class MouthFSM : FSM<MouthState, MouthParameter> {
	[SerializeField]
	[Range(2, 10)]
	private int _num_chewings = 5;

	[SerializeField]
	private PercentageCycleNotifier _chewings_counter;

	[SerializeField]
	private PercentageToggleManager _lift_doors;

	[SerializeField]
	private VideoPlayerManager _video_player;

	[SerializeField]
	private SceneReference _game_scene;

	[SerializeField]
	private GameObject _tmp_camera;

	[SerializeField]
	private SceneReference _next_scene;

	protected override MouthState GetInitialState() {
		// Assert.IsNotNull(_fading_manager, $"{name} does not have the fading manager");
		Assert.IsNotNull(_chewings_counter, $"{name} does not have the chewing counter");
		Assert.IsNotNull(_lift_doors, $"{name} does not have the lift doors");
		Assert.IsNotNull(_video_player, $"{name} does not have the video player");
		Assert.AreNotEqual(_game_scene.SceneName, "", $"{name} does not have the game scene reference");
		Assert.AreNotEqual(_next_scene.SceneName, "", $"{name} does not have the next scene reference");
		Assert.IsNotNull(_tmp_camera, $"{name} does not have the temporary camera");

		return new DarknessFadingOut_MouthState();
	}

	protected override MouthParameter GetParams() {
		return new MouthParameter(_num_chewings, _chewings_counter, _lift_doors, _video_player, this, _game_scene, _tmp_camera, _next_scene);
	}
}
