
using UnityEngine;

public class AntaPortaAscensore : MonoBehaviour {
	[SerializeField]
	private PercentageToggleManager _perc;

	void Start() {
		_perc.OnPercentageChange += UpdateDoorSize;
	}

	void UpdateDoorSize(float _percentage) {
		transform.localScale = new Vector3(_percentage, transform.localScale.y, transform.localScale.z);
	}
}
