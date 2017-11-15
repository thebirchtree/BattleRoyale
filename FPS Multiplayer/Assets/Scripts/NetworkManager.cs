using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

	public bool offlineMode = false;
	public Camera OverviewCam; 
	SpawnSpot[] spawnSpots;
	// Use this for initialization
	void Start () {
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
		Connect();		
	}

	void Connect () {
		if(offlineMode)
		{
			PhotonNetwork.offlineMode = true;
			OnJoinedLobby();
		}
		else{
			PhotonNetwork.ConnectUsingSettings("MultiFPS v001");
		}
		
		//PhotonNetwork.offlineMode = true;
		//Debug.Log("OfflineMode enabled");
	}

	void OnGUI() {
		GUILayout.Label( PhotonNetwork.connectionStateDetailed.ToString() );
	}

	private void OnConnectedToMaster() {
    	PhotonNetwork.JoinLobby();
	}
    
	void OnJoinedLobby() {
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed() {
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom( null );
		Debug.Log("Created Room");
	}

	void OnJoinedRoom() {
		Debug.Log ("Room Joined");		

		SpawnMyPlayer();
	}

	void SpawnMyPlayer() {	
		if(spawnSpots == null){
			Debug.LogError ("No spawnSpots available..");
			return;
		}
		OverviewCam.enabled = false;
		SpawnSpot mySpawnSpot = spawnSpots[ Random.Range (0, spawnSpots.Length) ];	
		GameObject myPlayer = (GameObject) PhotonNetwork.Instantiate("FPSController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
		myPlayer.GetComponentInChildren<Camera>().enabled = true; 
		myPlayer.GetComponent<CharacterController>().enabled = true;	
		myPlayer.GetComponentInChildren<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
		myPlayer.GetComponentInChildren<AudioListener>().enabled = true;
	}
}
