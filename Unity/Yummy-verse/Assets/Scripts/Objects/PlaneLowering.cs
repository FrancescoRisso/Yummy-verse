using System;
using UnityEngine;

public class PlaneLowering : MonoBehaviour {
	private float _start_h;

	[SerializeField]
	private float _final_h = 0;

	[SerializeField]
	private float _emptying_time = 1;

	public Action StartEmptying;

	public Action OnEmptied;

	private bool _emptying = false;

	private float _emptying_speed;

	void Start() {
		_start_h = transform.position.y;
		_emptying_speed = (_final_h - _start_h) / _emptying_time;

		OnEmptied += () => Destroy(gameObject);
		StartEmptying += () => _emptying = true;
	}

	void Update() {
		if(!_emptying) return;

		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;

		y += _emptying_speed * Time.deltaTime;
		y = Math.Clamp(y, Math.Min(_start_h, _final_h), Math.Max(_start_h, _final_h));

		transform.position = new Vector3(x, y, z);

		if(y == _final_h) OnEmptied?.Invoke();
	}
}
