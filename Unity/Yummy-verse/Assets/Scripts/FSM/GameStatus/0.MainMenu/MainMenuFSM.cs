using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class MainMenuParameter {
	public MainMenuParameter(float dark_time, SceneReference this_scene, SceneReference next_scene, MonoBehaviour monoBehaviour,
		GameObject start_game_button, VideoPlayerManager video_player, GameObject camera, SceneReference game_scene, MeshRenderer libro,
		Material libro_normal, Material libro_hover, Material libro_selected) {
		_dark_time = dark_time;
		_this_scene = this_scene;
		_next_scene = next_scene;
		_monoBehaviour = monoBehaviour;
		_start_game_button = start_game_button;
		_video_player = video_player;
		_camera = camera;
		_game_scene = game_scene;
		_libro = libro;
		_libro_normal = libro_normal;
		_libro_hover = libro_hover;
		_libro_selected = libro_selected;
	}

	public float _dark_time { get; set; }
	public SceneReference _this_scene { get; set; }
	public SceneReference _next_scene { get; set; }
	public MonoBehaviour _monoBehaviour { get; set; }
	public GameObject _start_game_button { get; set; }
	public VideoPlayerManager _video_player { get; set; }
	public GameObject _camera { get; set; }
	public SceneReference _game_scene { set; get; }
	public MeshRenderer _libro { set; get; }
	public Material _libro_normal { set; get; }
	public Material _libro_hover { set; get; }
	public Material _libro_selected { set; get; }
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

	[SerializeField]
	private GameObject _camera;

	[SerializeField]
	private SceneReference _game_scene;

	[SerializeField]
	private MeshRenderer _libro;

	[SerializeField]
	private Material _libro_normal;

	[SerializeField]
	private Material _libro_hover;

	[SerializeField]
	private Material _libro_selected;

	protected override MainMenuState GetInitialState() {
		Assert.AreNotEqual(_this_scene.SceneName, "", $"{name} does not have a current scene assigned");
		Assert.AreNotEqual(_next_scene.SceneName, "", $"{name} does not have a next scene assigned");
		Assert.IsNotNull(_start_game_button, $"{name} does not have a reference to the start game button");
		Assert.IsNotNull(_video_player, $"{name} does not have a reference to the video player");
		Assert.IsNotNull(_camera, $"{name} does not have a reference to the camera");
		Assert.AreNotEqual(_game_scene.SceneName, "", $"{name} does not have the game scene reference");
		Assert.IsNotNull(_libro, $"{name} does not have a reference to the book");
		Assert.IsNotNull(_libro_normal, $"{name} does not have a reference to the normal book material");
		Assert.IsNotNull(_libro_hover, $"{name} does not have a reference to the hover book material");
		Assert.IsNotNull(_libro_selected, $"{name} does not have a reference to the selected book material");

		return new Initial_MainMenuState();
	}

	protected override MainMenuParameter GetParams() {
		return new MainMenuParameter(_dark_time, _this_scene, _next_scene, this, _start_game_button, _video_player, _camera, _game_scene, _libro,
			_libro_normal, _libro_hover, _libro_selected);
	}
}
