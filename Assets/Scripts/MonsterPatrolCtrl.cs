using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrolCtrl : MonoBehaviour {

	public Transform Pos1, Pos2;
	public float speed=2f;
	public float waitTime=2f;
	Vector3 nextPos;
	Animator anim;
	SpriteRenderer sr;
	void Start () {
		anim= GetComponent<Animator>();
		sr=GetComponent<SpriteRenderer>();
		nextPos=Pos1.position;
		StartCoroutine(Move());
	}
	IEnumerator Move(){
		while(true){   
			if (transform.position ==Pos1.position){
				nextPos=Pos2.position;
				anim.SetInteger("State",1);
				yield return new WaitForSeconds(waitTime);
				anim.SetInteger("State",0);
				sr.flipX= !sr.flipX;
			}
			if (transform.position ==Pos2.position){
				nextPos=Pos1.position;
				anim.SetInteger("State",1);
				yield return new WaitForSeconds(waitTime);
				anim.SetInteger("State",0);
				sr.flipX= !sr.flipX;
			}
			transform.position=Vector3.MoveTowards(transform.position, nextPos, speed*Time.deltaTime);
			yield return null;
		}
	}
	void OnDrawGizmos () {
		Gizmos.DrawLine(Pos1.position, Pos2.position);
	}
}
