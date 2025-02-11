using System;
using UnityEngine;

public class SubstanceBoxLights : MonoBehaviour {
	private int _inserted_substances = 0;

	public Action InsertSubstance;

	[SerializeField] private GameObject lucina_rossa;
    [SerializeField] private GameObject lucina_verde;
	[SerializeField] private float emissionIntensity = 2f;

	void Start() {
		InsertSubstance += () => {
			_inserted_substances++; 
			UpdateLights();
		};

		UpdateLights(); 
	}

	void UpdateLights() {
        if (_inserted_substances < 4) {
            SetEmission(lucina_rossa, Color.red, emissionIntensity);
            SetEmission(lucina_verde, Color.green, 0);  // Spegne la luce verde
        } else {
            SetEmission(lucina_rossa, Color.red, 0);  // Spegne la luce rossa
            SetEmission(lucina_verde, Color.green, emissionIntensity);
        }
    }

	private void SetEmission(GameObject lightObj, Color color, float intensity) {
        if (lightObj == null) return;

        Renderer renderer = lightObj.GetComponent<Renderer>();
        if (renderer == null) return;

        Material mat = renderer.material;
        mat.EnableKeyword("_EMISSION"); // Attiva l'emissione se non lo è già
        mat.SetColor("_EmissionColor", color * intensity);
    }

	void Update() {}

	public Transform GetTransform() {
		return transform;
	}
}
