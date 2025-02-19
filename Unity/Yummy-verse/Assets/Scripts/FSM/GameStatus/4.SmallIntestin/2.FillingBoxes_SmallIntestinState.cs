using UnityEngine;

public class FillingBoxes_SmallIntestinState : SmallIntestinState {
	private SmallIntestinParam _param;
	private int _count = 0;

	public override void PrepareBeforeAction(SmallIntestinParam param) {
		// param._audio.Play();
		_param = param;

		param._NPC_start.NextAnimation();

		param._middle_trigger.Triggered += PlayerEnterIntestin;

		foreach(SubstanceBoxLights box in param._boxes) box.Filled += () => _count++;
		param._mono_behaviour.StartCoroutine(SceneLoader.LoadScene(param._next_scene));
	}

	public void PlayerEnterIntestin() {
		_param._middle_trigger.Triggered -= PlayerEnterIntestin;
		_param._NPC_start.NextAnimation();
	}

	public override void StateAction(SmallIntestinParam param) {}

	public override SmallIntestinState Transition(SmallIntestinParam param) {
		if(_count == param._boxes.Length) return new Exiting_SmallIntestinState();
		return this;
	}
}
