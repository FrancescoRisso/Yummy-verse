using UnityEngine;
using UnityEngine.Assertions;

public class Chain : EkeyInteractable {
	private PlaneLowering _lowering;

	public override void StartProcessing() {
		_lowering.StartEmptying.Invoke();
	}

	void Start() {
		_lowering = GetComponent<PlaneLowering>();
		Assert.IsNotNull(_lowering, $"{name} does not have the lowering mechanism");
	}
}
