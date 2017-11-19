using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour {

	public AudioClip BulletSound;	
	public GameObject impactEffect;

	[PunRPC]
	void BulletFX( Vector3 startPos, Vector3 endPos){
		 AudioSource.PlayClipAtPoint(BulletSound, startPos);
		 GameObject hitImpactFX = Instantiate(impactEffect, endPos, Quaternion.LookRotation(endPos));
         Destroy(hitImpactFX, 1f);
	}
	
}
