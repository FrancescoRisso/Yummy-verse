using UnityEngine;
using System.Collections;

public enum Movimenti { ascensore2bocca, bocca2ascensore, ascensore2finestra, finestra2sotto }

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class NPCSequenceAndMovement : MonoBehaviour {
	[Header("Tipo di Movimento")]
	[SerializeField]
	public Movimenti Movimento;

	[Header("Animazioni")]
	[SerializeField]
	private float greetingDuration = 2f;
	// Non usiamo talkingDuration e delay fissi; usiamo la sincronizzazione via WaitUntil

	[Header("Movement Delay")]
	[SerializeField]
	private float movementDelay = 0.5f;  // Delay per far partire il movimento dopo l'inizio dell'animazione Walking

	[Header("Waypoints")]
	[SerializeField]
	private Transform[] waypoints;

	[Header("Movement")]
	[SerializeField]
	private float moveSpeed = 3f;
	[SerializeField]
	private float reachThreshold = 0.2f;

	[Header("Rotazione")]
	[SerializeField]
	private float rotationSpeed = 5f;  // Nuova variabile per una rotazione più fluida

	[Header("Gravity")]
	[SerializeField]
	private float gravity = 20f;

	private int currentWaypointIndex = 0;
	private CharacterController controller;
	private Animator animator;
	private Vector3 verticalVelocity = Vector3.zero;
	private bool pathCompleted = false;
	private bool startMovement = false;  // Diventa true dopo il delay post-Walking

	public void InitialExplanation() {
		Debug.Log("Ora partono audio e animazioni relativi alla spiegazione iniziale");
	}

	public void AfterChewingExplanation() {
		Debug.Log("Ora partono audio e animazioni relativi alla spiegazione dopo la masticata");
	}

	void Start() {
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();

		if(waypoints == null || waypoints.Length == 0) {
			Debug.LogWarning("Nessun waypoint assegnato. L'NPC rimarrà fermo.");
			pathCompleted = true;
		}

		// Greeting è lo stato di default, quindi l'animazione Greeting parte automaticamente.
		StartCoroutine(AnimationSequence());
	}

	IEnumerator AnimationSequence() {
		// Attende la durata del saluto (stato default: Greeting)
		yield return new WaitForSeconds(greetingDuration);

		// Avvia l'animazione di Talking
		animator.SetTrigger("Talking");

		// Attende fino a quando l'animazione Talking è terminata:
		yield return new WaitUntil(() => {
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
			return stateInfo.IsName("Talking") && stateInfo.normalizedTime >= 1f;
		});

		// Appena Talking finisce, attiva la transizione a Walking
		animator.SetBool("IsWalking", true);

		// Attende un delay personalizzato: così ti assicuri che l'animazione Walking sia visibile prima che inizi il movimento
		yield return new WaitForSeconds(movementDelay);

		// Dopo il delay, il movimento verso i waypoint parte
		startMovement = true;
	}

	void Update() {
		// Applica sempre la gravità
		ApplyGravity();

		// Se il movimento non è ancora iniziato o il percorso è completato, applica solo la gravità
		if(!startMovement || pathCompleted) {
			controller.Move(verticalVelocity * Time.deltaTime);
			return;
		}

		// Logica di movimento verso il waypoint corrente
		Transform targetWaypoint = waypoints[currentWaypointIndex];
		// Mantieni l'altezza attuale: muoviamo solo in X e Z
		Vector3 targetPos = new Vector3(targetWaypoint.position.x, transform.position.y, targetWaypoint.position.z);
		Vector3 direction = targetPos - transform.position;
		float distance = direction.magnitude;

		if(distance <= reachThreshold) {
			// Se è l'ultimo waypoint, il percorso è terminato
			if(currentWaypointIndex == waypoints.Length - 1) {
				pathCompleted = true;
				animator.SetBool("IsWalking", false);
			} else {
				currentWaypointIndex++;
			}
		} else {
			Vector3 horizontalMovement = direction.normalized * moveSpeed;
			// Ruota gradualmente verso il waypoint usando rotationSpeed per una transizione più fluida
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

			// Combina il movimento orizzontale con la gravità
			Vector3 movement = (horizontalMovement + verticalVelocity) * Time.deltaTime;
			controller.Move(movement);
		}
	}

	/// <summary>
	/// Aggiorna la componente verticale applicando la gravità.
	/// </summary>
	void ApplyGravity() {
		if(controller.isGrounded)
			verticalVelocity.y = 0;
		else
			verticalVelocity.y += -gravity * Time.deltaTime;
	}
}
