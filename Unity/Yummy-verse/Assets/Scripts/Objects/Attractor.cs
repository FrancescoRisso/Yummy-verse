using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attractor : MonoBehaviour {
	// Start is called before the first frame update
	void Start() {}

	// Update is called once per frame
	void Update() {
		foreach(Attractable attractable in FindObjectsOfType<Attractable>()) { attractable.Attract(transform.position); }
	}
}
