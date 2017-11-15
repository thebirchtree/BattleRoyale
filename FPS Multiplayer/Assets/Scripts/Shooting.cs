using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public float damage = 25f;
    public float fireRate = 0.5f;
    public float range = 250f;

	public LayerMask Mask;

    float cooldown = 0;

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }

    void Fire()
    {
        if (cooldown > 0)
        {
            return;
        }

		Debug.Log("Pew");

		//RaycastHit _hit;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, range, Mask);
        Debug.Log(hits.Length);

        foreach (RaycastHit hit in hits){
            Debug.Log(hit.collider.name);
        }
        
    //    if ( Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out _hit, range, Mask)){
	// 		Debug.Log(_hit.collider.name);
	// 	}      

        cooldown = fireRate;
    }


}
