using UnityEngine;
using UnityEngine.Assertions;

public class ShooterInteractionManager : InteractionManager {
    private CameraEnabler _player_camera_enabler;
    private CameraEnabler _my_camera_enabler;

    // Riferimento al prefab del Particle System (da assegnare manualmente nell’Inspector)
    [SerializeField] private ParticleSystem liquidParticleSystem;

    // Istanza creata del Particle System
    private ParticleSystem currentParticleSystem;

    // Flag per verificare se il Particle System sta "sparando"
    private bool isShooting = false;

    // Offset calcolato a partire dalla mesh (non più serializzato)
    private Vector3 particleOffset;

    // Velocità di interpolazione (smooth)
    [SerializeField] private float smoothSpeed = 5f;

    protected override bool ShouldCheckMouseClick() {
        return true;
    }
    protected override bool ShouldCheckEkey() {
        return true;
    }
    protected override bool MouseClickWithRaycast() {
        return false;
    }
    protected override bool EkeyWithRaycast() {
        return false;
    }
    protected override float MouseClickRaycastRange() {
        throw new System.NotImplementedException();
    }
    protected override float EkeyRaycastRange() {
        throw new System.NotImplementedException();
    }
    protected override void EkeyAction(EkeyInteractable target) {
        _player_camera_enabler.Enable();
        _my_camera_enabler.Disable();
        this.enabled = false;
    }

    // Metodo invocato al click del mouse.
    protected override void MouseClickAction(MouseInteractable target) {
        Debug.Log("MouseClickAction invocato.");
        if (!isShooting) {
            StartShooting();
        }
    }

    // Avvia il Particle System istanziando il prefab come figlio della mesh
    private void StartShooting() {
        if (liquidParticleSystem != null) {
            if (currentParticleSystem == null) {
                // Istanzio il prefab come figlio del GameObject a cui è attaccato questo script
                currentParticleSystem = Instantiate(liquidParticleSystem, transform);
                // Imposto la posizione locale all'offset calcolato dalla mesh
                currentParticleSystem.transform.localPosition = particleOffset;
            }
            currentParticleSystem.Play();
            isShooting = true;
            Debug.Log("Particle system avviato.");
        } else {
            Debug.LogWarning($"{name}: il Particle System del liquido non è stato assegnato!");
        }
    }

    // Ferma il Particle System e lo distrugge
    private void StopShooting() {
        if (currentParticleSystem != null) {
            // Ferma l'emissione e distruggi l'istanza
            currentParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            Destroy(currentParticleSystem.gameObject);
            currentParticleSystem = null;
            isShooting = false;
            Debug.Log("Particle system fermato e distrutto.");
        }
    }

    void Update() {
        // Gestione dei comandi del mouse
        if (Input.GetMouseButtonDown(0)) {
            if (!isShooting) {
                StartShooting();
            }
        }
        if (isShooting && Input.GetMouseButtonUp(0)) {
            StopShooting();
        }

        // Se il Particle System è attivo, aggiorna la sua posizione locale in modo interpolato verso particleOffset
        if (isShooting && currentParticleSystem != null) {
            currentParticleSystem.transform.localPosition = Vector3.Lerp(
                currentParticleSystem.transform.localPosition,
                particleOffset,
                smoothSpeed * Time.deltaTime
            );
            currentParticleSystem.transform.localRotation = Quaternion.Lerp(
                currentParticleSystem.transform.localRotation,
                Quaternion.identity,
                smoothSpeed * Time.deltaTime
            );
        }
    }

    void Start() {
        // Recupero il riferimento al player e ai relativi CameraEnabler
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Assert.IsNotNull(player, $"{name} cannot find the player");

        _player_camera_enabler = player.GetComponentInChildren<CameraEnabler>();
        Assert.IsNotNull(_player_camera_enabler, $"{name} cannot find the player's camera enabler");

        _my_camera_enabler = GetComponentInChildren<CameraEnabler>();
        if (_my_camera_enabler == null) {
            Debug.LogWarning($"{name} non ha trovato un CameraEnabler nei suoi figli. Assicurati che ci sia un componente CameraEnabler!");
        }

        // Calcolo l'offset a partire dalle proprietà della mesh (il centro dei bounds)
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null) {
            // Il centro in coordinate mondiali della mesh
            Vector3 worldCenter = meshRenderer.bounds.center;
            // Converto in coordinate locali rispetto a questo GameObject
            particleOffset = transform.InverseTransformPoint(worldCenter);
            Debug.Log($"{name}: particleOffset calcolato a partire dalla mesh: {particleOffset}");
        } else {
            Debug.LogWarning($"{name} non ha trovato un MeshRenderer. Imposto particleOffset a zero.");
            particleOffset = Vector3.zero;
        }
    }
}
