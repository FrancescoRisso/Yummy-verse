using UnityEngine;
using UnityEngine.Assertions;

public class SmallIntestinParam {
	public SubstanceBoxLights[] _boxes;

	public SmallIntestinParam(SubstanceBoxLights[] boxes) {
		_boxes = boxes;
	}
}

public abstract class SmallIntestinState : FSMState<SmallIntestinState, SmallIntestinParam> {}

public class SmallIntestinFSM : FSM<SmallIntestinState, SmallIntestinParam> {
	[SerializeField]
	private SubstanceBoxLights[] _boxes;

	protected override SmallIntestinState GetInitialState() {
		Assert.AreNotEqual(_boxes.Length, 0, $"{name} does not have the boxes assigned");

		return new FillingBoxes_SmallIntestinState();
	}

	protected override SmallIntestinParam GetParams() {
		return new SmallIntestinParam(_boxes);
	}
}
