using UnityEngine;
using System.Collections;

public class NPC_IntestinoFine : NPC {
	// public override IEnumerator AfterExplanationSequence() {
	//     Debug.Log("NPC_CustomAfterExplanation: Triggering 'Pointing'");

	//     // Attiva il trigger per l'animazione "Pointing"
	//     animator.SetTrigger("Indicatore");

	//     // Attende 2 secondi
	//     yield return new WaitForSeconds(2f);

	//     Debug.Log("NPC_CustomAfterExplanation: Triggering 'Breathing'");

	//     // Attiva il trigger per l'animazione "Breathing"
	//     animator.SetTrigger("Breathing");

	//     // Termina la coroutine (non attiva il movimento)
	//     yield break;
	// }

	protected override AudioClip GetNthAudioClip(int n) {
		throw new System.NotImplementedException();
	}

	protected override void RunNthAnimation(int n)
	{
		throw new System.NotImplementedException();
	}
}
