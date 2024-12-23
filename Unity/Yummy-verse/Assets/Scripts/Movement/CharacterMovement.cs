// using UnityEngine;

// public class CharacterMovement : MonoBehaviour {
// 	[SerializeField]
// 	private float speed;

// 	// [SerializeField]

// 	void Start() {}
// 	void Update() {}


// 	private Vector3 GetKeyboardMovementVector() {
// 		Vector3 movement = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
// 		return movement.normalized;
// 	}
// }


using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour {
	[SerializeField]
	private float MoveSpeed = 10;

	[SerializeField]
	private float SprintSpeed = 30;

	protected CharacterController movementController;

	private Vector3 velocity;


	private void Start() {
		movementController = GetComponent<CharacterController>();  //  Character Controller
	}

	private void Update() {
		Vector3 prev_pos = transform.position;

		Vector3 direction = Vector3.zero;
		direction += transform.forward * Input.GetAxisRaw("Vertical");
		direction += transform.right * Input.GetAxisRaw("Horizontal");

		direction.Normalize();

		if(movementController.isGrounded)
			velocity = Vector3.zero;
		else
			velocity += -transform.up * (9.81f * 10) * Time.deltaTime;  // Gravity


		float currMoveSpeed = Input.GetKey(KeyCode.LeftShift) ? SprintSpeed : MoveSpeed;

		direction += velocity * Time.deltaTime;
		movementController.Move(Time.deltaTime * currMoveSpeed * direction);
	}
}
