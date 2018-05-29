using System.Collections;
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

		if (Input.GetKey ("c") == true || FollowPlayer) {
			
			Camera02.transform.position = Camera2Player + Talker.transform.position;
			FollowPlayer = true;
		}
		/*if (Input.GetKey("o") == true)
		{
			//第一人稱視角
			Camera01.SetActive(false);
			camObj01.SetActive(false);
			Camera02.SetActive(false);
			camObj02.SetActive(false);
		}*/
		if (Input.GetKey("x") == true)
		{
			//第三人稱視角(遠)
			/*Camera02.SetActive(false);
			camObj02.SetActive(false);
			Camera01.SetActive(true);
			camObj01.SetActive(true);
*/			FollowPlayer = false;
			Camera02.transform.position = Vector3.MoveTowards (Camera02.transform.position, camObj01.transform.position, 10f * Time.deltaTime);
		}
		else if (Input.GetKey("z") == true && FollowPlayer == false)
		{
			//第三人稱視角(近)
/*			Camera02.SetActive(true);
			camObj02.SetActive(true);
			Camera01.SetActive(false);
			camObj01.SetActive(false);*/
			FollowPlayer = false;
			Camera02.transform.position = Vector3.MoveTowards (Camera02.transform.position, camObj02.transform.position, 10f * Time.deltaTime);
			if (Camera02.transform.position.z - camObj02.transform.position.z < 1)
				FollowPlayer = true;
		}

	}
}
