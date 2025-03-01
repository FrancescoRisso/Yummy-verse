using UnityEngine;

public class Chewing_MouthState : MouthState {
	private bool _all_chewings_done = false;
	private int _num_chewings;

	public override void PrepareBeforeAction(MouthParameter param) {
		param._tmp_camera.GetComponent<Camera>().enabled = false;
		param._tmp_camera.GetComponent<AudioListener>().enabled = false;
		SceneLoader.SetActiveScene(param._game_scene);
		param._player.Enable();

		param._NPC.NextAnimation();

		_num_chewings = param._num_chewings;
		param._chewings_counter.OnNewIteration += (int num) => {
			if(num == _num_chewings) {
				param._mono_behaivour.StartCoroutine(SceneLoader.LoadScene(param._next_scene));
				_all_chewings_done = true;
			}
		};
	}

	public override void StateAction(MouthParameter param) {}

	public override MouthState Transition(MouthParameter param) {
		if(_all_chewings_done) return new LiftOpening_MouthState();
		return this;
	}
}
