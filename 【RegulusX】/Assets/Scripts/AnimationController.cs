using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

	public static bool GetItem;
	public static bool AnimationEnd = false;
	public static GameObject Item;

	public GameObject ItemHome;
	public GameObject ItemStep;
	private GameObject ItemTempt;


	void Start () {
		ItemStep = GameObject.Find ("Flower");
		ItemHome = GameObject.Find ("CubeMom");
	}


	void Update () {



		Item = MissionController.MissionObj;
		if(Item != null)
		ItemHome = GameObject.Find(Item.name);
		if (GetItem) {
			StartCoroutine(Wait_second());

		}

		if (ItemHome.transform.childCount < 3 && GetItem) {
			ItemTempt = Instantiate (ItemStep);
			ItemTempt.transform.parent = ItemHome.transform;
			ItemTempt.transform.localPosition = Vector3.zero;
			ItemTempt.transform.parent = null;
			Destroy (ItemTempt, 0.5f);
		}
	}
		

	IEnumerator Wait_second()
	{
		if (GetItem) {
			for (float i = 0; i <= 1.5f; i += Time.deltaTime) {
				PlayerController.moveState = false;
				CubeController.R_Button.SetActive (false);
				CubeController.StopMouse = true;
				CubeController.moveState = false;
				Item.transform.position = new Vector3 (Item.transform.position.x, Item.transform.position.y + 0.008f * i * Time.deltaTime, Item.transform.position.z);
				Item.transform.Rotate (Vector3.back * 3f * Time.deltaTime);
				//print (i + " seconds");



				yield return 0;
			}
		}


		AnimationEnd = true;
		GetItem = false;


	}

}
