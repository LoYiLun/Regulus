﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject Camera01;
	public GameObject Camera02;
	public GameObject camObj01;
	public GameObject camObj02;
	private Vector3 Camera2Player;
	public GameObject Player;
	public static GameObject Talker;
	private bool FollowPlayer = true;

	//0529
	private float ToClose;
	private float ToFar;

	void Awake(){
		Camera02 = GameObject.Find ("Camera02");

		Camera01.SetActive (false);
		Camera02.SetActive (true);

		camObj01.SetActive (true);
		camObj02.SetActive (true);
	}



	void Start () {
		Camera2Player = Camera02.transform.position - Player.transform.position;
		Talker = Player;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		ToClose = Vector3.Distance (camObj01.transform.position, Camera02.transform.position) / 5;
		ToFar = Vector3.Distance (camObj02.transform.position, Camera02.transform.position) / 5;



			if (Input.GetKey ("c") == true || FollowPlayer) {
			
				Camera02.transform.position = Camera2Player + Talker.transform.position;
				FollowPlayer = true;
			}


			if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
				FollowPlayer = false;
				Camera02.transform.position = Vector3.MoveTowards (Camera02.transform.position, camObj01.transform.position, ToClose);
			} 
			else if (Input.GetAxis ("Mouse ScrollWheel") > 0 && FollowPlayer == false) {


				Camera02.transform.position = Vector3.MoveTowards (Camera02.transform.position, camObj02.transform.position, ToFar);
				if ((Camera02.transform.position.z - camObj02.transform.position.z < 0.8f))
					FollowPlayer = true;
			}

			if (Input.GetMouseButtonDown (2))
				FollowPlayer = true;


	}
}
