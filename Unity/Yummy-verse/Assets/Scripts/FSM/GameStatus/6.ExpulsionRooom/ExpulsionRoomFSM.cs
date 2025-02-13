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

	public ExpulsionRoomParam(SceneReference prev_scene, SceneReference next_scene, MonoBehaviour monoBehaviour, ExpulsionRoomFSM fsm,
		Trigger enter_trigger, AudioSource audio, Button expulsion_button, PercentageToggleManager botola_state) {
		_prev_scene = prev_scene;
		_next_scene = next_scene;
		_monoBehaviour = monoBehaviour;
		_fsm = fsm;
		_enter_trigger = enter_trigger;
		_audio = audio;
		_expulsion_button = expulsion_button;
		_botola_state = botola_state;
	}
}

public abstract class ExpulsionRoomState : FSMState<ExpulsionRoomState, ExpulsionRoomParam> {}

public class ExpulsionRoomFSM : FSM<ExpulsionRoomState, ExpulsionRoomParam> {
	[SerializeField]
	private SceneReference _prev_scene;

	[SerializeField]
	private SceneReference _next_scene;

	[SerializeField]
	private Trigger _enter_trigger;

	[SerializeField]
	private AudioSource _audio;

	[SerializeField]
	private Button _expulsion_button;

	[SerializeField]
	public PercentageToggleManager _botola_state;


	protected override ExpulsionRoomState GetInitialState() {
		Assert.AreNotEqual(_prev_scene.SceneName, "", $"{name} is missing a reference to the previous scene");
		Assert.AreNotEqual(_next_scene.SceneName, "", $"{name} is not assigned the next scene");
		Assert.IsNotNull(_enter_trigger, $"{name} is not assigned the enter trigger");
		Assert.IsNotNull(_audio, $"{name} is missing a reference to the music source");
		Assert.IsNotNull(_expulsion_button, $"{name} is missing a reference to the expulsion button");
		Assert.IsNotNull(_botola_state, $"{name} is missing a reference to the percentage toggle manager for the botola");

		return new BeforeEntering_ExpulsionRoomState();
	}

	protected override ExpulsionRoomParam GetParams() {
		return new ExpulsionRoomParam(_prev_scene, _next_scene, this, this, _enter_trigger, _audio, _expulsion_button, _botola_state);
	}
}
