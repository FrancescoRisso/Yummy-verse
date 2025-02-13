using UnityEngine;

public class Attractable : MonoBehaviour {
	private float _speed = 5;

	public void Attract(Vector3 dst) {
		Vector3 dir = dst - transform.position;
		dir.y = 0;
		transform.position += _speed * Time.deltaTime * dir;
	}
}
