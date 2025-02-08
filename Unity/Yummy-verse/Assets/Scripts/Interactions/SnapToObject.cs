using UnityEngine;
using System.Collections;

/*public class SnapToObject : MonoBehaviour
{
    public Transform fixedObject; // Il riferimento all'oggetto fisso
    public float snapDistance = 1.0f; // La distanza alla quale l'oggetto inizierà a ruotare e posizionarsi
    public float moveSpeed = 5.0f; // La velocità di movimento lungo l'asse Z
    public float rotationSpeed = 5.0f; // La velocità di rotazione

    private bool isSnapping = false;

    void Update()
    {
        float distanceToFixedObject = Vector3.Distance(transform.position, fixedObject.position);

        if (distanceToFixedObject <= snapDistance && !isSnapping)
        {
            StartCoroutine(SnapToFixedObject());
        }
    }

    IEnumerator SnapToFixedObject()
    {
        isSnapping = true;

        // Ruota l'oggetto verso l'oggetto fisso
        Vector3 direction = fixedObject.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        // Muove l'oggetto lungo l'asse Z verso l'oggetto fisso
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, fixedObject.position.z);
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            yield return null;
        }

        isSnapping = false;
    }
} */


public class AutoInsert : MonoBehaviour {
    public Transform fixedSlot;  // Riferimento all'oggetto fisso
    public float rotationSpeed = 5f;  // Velocità di rotazione
    public float moveSpeed = 2f;  // Velocità di avvicinamento
    public float snapThreshold = 0.05f;  // Precisione di allineamento

    private bool isNear = false;
    private bool isInserting = false;
    private Rigidbody _rigidbody;
    private Draggable _draggable;

    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _draggable = GetComponent<Draggable>();
    }

    void Update() {
        if (isNear && !isInserting) {
            AlignAndInsert();
        }
    }

    private void AlignAndInsert() {
        // 1. Ruota lentamente verso l'orientamento del FixedSlot
        transform.rotation = Quaternion.Slerp(transform.rotation, fixedSlot.rotation, Time.deltaTime * rotationSpeed);

        // 2. Sposta l'oggetto verso il FixedSlot
        transform.position = Vector3.Lerp(transform.position, fixedSlot.position, Time.deltaTime * moveSpeed);

        // 3. Controlla se è perfettamente allineato e vicino
        float distance = Vector3.Distance(transform.position, fixedSlot.position);
        float angleDifference = Quaternion.Angle(transform.rotation, fixedSlot.rotation);

        if (distance < snapThreshold && angleDifference < 5f) {
            // 4. Blocca il movimento e rilascia l'oggetto
            isInserting = true;
            _rigidbody.isKinematic = true;
            transform.position = fixedSlot.position;
            transform.rotation = fixedSlot.rotation;

            // Se c'è uno script Draggable, forza il rilascio
            if (_draggable != null) {
                _draggable.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("FixedSlot")) {
            isNear = true;
            fixedSlot = other.transform;  // Salva il riferimento al FixedSlot
        }
    }
}
