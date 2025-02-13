using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class StomachParameter {
	public PlaneLowering _chain;
	public PlaneLowering _acid_plane;
	public PercentageToggleManager _doors;
	public SceneReference _next_scene;
	public MonoBehaviour _monoBehaviour;
	public StomachFSM _stomachFsm;
	public Trigger _exit_trigger;
	public AudioSource _audio;

	public StomachParameter(PlaneLowering chain, PlaneLowering acid_plane, PercentageToggleManager doors, SceneReference next_scene,
		MonoBehaviour monoBehaviour, StomachFSM stomachFsm, Trigger exit_trigger, AudioSource audio) {
		_chain = chain;
		_acid_plane = acid_plane;
		_doors = doors;
		_next_scene = next_scene;
		_monoBehaviour = monoBehaviour;
		_stomachFsm = stomachFsm;
		_exit_trigger = exit_trigger;
		_audio = audio;
	}
}

public abstract class StomachState : FSMState<StomachState, StomachParameter> {}

public class StomachFSM : FSM<StomachState, StomachParameter> {
	[SerializeField]
	private PlaneLowering _chain;

	[SerializeField]
	private PlaneLowering _acid_plane;

	[SerializeField]
	private PercentageToggleManager _doors;

	[SerializeField]
	private SceneReference _next_scene;

	[SerializeField]
	private Trigger _exit_trigger;

	[SerializeField]
	private AudioSource _audio;

	protected override StomachState GetInitialState() {
		Assert.IsNotNull(_acid_plane, $"{name} is not assigned its acid plane");
		Assert.IsNotNull(_chain, $"{name} is not assigned its chain");
		Assert.IsNotNull(_doors, $"{name} is not assigned the exit door");
		Assert.AreNotEqual(_next_scene.SceneName, "", $"{name} is not assigned the next scene");
		Assert.IsNotNull(_exit_trigger, $"{name} is not assigned the exit trigger");
		Assert.IsNotNull(_audio, $"{name} is missing a reference to the music source");

		return new LiftArriving_StomachState();
	}

	protected override StomachParameter GetParams() {
		return new StomachParameter(_chain, _acid_plane, _doors, _next_scene, this, this, _exit_trigger, _audio);
	}
}
