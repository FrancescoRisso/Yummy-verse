using System;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class cibiMasticati : MonoBehaviour {
	[SerializeField]
	private PercentageCycleNotifier _perc_notifier;

	[SerializeField]
	private GameObject step;

	private bool prefabInstanziato = false;

	[SerializeField]
	private SceneReference _current_scene;


	void Start() {
		Assert.IsNotNull(_perc_notifier, $"{name} does not have a percentage cycle notifier");
		Assert.IsNotNull(step, $"{name} does not have a step prefab assigned");
		Assert.IsNotNull(_current_scene, $"{name} does not have a reference to the current scene assigned");

		_perc_notifier.OnNewIteration += masticazione;
	}

	public void SetNotifier(PercentageCycleNotifier notifier) {
		_perc_notifier = notifier;
		_perc_notifier.OnNewIteration += masticazione;
	}


	private void masticazione(int numMasticazioni) {
		if(_perc_notifier != null) { _perc_notifier.OnNewIteration -= masticazione; }

		if(!prefabInstanziato) {
			prefabInstanziato = true;

        	GameObject nuovoStep = Instantiate(step, transform.position, Quaternion.identity);
			nuovoStep.transform.SetParent(null);
			SceneLoader.MoveObjToScene(nuovoStep, _current_scene);

			cibiMasticati nuovoScript = nuovoStep.GetComponent<cibiMasticati>();

			if(nuovoScript != null) { nuovoScript.SetNotifier(_perc_notifier); }

			Destroy(gameObject);
		}
	}
}
