using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FoodCollectionBox : MonoBehaviour {
	[SerializeField]
	private Button _button;


	[SerializeField]
	private PercentageToggleManager _doors;

	void Start() {
		Assert.IsNotNull(_button, $"{name} does not have a button assigned");
		Assert.IsNotNull(_doors, $"{name} does not have the doors assigned");

		_button.activated += CloseDoors;
	}

	private void CloseDoors() {
		_button.activated -= CloseDoors;
		_doors._toggle.Invoke();
	}
}
