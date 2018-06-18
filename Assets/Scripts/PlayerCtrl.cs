﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

	public float horizontalSpeed =10F;

	public float jumpSpeed=600F;
	
	Rigidbody2D rb;

	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		rb=GetComponent<Rigidbody2D>();
		sr=GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalInput=Input.GetAxisRaw("Horizontal"); // -1: esquerda, 1: direita
		float horizontalPlayerSpeed=horizontalSpeed*horizontalInput;
		if (horizontalPlayerSpeed!=0){
			MoveHorizontal(horizontalPlayerSpeed);
		}
		else{
			StopMoving();
		}

		if(Input.GetButtonDown("Jump")){
			Jump();
		}

	}

	void MoveHorizontal(float speed){
		rb.velocity=new Vector2 (speed, rb.velocity.y);

		if (speed<0F){
			sr.flipX=true;
		}
		else {
			sr.flipX=false;
		}
	}

	void StopMoving(){
		rb.velocity=new Vector2(0f,rb.velocity.y);
	}

	void Jump(){
		rb.AddForce(new Vector2(0F,jumpSpeed));
	}

}