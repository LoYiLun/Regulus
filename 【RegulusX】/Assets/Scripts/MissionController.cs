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
	public Material WateringCanSkin;
	public float ItemGoUp = 0.4f;
	private bool M1_Well;
	private bool M1_Rose;
	private bool M1_Rose_02;
	private bool M1_FirstGetWater;
	private bool M1_FirstFeedRose;
	private bool M2_FindWind;
	private bool M2_GiveWind;
	private bool M3_LastGetWater;
	private bool M3_LastFeedRose;
	private Collider W_Collider;
	public static GameObject MissionObj;

	// Use this for initialization
	void Start () {
		W_Collider = WateringCan.GetComponent<Collider>();

	}
	
	// Update is called once per frame
	void Update () {


			
			if (AnimationController.AnimationEnd) {
				AnimationController.Item.transform.position = new Vector3 (ItemTarget.transform.position.x, ItemTarget.transform.position.y + ItemGoUp, ItemTarget.transform.position.z);
				AnimationController.Item.transform.parent = GameObject.Find (ItemHead.name).transform;
				PlayerController.moveState = true;
				AnimationController.AnimationEnd = false;

		}



	}

	void OnCollisionEnter (UnityEngine.Collision Mission)
	{
		if (M1_FirstGetWater) {
			if (Mission.gameObject.name == "Well") {
				M1_FirstFeedRose = true;
				WateringCan.GetComponent<Renderer> ().material = Resources.Load("Universe01", typeof(Material)) as Material;

				Flowchart.BroadcastFungusMessage ("GotWater");

			}
		}

		if (M1_FirstFeedRose == false) {
			if (Mission.gameObject.name == "WateringCan") {
				MissionObj = WateringCan;
				AnimationController.GetItem = true;
				M1_FirstGetWater = true;
				W_Collider.enabled = !W_Collider.enabled;
				WateringCan.GetComponent<Renderer> ().material = Resources.Load("Glass01", typeof(Material)) as Material;

				Flowchart.BroadcastFungusMessage ("GotWaterCan");
			}
		}

		if (M1_FirstFeedRose) {
			if (Mission.gameObject.name == "Rose") {
				if (MissionObj == WateringCan) {
					//WateringCan.SetActive (false);
					M2_FindWind = true;
					M1_FirstGetWater = false;
					WateringCan.GetComponent<Renderer> ().material = Resources.Load("Glass01", typeof(Material)) as Material;
				}
				Flowchart.BroadcastFungusMessage ("RoseGotWater");

			}



		}

		if (M2_FindWind) {
			if (Mission.gameObject.name == "House" && M2_GiveWind != true) { //去房子找屏風
				MissionObj = GlassJog; //找到玻璃罐
				ItemTarget = Player;
				ItemHead = GameObject.Find ("Head");
				ItemGoUp = 0.6f;
				AnimationController.GetItem = true;
				M2_GiveWind = true;
				M2_FindWind = false;
                Flowchart.BroadcastFungusMessage("FindWind");//觸發屏風對話
            }
		}

		if (M2_GiveWind) {
			if (Mission.gameObject.name == "Rose" && MissionObj == GlassJog) {
				GlassJog.transform.parent = null;
				GlassJog.transform.position += new Vector3 (0, 0.5f, 0);
				GlassJog.transform.rotation = Quaternion.Euler (90, 0, 0);
				ItemTarget = Rose;
				ItemHead = Rose;
				ItemGoUp = 0.2f;
				AnimationController.GetItem = true;
				M3_LastGetWater = true;
				M1_FirstFeedRose = false;
				M2_GiveWind = false;
                Flowchart.BroadcastFungusMessage("GiveWind");//觸發給屏風對話
            }
		}

		if (M3_LastGetWater) {
			if (Mission.gameObject.name == "Well") {
				MissionObj = WateringCan;
			/*	ItemTarget = Player;
				ItemHead = GameObject.Find ("Head");
				ItemGoUp = 0.4f;
				AnimationController.GetItem = true;
				if(AnimationController.GetItem == false)*/
					WateringCan.GetComponent<Renderer> ().material = Resources.Load("Universe01", typeof(Material)) as Material;
				M3_LastFeedRose = true;
				M3_LastGetWater = false;
               
            }
		}

		if (M3_LastFeedRose) {
			if (Mission.gameObject.name == "Rose" && MissionObj == WateringCan) {
				WateringCan.transform.parent = null;
				ItemTarget = Rose;
				ItemHead = Rose;
				ItemGoUp = 0.2f;
				AnimationController.GetItem = true;
				WateringCan.GetComponent<Renderer> ().material = Resources.Load ("Glass01", typeof(Material)) as Material;
				M3_LastFeedRose = false;
                Flowchart.BroadcastFungusMessage("LastFeedWater");
            }


			}
	}
}
