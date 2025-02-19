using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class SmallIntestinParam {
	public SubstanceBoxLights[] _boxes;
	public AudioSource _audio;
	public Trigger _enter_trigger;
	public SceneReference _next_scene;
	public SceneReference _prev_scene;
	public Trigger _exit_trigger;
	public MonoBehaviour _mono_behaviour;
	public SmallIntestinFSM _fsm;
	public Trigger _middle_trigger;
	public NPC_Intestino _NPC_start;
	public NPC_IntestinoFine _NPC_end;

	public SmallIntestinParam(SubstanceBoxLights[] boxes, AudioSource audio, Trigger enter_trigger, SceneReference next_scene,
		SceneReference prev_scene, Trigger exit_trigger, MonoBehaviour mono_behaviour, SmallIntestinFSM fsm, Trigger middle_trigger,
		NPC_Intestino nPC_start, NPC_IntestinoFine nPC_end) {
		_boxes = boxes;
		_audio = audio;
		_enter_trigger = enter_trigger;
		_next_scene = next_scene;
		_prev_scene = prev_scene;
		_exit_trigger = exit_trigger;
		_mono_behaviour = mono_behaviour;
		_fsm = fsm;
		_middle_trigger = middle_trigger;
		_NPC_start = nPC_start;
		_NPC_end = nPC_end;
	}
}

public abstract class SmallIntestinState : FSMState<SmallIntestinState, SmallIntestinParam> {}

public class SmallIntestinFSM : FSM<SmallIntestinState, SmallIntestinParam> {
	[SerializeField]
	private SubstanceBoxLights[] _boxes;

	[SerializeField]
	private AudioSource _audio;

	[SerializeField]
	private Trigger _enter_trigger;

	[SerializeField]
	private SceneReference _next_scene;

	[SerializeField]
	private Trigger _exit_trigger;

	[SerializeField]
	private SceneReference _prev_scene;

	[SerializeField]
	private Trigger _middle_trigger;

	[SerializeField]
	private NPC_Intestino _NPC_start;

	[SerializeField]
	private NPC_IntestinoFine _NPC_end;


	protected override SmallIntestinState GetInitialState() {
		Assert.AreNotEqual(_boxes.Length, 0, $"{name} does not have the boxes assigned");
		Assert.IsNotNull(_audio, $"{name} is missing a reference to the music source");
		Assert.IsNotNull(_enter_trigger, $"{name} is not assigned the enter trigger");
		Assert.AreNotEqual(_next_scene.SceneName, "", $"{name} is not assigned the next scene");
		Assert.IsNotNull(_exit_trigger, $"{name} is not assigned the exit trigger");
		Assert.AreNotEqual(_prev_scene.SceneName, "", $"{name} is missing a reference to the previous scene");
		Assert.IsNotNull(_middle_trigger, $"{name} is not assigned the middle trigger");
		Assert.IsNotNull(_NPC_start, $"{name} is not assigned the room NPC");
		Assert.IsNotNull(_NPC_end, $"{name} is not assigned the tube NPC");

		return new BeforeEntering_SmallIntestinState();
	}

	protected override SmallIntestinParam GetParams() {
		return new SmallIntestinParam(
			_boxes, _audio, _enter_trigger, _next_scene, _prev_scene, _exit_trigger, this, this, _middle_trigger, _NPC_start, _NPC_end);
	}
}
