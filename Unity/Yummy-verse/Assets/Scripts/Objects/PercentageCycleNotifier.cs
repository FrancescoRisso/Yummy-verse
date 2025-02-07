using System;
using UnityEngine;
using UnityEngine.Assertions;

public class PercentageCycleNotifier : MonoBehaviour {
	[SerializeField]
	private PercentageDecayManager _perc_decay;

	public Action<int> OnNewIteration;

	private int _count = 0;

	private bool _has_reached_1 = false;

	void Start() {
		Assert.IsNotNull(_perc_decay, $"{name} does not have a percentage decay manager");
		
		_perc_decay.OnPercentageChange += PercentageUpdate;
	}

	private void PercentageUpdate(float perc) {
		if(_has_reached_1 && perc == 0) {
			_has_reached_1 = false;
			OnNewIteration?.Invoke(++_count);
		}
		if(perc == 1) _has_reached_1 = true;
	}
}
