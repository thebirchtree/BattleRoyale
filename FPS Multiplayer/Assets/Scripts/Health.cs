using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float maxHitPoints = 100f;
	private float currentHitPoints;
	// Use this for initialization
	void Start () {
		currentHitPoints = maxHitPoints;
	}
	
	[PunRPC]
	public void takeDamage(float dmg){
		currentHitPoints -= dmg;
		
		if(currentHitPoints <= 0){
			Die();
		}
	}

	void Die(){
		Debug.Log("Dead");
		if(GetComponent<PhotonView>().instantiationId==0) {
			Destroy(gameObject);
		}else{
			if(GetComponent<PhotonView>().isMine){ 
				Debug.Log("I AM MASTER");
				PhotonNetwork.Destroy(gameObject);
			}
		}
	}

	public float getHealth(){
		return currentHitPoints;
	}
}
