using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	[SerializeField]
	private GameObject[] _objs;
	[SerializeField]
	private int _tot;
	private int _cnt = 0;

	void Update() {
		if(_cnt < _tot) Instantiate(_objs[Random.Range(0, _objs.Length)], transform.position, Quaternion.identity, transform.parent);
		_cnt++;
	}
}
