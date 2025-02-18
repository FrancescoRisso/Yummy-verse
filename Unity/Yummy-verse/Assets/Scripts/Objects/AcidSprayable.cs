using System.Collections;
using UnityEngine;

public class AcidSprayable : MonoBehaviour {
	[Header("Impostazioni")]
	[SerializeField]
	private float destroyDelay = 0.5f;  // Ritardo prima della distruzione
	[SerializeField]
	private float autoGravityVelocityThreshold = 0.1f;  // Soglia per abilitare la gravità se l'oggetto si muove

	private bool isHit = false;  // Per evitare di processare più volte la collisione
	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
		// rb.useGravity = false;
	}

	/// <summary>
	/// Metodo dummy per compatibilità.
	/// </summary>

	/// <summary>
	/// Metodo chiamato quando il Particle System colpisce l'oggetto.
	/// Abilita la gravità, facendo cadere l'oggetto, e poi avvia la distruzione.
	/// </summary>
	void OnParticleCollision(GameObject other) {
		if(!isHit) {
			isHit = true;

			rb.useGravity = true;

			StartCoroutine(DelayedDestroy(destroyDelay));
		}
	}

	/// <summary>
	/// Controlla in Update se l'oggetto è stato "sparato" e sta muovendo,
	/// abilitando la gravità automaticamente se la velocità supera una soglia.
	/// </summary>
	void Update() {
		// Log continuo per il debug (puoi rimuoverlo in produzione)

		// Se la gravità è ancora disabilitata e l'oggetto si sta muovendo, abilitala
		if(!rb.useGravity && rb.velocity.magnitude > autoGravityVelocityThreshold) { rb.useGravity = true; }
	}

	/// <summary>
	/// Coroutine che attende il ritardo specificato e poi distrugge l'oggetto.
	/// </summary>
	IEnumerator DelayedDestroy(float delay) {
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
	}
}
