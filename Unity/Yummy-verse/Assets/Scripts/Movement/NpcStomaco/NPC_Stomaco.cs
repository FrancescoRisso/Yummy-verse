using UnityEngine;
using System.Collections;

public class NPC_Stomaco : NPCSequenceAndMovement {
    public override IEnumerator AfterExplanationSequence() {
        Debug.Log("NPC_Stomaco: comportamento custom per AfterExplanationSequence");

        // Gestione audio invariata
        if (audioSource != null && audioClips != null && audioClips.Length > 1) {
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }

        // Invece di gestire l'animazione Talking, controlla direttamente se ci sono waypoint
        if (waypoints == null || waypoints.Length == 0) {
            Debug.Log($"{name}: Nessun waypoint assegnato. Passo all'animazione Breathing.");
            animator.SetTrigger("Breathing");
            animator.SetBool("IsWalking", false);
            pathCompleted = true;
            yield break;
        }

        // Se ci sono waypoint, imposta isWalking a true
        animator.SetBool("IsWalking", true);

        // Attende il delay personalizzato per far partire il movimento
        yield return new WaitForSeconds(movementDelay);

        // Dopo il delay, il movimento verso i waypoint parte
        startMovement = true;

        Debug.Log("NPC_Stomaco: Fine AfterExplanationSequence");
    }

    public override void Update() {
        // Richiama la logica base di Update per gestire il movimento e la gravità
        base.Update();

        // Se il percorso è completato, attiva il trigger per "Breathing" (se non è già attivato)
        if (pathCompleted) {
            // Controlla lo stato attuale: se non sei già in "Breathing", attiva il trigger
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("Breathing")) {
                animator.SetTrigger("Breathing");
            }
        }
    }
}
