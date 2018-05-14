using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour {

	public GameObject Player;
	public GameObject Well;
	public GameObject Rose;
	public GameObject WateringCan;
	private bool M1_Well;
	private bool M1_Rose;
	private bool M1_Rose_02;
	private bool M1_WateringCan;
	private bool M1_WateringCan_02;
	private Collider W_Collider;
	public static GameObject MissionObj;

	// Use this for initialization
	void Start () {
		W_Collider = WateringCan.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {


		if (M1_WateringCan) {
			
			if (AnimationController.AnimationEnd) {
			WateringCan.transform.position = new Vector3 (Player.transform.position.x, Player.transform.position.y + 0.4f, Player.transform.position.z);
				WateringCan.transform.parent = GameObject.Find ("Head").transform;
				PlayerController.moveState = true;
				AnimationController.AnimationEnd = false;

			}

		}


	}

	void OnCollisionEnter (Collision Mission)
	{
		if (Mission.gameObject.name == "Well") {
			if (MissionObj == WateringCan) {
				M1_WateringCan_02 = true;


			}
		}


		if (Mission.gameObject.name == "WateringCan") {
			MissionObj = WateringCan;
			AnimationController.GetItem = true;
			//AnimationController.Get();
			M1_WateringCan = true;
			W_Collider.enabled = !W_Collider.enabled;

		}

		if (Mission.gameObject.name == "Rose") {
			if (MissionObj == WateringCan && M1_WateringCan_02) {
					WateringCan.SetActive (false);
			}

		}

	}
}
