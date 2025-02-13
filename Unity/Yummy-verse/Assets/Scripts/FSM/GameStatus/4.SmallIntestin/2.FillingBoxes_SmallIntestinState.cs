using UnityEngine;

public class FillingBoxes_SmallIntestinState : SmallIntestinState {
	private int _count = 0;

	public override void PrepareBeforeAction(SmallIntestinParam param) {
		param._audio.Play();
		foreach(SubstanceBoxLights box in param._boxes) box.Filled += () => _count++;
		param._mono_behaviour.StartCoroutine(SceneLoader.LoadScene(param._next_scene));
	}

	public override void StateAction(SmallIntestinParam param) {}

	public override SmallIntestinState Transition(SmallIntestinParam param) {
		if(_count == param._boxes.Length) return new Exiting_SmallIntestinState();
		return this;
	}
}
