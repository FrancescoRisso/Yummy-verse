using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FoodCollectionBox : MonoBehaviour {
	[SerializeField]
	private Button _button;


	[SerializeField]
	private PercentageToggleManager _doors;

	[SerializeField]
	private GameObject lucina_rossa;
	[SerializeField]
	private GameObject lucina_verde;
	[SerializeField]
	private float emissionIntensity = 2f;

	void Start() {
		Assert.IsNotNull(_button, $"{name} does not have a button assigned");
		Assert.IsNotNull(_doors, $"{name} does not have the doors assigned");

		SetEmission(lucina_rossa, Color.red, emissionIntensity);
		SetEmission(lucina_verde, Color.green, 0);

		_button.activated += CloseDoors;
	}

	private void CloseDoors() {
		_button.activated -= CloseDoors;
		SetEmission(lucina_rossa, Color.red, 0);  
		SetEmission(lucina_verde, Color.green, emissionIntensity);
		_doors._toggle.Invoke();
	}

	private void SetEmission(GameObject lightObj, Color color, float intensity) {
		if(lightObj == null) return;

		Renderer renderer = lightObj.GetComponent<Renderer>();
		if(renderer == null) return;

		Material mat = renderer.material;
		mat.EnableKeyword("_EMISSION"); 
		mat.SetColor("_EmissionColor", color * intensity);
	}
}
