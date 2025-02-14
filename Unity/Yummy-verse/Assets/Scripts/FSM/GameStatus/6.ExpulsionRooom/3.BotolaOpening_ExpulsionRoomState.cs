using UnityEngine;
using UnityEngine.Assertions;

public class BotolaOpening_ExpulsionRoomState : ExpulsionRoomState {
	private bool _open_botola = false;

	public override void PrepareBeforeAction(ExpulsionRoomParam param) {
		MovementInteractionEnabler mov = param._player.GetComponent<MovementInteractionEnabler>();
		Assert.IsNotNull(mov, $"{param._monoBehaviour.name} cannot find the player's movement script");
		mov.Disable();

		param._player.AddComponent<Attractable>();

		param._botola_state.OnPercentageChange += (float perc) => {
			if(perc == 1) _open_botola = true;
		};

		Attractor a = param._attractor.GetComponent<Attractor>();
		Assert.IsNotNull(a, $"{param._monoBehaviour.name} cannot find the attractor script");
		a.enabled = true;
	}

	public override void StateAction(ExpulsionRoomParam param) {
		Transform t = param._player.GetComponentInChildren<Camera>().transform;
		Vector3 newDirection = Vector3.RotateTowards(t.forward, new Vector3(0, -1, 0), 3 * Time.deltaTime, 0.0f);
		param._player.GetComponentInChildren<Camera>().transform.rotation = Quaternion.LookRotation(newDirection);
	}

	public override ExpulsionRoomState Transition(ExpulsionRoomParam param) {
		if(_open_botola) return new Falling_ExpulsionRoomState();
		return this;
	}
}
