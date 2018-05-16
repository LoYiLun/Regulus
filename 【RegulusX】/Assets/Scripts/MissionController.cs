using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class MissionController : MonoBehaviour {


	public GameObject Player;
	public GameObject Well;
	public GameObject Rose;
	public GameObject WateringCan;
	public GameObject GlassJog;
	public GameObject ItemTarget; 
	public GameObject ItemHead;
	public float ItemSink = 0.4f;
	private bool M1_Well;
	private bool M1_Rose;
	private bool M1_Rose_02;
	private bool M1_WateringCan;
	private bool M1_WateringCan_02;
	private bool M2_FindWind;
	private bool M2_GiveWind;
	private Collider W_Collider;
	public static GameObject MissionObj;

	// Use this for initialization
	void Start () {
		W_Collider = WateringCan.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {


			
			if (AnimationController.AnimationEnd) {
				AnimationController.Item.transform.position = new Vector3 (ItemTarget.transform.position.x, ItemTarget.transform.position.y + ItemSink, ItemTarget.transform.position.z);
				AnimationController.Item.transform.parent = GameObject.Find (ItemHead.name).transform;
				PlayerController.moveState = true;
				AnimationController.AnimationEnd = false;


		}


	}

	void OnCollisionEnter (UnityEngine.Collision Mission)
	{
		if (Mission.gameObject.name == "Well") {
			if (MissionObj == WateringCan) {
				M1_WateringCan_02 = true;
                Flowchart.BroadcastFungusMessage("GotWater");

			}
		}


		if (Mission.gameObject.name == "WateringCan") {
			MissionObj = WateringCan;
			AnimationController.GetItem = true;
			//AnimationController.Get();
			M1_WateringCan = true;
			W_Collider.enabled = !W_Collider.enabled;
            Flowchart.BroadcastFungusMessage("GotWaterCan");
        }

		if (Mission.gameObject.name == "Rose") {
			if (MissionObj == WateringCan && M1_WateringCan_02) {
					WateringCan.SetActive (false);
					MissionObj = null;
					M2_FindWind = true;
			}
            Flowchart.BroadcastFungusMessage("RoseGotWater");


        }

		if (Mission.gameObject.name == "House") {
			if (M2_FindWind) { //去房子找屏風
				MissionObj = GlassJog; //找到玻璃罐
				AnimationController.GetItem = true;
				M2_GiveWind = true;
				M2_FindWind = false;
			}
		}

		if (M2_GiveWind) {
			if (Mission.gameObject.name == "Rose"){
				GlassJog.transform.parent = null;
				GlassJog.transform.position += new Vector3 (0, 0.5f, 0);
				GlassJog.transform.rotation = Quaternion.Euler (90, 0, 0);
				ItemSink = 0.4f;
				ItemHead = Rose;
				AnimationController.GetItem = true;
				M2_GiveWind = false;
			}
		}


	}
}
