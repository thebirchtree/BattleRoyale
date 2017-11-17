using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    FXManager fxManager;
    public float damage = 5f;
    public float impact = 100f;
    public float range = 250f;
    public float fireRate = 0.1f;
    private float nextFire = 0.0f;

    AudioSource audioSource;
    public AudioClip gunShot;
    public GameObject impactEffect;
    //private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
	public LayerMask Mask;    

    void Start() {
        audioSource = GetComponent<AudioSource>();
        fxManager = GameObject.FindObjectOfType<FXManager>();
        if(fxManager == null){
            Debug.Log("Could not find fxManager..");
        }
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire) 
        {
            nextFire = Time.time + fireRate;
            Fire();                
        }
    }

    [PunRPC]
    void Fire()
    {       
       RaycastHit hit;  

        if( Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range, Mask ) ){
            Health h = hit.collider.GetComponent<Health>();
              
            if(h == null && hit.transform.parent){          
                
                 h = hit.transform.parent.GetComponent<Health>();
            }         

            if (h != null){
                h.GetComponent<PhotonView>().RPC ("takeDamage", PhotonTargets.AllBuffered, damage);  //h.takeDamage (damage);               
                Debug.Log("Health remainig: " + h.getHealth());
            }

            if (fxManager != null) {
                Vector3 maxRangePoint = Camera.main.transform.position + (Camera.main.transform.forward * 100f);
                fxManager.GetComponent<PhotonView>().RPC ("BulletFX", PhotonTargets.All,Camera.main.transform.position,maxRangePoint);
            }
            // if we want to use add force to the target, not using now because of MP.
            // if (hit.rigidbody != null){ 
            //     hit.rigidbody.AddForce ( -hit.normal * impact);
            // }
            audioSource.PlayOneShot(gunShot, 0.5F);
            GameObject hitImpactFX = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hitImpactFX, 1f);
        }else{
             if (fxManager != null) {
                fxManager.GetComponent<PhotonView>().RPC ("BulletFX", PhotonTargets.All,Camera.main.transform.position,hit.point);
            }
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
