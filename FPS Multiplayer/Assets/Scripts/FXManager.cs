using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour {

	[PunRPC]
	void BulletFX( Vector3 startPos, Vector3 endPos){
		 Debug.Log("Bulet FX!");
	}
}
