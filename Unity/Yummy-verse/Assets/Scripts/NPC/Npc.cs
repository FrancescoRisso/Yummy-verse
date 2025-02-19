using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using System;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public abstract class NPC : MonoBehaviour {
	[Header("Audio")]
	[SerializeField]
	private AudioSource audioSource;

	protected abstract AudioClip GetNthAudioClip(int n);
	protected abstract void RunNthAnimation(int n);
	protected virtual string NeutralPosition() {
		return "Breathing";
	}
	protected virtual int NumInteractions() {
		return 0;
	}

	private int _interaction_num = 0;

	protected Animator _animator;

	protected GameObject _player;

	void Start() {
		if(NumInteractions() != 0) Assert.IsNotNull(audioSource, $"{name} does not have an audio source assigned");
		for(int i = 0; i < NumInteractions(); i++) Assert.IsNotNull(GetNthAudioClip(i), $"{name} does not have the {i}-th audio clip assigned");

		_animator = GetComponent<Animator>();
		Assert.IsNotNull(_animator, $"{name} does not have an animator assigned");

		_player = GameObject.FindGameObjectWithTag("Player");
		Assert.IsNotNull(_player, $"{name} cannot find the player");

		SetNeutralPosition();
	}

	public void NextAnimation() {
		AudioClip next_audio = GetNthAudioClip(_interaction_num);
		if(next_audio != null) {
			audioSource.clip = next_audio;
			audioSource.Play();
			RunNthAnimation(_interaction_num);
		}

		_interaction_num++;
	}

	protected bool IsSpeaking() {
		return audioSource.isPlaying;
	}

	protected void SetNeutralPosition() {
		_animator.SetTrigger(NeutralPosition());
	}

	// OLD STUFF

	// public virtual void InitialExplanation() {
	// 	Debug.Log("Ora partono audio e animazioni relativi alla spiegazione iniziale");
	// 	// Attiva il trigger per l'animazione Greetings
	// 	animator.SetTrigger("Greetings");

	// 	// Riproduce il primo audio, se disponibile
	// 	if(audioSource != null && audioClips != null && audioClips.Length > 0) {
	// 		audioSource.clip = audioClips[0];
	// 		audioSource.Play();
	// 	}
	// }

	// /// <summary>
	// /// Avvia la seconda parte della spiegazione: attiva l'animazione Talking, riproduce il secondo audio e poi, al termine,
	// /// passa a Walking e avvia il movimento verso i waypoint.
	// /// </summary>
	// public virtual void AfterExplanation() {
	// 	Debug.Log("Ora partono audio e animazioni relativi alla spiegazione dopo la masticata");
	// 	StartCoroutine(AfterExplanationSequence());
	// }

	// public virtual IEnumerator AfterExplanationSequence() {
	// 	// Avvia l'animazione di Talking
	// 	animator.SetTrigger("Talking");

	// 	// Riproduce il secondo audio, se disponibile
	// 	if(audioClips != null && audioClips.Length > 1) {
	// 		audioSource.clip = audioClips[1];
	// 		audioSource.Play();
	// 	}

	// 	// Attende fino a quando l'animazione Talking è terminata:
	// 	yield return new WaitUntil(() => {
	// 		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
	// 		return stateInfo.IsName("Talking") && stateInfo.normalizedTime >= 1f;
	// 	});

	// 	// Dopo Talking, controlla se ci sono waypoint
	// 	if(waypoints == null || waypoints.Length == 0) {
	// 		Debug.Log($"{name}: Nessun waypoint assegnato. Passo all'animazione Breathing.");
	// 		animator.SetTrigger("Breathing");
	// 		animator.SetBool("IsWalking", false);
	// 		pathCompleted = true;  // Resta fermo
	// 		yield break;           // Fine della coroutine
	// 	}

	// 	// Altrimenti, se ci sono waypoint → passa allo stato Walking
	// 	animator.SetBool("IsWalking", true);

	// 	// Attende il delay personalizzato per far partire il movimento
	// 	yield return new WaitForSeconds(movementDelay);

	// 	// Dopo il delay, il movimento verso i waypoint parte
	// 	startMovement = true;
	// }

	// void OldStart() {
	// 	controller = GetComponent<CharacterController>();
	// 	animator = GetComponent<Animator>();

	// 	if(waypoints == null || waypoints.Length == 0) {
	// 		Debug.LogWarning("Nessun waypoint assegnato. L'NPC rimarrà fermo.");
	// 		pathCompleted = true;
	// 	}

	// 	// Nota: In questo esempio, l'avvio della spiegazione iniziale (InitialExplanation) e
	// 	// della seconda parte (AfterChewingExplanation) deve essere gestito esternamente, ad esempio
	// 	// tramite input o eventi, per controllare il flusso delle animazioni e degli audio.
	// }

	// IEnumerator AnimationSequence() {
	// 	// Attende la durata del saluto (durante la fase di Greetings)
	// 	yield return new WaitForSeconds(greetingDuration);

	// 	// Avvia l'animazione di Talking
	// 	animator.SetTrigger("Talking");

	// 	// Attende fino a quando l'animazione Talking è terminata:
	// 	yield return new WaitUntil(() => {
	// 		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
	// 		return stateInfo.IsName("Talking") && stateInfo.normalizedTime >= 1f;
	// 	});

	// 	// Dopo Talking, controlla se ci sono waypoint
	// 	if(waypoints == null || waypoints.Length == 0) {
	// 		Debug.Log($"{name}: Nessun waypoint assegnato. Passo all'animazione Breathing.");
	// 		animator.SetTrigger("Breathing");
	// 		animator.SetBool("IsWalking", false);
	// 		pathCompleted = true;  // Resta fermo
	// 		yield break;           // Fine della coroutine
	// 	}

	// 	// Altrimenti, se ci sono waypoint → passa allo stato Walking
	// 	animator.SetBool("IsWalking", true);

	// 	// Attende un delay personalizzato: assicura che l'animazione Walking sia visibile prima che inizi il movimento
	// 	yield return new WaitForSeconds(movementDelay);

	// 	// Dopo il delay, il movimento verso i waypoint parte
	// 	startMovement = true;
	// }

	// public virtual void Update() {
	// 	// Applica sempre la gravità
	// 	ApplyGravity();

	// 	// Se il movimento non è ancora iniziato o il percorso è completato, applica solo la gravità
	// 	if(!startMovement || pathCompleted) {
	// 		controller.Move(verticalVelocity * Time.deltaTime);
	// 		return;
	// 	}

	// 	// Logica di movimento verso il waypoint corrente
	// 	Transform targetWaypoint = waypoints[currentWaypointIndex];
	// 	// Mantieni l'altezza attuale: muoviamo solo in X e Z
	// 	Vector3 targetPos = new Vector3(targetWaypoint.position.x, transform.position.y, targetWaypoint.position.z);
	// 	Vector3 direction = targetPos - transform.position;
	// 	float distance = direction.magnitude;

	// 	if(distance <= reachThreshold) {
	// 		// Se è l'ultimo waypoint, il percorso è terminato
	// 		if(currentWaypointIndex == waypoints.Length - 1) {
	// 			pathCompleted = true;
	// 			animator.SetBool("IsWalking", false);
	// 		} else {
	// 			currentWaypointIndex++;
	// 		}
	// 	} else {
	// 		Vector3 horizontalMovement = direction.normalized * moveSpeed;
	// 		// Ruota gradualmente verso il waypoint utilizzando rotationSpeed per una transizione più fluida
	// 		Quaternion targetRotation = Quaternion.LookRotation(direction);
	// 		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

	// 		// Combina il movimento orizzontale con la gravità
	// 		Vector3 movement = (horizontalMovement + verticalVelocity) * Time.deltaTime;
	// 		controller.Move(movement);
	// 	}
	// }

	// /// <summary>
	// /// Aggiorna la componente verticale applicando la gravità.
	// /// </summary>
	// void ApplyGravity() {
	// 	if(controller.isGrounded)
	// 		verticalVelocity.y = 0;
	// 	else
	// 		verticalVelocity.y += -gravity * Time.deltaTime;
	// }
}
