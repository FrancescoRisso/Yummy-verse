using UnityEngine;
using UnityEngine.Assertions;

public class Chain : EkeyInteractable {
	private PercentageToggleManager _lowering;

	public override void StartProcessing() {
		_lowering._toggle.Invoke();
	}

	void Start() {
		_lowering = GetComponent<PercentageToggleManager>();
		Assert.IsNotNull(_lowering, $"{name} does not have the lowering mechanism");
	}
}
