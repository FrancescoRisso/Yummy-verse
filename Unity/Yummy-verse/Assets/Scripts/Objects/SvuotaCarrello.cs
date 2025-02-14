using System;
using UnityEngine;
using UnityEngine.Assertions;

public class SvuotaCarrello : MonoBehaviour {
    [SerializeField]
    public Button _activator;
    
    [SerializeField]
    private Transform piattaformaInclinabile;
    
    [SerializeField]
    private Transform posizioneFinale;
    
    [SerializeField]
    private float durataInclinazione = 0.5f; 
    
    private Vector3 posizioneIniziale;
    private Quaternion rotazioneIniziale;
    private bool inPosizioneFinale = false;
    
    void Start() {
        Assert.IsNotNull(_activator, $"{name} does not have a one shot interactable");
        Assert.IsNotNull(piattaformaInclinabile, $"{name} does not have a valid piattaforma inclinabile");
        Assert.IsNotNull(posizioneFinale, $"{name} does not have a valid final position");
        
        posizioneIniziale = piattaformaInclinabile.position;
        rotazioneIniziale = piattaformaInclinabile.rotation;
        
        _activator.activated += TogglePosizione;
    }

    private void TogglePosizione() {
        if (inPosizioneFinale) {
            StartCoroutine(InterpolazionePiattaforma(posizioneIniziale, rotazioneIniziale));
        } else {
            StartCoroutine(InterpolazionePiattaforma(posizioneFinale.position, posizioneFinale.rotation));
        }
        inPosizioneFinale = !inPosizioneFinale;
    }

    private System.Collections.IEnumerator InterpolazionePiattaforma(Vector3 posizioneTarget, Quaternion rotazioneTarget) {
        Vector3 posizioneStart = piattaformaInclinabile.position;
        Quaternion rotazioneStart = piattaformaInclinabile.rotation;
        
        float tempoTrascorso = 0f;
        while (tempoTrascorso < durataInclinazione) {
            float t = tempoTrascorso / durataInclinazione;
            piattaformaInclinabile.position = Vector3.Lerp(posizioneStart, posizioneTarget, t);
            piattaformaInclinabile.rotation = Quaternion.Slerp(rotazioneStart, rotazioneTarget, t);
            tempoTrascorso += Time.deltaTime;
            yield return null;
        }
        piattaformaInclinabile.position = posizioneTarget;
        piattaformaInclinabile.rotation = rotazioneTarget;
    }
}
