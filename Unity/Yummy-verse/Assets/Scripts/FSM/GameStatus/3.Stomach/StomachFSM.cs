using System;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class StomachParameter {
	public PercentageToggleManager _chain;
	public PercentageToggleManager _acid_plane;
	public PercentageToggleManager _doors;
	public SceneReference _next_scene;
	public MonoBehaviour _monoBehaviour;
	public StomachFSM _stomachFsm;
	public Trigger _exit_trigger;
	public AudioSource _audio;
	public SceneReference _prev_scene;
	public Action _liftArrived;
	public NpcMovementSequence _NPC;
	public GameObject _cart;
	public GameObject _player;

	public StomachParameter(PercentageToggleManager chain, PercentageToggleManager acid_plane, PercentageToggleManager doors,
		SceneReference next_scene, MonoBehaviour monoBehaviour, StomachFSM stomachFsm, Trigger exit_trigger, AudioSource audio,
		SceneReference prev_scene, Action liftArrived, NpcMovementSequence nPC, GameObject cart, GameObject player) {
		_chain = chain;
		_acid_plane = acid_plane;
		_doors = doors;
		_next_scene = next_scene;
		_monoBehaviour = monoBehaviour;
		_stomachFsm = stomachFsm;
		_exit_trigger = exit_trigger;
		_audio = audio;
		_prev_scene = prev_scene;
		_liftArrived = liftArrived;
		_NPC = nPC;
		_cart = cart;
		_player = player;
	}
}

public abstract class StomachState : FSMState<StomachState, StomachParameter> {}

public class StomachFSM : FSM<StomachState, StomachParameter> {
	[SerializeField]
	private PercentageToggleManager _chain;

	[SerializeField]
	private PercentageToggleManager _acid_plane;

	[SerializeField]
	private PercentageToggleManager _doors;

	[SerializeField]
	private SceneReference _next_scene;

	[SerializeField]
	private Trigger _exit_trigger;

	[SerializeField]
	private AudioSource _audio;

	[SerializeField]
	private SceneReference _prev_scene;

	public Action _liftArrived;

	private NpcMovementSequence _NPC;

	private GameObject _cart;
	private GameObject _player;

	public void setActionHandler(Action handler) {
		_liftArrived += handler;
	}

	protected override StomachState GetInitialState() {
		Assert.IsNotNull(_acid_plane, $"{name} is not assigned its acid plane");
		Assert.IsNotNull(_chain, $"{name} is not assigned its chain");
		Assert.IsNotNull(_doors, $"{name} is not assigned the exit door");
		Assert.AreNotEqual(_next_scene.SceneName, "", $"{name} is not assigned the next scene");
		Assert.IsNotNull(_exit_trigger, $"{name} is not assigned the exit trigger");
		Assert.IsNotNull(_audio, $"{name} is missing a reference to the music source");
		Assert.AreNotEqual(_prev_scene.SceneName, "", $"{name} is missing a reference to the previous scene");

		GameObject NPC = GameObject.FindGameObjectWithTag("NPC");
		Assert.IsNotNull(NPC, $"{name} cannot find the NPC");
		_NPC = NPC.GetComponent<NpcMovementSequence>();
		Assert.IsNotNull(_NPC, $"{name} cannot find the NPC movement sequence");

		_cart = GameObject.FindGameObjectWithTag("Carrello");
		Assert.IsNotNull(_cart, $"{name} cannot find the Carrello");

		_player = GameObject.FindGameObjectWithTag("Player");
		Assert.IsNotNull(_player, $"{name} cannot find the player");

		return new LiftArriving_StomachState();
	}

	protected override StomachParameter GetParams() {
		return new StomachParameter(
			_chain, _acid_plane, _doors, _next_scene, this, this, _exit_trigger, _audio, _prev_scene, _liftArrived, _NPC, _cart, _player);
	}
}
