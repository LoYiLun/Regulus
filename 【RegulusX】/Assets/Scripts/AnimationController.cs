using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

	public static bool GetItem;
	public static bool AnimationEnd = false;
	private GameObject Player;
	public static GameObject Item;
	private GameObject P_GetItem;

	void Start () {
		Player = GameObject.Find ("Player");
		P_GetItem = GameObject.Find ("P_GetItem");
		//P_GetItem.SetActive (false);
	}


	void Update () {
		Item = MissionController.MissionObj;
		if (GetItem) {
			P_GetItem.SetActive (true);
			StartCoroutine(Wait_second());

		}
	}
		

	IEnumerator Wait_second()
	{
		if (GetItem) {
			for (float i = 0; i <= 2f; i += Time.deltaTime) {
				PlayerController.moveState = false;
				Item.transform.position = new Vector3 (Item.transform.position.x, Item.transform.position.y + 0.008f * i * Time.deltaTime, Item.transform.position.z);
				Item.transform.Rotate (Vector3.back * 3f * Time.deltaTime);
				//print (i + " seconds");
				yield return 0;
			}
		}


		AnimationEnd = true;
		P_GetItem.SetActive (false);
		GetItem = false;


	}

}
