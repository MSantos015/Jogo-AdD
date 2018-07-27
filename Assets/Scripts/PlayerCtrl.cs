using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	/* 
	Idle- 0
	Jump- 1
	Run- 2
	Falling- 3
	Hurt- 4
	*/

public class PlayerCtrl : MonoBehaviour {

	public float horizontalSpeed =10F;

	public float jumpSpeed=600F;
	
	Rigidbody2D rb;

	SpriteRenderer sr;

	Animator anim;

	bool isJumping=false;

	public Transform feet;
	public float feetWidth=0.5f;
	public float feetHeight=0.1f;
	public bool isGrounded;
	public LayerMask whatIsGround;
	bool canDoubleJump=false;
	public float delayForDoubleJump;
	// Use this for initialization
	void Start () {
		rb=GetComponent<Rigidbody2D>();
		sr=GetComponent<SpriteRenderer>();
		anim=GetComponent<Animator>();
	}
	
		void OnDrawGizmos(){
			Gizmos.DrawWireCube(feet.position, new Vector3(feetWidth, feetHeight, 0f));
		}

	// Update is called once per frame
	void Update () {

		if(transform.position.y <GM.insatance.yMinLive){
			GM.insatance.KillPlayer();
		}
		isGrounded=Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(feetWidth, feetHeight),360f, whatIsGround);
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

		ShowFalling();

	}

	void MoveHorizontal(float speed){
		rb.velocity=new Vector2 (speed, rb.velocity.y);

		if (speed<0F){
			sr.flipX=true;
		}
		else if (speed>0F) {
			sr.flipX=false;
		}

		if(!isJumping){
		anim.SetInteger("State", 2);
		}
	}

	void StopMoving(){
		rb.velocity=new Vector2(0f,rb.velocity.y);
		if(!isJumping){
		anim.SetInteger("State", 0);
		}
	}

	void ShowFalling (){
		if(rb.velocity.y<0F){
			anim.SetInteger("State", 3);
		}
	}
	void Jump(){
		if(isGrounded){
		isJumping=true;
		rb.AddForce(new Vector2(0F,jumpSpeed));
		anim.SetInteger("State", 1);
		
		Invoke("EnableDoubleJump", delayForDoubleJump);
		}
		if (canDoubleJump && !isGrounded){
			rb.velocity=Vector2.zero;
			rb.AddForce(new Vector2(0F,jumpSpeed));
			anim.SetInteger("State", 1);
			canDoubleJump=false;
		}
	}
	void EnableDoubleJump(){
		canDoubleJump=true;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.layer == LayerMask. NameToLayer("Ground")){
			isJumping=false;
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Coin")){
			Destroy(other.gameObject);	
		}
	
	}

}
