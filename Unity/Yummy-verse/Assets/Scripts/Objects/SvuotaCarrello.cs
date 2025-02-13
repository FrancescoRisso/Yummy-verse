using System;
using UnityEngine;
using UnityEngine.Assertions;

public class SvuotaCarrello : MonoBehaviour {
    [SerializeField]
    private OneShotInteractable _activator;
    
    [SerializeField]
    private Transform piattaformaInclinabile;
    
    [SerializeField]
    private float angoloInclinazione = 30f; 
    
    [SerializeField]
    private float durataInclinazione = 0.5f;
    
    void Start() {
        Assert.IsNotNull(_activator, $"{name} does not have a one shot interactable");
        Assert.IsNotNull(piattaformaInclinabile, $"{name} does not have a valid piattaforma inclinabile");
        _activator.activated += svuotaCarrello;
    }

    private void svuotaCarrello() {
        Vector3 puntoRotazione = piattaformaInclinabile.position + (piattaformaInclinabile.right * (piattaformaInclinabile.localScale.x / 2));
        StartCoroutine(InclinaPiattaforma(puntoRotazione));
    }

    private System.Collections.IEnumerator InclinaPiattaforma(Vector3 puntoRotazione) {
        Quaternion rotazioneIniziale = piattaformaInclinabile.rotation;
        Quaternion rotazioneFinale = Quaternion.Euler(piattaformaInclinabile.eulerAngles.x, piattaformaInclinabile.eulerAngles.y, piattaformaInclinabile.eulerAngles.z + angoloInclinazione);
        
        float tempoTrascorso = 0f;
        while (tempoTrascorso < durataInclinazione) {
            float t = tempoTrascorso / durataInclinazione;
            piattaformaInclinabile.RotateAround(puntoRotazione, Vector3.forward, (angoloInclinazione / durataInclinazione) * Time.deltaTime);
            tempoTrascorso += Time.deltaTime;
            yield return null;
        }
        piattaformaInclinabile.rotation = rotazioneFinale;
    }
}
