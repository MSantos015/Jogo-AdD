using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {

	public static GM insatance=null;
	public float yMinLive=-5f;
	public Transform spawnPoint;
	public GameObject playerPrefab;
	PlayerCtrl player;
	public float timeToRespawn =2f;
	void Awake(){
		if(insatance==null){
			insatance=this;
		}
		else if(insatance !=this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
	// Use this for initialization
	void Start () {
		if(player==null){
			RespawnPlayer();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(player==null){
			GameObject obj=GameObject.FindGameObjectWithTag("Player");
			if(obj!=null){
				player=obj.GetComponent<PlayerCtrl>();
			}
		}
	}
	public void RespawnPlayer(){
		Instantiate(playerPrefab,spawnPoint.position, spawnPoint.rotation);
	}
	public void KillPlayer(){
		if(player!=null){
			Destroy (player.gameObject);
			Invoke("RespawnPlayer", timeToRespawn);
		}
	}
}
	