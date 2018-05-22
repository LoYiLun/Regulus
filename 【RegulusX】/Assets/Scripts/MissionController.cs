using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class MissionController : MonoBehaviour {

	// General
	public GameObject ItemTarget; 
	public GameObject ItemHead;
	public GameObject NoItem;
	public float ItemGoUp = 0.4f;
	public static GameObject MissionObj;

	// Level 01 - Rose
	public GameObject Player;
	public GameObject Well;
	public GameObject Rose;
	public GameObject WateringCan;
	public GameObject GlassJog;
	public Material WateringCanSkin;
	private Collider W_Collider;
	private bool L1M1_Well;
	private bool L1M1_Rose;
	private bool L1M1_FirstGetWater;
	private bool L1M1_FirstFeedRose;
	private bool L1M2_FindWind;
	private bool L1M2_GiveWind;
	private bool L1M3_LastGetWater;
	private bool L1M3_LastFeedRose;

	// Level 02 - King
	public GameObject King;
	public GameObject WaterBottle;
	public GameObject Calender;
	private Collider WB_Collider;
	private bool L2M1_FindKing;
	private bool L2M2_FindWater;
	private bool L2M2_GiveWater;
	private bool L2M3_FindCalender;
	private bool L2M3_GiveCalender;

	// Use this for initialization
	void Start () {
		W_Collider = WateringCan.GetComponent<Collider>();
		WB_Collider = WaterBottle.GetComponent<Collider>();
		MissionObj = NoItem;
		NoItem = GameObject.Find ("NoItem");

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

//Level 01 - Rose
		if (L1M1_FirstGetWater) {
			if (Mission.gameObject.name == "Well") {
				L1M1_FirstFeedRose = true;
				WateringCan.GetComponent<Renderer> ().material = Resources.Load("Universe01", typeof(Material)) as Material;

				Flowchart.BroadcastFungusMessage ("GotWater");
			}
		}
			
		if (L1M1_FirstFeedRose == false) {
			if (Mission.gameObject.name == "WateringCan") {
				MissionObj = WateringCan;
				AnimationController.GetItem = true;
				L1M1_FirstGetWater = true;
				W_Collider.enabled = !W_Collider.enabled;
				WateringCan.GetComponent<Renderer> ().material = Resources.Load("Glass01", typeof(Material)) as Material;

				Flowchart.BroadcastFungusMessage ("GotWaterCan");
			}
		}

		if (L1M1_FirstFeedRose) {
			if (Mission.gameObject.name == "Rose" && MissionObj == WateringCan) {
				//WateringCan.SetActive (false);
				L1M2_FindWind = true;
				L1M1_FirstGetWater = false;
				WateringCan.GetComponent<Renderer> ().material = Resources.Load("Glass01", typeof(Material)) as Material;

				Flowchart.BroadcastFungusMessage ("RoseGotWater");
			}
		}

		if (L1M2_FindWind) {
			if (Mission.gameObject.name == "House" && L1M2_GiveWind != true) { //去房子找屏風
				MissionObj = GlassJog; //找到玻璃罐
				ItemTarget = Player;
				ItemHead = GameObject.Find ("Head");
				ItemGoUp = 0.6f;
				AnimationController.GetItem = true;
				L1M2_GiveWind = true;
				L1M2_FindWind = false;
               
				Flowchart.BroadcastFungusMessage("FindWind");//觸發屏風對話
            }
		}

		if (L1M2_GiveWind) {
			if (Mission.gameObject.name == "Rose" && MissionObj == GlassJog) {
				GlassJog.transform.parent = null;
				GlassJog.transform.position += new Vector3 (0, 0.5f, 0);
				GlassJog.transform.rotation = Quaternion.Euler (90, 0, 0);
				ItemTarget = Rose;
				ItemHead = Rose;
				ItemGoUp = 0.2f;
				AnimationController.GetItem = true;
				L1M3_LastGetWater = true;
				L1M1_FirstFeedRose = false;
				L1M2_GiveWind = false;
                
				Flowchart.BroadcastFungusMessage("GiveWind");//觸發給屏風對話
            }
		}

		if (L1M3_LastGetWater) {
			if (Mission.gameObject.name == "Well") {
				MissionObj = WateringCan;
				WateringCan.GetComponent<Renderer> ().material = Resources.Load("Universe01", typeof(Material)) as Material;
				L1M3_LastFeedRose = true;
				L1M3_LastGetWater = false;
               
            }
		}

		if (L1M3_LastFeedRose) {
			if (Mission.gameObject.name == "Rose" && MissionObj == WateringCan) {
				WateringCan.transform.parent = null;
				ItemTarget = Rose;
				ItemHead = Rose;
				ItemGoUp = 0.2f;
				AnimationController.GetItem = true;
				WateringCan.GetComponent<Renderer> ().material = Resources.Load ("Glass01", typeof(Material)) as Material;
				L1M3_LastFeedRose = false;
                
				Flowchart.BroadcastFungusMessage("LastFeedWater");
           	 }
		}

// Level 02 - King

		if (L2M1_FindKing == false) { // 找國王
			if (Mission.gameObject.name == "King") {
				L2M2_FindWater = true;
				L2M1_FindKing = true;
			}
		}

		if (L2M2_FindWater) { // 幫國王找水
			if (Mission.gameObject.name == "WaterBottle") {
				MissionObj = WaterBottle;
				WB_Collider.enabled = !WB_Collider.enabled;
				ItemTarget = Player;
				ItemHead = GameObject.Find ("Head");
				ItemGoUp = 0.4f;
				AnimationController.GetItem = true;
				L2M2_GiveWater = true;
				L2M2_FindWater = false;
			}
		}

		if (L2M2_GiveWater) { // 給國王喝水
			if(Mission.gameObject.name == "King" && MissionObj == WaterBottle){
				WaterBottle.transform.parent = null;
				ItemTarget = King;
				ItemHead = King;
				ItemGoUp = 0.35f;
				AnimationController.GetItem = true;
				L2M3_FindCalender = true;
				L2M2_GiveWater = false;
			}
		}

		if(L2M3_FindCalender){ // 幫國王找日曆
			if (Mission.gameObject.name == "Calender") {
				MissionObj = Calender;
				ItemTarget = Player;
				ItemHead = GameObject.Find ("Head");
				ItemGoUp = 0.4f;
				AnimationController.GetItem = true;
				L2M3_GiveCalender = true;
				L2M3_FindCalender = false;
			}
		}

		if(L2M3_GiveCalender){ // 給國王日曆
			if(Mission.gameObject.name == "King" && MissionObj == Calender){
				ItemTarget = King;
				ItemHead = King;
				ItemGoUp = 0.6f;
				AnimationController.GetItem = true;
				L2M3_GiveCalender = false;
			}
		}

	//Mission Bottom
	}
}
