using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemController : MonoBehaviour {

	public GameObject BeTouchedObj;
	public int save = 0;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("save01", save);
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;

		if (Input.GetMouseButton (0) && Physics.Raycast (ray, out hitInfo)) {
			Debug.DrawLine (Camera.main.transform.position, hitInfo.transform.position, Color.yellow, 0.1f, true);
			BeTouchedObj = hitInfo.collider.gameObject;
			if (hitInfo.transform.gameObject.tag == "ResetButton" || Input.GetKeyDown("R")) {
				save = PlayerPrefs.GetInt ("save01");
			}
		}





	}
}
