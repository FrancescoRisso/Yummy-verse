using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class MainMenuParameter {
	public MainMenuParameter(
		float darkening_speed, SceneReference this_scene, SceneReference next_scene, MonoBehaviour monoBehaviour, GameObject start_game_button) {
		_darkening_speed = darkening_speed;
		_this_scene = this_scene;
		_next_scene = next_scene;
		_monoBehaviour = monoBehaviour;
		_start_game_button = start_game_button;
	}

	public float _darkening_speed { get; set; }
	public SceneReference _this_scene { get; set; }
	public SceneReference _next_scene { get; set; }
	public MonoBehaviour _monoBehaviour { get; set; }
	public GameObject _start_game_button { get; set; }
}

public abstract class MainMenuState : FSMState<MainMenuState, MainMenuParameter> {}

public class MainMenuFSM : FSM<MainMenuState, MainMenuParameter> {
	[SerializeField]
	[Range(0.1f, 10)]
	private float _darkening_speed = 1;

	[SerializeField]
	private SceneReference _this_scene = null;

	[SerializeField]
	private SceneReference _next_scene = null;

	[SerializeField]
	private GameObject _start_game_button;

	protected override MainMenuState GetInitialState() {
		Assert.AreNotEqual(_this_scene.SceneName, "", $"{name} does not have a current scene assigned");
		Assert.AreNotEqual(_next_scene.SceneName, "", $"{name} does not have a next scene assigned");
		Assert.IsNotNull(_start_game_button, $"{name} does not have a reference to the start game button");

		return new Initial_MainMenuState();
	}

	protected override MainMenuParameter GetParams() {
		return new MainMenuParameter(_darkening_speed, _this_scene, _next_scene, this, _start_game_button);
	}
}
