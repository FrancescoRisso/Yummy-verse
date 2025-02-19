using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class ExpulsionRoomParam {
	public SceneReference _prev_scene;
	public SceneReference _next_scene;
	public MonoBehaviour _monoBehaviour;
	public ExpulsionRoomFSM _fsm;
	public Trigger _enter_trigger;
	public AudioSource _audio;
	public Button _expulsion_button;
	public PercentageToggleManager _botola_state;
	public GameObject _player;
	public Attractor _attractor;
	public VideoPlayerManager _video;
	public float _wait_before_restarting;
	public SceneReference _this_scene;
	public SceneReference _final_scene;
	public SceneReference _game_scene;
	public NPC_Espulsione _NPC;

	public ExpulsionRoomParam(SceneReference prev_scene, SceneReference next_scene, MonoBehaviour monoBehaviour, ExpulsionRoomFSM fsm,
		Trigger enter_trigger, AudioSource audio, Button expulsion_button, PercentageToggleManager botola_state, GameObject player,
		Attractor attractor, VideoPlayerManager video, float wait_before_restarting, SceneReference this_scene, SceneReference final_scene,
		SceneReference game_scene, NPC_Espulsione nPC) {
		_prev_scene = prev_scene;
		_next_scene = next_scene;
		_monoBehaviour = monoBehaviour;
		_fsm = fsm;
		_enter_trigger = enter_trigger;
		_audio = audio;
		_expulsion_button = expulsion_button;
		_botola_state = botola_state;
		_player = player;
		_attractor = attractor;
		_video = video;
		_wait_before_restarting = wait_before_restarting;
		_this_scene = this_scene;
		_final_scene = final_scene;
		_game_scene = game_scene;
		_NPC = nPC;
	}
}

public abstract class ExpulsionRoomState : FSMState<ExpulsionRoomState, ExpulsionRoomParam> {}

public class ExpulsionRoomFSM : FSM<ExpulsionRoomState, ExpulsionRoomParam> {
	[SerializeField]
	private SceneReference _prev_scene;

	[SerializeField]
	private SceneReference _next_scene;

	[SerializeField]
	private SceneReference _this_scene;

	[SerializeField]
	private SceneReference _final_scene;

	[SerializeField]
	private SceneReference _game_scene;

	[SerializeField]
	private Trigger _enter_trigger;

	[SerializeField]
	private AudioSource _audio;

	[SerializeField]
	private Button _expulsion_button;

	[SerializeField]
	public PercentageToggleManager _botola_state;

	private GameObject _player;

	[SerializeField]
	private Attractor _attractor;

	private VideoPlayerManager _video;

	[SerializeField]
	[Range(0.1f, 10)]
	private float _wait_before_restarting;

	[SerializeField]
	private NPC_Espulsione _NPC;

	protected override ExpulsionRoomState GetInitialState() {
		Assert.AreNotEqual(_prev_scene.SceneName, "", $"{name} is missing a reference to the previous scene");
		Assert.AreNotEqual(_next_scene.SceneName, "", $"{name} is not assigned the next scene");
		Assert.AreNotEqual(_this_scene.SceneName, "", $"{name} is not assigned the current scene");
		Assert.AreNotEqual(_final_scene.SceneName, "", $"{name} is not assigned the final scene");
		Assert.AreNotEqual(_game_scene.SceneName, "", $"{name} is not assigned the game scene");
		Assert.IsNotNull(_enter_trigger, $"{name} is not assigned the enter trigger");
		Assert.IsNotNull(_audio, $"{name} is missing a reference to the music source");
		Assert.IsNotNull(_expulsion_button, $"{name} is missing a reference to the expulsion button");
		Assert.IsNotNull(_botola_state, $"{name} is missing a reference to the percentage toggle manager for the botola");
		Assert.IsNotNull(_attractor, $"{name} cannot find the attractor");
		Assert.IsNotNull(_NPC, $"{name} cannot find the NPC");

		_player = GameObject.FindGameObjectWithTag("Player");
		Assert.IsNotNull(_player, $"{name} cannot find the player");

		_video = FindObjectOfType<VideoPlayerManager>();
		Assert.IsNotNull(_video, $"{name} cannot find the video player");

		return new BeforeEntering_ExpulsionRoomState();
	}

	protected override ExpulsionRoomParam GetParams() {
		return new ExpulsionRoomParam(_prev_scene, _next_scene, this, this, _enter_trigger, _audio, _expulsion_button, _botola_state, _player,
			_attractor, _video, _wait_before_restarting, _this_scene, _final_scene, _game_scene, _NPC);
	}
}
