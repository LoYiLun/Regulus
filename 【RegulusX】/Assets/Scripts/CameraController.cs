using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject Camera01;
	public GameObject Camera02;
	public GameObject Camera04;
	public GameObject camObj01;
	public GameObject camObj02;
	public GameObject camObj04;
	private Vector3 Camera2Player;
	private Vector3 Camera4Player;
	public GameObject Player;

	void Awake(){
		Camera01.SetActive (false);
		Camera02.SetActive (true);
		Camera04.SetActive (false);

		camObj01.SetActive (false);
		camObj02.SetActive (true);
		camObj04.SetActive (false);



	}



	void Start () {
		Camera2Player = Camera02.transform.position - Player.transform.position;
		Camera4Player = Camera04.transform.position - Player.transform.position;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Camera02.transform.position = Player.transform.position + Camera2Player;
		Camera04.transform.position = Player.transform.position + Camera4Player;

		/*if (Input.GetKey("o") == true)
		{
			//第一人稱視角
			Camera01.SetActive(false);
			camObj01.SetActive(false);
			Camera02.SetActive(false);
			camObj02.SetActive(false);
			Camera04.SetActive(true);
			camObj04.SetActive(true);
		}*/
		if (Input.GetKey("x") == true)
		{
			//第三人稱視角(遠)
			Camera04.SetActive(false);
			camObj04.SetActive(false);
			Camera02.SetActive(false);
			camObj02.SetActive(false);
			Camera01.SetActive(true);
			camObj01.SetActive(true);
			CubeController.R_Button.SetActive (true);
		}
		else if (Input.GetKey("z") == true)
		{
			//第三人稱視角(近)
			Camera04.SetActive(false);
			camObj04.SetActive(false);
			Camera02.SetActive(true);
			camObj02.SetActive(true);
			Camera01.SetActive(false);
			camObj01.SetActive(false);
			CubeController.R_Button.SetActive (false);

		}

	}
}
