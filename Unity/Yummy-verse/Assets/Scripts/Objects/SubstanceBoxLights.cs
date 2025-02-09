using System;
using UnityEngine;

public class SubstanceBoxLights : MonoBehaviour {
	private int _inserted_substances = 0;

	public Action InsertSubstance;

	void Start() {
		InsertSubstance += () => { _inserted_substances++; };
	}

	void Update() {}
}
