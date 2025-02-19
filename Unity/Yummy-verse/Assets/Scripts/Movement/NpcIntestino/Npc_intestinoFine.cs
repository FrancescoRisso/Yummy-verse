using UnityEngine;
using System.Collections;

public class NPC_IntestinoFine : NPCSequenceAndMovement {
    public override IEnumerator AfterExplanationSequence() {
        Debug.Log("NPC_CustomAfterExplanation: Triggering 'Pointing'");

        // Attiva il trigger per l'animazione "Pointing"
        animator.SetTrigger("Indicatore");

        // Attende 2 secondi
        yield return new WaitForSeconds(2f);

        Debug.Log("NPC_CustomAfterExplanation: Triggering 'Breathing'");

        // Attiva il trigger per l'animazione "Breathing"
        animator.SetTrigger("Breathing");

        // Termina la coroutine (non attiva il movimento)
        yield break;
    }
}
