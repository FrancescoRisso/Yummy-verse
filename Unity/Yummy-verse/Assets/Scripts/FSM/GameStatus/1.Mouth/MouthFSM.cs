using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class MouthParameter {
	public MouthParameter(int num_chewings, PercentageCycleNotifier chewings_counter, PercentageToggleManager lift_doors,
		VideoPlayerManager video_player, MonoBehaviour mono_behaivour, SceneReference game_scene, GameObject tmp_camera, SceneReference next_scene,
		MouthFSM fsm, Button call_lift, float door_open_time, CameraEnabler player, AudioSource opening_doors_audio, NPC nPC) {
		_num_chewings = num_chewings;
		_chewings_counter = chewings_counter;
		_lift_doors = lift_doors;
		_video_player = video_player;
		_mono_behaivour = mono_behaivour;
		_game_scene = game_scene;
		_tmp_camera = tmp_camera;
		_next_scene = next_scene;
		_fsm = fsm;
		_call_lift = call_lift;
		_door_open_time = door_open_time;
		_player = player;
		_opening_doors_audio = opening_doors_audio;
		_NPC = nPC;
	}

	public int _num_chewings { set; get; }
	public PercentageCycleNotifier _chewings_counter { set; get; }
	public PercentageToggleManager _lift_doors { set; get; }
	public VideoPlayerManager _video_player { set; get; }
	public MonoBehaviour _mono_behaivour { set; get; }
	public SceneReference _game_scene { set; get; }
	public GameObject _tmp_camera { set; get; }
	public SceneReference _next_scene { set; get; }
	public MouthFSM _fsm { set; get; }
	public Button _call_lift { set; get; }
	public float _door_open_time { set; get; }
	public CameraEnabler _player { set; get; }
	public AudioSource _opening_doors_audio { set; get; }
	public NPC _NPC { set; get; }
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

	[SerializeField]
	private Button _call_lift;

	[SerializeField]
	private float _door_open_time;

	private CameraEnabler _player;

	[SerializeField]
	private AudioSource _opening_doors_audio;

	[SerializeField]
	private NPC _NPC;

	protected override MouthState GetInitialState() {
		Assert.IsNotNull(_chewings_counter, $"{name} does not have the chewing counter");
		Assert.IsNotNull(_lift_doors, $"{name} does not have the lift doors");
		Assert.IsNotNull(_video_player, $"{name} does not have the video player");
		Assert.AreNotEqual(_game_scene.SceneName, "", $"{name} does not have the game scene reference");
		Assert.AreNotEqual(_next_scene.SceneName, "", $"{name} does not have the next scene reference");
		Assert.IsNotNull(_tmp_camera, $"{name} does not have the temporary camera");
		Assert.IsNotNull(_call_lift, $"{name} does not have the button to call the lift");
		Assert.IsNotNull(_opening_doors_audio, $"{name} does not have the audio source for the lift arriving");
		Assert.IsNotNull(_NPC, $"{name} does not have the NPC");

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Assert.IsNotNull(player, $"{name} cannot find the player");
		_player = player.GetComponent<CameraEnabler>();
		Assert.IsNotNull(_player, $"{name} cannot find the camera enabler for the player");

		return new DarknessFadingOut_MouthState();
	}

	protected override MouthParameter GetParams() {
		return new MouthParameter(_num_chewings, _chewings_counter, _lift_doors, _video_player, this, _game_scene, _tmp_camera, _next_scene, this,
			_call_lift, _door_open_time, _player, _opening_doors_audio, _NPC);
	}
}
