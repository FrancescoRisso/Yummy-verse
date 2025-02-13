using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]
public class AcidSprayable : MonoBehaviour {
	[SerializeField]
	private float _disappear_height = -0.928f;

	private Rigidbody _my_rigid_body;

	void Start() {
		_my_rigid_body = GetComponent<Rigidbody>();
		Assert.IsNotNull(_my_rigid_body, $"{name} cannot find its rigid body");
	}

	public void Push(Vector3 direction) {
		_my_rigid_body.AddForce(direction.normalized, ForceMode.Impulse);
	}

	void Update() {
		if(transform.position.y < _disappear_height) Destroy(gameObject);
	}
}
