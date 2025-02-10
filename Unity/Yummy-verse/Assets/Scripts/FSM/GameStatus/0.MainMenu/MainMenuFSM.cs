using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class MainMenuParameter {
	public MainMenuParameter(float dark_time, SceneReference this_scene, SceneReference next_scene, MonoBehaviour monoBehaviour,
		GameObject start_game_button, VideoPlayerManager video_player) {
		_dark_time = dark_time;
		_this_scene = this_scene;
		_next_scene = next_scene;
		_monoBehaviour = monoBehaviour;
		_start_game_button = start_game_button;
		_video_player = video_player;
	}

	public float _dark_time { get; set; }
	public SceneReference _this_scene { get; set; }
	public SceneReference _next_scene { get; set; }
	public MonoBehaviour _monoBehaviour { get; set; }
	public GameObject _start_game_button { get; set; }
	public VideoPlayerManager _video_player { get; set; }
}

public abstract class MainMenuState : FSMState<MainMenuState, MainMenuParameter> {}

public class MainMenuFSM : FSM<MainMenuState, MainMenuParameter> {
	[SerializeField]
	[Range(0.1f, 10)]
	private float _dark_time = 1;

	[SerializeField]
	private SceneReference _this_scene = null;

	[SerializeField]
	private SceneReference _next_scene = null;

	[SerializeField]
	private GameObject _start_game_button;

	[SerializeField]
	private VideoPlayerManager _video_player;

	protected override MainMenuState GetInitialState() {
		Assert.AreNotEqual(_this_scene.SceneName, "", $"{name} does not have a current scene assigned");
		Assert.AreNotEqual(_next_scene.SceneName, "", $"{name} does not have a next scene assigned");
		Assert.IsNotNull(_start_game_button, $"{name} does not have a reference to the start game button");
		Assert.IsNotNull(_video_player, $"{name} does not have a reference to the video player");

		return new Initial_MainMenuState();
	}

	protected override MainMenuParameter GetParams() {
		return new MainMenuParameter(_dark_time, _this_scene, _next_scene, this, _start_game_button, _video_player);
	}
}
