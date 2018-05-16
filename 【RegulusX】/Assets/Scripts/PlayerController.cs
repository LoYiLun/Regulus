using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private GameObject Player;
	private GameObject Player2;
	public GameObject PlayerHome;
	public GameObject PlayerStep;
	public static bool moveState = false;
	private bool OnIce = false;
	private float MoveDistance = 0.43f;
	public static int FloorR;
	public static int FloorL;
	public static Vector3 MoveTarget = new Vector3(1.29f,2.566f,0);
	private int FTR;
	private int FTL;
	private int BL = 5;
	private Vector3 TargetTemp;
	public static bool ICanGo = true;
	public static bool OneShot = false;

	//主角身體部位
	public string MoveType;
	public GameObject Head;
	public GameObject HandLeft;
	public GameObject HandRight;
	public GameObject FootLeft;
	public GameObject FootRight;
	public GameObject BrainA;
	public GameObject BrainB;


	//主角移動動畫的變數

	private float speed = 7.5f;
	private int i = 1;
	private bool R60 = false;
	public float XXX;


	//V2
	public bool CubeV2 = true;

	void Start () {
		FloorR = 1;
		FloorL = 4;
		Player = GameObject.Find("Player");

		//V2 Cube
		if(CubeV2 == true)
			MoveTarget = new Vector3(1.29f,1.8f,0);


	}



	void OnCollisionEnter(Collision Noway)
	{
		if (Noway.gameObject.tag == "Obstacle")
			ICanGo = false;
		
		if (Noway.gameObject.tag == "Ice")
			OnIce = true;
	}

	void SavePosition(){
		TargetTemp = MoveTarget;
		FTL = FloorL;
		FTR = FloorR;
	}

	void BodyRotate()
	{
		//方法A
			switch (MoveType) {

		case"Forward":
				XXX++;
				Head.transform.RotateAround (BrainA.transform.position, Vector3.left, speed / 6 * i);
				HandLeft.transform.RotateAround (BrainA.transform.position, Vector3.forward, speed * i);
				HandRight.transform.RotateAround (BrainA.transform.position, Vector3.forward, -speed * i);
				FootLeft.transform.RotateAround (BrainB.transform.position, Vector3.forward, -speed * i);
				FootRight.transform.RotateAround (BrainB.transform.position, Vector3.forward, speed * i);
			if(OnIce)
			OneShot = false;
			break;

			case"Back":
			XXX++;
			Head.transform.RotateAround (BrainA.transform.position, Vector3.right, speed/6 * i);
			HandLeft.transform.RotateAround (BrainA.transform.position, Vector3.back, speed * i);
			HandRight.transform.RotateAround (BrainA.transform.position, Vector3.back, -speed * i);
			FootLeft.transform.RotateAround (BrainB.transform.position, Vector3.back, -speed * i);
			FootRight.transform.RotateAround (BrainB.transform.position, Vector3.back, speed * i);
			if(OnIce)
			OneShot = false;
				break;

			case"Left":
			XXX++;
			Head.transform.RotateAround (BrainA.transform.position, Vector3.forward, -speed/6 * i);
			HandLeft.transform.RotateAround (BrainA.transform.position, Vector3.left, speed * i);
			HandRight.transform.RotateAround (BrainA.transform.position, Vector3.left, -speed * i);
			FootLeft.transform.RotateAround (BrainB.transform.position, Vector3.left, -speed * i);
			FootRight.transform.RotateAround (BrainB.transform.position, Vector3.left, speed * i);
			if(OnIce)
			OneShot = false;
				break;

			case"Right":
			XXX++;
			Head.transform.RotateAround (BrainA.transform.position, Vector3.back, -speed/6 * i);
			HandLeft.transform.RotateAround (BrainA.transform.position, Vector3.right, speed * i);
			HandRight.transform.RotateAround (BrainA.transform.position, Vector3.right, -speed * i);
			FootLeft.transform.RotateAround (BrainB.transform.position, Vector3.right, -speed * i);
			FootRight.transform.RotateAround (BrainB.transform.position, Vector3.right, speed * i);
			if(OnIce)
			OneShot = false;
				break;



		}


		//方法B
		//HandLeft.transform.Rotate(new Vector3(speed * i, 0f, 0f));
		//HandRight.transform.Rotate(new Vector3(-speed * i, 0f, 0f));



		if (XXX >= BL)
			R60 = true;

		if (R60 == true) {
			i *= -1;
			XXX= -BL;
			R60 = false;
		}


	}


	void FixedUpdate () {




		//九宮格走路
		//print (FloorR);
		if (Input.GetKey (KeyCode.UpArrow)) {

			if (OneShot == false) {
				Player.transform.rotation = Quaternion.Euler (0f, 270f, 0f);
				if (FloorR < 8) {
					SavePosition ();
					MoveTarget -= new Vector3 (MoveDistance, 0f, 0f);
					FloorR += 1;
					OneShot = true;
					MoveType = "Forward";
					moveState = true;


				}
			}
		}

		if (Input.GetKey (KeyCode.DownArrow)) {

			if (OneShot == false) {
				Player.transform.rotation = Quaternion.Euler (0f, 90f, 0f);
				if (FloorR > 0) {
					SavePosition ();
					MoveTarget += new Vector3 (MoveDistance, 0f, 0f);
					FloorR -= 1;
					OneShot = true;
					MoveType = "Back";
					moveState = true;

				}
			}
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {

			if (OneShot == false) {
				Player.transform.rotation = Quaternion.Euler (0f, 180f, 0f);
				if (FloorL < 8) {
					SavePosition ();
					MoveTarget -= new Vector3 (0f, 0f, MoveDistance);
					FloorL += 1;
					OneShot = true;
					MoveType = "Left";
					moveState = true;

				}
			}
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			
			if(OneShot == false){
			Player.transform.rotation = Quaternion.Euler (0f, 0f, 0f);
				if (FloorL > 0) {
					SavePosition ();
					MoveTarget += new Vector3 (0f, 0f, MoveDistance);
					FloorL -= 1;
					OneShot = true;
					MoveType = "Right";
					moveState = true;

				}
			}
		}

		if (moveState) {

			if (PlayerHome.transform.childCount < 1) {
				Player2 = Instantiate (PlayerStep);
				Player2.transform.parent = PlayerHome.transform;
				Player2.transform.localPosition = Vector3.zero;
				Player2.transform.parent = null;
				Destroy (Player2, 0.5f);
			}
			
			if (Vector3.Distance (Player.transform.position, MoveTarget) < 0.01f) {
				ICanGo = true;
				OneShot = false;
				moveState = false;
			}
			if (ICanGo == false) {
				MoveTarget = TargetTemp;
				FloorR = FTR;
				FloorL = FTL;
			}
			BodyRotate ();
			Player.transform.position = Vector3.MoveTowards (Player.transform.position, MoveTarget, 1.2f * Time.deltaTime);


		} else {
			//BodyReset ();
		}
			

	}






}
