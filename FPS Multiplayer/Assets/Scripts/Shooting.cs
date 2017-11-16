using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public float damage = 25f;
    public float impact = 100f;
    public float range = 5000f;
    public float fireRate = 0.01f;

    public GameObject impactEffect;
    //private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
	public LayerMask Mask;

    float cooldown = 0;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > cooldown)
        {
            Fire();
            cooldown = Time.time + fireRate;
        }
    }

    void Fire()
    {       
       RaycastHit hit;

        if( Physics.Raycast( Camera.main.transform.position, Camera.main.transform.forward, out hit, range, Mask ) ){
            Health h = hit.collider.GetComponent<Health>();
            if (h != null){
                h.takeDamage (damage);
                Debug.Log("Health remainig: " + h.getHealth());
            }
            if (hit.rigidbody != null){
                Debug.Log("Move Bitch");
                hit.rigidbody.AddForce ( -hit.normal * impact);
            }
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(-  hit.normal));
        }

        

         Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * range, Color.green, 0, true);

        //For shoothing through objects, but for now easy.
        //RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, range, Mask);
        //Debug.Log(hits.Length);
        // foreach (RaycastHit hit in hits){            
        //     if(hit.distance < ClosestHit.distance){
        //         ClosestHit = hit;
        //     }
        // }       
    }

}
