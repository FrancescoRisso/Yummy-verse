using UnityEngine;

public enum Movimenti { ascensore2bocca, bocca2ascensore, ascensore2finestra, finestra2sotto }

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class NPCPredefinedPathCharacterController : MonoBehaviour {
	[SerializeField]
	public Movimenti Movimento;

	[Header("Waypoints")]
	[SerializeField]
	private Transform[] waypoints;

	[Header("Movement")]
	[SerializeField]
	private float moveSpeed = 3f;
	[SerializeField]
	private float reachThreshold = 0.2f;

	[Header("Gravity")]
	[SerializeField]
	private float gravity = 20f;

	[Header("Idle Settings")]
	[SerializeField]
	private float idleHeight = 2f;
	[SerializeField]
	private float idlePivotY = 1f;  // Valore Y del centro in idle

	[Header("Walking Settings")]
	[SerializeField]
	private float walkingHeight = 1.8f;
	[SerializeField]
	private float walkingPivotY = 0.9f;  // Valore Y del centro in walking

	[Header("Transition")]
	[SerializeField]
	private float transitionSpeed = 5f;  // Velocità di transizione tra gli stati

	private int currentWaypointIndex = 0;
	private CharacterController controller;
	private Animator animator;
	private Vector3 verticalVelocity = Vector3.zero;
	private bool pathCompleted = false;
	private bool isWalking = false;

	void Start() {
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();

		animator.SetBool("IsWalking", false);

		if(waypoints == null || waypoints.Length == 0) {
			Debug.LogWarning("No waypoints assigned. NPC will remain idle.");
			pathCompleted = true;
		}

		// Imposta inizialmente le impostazioni idle.
		controller.height = idleHeight;
		Vector3 center = controller.center;
		center.y = idlePivotY;
		controller.center = center;
	}

	void Update() {
		Vector3 horizontalMovement = Vector3.zero;

		if(pathCompleted) {
			animator.SetBool("IsWalking", false);

			// Gestione della gravità in idle.
			if(controller.isGrounded)
				verticalVelocity.y = 0;
			else
				verticalVelocity.y += -gravity * Time.deltaTime;

			controller.Move(verticalVelocity * Time.deltaTime);
			// Transizione graduale verso le impostazioni idle.
			SmoothTransitionSettings(false);
			this.enabled = false;
			return;
		}

		// Calcola il movimento orizzontale (solo X e Z).
		Transform targetWaypoint = waypoints[currentWaypointIndex];
		Vector3 targetPos = new Vector3(targetWaypoint.position.x, transform.position.y, targetWaypoint.position.z);
		Vector3 direction = targetPos - transform.position;
		float distance = direction.magnitude;

		if(distance <= reachThreshold) {
			if(currentWaypointIndex == waypoints.Length - 1) {
				pathCompleted = true;
				animator.SetBool("IsWalking", false);
				SmoothTransitionSettings(false);
			} else {
				currentWaypointIndex++;
			}
		} else {
			horizontalMovement = direction.normalized * moveSpeed;
			// Ruota gradualmente verso il target.
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
		}

		isWalking = horizontalMovement.magnitude > 0.01f;
		animator.SetBool("IsWalking", isWalking);

		// Transizione graduale: se cammina, passa alle impostazioni walking, altrimenti idle.
		SmoothTransitionSettings(isWalking);

		// Gestione della gravità.
		if(!controller.isGrounded)
			verticalVelocity.y += -gravity * Time.deltaTime;
		else
			verticalVelocity.y = 0;

		Vector3 movement = (horizontalMovement + verticalVelocity) * Time.deltaTime;
		controller.Move(movement);
	}

	/// <summary>
	/// Aggiorna in maniera fluida l'altezza e il pivot del CharacterController in base allo stato (walking o idle).
	/// </summary>
	/// <param name="walking">Se true applica le impostazioni walking, altrimenti quelle idle.</param>
	void SmoothTransitionSettings(bool walking) {
		float targetHeight = walking ? walkingHeight : idleHeight;
		float targetPivotY = walking ? walkingPivotY : idlePivotY;

		// Interpola l'altezza.
		controller.height = Mathf.Lerp(controller.height, targetHeight, transitionSpeed * Time.deltaTime);

		// Interpola il centro (pivot) in Y.
		Vector3 center = controller.center;
		center.y = Mathf.Lerp(center.y, targetPivotY, transitionSpeed * Time.deltaTime);
		controller.center = center;
	}
}
