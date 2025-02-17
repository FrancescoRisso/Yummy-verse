using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class CharacterMovement : MonoBehaviour {
    [SerializeField]
    private float MoveSpeed = 10;

    [SerializeField]
    private float SprintSpeed = 30;

    [SerializeField]
    private AudioClip footstepSound; // Aggiungi questa variabile per il suono dei passi

    protected CharacterController movementController;

    private Vector3 fallVelocity;

    private AudioSource audioSource;  // Variabile per l'AudioSource
    private bool isPlayingFootstep = false;

    private void Start() {
        movementController = GetComponent<CharacterController>();  // Character Controller
        audioSource = GetComponent<AudioSource>();  // Ottieni il componente AudioSource
    }

    private void Update() {
        Vector3 walkDirection = Vector3.zero;
        walkDirection += transform.forward * Input.GetAxisRaw("Vertical");
        walkDirection += transform.right * Input.GetAxisRaw("Horizontal");

        walkDirection.Normalize();

        fallVelocity += -9.81f * Time.deltaTime * transform.up;  

        float currMoveSpeed = Input.GetKey(KeyCode.LeftShift) ? SprintSpeed : MoveSpeed;

        movementController.Move(Time.deltaTime * (currMoveSpeed * walkDirection + fallVelocity));

        if (walkDirection.magnitude > 0 && movementController.isGrounded) {
            if (!isPlayingFootstep) {
                audioSource.clip = footstepSound;  
                audioSource.loop = true;         
                audioSource.Play();
                isPlayingFootstep = true;
            }
        } else {
            if (isPlayingFootstep) {
                audioSource.Stop();
                isPlayingFootstep = false;
            }
        }
    }
}
