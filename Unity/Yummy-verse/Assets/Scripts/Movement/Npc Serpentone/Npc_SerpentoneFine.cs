using UnityEngine;
using System.Collections;

public class NPC_CustomAfterExplanationYelling : NPCSequenceAndMovement {
    public override IEnumerator AfterExplanationSequence() {
        Debug.Log("NPC_CustomAfterExplanationYelling: Triggering 'Yelling'");

        // Attiva il trigger per l'animazione "Yelling"
        animator.SetTrigger("Yelling");

        // Attende 2 secondi
        yield return new WaitForSeconds(2f);

        Debug.Log("NPC_CustomAfterExplanationYelling: Triggering 'Breathing'");

        // Attiva il trigger per l'animazione "Breathing"
        animator.SetTrigger("Breathing");

        // Termina la coroutine (non attiva il movimento)
        yield break;
    }
}
