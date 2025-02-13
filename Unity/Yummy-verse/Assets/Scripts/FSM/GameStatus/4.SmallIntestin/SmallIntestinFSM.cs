using UnityEngine;
using UnityEngine.Assertions;

public class SmallIntestinParam {
	public SubstanceBoxLights[] _boxes;
	public AudioSource _audio;
	public Trigger _enter_trigger;

	public SmallIntestinParam(SubstanceBoxLights[] boxes, AudioSource audio, Trigger enter_trigger) {
		_boxes = boxes;
		_audio = audio;
		_enter_trigger = enter_trigger;
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

	protected override SmallIntestinState GetInitialState() {
		Assert.AreNotEqual(_boxes.Length, 0, $"{name} does not have the boxes assigned");
		Assert.IsNotNull(_audio, $"{name} is missing a reference to the music source");
		Assert.IsNotNull(_enter_trigger, $"{name} is not assigned the enter trigger");

		return new BeforeEntering_SmallIntestinState();
	}

	protected override SmallIntestinParam GetParams() {
		return new SmallIntestinParam(_boxes, _audio, _enter_trigger);
	}
}
