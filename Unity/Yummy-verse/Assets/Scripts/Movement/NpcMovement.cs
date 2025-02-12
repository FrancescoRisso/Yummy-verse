using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class NPCPredefinedPathCharacterController : MonoBehaviour
{
    [Header("Impostazioni Waypoint")]
    [Tooltip("Assegna qui i waypoint (in ordine) tramite l'Inspector")]
    [SerializeField] private Transform[] waypoints;

    [Header("Impostazioni Movimento")]
    [SerializeField] private float moveSpeed = 3f;       // Velocità orizzontale
    [SerializeField] private float reachThreshold = 0.2f; // Distanza per considerare il waypoint raggiunto

    [Header("Impostazioni Gravità")]
    [SerializeField] private float gravity = 20f;         // Forza della gravità
    [SerializeField] private float groundStickForce = 0.5f; // Forza verso il basso per "incollare" l'NPC al terreno

    private int currentWaypointIndex = 0;
    private CharacterController controller;
    private Animator animator;
    private Vector3 fallVelocity = Vector3.zero;
    private bool pathCompleted = false;  // Indica se il percorso è completato

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning("Nessun waypoint assegnato. L'NPC resterà fermo.");
            return;
        }
        currentWaypointIndex = 0;
    }

    void Update()
    {
        // Se il percorso è completato, fermiamo il movimento e l'animazione.
        if (pathCompleted)
        {
            // Assicuriamoci che l'animazione passi in idle
            animator.SetBool("IsWalking", false);

            // Gestione della gravità: se l'NPC è a terra non accumula caduta
            if (controller.isGrounded)
            {
                fallVelocity = Vector3.zero;
            }
            else
            {
                fallVelocity += Vector3.down * gravity * Time.deltaTime;
            }
            controller.Move(fallVelocity * Time.deltaTime);
            return;
        }
        else
        {
            // Durante il movimento, l'NPC deve mostrare l'animazione di camminata.
            animator.SetBool("IsWalking", true);
        }

        Vector3 horizontalMovement = Vector3.zero;

        // Se ci sono ancora waypoint da raggiungere...
        if (currentWaypointIndex < waypoints.Length)
        {
            // Calcola il target mantenendo la Y corrente (per evitare spostamenti indesiderati in altezza)
            Vector3 targetPos = new Vector3(waypoints[currentWaypointIndex].position.x,
                                            transform.position.y,
                                            waypoints[currentWaypointIndex].position.z);
            Vector3 direction = targetPos - transform.position;
            float distance = direction.magnitude;

            if (distance <= reachThreshold)
            {
                // Se è l'ultimo waypoint, segnala la fine del percorso
                if (currentWaypointIndex == waypoints.Length - 1)
                {
                    pathCompleted = true;
                    Debug.Log("Waypoint finale raggiunto. NPC si ferma.");
                }
                else
                {
                    // Altrimenti passa al waypoint successivo
                    currentWaypointIndex++;
                }
            }
            else
            {
                // Calcola il movimento orizzontale verso il waypoint
                horizontalMovement = direction.normalized * moveSpeed;

                // Ruota gradualmente l'NPC verso il waypoint
                if (direction.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
                }
            }
        }

        // Gestione della gravità:
        if (controller.isGrounded)
        {
            // Se a terra, applica una leggera spinta verso il basso per mantenerlo "incollato" al terreno
            fallVelocity = Vector3.down * groundStickForce;
        }
        else
        {
            // Se non a terra, accumula la gravità
            fallVelocity += Vector3.down * gravity * Time.deltaTime;
        }

        // Combina il movimento orizzontale e quello verticale e applica il movimento
        Vector3 totalMovement = (horizontalMovement + fallVelocity) * Time.deltaTime;
        controller.Move(totalMovement);
    }
}
