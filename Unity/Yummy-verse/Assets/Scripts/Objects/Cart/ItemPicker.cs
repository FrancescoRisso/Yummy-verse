using UnityEngine;
using UnityEngine.Assertions;

public class ItemPicker : MonoBehaviour {
	[SerializeField]
	private Transform _platform;

	[SerializeField]
	private float _delta_h;

	void Start() {
		Assert.IsNotNull(_platform, $"{name} does not have a reference to its platform");
	}

	void OnTriggerEnter(Collider other) {
		if(other.GetComponent<DraggableOnCart>() != null) other.transform.position = _platform.position + _delta_h * _platform.up;
	}
}
