using System.Collections;
using UnityEngine;

public class AcidSprayable : MonoBehaviour {
    [Header("Impostazioni")]
    [SerializeField] private float destroyDelay = 0.5f; // Ritardo prima della distruzione
    [SerializeField] private float autoGravityVelocityThreshold = 0.1f; // Soglia per abilitare la gravità se l'oggetto si muove

    private bool isHit = false; // Per evitare di processare più volte la collisione
    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        Debug.Log($"{name}: Start - Gravity disabled. rb.useGravity = {rb.useGravity}");
    }

    /// <summary>
    /// Metodo dummy per compatibilità.
    /// </summary>
    public void Push(Vector3 direction) {
        Debug.Log($"{name}: Dummy Push called with direction: {direction}");
    }

    /// <summary>
    /// Metodo chiamato quando il Particle System colpisce l'oggetto.
    /// Abilita la gravità, facendo cadere l'oggetto, e poi avvia la distruzione.
    /// </summary>
    void OnParticleCollision(GameObject other) {
        Debug.Log($"{name}: OnParticleCollision triggered by {other.name}");
        if (!isHit) {
            isHit = true;
            Debug.Log($"{name}: First collision detected. Enabling gravity via collision.");
            
            rb.useGravity = true;
            Debug.Log($"{name}: Gravity enabled. rb.useGravity = {rb.useGravity}");
            
            StartCoroutine(DelayedDestroy(destroyDelay));
        } else {
            Debug.Log($"{name}: OnParticleCollision triggered, but collision already processed (isHit = true).");
        }
    }

    /// <summary>
    /// Controlla in Update se l'oggetto è stato "sparato" e sta muovendo,
    /// abilitando la gravità automaticamente se la velocità supera una soglia.
    /// </summary>
    void Update() {
        // Log continuo per il debug (puoi rimuoverlo in produzione)
        Debug.Log($"{name}: Update - rb.useGravity = {rb.useGravity}, velocity = {rb.velocity}");

        // Se la gravità è ancora disabilitata e l'oggetto si sta muovendo, abilitala
        if (!rb.useGravity && rb.velocity.magnitude > autoGravityVelocityThreshold) {
            rb.useGravity = true;
            Debug.Log($"{name}: Movement detected (velocity = {rb.velocity}). Enabling gravity automatically.");
        }
    }

    /// <summary>
    /// Coroutine che attende il ritardo specificato e poi distrugge l'oggetto.
    /// </summary>
    IEnumerator DelayedDestroy(float delay) {
        Debug.Log($"{name}: DelayedDestroy started. Object will be destroyed in {delay} seconds.");
        yield return new WaitForSeconds(delay);
        Debug.Log($"{name}: Delay elapsed. Destroying object.");
        Destroy(gameObject);
    }
}
