using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float maxHitPoints = 100f;
	float currentHitPoints;
	// Use this for initialization
	void Start () {
		currentHitPoints = maxHitPoints;
	}
	
	public void takeDamage(float dmg){
		currentHitPoints -= dmg;
		
		if(currentHitPoints <= 0){
			Die();
		}
	}

	void Die(){
		Destroy(gameObject);
	}

	public float getHealth(){
		return currentHitPoints;
	}
}
