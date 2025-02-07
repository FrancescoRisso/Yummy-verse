using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using Utilities;

public class MainMenuParameter {
	public float _darkening_speed { get; set; }
	public SceneReference _this_scene { get; set; }
	public SceneReference _next_scene { get; set; }

	public MainMenuParameter(float darkening_speed, SceneReference this_scene, SceneReference next_scene) {
		_darkening_speed = darkening_speed;
		_this_scene = this_scene;
		_next_scene = next_scene;
	}
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

	protected override MainMenuState GetInitialState() {
		Assert.AreNotEqual(_this_scene.SceneName, "", $"{name} does not have a current scene assigned");
		Assert.AreNotEqual(_next_scene.SceneName, "", $"{name} does not have a next scene assigned");

		return new Initial_MainMenuState();
	}

	protected override MainMenuParameter GetParams() {
		return new MainMenuParameter(_darkening_speed, _this_scene, _next_scene);
	}
}
