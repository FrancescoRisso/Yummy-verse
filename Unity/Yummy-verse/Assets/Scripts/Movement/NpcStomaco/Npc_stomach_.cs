using UnityEngine;
using System.Collections;

public class NPC_Stomach_ : NPCSequenceAndMovement {
public override IEnumerator AfterExplanationSequence() {
    Debug.Log("NPC_Stomaco: Avvio AfterExplanationSequence custom");

    // Attiva il trigger per l'animazione "Working" anziché "Talking"
    animator.SetTrigger("Working");

    // Riproduce il secondo audio, se disponibile
    if (audioSource != null && audioClips != null && audioClips.Length > 1) {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    // Attende fino a quando l'animazione "Working" è terminata:
    yield return new WaitUntil(() => {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("Working") && stateInfo.normalizedTime >= 1f;
    });

    // Dopo "Working", controlla se ci sono waypoint
    if (waypoints == null || waypoints.Length == 0) {
        Debug.Log($"{name}: Nessun waypoint assegnato. Resto in animazione Working.");
        animator.SetBool("IsWalking", false);
        pathCompleted = true;  // L'NPC resta fermo
        yield break;           // Fine della coroutine
    }

    // Se ci sono waypoint, imposta il bool IsWalking a true in modo che il movimento parta
    animator.SetBool("IsWalking", true);

    // Attende il delay personalizzato per dare il tempo all'animazione di Working di essere visibile
    yield return new WaitForSeconds(movementDelay);

    // Dopo il delay, il movimento verso i waypoint parte
    startMovement = true;

    Debug.Log("NPC_Stomaco: AfterExplanationSequence completata; il movimento è iniziato");
}
}