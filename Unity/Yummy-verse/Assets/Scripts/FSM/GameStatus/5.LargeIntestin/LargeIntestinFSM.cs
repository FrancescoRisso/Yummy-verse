using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class LargeIntestinParam {
	public SceneReference _prev_scene;
	public SceneReference _next_scene;
	public MonoBehaviour _monoBehaviour;
	public LargeIntestinFSM _fsm;
	public Trigger _enter_trigger;
	public Trigger _exit_trigger;
	public AudioSource _audio;
	public NPC_Serpentone _NPC;

	public LargeIntestinParam(SceneReference prev_scene, SceneReference next_scene, MonoBehaviour monoBehaviour, LargeIntestinFSM fsm,
		Trigger enter_trigger, Trigger exit_trigger, AudioSource audio, NPC_Serpentone nPC) {
		_prev_scene = prev_scene;
		_next_scene = next_scene;
		_monoBehaviour = monoBehaviour;
		_fsm = fsm;
		_enter_trigger = enter_trigger;
		_exit_trigger = exit_trigger;
		_audio = audio;
		_NPC = nPC;
	}
}

public abstract class LargeIntestinState : FSMState<LargeIntestinState, LargeIntestinParam> {}

public class LargeIntestinFSM : FSM<LargeIntestinState, LargeIntestinParam> {
	[SerializeField]
	private SceneReference _prev_scene;

	[SerializeField]
	private SceneReference _next_scene;

	[SerializeField]
	private Trigger _exit_trigger;

	[SerializeField]
	private Trigger _enter_trigger;

	[SerializeField]
	private AudioSource _audio;

	[SerializeField]
	private NPC_Serpentone _NPC;
	protected override LargeIntestinState GetInitialState() {
		Assert.AreNotEqual(_prev_scene.SceneName, "", $"{name} is missing a reference to the previous scene");
		Assert.AreNotEqual(_next_scene.SceneName, "", $"{name} is not assigned the next scene");
		Assert.IsNotNull(_exit_trigger, $"{name} is not assigned the exit trigger");
		Assert.IsNotNull(_enter_trigger, $"{name} is not assigned the enter trigger");
		Assert.IsNotNull(_audio, $"{name} is missing a reference to the music source");
		Assert.IsNotNull(_NPC, $"{name} is missing a reference to the NPC");

		return new BeforeEntering_LargeIntestinState();
	}

	protected override LargeIntestinParam GetParams() {
		return new LargeIntestinParam(_prev_scene, _next_scene, this, this, _enter_trigger, _exit_trigger, _audio, _NPC);
	}
}
