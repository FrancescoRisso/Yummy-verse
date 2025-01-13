using UnityEngine;

public class PlayerMouseInteract : MonoBehaviour {
	[SerializeField]
	private float _interact_distance = 10.0f;

	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			Ray ray = new(transform.position, transform.forward);
			if(Physics.Raycast(ray, out RaycastHit hit, _interact_distance)) {
				MouseInteractable target = hit.collider.GetComponent<MouseInteractable>();
				if(target != null) target.StartProcessing();
			}
		}
	}
}
