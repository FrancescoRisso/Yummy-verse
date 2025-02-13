using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class LiftProps {
	public PulsantiLuciAscensore _ascensore;
	public PercentageToggleManager _porte;
	public SceneReference _next_scene;
	public SceneReference _prev_scene;
	public MonoBehaviour _mono_behaviour;
	public float _descend_time;
	public AudioSource _audio;
	public GameObject gameObject;

	public LiftProps(PulsantiLuciAscensore ascensore, PercentageToggleManager porte, SceneReference next_scene, SceneReference prev_scene,
		MonoBehaviour mono_behaviour, float descend_time, AudioSource audio, GameObject gameObject) {
		_ascensore = ascensore;
		_porte = porte;
		_next_scene = next_scene;
		_prev_scene = prev_scene;
		_mono_behaviour = mono_behaviour;
		_descend_time = descend_time;
		_audio = audio;
		this.gameObject = gameObject;
	}
}

public abstract class LiftState : FSMState<LiftState, LiftProps> {}

public class LiftFSM : FSM<LiftState, LiftProps> {
	[SerializeField]
	private PulsantiLuciAscensore _ascensore;

	[SerializeField]
	private PercentageToggleManager _porte;

	[SerializeField]
	private SceneReference _next_scene;

	[SerializeField]
	private SceneReference _prev_scene;

	[SerializeField]
	[Range(0.1f, 10)]
	private float _descend_time;

	[SerializeField]
	private AudioSource _audio;

	protected override LiftState GetInitialState() {
		Assert.IsNotNull(_ascensore, $"{name} is missing a reference to the ascensore");
		Assert.IsNotNull(_porte, $"{name} is missing a reference to the doors");
		Assert.AreNotEqual(_prev_scene.SceneName, "", $"{name} is missing a reference to the previous scene");
		Assert.AreNotEqual(_next_scene.SceneName, "", $"{name} is missing a reference to the next scene");
		Assert.IsNotNull(_audio, $"{name} is missing a reference to the music source");

		return new BeforeLeaving_LiftStatus();
	}

	protected override LiftProps GetParams() {
		return new LiftProps(_ascensore, _porte, _next_scene, _prev_scene, this, _descend_time, _audio, gameObject);
	}
}
