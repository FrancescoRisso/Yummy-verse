using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class ShooterInteractionManager : InteractionManager {
	private CameraEnabler _player_camera_enabler;
	private CameraEnabler _my_camera_enabler;

	// Riferimento al prefab del Particle System (da assegnare manualmente nell’Inspector)
	[SerializeField]
	private ParticleSystem liquidParticleSystem;

	// Istanza creata del Particle System
	private ParticleSystem currentParticleSystem;

	// Flag per verificare se il Particle System sta "sparando"
	private bool isShooting = false;

	private AcidShoot _shooter;

	[SerializeField]
	private GameObject _corpo_principale;

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
		if(!isShooting) StartShooting();
	}

	// Avvia il Particle System istanziando il prefab come figlio della mesh
	private void StartShooting() {
		Assert.IsNotNull(liquidParticleSystem, $"{name}: il Particle System del liquido non è stato assegnato!");

		if(currentParticleSystem == null) {
			// Istanzio il prefab come figlio del GameObject a cui è attaccato questo script
			currentParticleSystem = Instantiate(liquidParticleSystem, _corpo_principale.transform);
			// Imposto la posizione locale all'offset calcolato dalla mesh
			currentParticleSystem.transform.localPosition = new Vector3(0, 0.061f, 0.31f);
			currentParticleSystem.transform.localEulerAngles = new Vector3(0, 0, 0);
		}
		currentParticleSystem.Play();
		isShooting = true;
		_shooter.enabled = true;
	}

	// Ferma il Particle System e lo distrugge
	private void StopShooting() {
		if(currentParticleSystem != null) {
			// Ferma l'emissione e distruggi l'istanza
			currentParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
			Destroy(currentParticleSystem.gameObject);
			currentParticleSystem = null;
			isShooting = false;
			_shooter.enabled = false;
		}
	}

	protected override void ExtraUpdateAction() {
		if(isShooting && Input.GetMouseButtonUp(0)) StopShooting();
	}

	void Start() {
		// Recupero il riferimento al player e ai relativi CameraEnabler
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Assert.IsNotNull(player, $"{name} cannot find the player");

		_player_camera_enabler = player.GetComponentInChildren<CameraEnabler>();
		Assert.IsNotNull(_player_camera_enabler, $"{name} cannot find the player's camera enabler");

		_my_camera_enabler = GetComponentInChildren<CameraEnabler>();
		Assert.IsNotNull(
			_my_camera_enabler, $"{name} non ha trovato un CameraEnabler nei suoi figli. Assicurati che ci sia un componente CameraEnabler!");

		Assert.IsNotNull(_corpo_principale, $"{name} does not have the \"Corpo principale\" assigned");

		_shooter = GetComponentInChildren<AcidShoot>();
		Assert.IsNotNull(_shooter, $"{name} cannot find the shooter script");
		_shooter.enabled = false;
	}
}
