using UnityEngine;
using UnityEngine.Assertions;

public enum Direction { Vertical, Right, LocalUp }

public class MoveObject : MonoBehaviour {
	[SerializeField]
	private bool _destroy = true;

	[SerializeField]
	private float _delta = 0;

	[SerializeField]
	private PercentageToggleManager _perc;

	private Vector3 _initial_pos;

	[SerializeField]
	private Direction _movement_direction;

	void Start() {
		Assert.IsNotNull(_perc, $"{name} does not have a percentage manager assigned");
		_initial_pos = transform.position;
		if(_perc._speed == 0) _perc._speed = 1;

		_perc.OnPercentageChange += OnPercChange;
	}

	private void OnPercChange(float perc) {
		Vector3 dir;

		switch(_movement_direction) {
			case Direction.Vertical: dir = new Vector3(0, 1, 0); break;
			case Direction.Right: dir = transform.right; break;
			case Direction.LocalUp: dir = transform.up; break;
			default: dir = new Vector3(0, 0, 0); break;
		}

		transform.position = _initial_pos + perc * _delta * dir;

		if(perc == 1 && _destroy) Destroy(gameObject);
	}

	public void SetDelta(float delta) {
		_delta = delta;
	}

	public void SetDirection(Direction dir) {
		_movement_direction = dir;
	}

	public void Trigger() {
		_perc._toggle.Invoke();
	}
}
