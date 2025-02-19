using UnityEngine;
using UnityEngine.Assertions;

public class Shooting_StomachState : StomachState {
	private bool _chain_pulled = false;

	public override void PrepareBeforeAction(StomachParameter param) {
		// param._NPC.ExecMovementWithCallback(
		// 	Movimenti.ascensore2finestra, () => { param._cart.GetComponent<SvuotaCarrello>()._activator.activated.Invoke(); });
		param._audio.Play();

		param._all_destroyed.AllItemsDestroyed += () => {
			param._chain.AddComponent<Chain>();

			PercentageToggleManager chain_perc = param._chain.GetComponent<PercentageToggleManager>();
			Assert.IsNotNull(chain_perc, $"{param._monoBehaviour.name} cannot find the percentage toggle manager of the chain");

			chain_perc.OnPercentageChange += (float perc) => {
				if(perc == 1) _chain_pulled = true;
			};
		};
	}

	public override void StateAction(StomachParameter param) {}

	public override StomachState Transition(StomachParameter param) {
		if(!_chain_pulled) return this;
		return new Emptying_StomachState();
	}
}
