﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float movePower = 2f;
	public float jumpPower = 1f;

	Rigidbody2D rigid;
	Animator animator;

	Vector3 movement;
	bool isJumping = false;
	bool onJumping = false;

	void Start() {
		rigid = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponentInChildren<Animator>();
	}

	void Update() {
		if (Input.GetAxisRaw("Horizontal") == 0) {
			animator.SetBool("isMoving", false);
		}
		else if (Input.GetAxisRaw("Horizontal") < 0) {
			animator.SetBool("isMoving", true);
			animator.SetInteger("WalkDirection", -1);
		}
		else if (Input.GetAxisRaw("Horizontal") > 0) {
			animator.SetBool("isMoving", true);
			animator.SetInteger("WalkDirection", 1);
		}

		if (Input.GetButtonDown("Jump") && !onJumping) {
			isJumping = true;
			onJumping = true;
		}
	}

	void FixedUpdate() {
		Move();
		Jump();
	}

	void Move() {
		Vector3 moveVelocity = Vector3.zero;

		if (Input.GetAxisRaw("Horizontal") < 0) {
			moveVelocity = Vector3.left;
		}
		else if (Input.GetAxisRaw("Horizontal") > 0) {
			moveVelocity = Vector3.right;
		}

		transform.position += moveVelocity * movePower * Time.deltaTime;
	}

	void Jump() {
		if (!isJumping) {
			return;
		}

		rigid.velocity = Vector2.zero;

		Vector2 jumpVelocity = new Vector2(0, jumpPower);
		rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

		isJumping = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Ground") {
			onJumping = false;
		}
	}
}
