using System;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class PulsantiLuciAscensore : MonoBehaviour {
	private Button _faringe_button;
	private Button _laringe_button;
	private Button _esofago_button;
	private MeshRenderer _faringe_light;
	private MeshRenderer _laringe_light;
	private MeshRenderer _esofago_light;

	private Material _active_light;
	private Material _inactive_light;

	public Action OnCorrectButtonPress;

	void Start() {
		_faringe_button = Children.FindChild(gameObject, "Button (1)").GetComponent<Button>();
		_laringe_button = Children.FindChild(gameObject, "Button").GetComponent<Button>();
		_esofago_button = Children.FindChild(gameObject, "Button (2)").GetComponent<Button>();

		_faringe_light = Children.FindChild(gameObject, "tastoFaringe").GetComponent<MeshRenderer>();
		_laringe_light = Children.FindChild(gameObject, "tastoLaringe").GetComponent<MeshRenderer>();
		_esofago_light = Children.FindChild(gameObject, "tastoEsofago").GetComponent<MeshRenderer>();

		Assert.IsNotNull(_faringe_button, $"{name} cannot find the button for the faringe");
		Assert.IsNotNull(_laringe_button, $"{name} cannot find the button for the laringe");
		Assert.IsNotNull(_esofago_button, $"{name} cannot find the button for the esofago");

		Assert.IsNotNull(_faringe_light, $"{name} cannot find the light for the faringe");
		Assert.IsNotNull(_laringe_light, $"{name} cannot find the light for the laringe");
		Assert.IsNotNull(_esofago_light, $"{name} cannot find the light for the esofago");

		_active_light = _faringe_light.material;
		_inactive_light = _esofago_light.material;

		Assert.IsNotNull(_active_light, $"{name} cannot find the active light material");
		Assert.IsNotNull(_inactive_light, $"{name} cannot find the inactive light material");

		_esofago_button.activated += CorrectChoice;
		_laringe_button.activated += WrongChoice;
	}

	private void CorrectChoice() {
		OnCorrectButtonPress?.Invoke();
		
	}

	private void WrongChoice() {
		// TODO accendere luce "Pericolo cibo traverso
	}

	public void LeavingFaringe() {
		_faringe_light.material = _inactive_light;
	}

	public void ArrivingEsofago() {
		_esofago_light.material = _active_light;
	}
}
